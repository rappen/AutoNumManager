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
using System.ComponentModel;
using Microsoft.Crm.Sdk.Messages;

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
                            entities.AddRange(metaresponse
                                .Where(e => e.IsCustomizable.Value == true && e.IsIntersect.Value != true)
                                .Select(m => new EntityMetadataProxy(m))
                                .OrderBy(e => e.ToString()));
                        }
                    }
                    EnableControls(true);
                }
            });
        }

        private void FilterEntities()
        {
            cmbEntities.Items.Clear();
            var solution = cmbSolution.SelectedItem as SolutionProxy;
            if (solution == null)
            {
                return;
            }
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

        private void LoadAttributes()
        {
            gridAttributes.DataSource = null;
            var entity = cmbEntities.SelectedItem as EntityMetadataProxy;
            var details = MetadataHelper.LoadEntityDetails(Service, entity.Metadata.LogicalName).EntityMetadata.FirstOrDefault();
            if (details != null)
            {
                var attributes = details.Attributes
                    .Where(a => a.AttributeType == AttributeTypeCode.String && !string.IsNullOrEmpty(a.AutoNumberFormat))
                    .Select(a => new AttributeProxy((StringAttributeMetadata)a)).ToList();
                var bindingList = new BindingList<AttributeProxy>(attributes);
                var source = new BindingSource(bindingList, null);
                gridAttributes.DataSource = source;
            }
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
            if (DialogResult.OK != MessageBox.Show("Confirm!", "Confirm", MessageBoxButtons.OKCancel))
            {
                return;
            }
            if (txtLogicalName.Enabled)
            {
                ExecuteMethod(CreateAttribute);
            }
            else
            {
                ExecuteMethod(UpdateAttribute);
            }
        }

        private void UpdateAttribute()
        {
            var langid = int.Parse(txtLanguageId.Text);
            var attributename = lblPrefix.Text + txtLogicalName.Text;
            var req = new UpdateAttributeRequest
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
                SolutionUniqueName = (cmbSolution.SelectedItem as SolutionProxy)?.UniqueName
            };

            try
            {
                Service.Execute(req);
                if (!string.IsNullOrEmpty(txtSeed.Text) && txtSeed.Text != "1")
                {
                    SetSeed();
                }
                MessageBox.Show("Attribute updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                SolutionUniqueName = (cmbSolution.SelectedItem as SolutionProxy)?.UniqueName
            };

            try
            {
                Service.Execute(req);
                if (!string.IsNullOrEmpty(txtSeed.Text) && txtSeed.Text != "1")
                {
                    SetSeed();
                }
                MessageBox.Show("Attribute created!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Creation failed:\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetSeed()
        {
            var attributename = lblPrefix.Text + txtLogicalName.Text;
            var req = new SetAutoNumberSeedRequest
            {
                EntityName = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata.LogicalName,
                AttributeName = attributename,
                Value = int.Parse(txtSeed.Text)
            };
            try
            {
                Service.Execute(req);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Seed update failed:\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadAttributes();
        }

        private void txtNumberFormat_TextChanged(object sender, EventArgs e)
        {
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text, txtSeed.Text);
        }

        private void txtSeed_TextChanged(object sender, EventArgs e)
        {
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text, txtSeed.Text);
        }

        private string ParseNumberFormat(string format, string seed)
        {
            try
            {
                format = ParseFormatSEQNUM(format, seed);
                format = ParseFormatRANDSTRING(format);
                format = ParseFormatDATETIMEUTC(format);
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs("Format successfully parsed"));
                btnCreateNew.Enabled = true;
            }
            catch (Exception ex)
            {
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(ex.Message));
                format = $"Format error: {ex.Message}";
                btnCreateNew.Enabled = false;
            }
            return format;
        }

        private string ParseFormatSEQNUM(string format, string seed)
        {
            if (!format.Contains("{SEQNUM:") || !format.Contains("}"))
            {
                throw new FormatException("Format string must contain a {SEQNUM:n} placeholder.");
            }
            var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
            lenghtstr = lenghtstr.Split('}')[0];
            if (int.TryParse(lenghtstr, out int length))
            {
                if (length < 1)
                {
                    throw new FormatException("SEQNUM length must be 1 or higher.");
                }
                var seedno = string.IsNullOrEmpty(seed) ? 1 : int.Parse(seed);
                var sequence = string.Format("{0:" + new string('0', length) + "}", seedno);
                format = format.Replace("{SEQNUM:" + lenghtstr + "}", sequence);
            }
            else
            {
                throw new FormatException("Invalid SEQNUM format. Enter as {SEQNUM:n} where n is length of sequence.");
            }
            if (format.Contains("{SEQNUM:"))
            {
                throw new FormatException("Format string must only contain one {SEQNUM:n} placeholder.");
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblPrefix.Text = (cmbSolution.SelectedItem as SolutionProxy)?.Prefix;
            txtLogicalName.Enabled = true;
            txtLogicalName.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtMaxLen.Text = "100";
            txtNumberFormat.Text = "{SEQNUM:5}";
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text, "1");
            txtSeed.Text = string.Empty;
            btnCreateNew.Text = "Create";
            gbNewAttribute.Enabled = true;
        }

        private void gridAttributes_SelectionChanged(object sender, EventArgs e)
        {
            gbNewAttribute.Enabled = false;
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs(string.Empty));
            var grid = sender as DataGridView;
            if (grid?.SelectedRows?.Count == 0)
            {
                return;
            }
            var row = grid.SelectedRows[0];
            var attribute = row.DataBoundItem as AttributeProxy;
            if (attribute == null)
            {
                return;
            }
            var logical = attribute.Attribute;
            if (!logical.Contains("_"))
            {
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs("Attribute does not seem to be custom."));
                return;
            }
            lblPrefix.Text = logical.Split('_')[0] + "_";
            txtLogicalName.Text = logical.Substring(logical.IndexOf("_") + 1);
            txtDisplayName.Text = attribute.attributeMetadata.DisplayName?.UserLocalizedLabel?.Label;
            txtDescription.Text = attribute.attributeMetadata.Description?.UserLocalizedLabel?.Label;
            txtMaxLen.Text = attribute.attributeMetadata.MaxLength?.ToString();
            txtNumberFormat.Text = attribute.attributeMetadata.AutoNumberFormat;
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text, "1");
            txtSeed.Text = string.Empty;
            txtLogicalName.Enabled = false;
            btnCreateNew.Text = "Update";
            gbNewAttribute.Enabled = true;
        }
    }
}
