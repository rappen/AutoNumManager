using System;
using System.Windows.Forms;

namespace Rappen.XTB.AutoNumManager
{
    public partial class About : Form
    {
        #region Private Fields

        private AutoNumMgr autoNumMgr;

        #endregion Private Fields

        #region Public Constructors

        public About(AutoNumMgr autoNumMgr)
        {
            InitializeComponent();
            this.autoNumMgr = autoNumMgr;
        }

        #endregion Public Constructors

        #region Private Methods

        private void chkStatAllow_CheckedChanged(object sender, EventArgs e)
        {
            if (Visible && chkStatAllow.Checked)
            {
                MessageBox.Show("Thank You!\n\nHappy numbering :)\n\n/Jonas", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            autoNumMgr.LogUse("About-OpenHomepage");
            System.Diagnostics.Process.Start("http://anm.xrmtoolbox.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            autoNumMgr.LogUse("About-OpenBlog");
            System.Diagnostics.Process.Start("http://jonasrapp.innofactor.se");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            autoNumMgr.LogUse("About-OpenInnofactor");
            System.Diagnostics.Process.Start("http://www.innofactor.se");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            autoNumMgr.LogUse("About-OpenTwitter");
            System.Diagnostics.Process.Start("http://twitter.com/rappen");
        }

        #endregion Private Methods
    }
}