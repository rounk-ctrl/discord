using System;
using System.Windows.Forms;

#pragma warning disable IDE1006 // Naming Styles
namespace discord
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
			Load += LoginForm_Load;
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			rememberMeChk.Checked = Properties.Settings.Default.rememberMe;
			autoSignChk.Checked = Properties.Settings.Default.autoSignIn;
			if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.id))
			{
				userTokenField.Text = Properties.Settings.Default.id;
				signInBtn.Enabled = true;
			}
			if (Properties.Settings.Default.autoSignIn)
			{
				DiscordHelper.token = Properties.Settings.Default.id;
				Close();
			}
		}

		private void rememberMeChk_CheckedChanged(object sender, EventArgs e)
		{
			if (rememberMeChk.Checked)
			{
				Properties.Settings.Default.rememberMe = true;
			}
			else
			{
				Properties.Settings.Default.rememberMe = false;
			}
			Properties.Settings.Default.Save();
		}

		private void autoSignChk_CheckedChanged(object sender, EventArgs e)
		{
			if (autoSignChk.Checked)
			{
				Properties.Settings.Default.autoSignIn = true;
			}
			else
			{
				Properties.Settings.Default.autoSignIn = false;
			}
			Properties.Settings.Default.Save();
		}

		private void signInBtn_Click(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.rememberMe)
			{
				Properties.Settings.Default.id = userTokenField.Text;
				Properties.Settings.Default.Save();
			}
			DiscordHelper.token = userTokenField.Text;
			Close();
		}

		private void helpLnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void userTokenField_TextUpdate(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(userTokenField.Text))
			{
				signInBtn.Enabled = true;
			}
			else
			{
				signInBtn.Enabled = false;
			}
		}
	}
}
