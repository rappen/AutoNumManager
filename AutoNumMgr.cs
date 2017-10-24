using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Rappen.XTB.AutoNumManager
{
    public partial class AutoNumMgr : PluginControlBase, IStatusBarMessenger, IMessageBusHost, IGitHubPlugin, IPayPalPlugin, IHelpPlugin
    {
        #region Private Fields

        private List<EntityMetadataProxy> entities;
        private Settings settings;

        #endregion Private Fields

        #region Public Constructors

        public AutoNumMgr()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        #endregion Public Events

        #region Public Properties

        public string DonationDescription { get { return "Auto Number Manager Fan Club"; } }
        public string EmailAccount { get { return "jonas@rappen.net"; } }
        public string HelpUrl { get { return "http://anm.xrmtoolbox.com"; } }
        public string RepositoryName { get { return "AutoNumManager"; } }

        public string UserName { get { return "Rappen"; } }

        #endregion Public Properties

        #region Public Methods

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            SettingsManager.Instance.Save(GetType(), settings);
            LogUse("Close");
            base.ClosingPlugin(info);
        }

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            // This plugin does not accept incoming messages
        }

        #endregion Public Methods

        #region Form Event Handlers

        private void AutoNumMgr_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            LogInfo("Connection has changed to: {0}", e.ConnectionDetail.WebApplicationUrl);
            gbAttribute.Enabled = false;
            tsbFXB.Enabled = false;
            cmbSolution.Enabled = false;
            cmbEntities.Enabled = false;
            rbShowAttributesOnlyNumber.Enabled = false;
            rbShowAttributesAllString.Enabled = false;
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
                LogUse("IncompatibleCRM");
                MessageBox.Show($"Auto Number feature was introduced in\nMicrosoft Dynamics 365 July 2017 (9.0)\nCurrent version is {orgver}\n\nPlease connect to a newer organization to use this cool tool.",
                    "Organization too old", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AutoNumMgr_Load(object sender, EventArgs e)
        {
            if (settings == null)
            {
                LoadSettings();
            }
            LogUse("Load");
        }

        private void LoadSettings()
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out settings))
            {
                settings = new Settings();
                LogWarning("Settings not found => created");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
            var ass = Assembly.GetExecutingAssembly().GetName();
            var version = ass.Version.ToString();
            if (!version.Equals(settings.Version))
            {
                // Reset some settings when new version is deployed
                settings.UseLog = null;
            }
            if (settings.UseLog == null)
            {
                settings.UseLog = LogUsage.PromptToLog();
            }
            settings.Version = version;
        }

        private void btnCreateUpdate_Click(object sender, EventArgs e)
        {
            var seed = txtSeed.Enabled ? txtSeed.Text.Trim() : string.Empty;
            var format = txtNumberFormat.Text.Trim();
            var message = "Creating auto number attribute.";
            var log = "Create";
            if (!txtLogicalName.Enabled)
            {
                log = "Update";
                var attribute = gridAttributes.SelectedRows[0].DataBoundItem as AttributeProxy;
                if (string.IsNullOrEmpty(attribute.Format) && !string.IsNullOrEmpty(format))
                {   // Numbering a previously not numbered attribute
                    message = "Adding auto number format to an existing attribute will make this field read-only on all forms.\nNumber will be assigned instead.";
                    log = "ConvertToNumbered";
                }
                else if (!string.IsNullOrEmpty(attribute.Format) && string.IsNullOrWhiteSpace(format))
                {   // Removing numbering from an attribute
                    message = "This will remove auto numbering from the attribute.\nAttribute will now be editable by users.";
                    log = "ConvertFromNumbered";
                }
                else if (!attribute.Format.Trim().Equals(format))
                {   // Changing the number format
                    message = $"Auto Number format will be changed from\n  {attribute.Format}\nto\n  {format}";
                }
                else
                {   // Format is not changed
                    if (!string.IsNullOrWhiteSpace(seed))
                    {
                        if (!format.Contains("{SEQNUM:"))
                        {   // Setting seed on attribute without numbering
                            message = "Setting seed for attribute without auto numbering sequence does not make sense.";
                        }
                        else
                        {   // Updating seed for existing numbering
                            message = $"Setting seed to: {seed}";
                        }
                    }
                    else
                    {
                        message = "Update seems totally irrelevant...";
                    }
                }
            }
            if (DialogResult.OK != MessageBox.Show($"{message}\n\nPlease confirm!", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                return;
            }
            LogUse(log);
            WriteAttribute(!txtLogicalName.Enabled);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show($"This will delete the attribute.\nAny data in existing records WILL be lost.\nThis is a one way ticket with no refund!\nDo you really want to delete attribute {lblPrefix.Text + txtLogicalName.Text}?", "Confirm delete", MessageBoxButtons.OKCancel))
            {
                return;
            }
            DeleteAttribute();
        }

        private void btnGuessSeed_Click(object sender, EventArgs e)
        {
            try
            {
                var seed = GuessSeed();
                MessageBox.Show($"Parsed existing value as:\n  {seed}.", "Guess current SEQNUM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Guess current SEQNUM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            txtSeed.Text = string.Empty;
            NumberConditionsChanged();
            btnCreateUpdate.Text = "Create";
            btnDelete.Enabled = false;
            gbAttribute.Enabled = true;
        }

        private void chkAllowNoSeqNo_CheckedChanged(object sender, EventArgs e)
        {
            NumberConditionsChanged();
        }

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityselected = cmbEntities.SelectedItem is EntityMetadataProxy;
            gridAttributes.Enabled = false;
            gbAttribute.Enabled = false;
            rbShowAttributesOnlyNumber.Enabled = entityselected;
            rbShowAttributesAllString.Enabled = entityselected;
            tsbFXB.Enabled = entityselected;
            btnNew.Enabled = entityselected;
            LoadAttributes(false);
        }

        private void cmbSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterEntities();
            cmbEntities.Enabled = true;
            rbShowAttributesOnlyNumber.Enabled = false;
            rbShowAttributesAllString.Enabled = false;
            tsbFXB.Enabled = false;
            gridAttributes.Enabled = false;
            gbAttribute.Enabled = false;
            gridAttributes.DataSource = null;
        }

        private void gridAttributes_SelectionChanged(object sender, EventArgs e)
        {
            gbAttribute.Enabled = false;
            txtHint.Text = string.Empty;
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
            var logical = attribute.LogicalName;
            if (logical.Contains("_"))
            {
                lblPrefix.Text = logical.Split('_')[0] + "_";
                txtLogicalName.Text = logical.Substring(logical.IndexOf("_") + 1);
            }
            else
            {
                lblPrefix.Text = "";
                txtLogicalName.Text = logical;
            }
            txtDisplayName.Text = attribute.attributeMetadata.DisplayName?.UserLocalizedLabel?.Label;
            txtDescription.Text = attribute.attributeMetadata.Description?.UserLocalizedLabel?.Label;
            txtMaxLen.Text = attribute.attributeMetadata.MaxLength?.ToString();
            txtNumberFormat.Text = attribute.attributeMetadata.AutoNumberFormat;
            txtSeed.Text = string.Empty;
            NumberConditionsChanged();
            txtLogicalName.Enabled = false;
            btnCreateUpdate.Text = "Update";
            btnDelete.Enabled = attribute.attributeMetadata.IsPrimaryName.Value != true;
            gbAttribute.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("jonasrapp.innofactor.se/2017/10/anm.html");
        }

        private void llDatetime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{DATETIMEUTC:yyyyMMddhhmmss}");
        }

        private void llRandom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{RANDSTRING:4}");
        }

        private void llSeqnum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddMacro("{SEQNUM:5}");
        }

        private void rbShowAttributes_CheckedChanged(object sender, EventArgs e)
        {
            LoadAttributes(false);
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            LogUse("OpenAbout");
            var about = new About(this);
            about.StartPosition = FormStartPosition.CenterParent;
            about.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            about.chkStatAllow.Checked = settings.UseLog != false;
            about.ShowDialog();
            if (settings.UseLog != about.chkStatAllow.Checked)
            {
                settings.UseLog = about.chkStatAllow.Checked;
                if (settings.UseLog == true)
                {
                    LogUse("Accept", true);
                }
                else if (settings.UseLog == false)
                {
                    LogUse("Deny", true);
                }
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbFXB_Click(object sender, EventArgs e)
        {
            OpenFXB();
        }

        private void txtLanguageId_TextChanged(object sender, EventArgs e)
        {
            txtHint.Text = string.Empty;
            if (!int.TryParse(txtLanguageId.Text, out int max))
            {
                txtHint.Text = $"Language Id '{txtLanguageId.Text}' is not a valid number.";
                return;
            }
        }

        private void txtMaxLen_TextChanged(object sender, EventArgs e)
        {
            NumberConditionsChanged();
        }

        private void txtNumberFormat_TextChanged(object sender, EventArgs e)
        {
            NumberConditionsChanged();
        }

        private void txtSeed_TextChanged(object sender, EventArgs e)
        {
            NumberConditionsChanged();
            NewMethod();
        }

        #endregion Form Event Handlers

        #region My Methods

        internal void LogUse(string action, bool forceLog = false)
        {
            if (settings == null)
            {
                LoadSettings();
            }
            if (settings.UseLog == true || forceLog)
            {
                LogUsage.DoLog(action);
            }
        }

        internal void UpdateUI(Action action)
        {
            MethodInvoker mi = delegate
            {
                action();
            };
            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
            }
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

        private void DeleteAttribute()
        {
            var entityname = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata.LogicalName;
            var attributename = lblPrefix.Text + txtLogicalName.Text;
            var req = new DeleteAttributeRequest
            {
                EntityLogicalName = entityname,
                LogicalName = attributename
            };
            WorkAsync(new WorkAsyncInfo("Deleting attribute...",
            (eventargs) =>
            {
                LogUse("Delete");
                Service.Execute(req);
            })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show($"Delete attribute failed:\n{completedargs.Error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Attribute deleted!");
                    }
                    UpdateUI(ForceLoadAttributes);
                }
            });
        }

        private void EnableControls(bool enabled)
        {
            //Enabled = enabled;
        }

        private void FilterEntities()
        {
            cmbEntities.Items.Clear();
            btnNew.Enabled = false;
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

        private void ForceLoadAttributes()
        {
            LoadAttributes(true);
        }

        private int GuessSeed()
        {
            LogUse("GuessSeed");
            var format = txtNumberFormat.Text;
            var sample = ParseNumberFormat(format, "9999999999");

            if (!format.Contains("{SEQNUM:") || !format.Contains("}"))
            {
                throw new FormatException("Format string must contain a {SEQNUM:n} placeholder.");
            }
            var seqstart = sample.IndexOf("9999999999");
            var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
            lenghtstr = lenghtstr.Split('}')[0];
            var length = 0;
            if (int.TryParse(lenghtstr, out length))
            {
                if (length < 1)
                {
                    throw new FormatException("Failed to parse SEQNUM length.");
                }
            }
            var entity = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata;
            var attributename = lblPrefix.Text + txtLogicalName.Text;
            var fetchxml = "<fetch top='1' ><entity name='" + entity.LogicalName + "' >" +
                "<attribute name='" + attributename + "' />" +
                "<filter><condition attribute='" + attributename + "' operator='not-null' /></filter>" +
                "<order attribute='createdon' descending='true' /></entity></fetch>";
            var lastrecord = Service.RetrieveMultiple(new FetchExpression(fetchxml)).Entities.FirstOrDefault();
            var result = 0;
            if (lastrecord == null)
            {
                throw new Exception("No numbered data found for attribute " + attributename);
            }
            var lastvalue = lastrecord[attributename].ToString();
            if (lastvalue.Length >= seqstart + length)
            {
                var lastseqstr = lastvalue.Substring(seqstart, length);
                if (int.TryParse(lastseqstr, out int lastseq))
                {
                    LogUse("GuessSeed succeeded");
                    result = lastseq;
                }
            }
            if (result == 0)
            {
                LogUse("GuessSeed failed");
                throw new Exception("That was hard. Couldn't even guess what current SEQNUM is.\n" +
                    "Numbered value for last created record is:  \n" + lastvalue);
            }
            return result;
        }

        private void LoadAttributes(bool force)
        {
            gridAttributes.DataSource = null;
            var entity = cmbEntities.SelectedItem as EntityMetadataProxy;
            var onlyNumbered = rbShowAttributesOnlyNumber.Checked;
            WorkAsync(new WorkAsyncInfo("Loading auto number attributes...",
                (eventargs) =>
                {
                    if (force || entity.Metadata.Attributes == null)
                    {
                        eventargs.Result = MetadataHelper.LoadEntityDetails(Service, entity.Metadata.LogicalName).EntityMetadata.FirstOrDefault();
                    }
                    else
                    {
                        eventargs.Result = entity.Metadata;
                    }
                })
            {
                PostWorkCallBack = (completedargs) =>
                  {
                      if (completedargs.Result is EntityMetadata)
                      {
                          try
                          {
                              entity.Metadata = (EntityMetadata)completedargs.Result;
                              var attributes = entity.Metadata.Attributes
                                .Where(a => a.AttributeType == AttributeTypeCode.String &&
                                    a.IsValidForCreate.Value == true &&
                                    a.IsCustomizable.Value == true &&
                                    (!onlyNumbered || !string.IsNullOrEmpty(a.AutoNumberFormat)))
                                .Select(a => new AttributeProxy((StringAttributeMetadata)a)).OrderBy(a => a.LogicalName).ToList();
                              var bindingList = new BindingList<AttributeProxy>(attributes);
                              var source = new BindingSource(bindingList, null);
                              UpdateUI(() =>
                              {
                                  gridAttributes.DataSource = source;
                                  gridAttributes.Enabled = true;
                                  gridAttributes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                              });
                          }
                          catch (MissingMethodException mex)
                          {
                              LogUse("IncompatibleSDK");
                              MessageBox.Show("It seems you are using too old SDK, that is unaware of the AutoNumberFormat property.", "SDK error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          }
                      }
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

        private void LoadSolutions()
        {
            cmbSolution.Items.Clear();
            cmbSolution.Enabled = false;
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
                            cmbSolution.Enabled = true;
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

        private void NewMethod()
        {
            throw new NotImplementedException();
        }

        private void NumberConditionsChanged()
        {
            txtHint.Text = string.Empty;
            var seed = txtSeed.Enabled ? txtSeed.Text.Trim() : string.Empty;
            if (!string.IsNullOrEmpty(seed) && !int.TryParse(seed, out int max))
            {
                txtHint.Text = $"Seed '{seed}' is not a valid number.";
                return;
            }
            if (!int.TryParse(txtMaxLen.Text, out int maxlen))
            {
                txtHint.Text = $"Max Length '{txtMaxLen.Text}' is not a valid number.";
                return;
            }
            txtSample.Text = ParseNumberFormat(txtNumberFormat.Text, seed);
            if (txtSample.Text.Length > maxlen)
            {
                txtHint.Text = "It looks like the maximum length of the attribute will be exceeded.\n\rCorrect this before saving the attribute.";
            }
        }

        private void OpenFXB()
        {
            var entity = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata;
            var attributes = ((gridAttributes.DataSource as BindingSource)?.DataSource as BindingList<AttributeProxy>).Select(a => a.attributeMetadata.LogicalName);
            var fetchxml = "<fetch top='10' ><entity name='" + entity.LogicalName + "' >" +
                "<attribute name='" + entity.PrimaryNameAttribute + "' /><attribute name='createdon' />" +
                string.Join("", attributes.Select(a => "<attribute name='" + a + "' />")) +
                "<order attribute='createdon' descending='true' /></entity></fetch>";
            var messageBusEventArgs = new MessageBusEventArgs("FetchXML Builder")
            {
                TargetArgument = fetchxml
            };
            OnOutgoingMessage(this, messageBusEventArgs);
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

        private string ParseFormatSEQNUM(string format, string seed)
        {
            var validseqnum = false;
            try
            {
                if (!format.Contains("{SEQNUM:") || !format.Contains("}"))
                {
                    if (!chkAllowNoSeqNo.Checked)
                    {
                        throw new FormatException("Format string must contain a {SEQNUM:n} placeholder.");
                    }
                    else
                    {
                        return format;
                    }
                }
                var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                if (int.TryParse(lenghtstr, out int length))
                {
                    if (length < 1)
                    {
                        throw new FormatException("SEQNUM length must be 1 or higher.");
                    }
                    var seedno = string.IsNullOrEmpty(seed) ? 1 : Int64.Parse(seed);
                    var sequence = string.Format("{0:" + new string('0', length) + "}", seedno);
                    format = format.Replace("{SEQNUM:" + lenghtstr + "}", sequence);
                    validseqnum = true;
                }
                else
                {
                    throw new FormatException("Invalid SEQNUM format. Enter as {SEQNUM:n} where n is length of sequence.");
                }
                if (format.Contains("{SEQNUM:"))
                {
                    throw new FormatException("Format string must only contain one {SEQNUM:n} placeholder.");
                }
            }
            finally
            {
                txtSeed.Enabled = validseqnum;
                btnGuessSeed.Enabled = validseqnum && !txtLogicalName.Enabled;
            }
            return format;
        }

        private string ParseNumberFormat(string format, string seed)
        {
            try
            {
                format = ParseFormatSEQNUM(format, seed);
                format = ParseFormatRANDSTRING(format);
                format = ParseFormatDATETIMEUTC(format);
                txtHint.Text = "Format successfully parsed.";
                btnCreateUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                txtHint.Text = ex.Message;
                format = string.Empty;
                btnCreateUpdate.Enabled = false;
            }
            return format;
        }

        private void WriteAttribute(bool update)
        {
            var langid = int.Parse(txtLanguageId.Text.Trim());
            var solutionname = (cmbSolution.SelectedItem as SolutionProxy)?.UniqueName;
            var entity = ((EntityMetadataProxy)cmbEntities.SelectedItem).Metadata;
            var logicalname = lblPrefix.Text + txtLogicalName.Text.Trim();
            var schemaname = update ? (gridAttributes.SelectedRows[0].DataBoundItem as AttributeProxy).attributeMetadata.SchemaName : logicalname;
            var format = txtNumberFormat.Text.Trim();
            if (format.Equals(ParseFormatSEQNUM(format, string.Empty)))
            {
                if (DialogResult.Cancel==MessageBox.Show("Creating number formats without SEQNUM placeholder can result in non-unique values.\n\nPlease confirm!", "No sequence number", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    return;
                }
            }
            var seed = txtSeed.Enabled ? txtSeed.Text.Trim() : string.Empty;
            var attribute = new StringAttributeMetadata
            {
                AutoNumberFormat = format,
                LogicalName = logicalname,
                SchemaName = schemaname,
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = int.Parse(txtMaxLen.Text.Trim()),
                DisplayName = new Microsoft.Xrm.Sdk.Label(txtDisplayName.Text, langid),
                Description = new Microsoft.Xrm.Sdk.Label(txtDescription.Text, langid)
            };
            OrganizationRequest req;
            if (update)
            {
                req = new UpdateAttributeRequest
                {
                    EntityName = entity.LogicalName,
                    Attribute = attribute,
                    SolutionUniqueName = solutionname
                };
                if (update && !string.IsNullOrEmpty(seed))
                {
                    if (DialogResult.Yes != MessageBox.Show("Setting the seed for existing an attribute MAY result in duplicate data!\nDo you really want to change the seed?", "Confirm seed", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        return;
                    }
                }
            }
            else
            {
                req = new CreateAttributeRequest
                {
                    EntityName = entity.LogicalName,
                    Attribute = attribute,
                    SolutionUniqueName = solutionname
                };
            }
            WorkAsync(new WorkAsyncInfo("Saving attribute...",
            (eventargs) =>
            {
                Service.Execute(req);
                if (!string.IsNullOrEmpty(seed))
                {
                    LogUse("SetSeed");
                    Service.Execute(new SetAutoNumberSeedRequest
                    {
                        EntityName = entity.LogicalName,
                        AttributeName = logicalname,
                        Value = int.Parse(seed)
                    });
                }
            })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show($"Save attribute failed:\n{completedargs.Error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Attribute saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    UpdateUI(ForceLoadAttributes);
                }
            });
        }

        #endregion My Methods
    }
}