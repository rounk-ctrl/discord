
namespace discord
{
	partial class LoginForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.userTokenField = new System.Windows.Forms.ComboBox();
			this.rememberMeChk = new System.Windows.Forms.CheckBox();
			this.autoSignChk = new System.Windows.Forms.CheckBox();
			this.helpLnk = new System.Windows.Forms.LinkLabel();
			this.signInBtn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Actions";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Tools";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
			this.menuItem4.Text = "Help";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "About";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = global::discord.Properties.Resources.logo;
			this.pictureBox1.Location = new System.Drawing.Point(29, 60);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(274, 73);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(26, 156);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "User Token";
			// 
			// userTokenField
			// 
			this.userTokenField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userTokenField.FormattingEnabled = true;
			this.userTokenField.Location = new System.Drawing.Point(29, 175);
			this.userTokenField.Name = "userTokenField";
			this.userTokenField.Size = new System.Drawing.Size(274, 21);
			this.userTokenField.TabIndex = 2;
			this.userTokenField.TextUpdate += new System.EventHandler(this.userTokenField_TextUpdate);
			// 
			// rememberMeChk
			// 
			this.rememberMeChk.AutoSize = true;
			this.rememberMeChk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rememberMeChk.Location = new System.Drawing.Point(29, 203);
			this.rememberMeChk.Name = "rememberMeChk";
			this.rememberMeChk.Size = new System.Drawing.Size(104, 19);
			this.rememberMeChk.TabIndex = 3;
			this.rememberMeChk.Text = "Remember me";
			this.rememberMeChk.UseVisualStyleBackColor = true;
			this.rememberMeChk.CheckedChanged += new System.EventHandler(this.rememberMeChk_CheckedChanged);
			// 
			// autoSignChk
			// 
			this.autoSignChk.AutoSize = true;
			this.autoSignChk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.autoSignChk.Location = new System.Drawing.Point(29, 224);
			this.autoSignChk.Name = "autoSignChk";
			this.autoSignChk.Size = new System.Drawing.Size(138, 19);
			this.autoSignChk.TabIndex = 4;
			this.autoSignChk.Text = "Automatically sign in";
			this.autoSignChk.UseVisualStyleBackColor = true;
			this.autoSignChk.CheckedChanged += new System.EventHandler(this.autoSignChk_CheckedChanged);
			// 
			// helpLnk
			// 
			this.helpLnk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.helpLnk.AutoSize = true;
			this.helpLnk.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
			this.helpLnk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.helpLnk.LinkColor = System.Drawing.SystemColors.Highlight;
			this.helpLnk.Location = new System.Drawing.Point(232, 204);
			this.helpLnk.Name = "helpLnk";
			this.helpLnk.Size = new System.Drawing.Size(71, 15);
			this.helpLnk.TabIndex = 5;
			this.helpLnk.TabStop = true;
			this.helpLnk.Text = "How to get?";
			this.helpLnk.VisitedLinkColor = System.Drawing.SystemColors.Highlight;
			this.helpLnk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLnk_LinkClicked);
			// 
			// signInBtn
			// 
			this.signInBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.signInBtn.Enabled = false;
			this.signInBtn.Location = new System.Drawing.Point(29, 250);
			this.signInBtn.Name = "signInBtn";
			this.signInBtn.Size = new System.Drawing.Size(274, 27);
			this.signInBtn.TabIndex = 6;
			this.signInBtn.Text = "Sign in";
			this.signInBtn.UseVisualStyleBackColor = true;
			this.signInBtn.Click += new System.EventHandler(this.signInBtn_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label2.Location = new System.Drawing.Point(12, 417);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Copyright © 1997-2005 Discord Technologies Ltd.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(328, 444);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.signInBtn);
			this.Controls.Add(this.helpLnk);
			this.Controls.Add(this.autoSignChk);
			this.Controls.Add(this.rememberMeChk);
			this.Controls.Add(this.userTokenField);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Discord Messenger";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox userTokenField;
		private System.Windows.Forms.CheckBox rememberMeChk;
		private System.Windows.Forms.CheckBox autoSignChk;
		private System.Windows.Forms.LinkLabel helpLnk;
		private System.Windows.Forms.Button signInBtn;
		private System.Windows.Forms.Label label2;
	}
}