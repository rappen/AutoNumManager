using Cinteros.Xrm.CRMWinForm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.Args;

namespace Rappen.XTB.AutoNumManager
{
    public partial class AutoNumMgr : PluginControlBase, IStatusBarMessager
    {
        private Settings mySettings;
        private List<EntityMetadataProxy> entities;

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public AutoNumMgr()
        {
            InitializeComponent();
        }

        private void AutoNumMgr_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => created");
            }
            else
            {
                mySettings = new Settings();
                LogInfo("Settings found and loaded");
            }
        }

        private void AutoNumMgr_OnCloseTool(object sender, EventArgs e)
        {
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        private void AutoNumMgr_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            if (mySettings != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = e.ConnectionDetail.WebApplicationUrl;
            }
            LogInfo("Connection has changed to: {0}", e.ConnectionDetail.WebApplicationUrl);
            gbNewAttribute.Enabled = false;
            cmbEntities.Enabled = false;
            entities = new List<EntityMetadataProxy>();
            var orgver = new Version(e.ConnectionDetail.OrganizationVersion);
            LogInfo("Connected CRM version: {0}", orgver);
            var orgok = orgver >= new Version(9, 0);
            if (orgok)
            {
                LoadSolutions();
                LoadEntities();
                LoadUserSettings();
            }
            else
            {
                LogError("CRM version too old for Auto Number Manager");
                MessageBox.Show($"Auto Number feature was introduced in\nMicrosoft Dynamics 365 July 2017 (9.0)\nCurrent version is {orgver}\n\nPlease connect to a newer organization to use this cool tool.",
                    "Organization too old", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EnableControls(bool enabled)
        {
            //Enabled = enabled;
        }

        private void LoadSolutions()
        {
            cmbSolution.Items.Clear();
            WorkAsync(new WorkAsyncInfo("Loading entities...",
                (eventargs) =>
                {
                    EnableControls(false);
                    var qx = new QueryExpression("solution");
                    qx.ColumnSet.AddColumns("friendlyname", "uniquename");
                    qx.AddOrder("installedon", OrderType.Ascending);
                    qx.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
                    qx.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);
                    var lePub = qx.AddLink("publisher", "publisherid", "publisherid");
                    lePub.EntityAlias = "P";
                    lePub.Columns.AddColumns("customizationprefix");
                    eventargs.Result = Service.RetrieveMultiple(qx);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is EntityCollection)
                        {
                            var solutions = (EntityCollection)completedargs.Result;
                            var proxiedsolutions = solutions.Entities.Select(s => new SolutionProxy(s)).OrderBy(s => s.ToString());
                            cmbSolution.Items.AddRange(proxiedsolutions.ToArray());
                        }
                    }
                    EnableControls(true);
                }
            });
        }

        private void LoadEntities()
        {
            entities = new List<EntityMetadataProxy>();
            WorkAsync(new WorkAsyncInfo("Loading entities...",
                (eventargs) =>
                {
                    EnableControls(false);
                    eventargs.Result = MetadataHelper.LoadEntities(Service);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is RetrieveMetadataChangesResponse)
                        {
                            var metaresponse = ((RetrieveMetadataChangesResponse)completedargs.Result).EntityMetadata;
                            entities.AddRange(metaresponse.Select(m => new EntityMetadataProxy(m)).OrderBy(e => e.ToString()));
                        }
                    }
                    EnableControls(true);
                }
            });
        }

        private void FilterEntities()
        {
            cmbEntities.Items.Clear();
            if (!(cmbSolution.SelectedItem is SolutionProxy))
            {
                return;
            }
            var solution = ((SolutionProxy)cmbSolution.SelectedItem);
            lblPrefix.Text = solution.Prefix;
            WorkAsync(new WorkAsyncInfo("Filtering entities...",
                (eventargs) =>
                {
                    EnableControls(false);
                    var qx = new QueryExpression("solutioncomponent");
                    qx.ColumnSet.AddColumns("objectid");
                    qx.Criteria.AddCondition("componenttype", ConditionOperator.Equal, 1);
                    qx.Criteria.AddCondition("solutionid", ConditionOperator.Equal, solution.Solution.Id);
                    eventargs.Result = Service.RetrieveMultiple(qx);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is EntityCollection)
                        {
                            var includedentities = (EntityCollection)completedargs.Result;
                            var filteredentities = entities.Where(e => includedentities.Entities.Select(i => i["objectid"]).Contains(e.Metadata.MetadataId));
                            cmbEntities.Items.AddRange(filteredentities.ToArray());
                        }
                    }
                    EnableControls(true);
                }
            });
        }

        private void LoadUserSettings()
        {
            var qx = new QueryExpression("usersettings");
            qx.ColumnSet.AddColumns("uilanguageid", "localeid");
            qx.Criteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);
            var result = Service.RetrieveMultiple(qx);
            if (result.Entities.Count > 0)
            {
                txtLanguageId.Text = result.Entities[0]["uilanguageid"].ToString();
            }
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("Confirm creation!", "Confirm", MessageBoxButtons.OKCancel))
            {
                return;
            }
            ExecuteMethod(CreateAttribute);
        }

        private void CreateAttribute()
        {
            var langid = int.Parse(txtLanguageId.Text);
            var attributename = lblPrefix.Text + txtLogicalName.Text;
            var req = new CreateAttributeRequest
            {
                EntityName = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata.LogicalName,
                Attribute = new StringAttributeMetadata
                {
                    AutoNumberFormat = txtNumberFormat.Text,
                    LogicalName = attributename,
                    SchemaName = attributename,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    MaxLength = int.Parse(txtMaxLen.Text),
                    DisplayName = new Microsoft.Xrm.Sdk.Label(txtDisplayName.Text, langid),
                    Description = new Microsoft.Xrm.Sdk.Label(txtDescription.Text, langid)
                },
                SolutionUniqueName = cmbSolution.Text
            };

            try
            {
                var resp = (CreateAttributeResponse)Service.Execute(req);
                MessageBox.Show("Attribute created!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Creation failed:\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var help = @"AutoNumberFormat value	Example value
“CAR-{SEQNUM:3}-{RANDSTRING:6}“	CAR-123-AB7LSF
“CNR-{RANDSTRING:4}-{SEQNUM:4}“	CNR-WXYZ-1000
“{SEQNUM:6}-#-{RANDSTRING:3}“	123456-#-R3V
“KA-{SEQNUM:4}“	KA-0001
“{SEQNUM:10}“	1234567890
“QUO-{SEQNUM:3}#{RANDSTRING:3}#{RANDSTRING:5}“	QUO-123#ABC#PQ2ST
“QUO-{SEQNUM:7}{RANDSTRING:5}“	QUO-0001000P9G3R
“{RANDSTRING:5}“	N7C3V
“CAS-{SEQNUM:6}-{RANDSTRING:6}-{DATETIMEUTC:yyyyMMddhhmmss}	CAS-002000-S1P0H0-20170913091544
CAS-{SEQNUM:6}-{DATETIMEUTC:yyyyMMddhh}-{RANDSTRING:6}	CAS-002002-2017091309-HTZOUR
CAS-{SEQNUM:6}-{DATETIMEUTC:yyyyMM}-{RANDSTRING:6}-{DATETIMEUTC:hhmmss}	CAS-002000-201709-Z8M2Z6-110901";
            MessageBox.Show(help, "Auto Number Format guide");
        }

        private void cmbSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterEntities();
            cmbEntities.Enabled = true;
        }

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbNewAttribute.Enabled = cmbEntities.SelectedItem is EntityMetadataProxy;
        }

        private void txtNumberFormat_TextChanged(object sender, EventArgs e)
        {
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text);
        }

        private string ParseNumberFormat(string format)
        {
            try
            {
                VerifySEQNUM(format);
                format = ParseFormatSEQNUM(format);
                format = ParseFormatRANDSTRING(format);
                format = ParseFormatDATETIMEUTC(format);
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs("Format successfully parsed"));
            }
            catch (Exception ex)
            {
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(ex.Message));
                format = $"Format error: {ex.Message}";
            }

            return format;
        }

        private void VerifySEQNUM(string format)
        {
            var numbered = ParseFormatSEQNUM(format);
            if (numbered.Equals(format))
            {
                throw new FormatException("Format string must contain at least one {SEQNUM:n} placeholder.");
            }
        }

        private string ParseFormatSEQNUM(string format)
        {
            while (format.Contains("{SEQNUM:") && format.Contains("}"))
            {
                var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                if (int.TryParse(lenghtstr, out int length))
                {
                    if (length < 1)
                    {
                        throw new FormatException("SEQNUM length must be 1 or higher.");
                    }
                    var sequence = "0123456789".Substring(0, length);
                    format = format.Replace("{SEQNUM:" + lenghtstr + "}", sequence);
                }
                else
                {
                    throw new FormatException("Invalid SEQNUM format. Enter as {SEQNUM:n} where n is length of sequence.");
                }
            }
            return format;
        }

        private string ParseFormatRANDSTRING(string format)
        {
            while (format.Contains("{RANDSTRING:") && format.Contains("}"))
            {
                var lenghtstr = format.Split(new string[] { "{RANDSTRING:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                if (int.TryParse(lenghtstr, out int length))
                {
                    if (length < 1 || length > 6)
                    {
                        throw new FormatException("RANDSTRING length must be between 1 and 6");
                    }
                    var randomstring = "X7C7D8EK3MR2L4".Substring(0, length);
                    format = format.Replace("{RANDSTRING:" + lenghtstr + "}", randomstring);
                }
                else
                {
                    throw new FormatException("Invalid RANDSTRING format. Enter as {RANDSTRING:n} where n is length of sequence.");
                }
            }
            return format;
        }

        private string ParseFormatDATETIMEUTC(string format)
        {
            while (format.Contains("{DATETIMEUTC:") && format.Contains("}"))
            {
                var formatstr = format.Split(new string[] { "{DATETIMEUTC:" }, StringSplitOptions.None)[1];
                formatstr = formatstr.Split('}')[0];
                var datestr = DateTime.Now.ToString(formatstr);
                format = format.Replace("{DATETIMEUTC:" + formatstr + "}", datestr);
            }
            return format;
        }

        private void AddMacro(string macro)
        {
            var selstart = txtNumberFormat.SelectionStart;
            var seloffset = macro.IndexOf(':') + 1;
            var sellen = macro.IndexOf('}') - seloffset;
            txtNumberFormat.SelectedText = "";
            txtNumberFormat.Text = txtNumberFormat.Text.Insert(selstart, macro);
            txtNumberFormat.SelectionStart = selstart + seloffset;
            txtNumberFormat.SelectionLength = sellen;
            txtNumberFormat.Focus();
        }

        private void llSeqnum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{SEQNUM:5}");
        }

        private void llDatetime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{DATETIMEUTC:yyyyMMddhhmmss}");
        }

        private void llRandom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{RANDSTRING:4}");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
