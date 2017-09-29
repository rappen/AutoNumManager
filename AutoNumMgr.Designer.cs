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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogicalName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gbNewAttribute = new System.Windows.Forms.GroupBox();
            this.llRandom = new System.Windows.Forms.LinkLabel();
            this.llDatetime = new System.Windows.Forms.LinkLabel();
            this.llSeqnum = new System.Windows.Forms.LinkLabel();
            this.txtSample = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLanguageId = new System.Windows.Forms.TextBox();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtMaxLen = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumberFormat = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.cmbSolution = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gbExisting = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSetSeed = new System.Windows.Forms.Button();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.gbNewAttribute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.gbExisting.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(906, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 22);
            this.toolStripButton1.Text = "Close";
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
            this.cmbEntities.Size = new System.Drawing.Size(351, 21);
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
            this.txtLogicalName.Location = new System.Drawing.Point(160, 26);
            this.txtLogicalName.Name = "txtLogicalName";
            this.txtLogicalName.Size = new System.Drawing.Size(297, 20);
            this.txtLogicalName.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // gbNewAttribute
            // 
            this.gbNewAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNewAttribute.Controls.Add(this.lblPrefix);
            this.gbNewAttribute.Controls.Add(this.llRandom);
            this.gbNewAttribute.Controls.Add(this.llDatetime);
            this.gbNewAttribute.Controls.Add(this.llSeqnum);
            this.gbNewAttribute.Controls.Add(this.txtSample);
            this.gbNewAttribute.Controls.Add(this.label3);
            this.gbNewAttribute.Controls.Add(this.label6);
            this.gbNewAttribute.Controls.Add(this.txtLanguageId);
            this.gbNewAttribute.Controls.Add(this.btnCreateNew);
            this.gbNewAttribute.Controls.Add(this.linkLabel1);
            this.gbNewAttribute.Controls.Add(this.txtMaxLen);
            this.gbNewAttribute.Controls.Add(this.label7);
            this.gbNewAttribute.Controls.Add(this.txtNumberFormat);
            this.gbNewAttribute.Controls.Add(this.txtDescription);
            this.gbNewAttribute.Controls.Add(this.label5);
            this.gbNewAttribute.Controls.Add(this.txtDisplayName);
            this.gbNewAttribute.Controls.Add(this.label4);
            this.gbNewAttribute.Controls.Add(this.txtLogicalName);
            this.gbNewAttribute.Controls.Add(this.label2);
            this.gbNewAttribute.Enabled = false;
            this.gbNewAttribute.Location = new System.Drawing.Point(12, 103);
            this.gbNewAttribute.Name = "gbNewAttribute";
            this.gbNewAttribute.Size = new System.Drawing.Size(471, 272);
            this.gbNewAttribute.TabIndex = 2;
            this.gbNewAttribute.TabStop = false;
            this.gbNewAttribute.Text = "New Auto Numbered attribute";
            // 
            // llRandom
            // 
            this.llRandom.AutoSize = true;
            this.llRandom.Location = new System.Drawing.Point(258, 181);
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
            this.llDatetime.Location = new System.Drawing.Point(167, 182);
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
            this.txtSample.Size = new System.Drawing.Size(351, 20);
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
            // btnCreateNew
            // 
            this.btnCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNew.Location = new System.Drawing.Point(323, 238);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(134, 23);
            this.btnCreateNew.TabIndex = 11;
            this.btnCreateNew.Text = "Create attribute";
            this.btnCreateNew.UseVisualStyleBackColor = true;
            this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
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
            this.txtMaxLen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxLen.Location = new System.Drawing.Point(106, 104);
            this.txtMaxLen.Name = "txtMaxLen";
            this.txtMaxLen.Size = new System.Drawing.Size(351, 20);
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
            this.txtNumberFormat.Size = new System.Drawing.Size(351, 20);
            this.txtNumberFormat.TabIndex = 6;
            this.txtNumberFormat.TextChanged += new System.EventHandler(this.txtNumberFormat_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(106, 78);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(351, 20);
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
            this.txtDisplayName.Size = new System.Drawing.Size(351, 20);
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
            this.splitContainer1.Panel1.Controls.Add(this.gbTarget);
            this.splitContainer1.Panel1.Controls.Add(this.gbNewAttribute);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbExisting);
            this.splitContainer1.Size = new System.Drawing.Size(906, 552);
            this.splitContainer1.SplitterDistance = 495;
            this.splitContainer1.TabIndex = 7;
            // 
            // gbTarget
            // 
            this.gbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTarget.Controls.Add(this.cmbSolution);
            this.gbTarget.Controls.Add(this.label8);
            this.gbTarget.Controls.Add(this.cmbEntities);
            this.gbTarget.Controls.Add(this.label1);
            this.gbTarget.Location = new System.Drawing.Point(12, 13);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(471, 84);
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
            this.cmbSolution.Size = new System.Drawing.Size(351, 21);
            this.cmbSolution.TabIndex = 1;
            this.cmbSolution.SelectedIndexChanged += new System.EventHandler(this.cmbSolution_SelectedIndexChanged);
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
            // gbExisting
            // 
            this.gbExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExisting.Controls.Add(this.btnSetSeed);
            this.gbExisting.Controls.Add(this.textBox1);
            this.gbExisting.Controls.Add(this.label10);
            this.gbExisting.Controls.Add(this.label9);
            this.gbExisting.Enabled = false;
            this.gbExisting.Location = new System.Drawing.Point(14, 13);
            this.gbExisting.Name = "gbExisting";
            this.gbExisting.Size = new System.Drawing.Size(379, 525);
            this.gbExisting.TabIndex = 3;
            this.gbExisting.TabStop = false;
            this.gbExisting.Text = "Existing Auto Number attributes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(103, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Available in next version!";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "New Seed";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 293);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 18;
            // 
            // btnSetSeed
            // 
            this.btnSetSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetSeed.Location = new System.Drawing.Point(228, 291);
            this.btnSetSeed.Name = "btnSetSeed";
            this.btnSetSeed.Size = new System.Drawing.Size(134, 23);
            this.btnSetSeed.TabIndex = 19;
            this.btnSetSeed.Text = "Set new seed";
            this.btnSetSeed.UseVisualStyleBackColor = true;
            this.btnSetSeed.Click += new System.EventHandler(this.button1_Click);
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
            // AutoNumMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AutoNumMgr";
            this.Size = new System.Drawing.Size(906, 577);
            this.OnCloseTool += new System.EventHandler(this.AutoNumMgr_OnCloseTool);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.AutoNumMgr_ConnectionUpdated);
            this.Load += new System.EventHandler(this.AutoNumMgr_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbNewAttribute.ResumeLayout(false);
            this.gbNewAttribute.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.gbExisting.ResumeLayout(false);
            this.gbExisting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEntities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogicalName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox gbNewAttribute;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtMaxLen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumberFormat;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateNew;
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
        private System.Windows.Forms.Button btnSetSeed;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPrefix;
    }
}
