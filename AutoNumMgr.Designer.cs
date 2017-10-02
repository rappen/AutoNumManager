namespace Rappen.XTB.AutoNumManager
{
    partial class AutoNumMgr
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoNumMgr));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFXB = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogicalName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gbAttribute = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.llRandom = new System.Windows.Forms.LinkLabel();
            this.llDatetime = new System.Windows.Forms.LinkLabel();
            this.llSeqnum = new System.Windows.Forms.LinkLabel();
            this.txtSample = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLanguageId = new System.Windows.Forms.TextBox();
            this.btnCreateUpdate = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtMaxLen = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumberFormat = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbExisting = new System.Windows.Forms.GroupBox();
            this.gridAttributes = new System.Windows.Forms.DataGridView();
            this.Attribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.cmbSolution = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.gbAttribute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbExisting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttributes)).BeginInit();
            this.gbTarget.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbFXB,
            this.tsbAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(870, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(56, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFXB
            // 
            this.tsbFXB.Enabled = false;
            this.tsbFXB.Image = ((System.Drawing.Image)(resources.GetObject("tsbFXB.Image")));
            this.tsbFXB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFXB.Name = "tsbFXB";
            this.tsbFXB.Size = new System.Drawing.Size(204, 22);
            this.tsbFXB.Text = "Show data with FetchXML Builder";
            this.tsbFXB.Click += new System.EventHandler(this.tsbFXB_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(91, 22);
            this.tsbAbout.Text = "About ANM";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entity";
            // 
            // cmbEntities
            // 
            this.cmbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntities.Enabled = false;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(106, 52);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(314, 21);
            this.cmbEntities.TabIndex = 2;
            this.cmbEntities.SelectedIndexChanged += new System.EventHandler(this.cmbEntities_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Logical Name";
            // 
            // txtLogicalName
            // 
            this.txtLogicalName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogicalName.Enabled = false;
            this.txtLogicalName.Location = new System.Drawing.Point(160, 26);
            this.txtLogicalName.Name = "txtLogicalName";
            this.txtLogicalName.Size = new System.Drawing.Size(263, 20);
            this.txtLogicalName.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // gbAttribute
            // 
            this.gbAttribute.Controls.Add(this.btnDelete);
            this.gbAttribute.Controls.Add(this.label9);
            this.gbAttribute.Controls.Add(this.txtSeed);
            this.gbAttribute.Controls.Add(this.lblPrefix);
            this.gbAttribute.Controls.Add(this.llRandom);
            this.gbAttribute.Controls.Add(this.llDatetime);
            this.gbAttribute.Controls.Add(this.llSeqnum);
            this.gbAttribute.Controls.Add(this.txtSample);
            this.gbAttribute.Controls.Add(this.label3);
            this.gbAttribute.Controls.Add(this.label6);
            this.gbAttribute.Controls.Add(this.txtLanguageId);
            this.gbAttribute.Controls.Add(this.btnCreateUpdate);
            this.gbAttribute.Controls.Add(this.linkLabel1);
            this.gbAttribute.Controls.Add(this.txtMaxLen);
            this.gbAttribute.Controls.Add(this.label7);
            this.gbAttribute.Controls.Add(this.txtNumberFormat);
            this.gbAttribute.Controls.Add(this.txtDescription);
            this.gbAttribute.Controls.Add(this.label5);
            this.gbAttribute.Controls.Add(this.txtDisplayName);
            this.gbAttribute.Controls.Add(this.label4);
            this.gbAttribute.Controls.Add(this.txtLogicalName);
            this.gbAttribute.Controls.Add(this.label2);
            this.gbAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAttribute.Enabled = false;
            this.gbAttribute.Location = new System.Drawing.Point(0, 0);
            this.gbAttribute.Name = "gbAttribute";
            this.gbAttribute.Size = new System.Drawing.Size(437, 294);
            this.gbAttribute.TabIndex = 2;
            this.gbAttribute.TabStop = false;
            this.gbAttribute.Text = "New Auto Numbered attribute";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Seed";
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(106, 229);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(100, 20);
            this.txtSeed.TabIndex = 11;
            this.txtSeed.TextChanged += new System.EventHandler(this.txtSeed_TextChanged);
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(106, 29);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblPrefix.TabIndex = 24;
            this.lblPrefix.Text = "new_";
            // 
            // llRandom
            // 
            this.llRandom.AutoSize = true;
            this.llRandom.Location = new System.Drawing.Point(256, 183);
            this.llRandom.Name = "llRandom";
            this.llRandom.Size = new System.Drawing.Size(79, 13);
            this.llRandom.TabIndex = 9;
            this.llRandom.TabStop = true;
            this.llRandom.Text = "RANDSTRING";
            this.llRandom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRandom_LinkClicked);
            // 
            // llDatetime
            // 
            this.llDatetime.AutoSize = true;
            this.llDatetime.Location = new System.Drawing.Point(166, 183);
            this.llDatetime.Name = "llDatetime";
            this.llDatetime.Size = new System.Drawing.Size(84, 13);
            this.llDatetime.TabIndex = 8;
            this.llDatetime.TabStop = true;
            this.llDatetime.Text = "DATETIMEUTC";
            this.llDatetime.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDatetime_LinkClicked);
            // 
            // llSeqnum
            // 
            this.llSeqnum.AutoSize = true;
            this.llSeqnum.Location = new System.Drawing.Point(106, 183);
            this.llSeqnum.Name = "llSeqnum";
            this.llSeqnum.Size = new System.Drawing.Size(54, 13);
            this.llSeqnum.TabIndex = 7;
            this.llSeqnum.TabStop = true;
            this.llSeqnum.Text = "SEQNUM";
            this.llSeqnum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSeqnum_LinkClicked);
            // 
            // txtSample
            // 
            this.txtSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSample.Location = new System.Drawing.Point(106, 203);
            this.txtSample.Name = "txtSample";
            this.txtSample.ReadOnly = true;
            this.txtSample.Size = new System.Drawing.Size(317, 20);
            this.txtSample.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Sample number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Language Id";
            // 
            // txtLanguageId
            // 
            this.txtLanguageId.Location = new System.Drawing.Point(106, 130);
            this.txtLanguageId.Name = "txtLanguageId";
            this.txtLanguageId.Size = new System.Drawing.Size(100, 20);
            this.txtLanguageId.TabIndex = 5;
            // 
            // btnCreateUpdate
            // 
            this.btnCreateUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateUpdate.Location = new System.Drawing.Point(106, 259);
            this.btnCreateUpdate.Name = "btnCreateUpdate";
            this.btnCreateUpdate.Size = new System.Drawing.Size(134, 23);
            this.btnCreateUpdate.TabIndex = 12;
            this.btnCreateUpdate.Text = "Create attribute";
            this.btnCreateUpdate.UseVisualStyleBackColor = true;
            this.btnCreateUpdate.Click += new System.EventHandler(this.btnCreateUpdate_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 159);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(79, 13);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Number Format";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtMaxLen
            // 
            this.txtMaxLen.Location = new System.Drawing.Point(106, 104);
            this.txtMaxLen.Name = "txtMaxLen";
            this.txtMaxLen.Size = new System.Drawing.Size(100, 20);
            this.txtMaxLen.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Maximum Length";
            // 
            // txtNumberFormat
            // 
            this.txtNumberFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumberFormat.Location = new System.Drawing.Point(106, 156);
            this.txtNumberFormat.Name = "txtNumberFormat";
            this.txtNumberFormat.Size = new System.Drawing.Size(317, 20);
            this.txtNumberFormat.TabIndex = 6;
            this.txtNumberFormat.TextChanged += new System.EventHandler(this.txtNumberFormat_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(106, 78);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(317, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Description";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayName.Location = new System.Drawing.Point(106, 52);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(317, 20);
            this.txtDisplayName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Display Name";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbExisting);
            this.splitContainer1.Panel1.Controls.Add(this.gbTarget);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbAttribute);
            this.splitContainer1.Size = new System.Drawing.Size(870, 552);
            this.splitContainer1.SplitterDistance = 429;
            this.splitContainer1.TabIndex = 7;
            // 
            // gbExisting
            // 
            this.gbExisting.Controls.Add(this.gridAttributes);
            this.gbExisting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExisting.Location = new System.Drawing.Point(0, 111);
            this.gbExisting.Name = "gbExisting";
            this.gbExisting.Size = new System.Drawing.Size(429, 441);
            this.gbExisting.TabIndex = 3;
            this.gbExisting.TabStop = false;
            this.gbExisting.Text = "Existing Auto Number attributes";
            // 
            // gridAttributes
            // 
            this.gridAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAttributes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Attribute,
            this.Format});
            this.gridAttributes.Location = new System.Drawing.Point(9, 22);
            this.gridAttributes.Name = "gridAttributes";
            this.gridAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAttributes.Size = new System.Drawing.Size(411, 413);
            this.gridAttributes.TabIndex = 20;
            this.gridAttributes.SelectionChanged += new System.EventHandler(this.gridAttributes_SelectionChanged);
            // 
            // Attribute
            // 
            this.Attribute.DataPropertyName = "Attribute";
            this.Attribute.FillWeight = 150F;
            this.Attribute.HeaderText = "Attribute";
            this.Attribute.Name = "Attribute";
            this.Attribute.ReadOnly = true;
            // 
            // Format
            // 
            this.Format.DataPropertyName = "Format";
            this.Format.FillWeight = 200F;
            this.Format.HeaderText = "Format";
            this.Format.Name = "Format";
            this.Format.ReadOnly = true;
            // 
            // gbTarget
            // 
            this.gbTarget.Controls.Add(this.cmbSolution);
            this.gbTarget.Controls.Add(this.btnNew);
            this.gbTarget.Controls.Add(this.label8);
            this.gbTarget.Controls.Add(this.cmbEntities);
            this.gbTarget.Controls.Add(this.label1);
            this.gbTarget.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTarget.Location = new System.Drawing.Point(0, 0);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(429, 111);
            this.gbTarget.TabIndex = 1;
            this.gbTarget.TabStop = false;
            this.gbTarget.Text = "Target";
            // 
            // cmbSolution
            // 
            this.cmbSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolution.FormattingEnabled = true;
            this.cmbSolution.Location = new System.Drawing.Point(106, 26);
            this.cmbSolution.Name = "cmbSolution";
            this.cmbSolution.Size = new System.Drawing.Size(314, 21);
            this.cmbSolution.TabIndex = 1;
            this.cmbSolution.SelectedIndexChanged += new System.EventHandler(this.cmbSolution_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(287, 79);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(134, 23);
            this.btnNew.TabIndex = 19;
            this.btnNew.Text = "New Attribute";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Solution";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(289, 259);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(134, 23);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "Delete attribute";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AutoNumMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AutoNumMgr";
            this.Size = new System.Drawing.Size(870, 577);
            this.OnCloseTool += new System.EventHandler(this.AutoNumMgr_OnCloseTool);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.AutoNumMgr_ConnectionUpdated);
            this.Load += new System.EventHandler(this.AutoNumMgr_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbAttribute.ResumeLayout(false);
            this.gbAttribute.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbExisting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAttributes)).EndInit();
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEntities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogicalName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox gbAttribute;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtMaxLen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumberFormat;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLanguageId;
        private System.Windows.Forms.GroupBox gbTarget;
        private System.Windows.Forms.ComboBox cmbSolution;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSample;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llRandom;
        private System.Windows.Forms.LinkLabel llDatetime;
        private System.Windows.Forms.LinkLabel llSeqnum;
        private System.Windows.Forms.GroupBox gbExisting;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.DataGridView gridAttributes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Attribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Format;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.ToolStripButton tsbFXB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.Button btnDelete;
    }
}
