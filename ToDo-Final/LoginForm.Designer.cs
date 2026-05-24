namespace ToDo_Final
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            elipseLoginForm = new Guna.UI2.WinForms.Guna2Elipse(components);
            shadowLoginForm = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btnLogin = new Guna.UI2.WinForms.Guna2Button();
            btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            pnlLeft = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            Logotxt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Headertxt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubHeader = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lnkRegister = new LinkLabel();
            lnkForgot = new LinkLabel();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // elipseLoginForm
            // 
            elipseLoginForm.BorderRadius = 25;
            elipseLoginForm.TargetControl = this;
            // 
            // shadowLoginForm
            // 
            shadowLoginForm.TargetForm = this;
            // 
            // txtUsername
            // 
            txtUsername.BorderColor = Color.FromArgb(45, 50, 70);
            txtUsername.BorderRadius = 12;
            txtUsername.CustomizableEdges = customizableEdges11;
            txtUsername.DefaultText = "";
            txtUsername.FillColor = Color.FromArgb(23, 25, 35);
            txtUsername.FocusedState.BorderColor = Color.FromArgb(99, 102, 241);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.White;
            txtUsername.HoverState.BorderColor = Color.FromArgb(99, 102, 241);
            txtUsername.Location = new Point(386, 160);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtUsername.PlaceholderText = "Kullanıcı Adı";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtUsername.Size = new Size(340, 50);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BorderColor = Color.FromArgb(45, 50, 70);
            txtPassword.BorderRadius = 12;
            txtPassword.CustomizableEdges = customizableEdges9;
            txtPassword.DefaultText = "";
            txtPassword.FillColor = Color.FromArgb(23, 25, 35);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(99, 102, 241);
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.White;
            txtPassword.HoverState.BorderColor = Color.FromArgb(99, 102, 241);
            txtPassword.Location = new Point(386, 230);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtPassword.PlaceholderText = "Şifre";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtPassword.Size = new Size(340, 50);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BorderRadius = 12;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.CustomizableEdges = customizableEdges7;
            btnLogin.FillColor = Color.FromArgb(99, 102, 241);
            btnLogin.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(386, 310);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnLogin.Size = new Size(340, 50);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Giriş Yap";
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.CustomizableEdges = customizableEdges5;
            btnExit.FillColor = Color.Transparent;
            btnExit.IconColor = Color.FromArgb(148, 163, 184);
            btnExit.Location = new Point(768, 0);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnExit.Size = new Size(50, 35);
            btnExit.TabIndex = 3;
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(picLogo);
            pnlLeft.Controls.Add(Logotxt);
            pnlLeft.Controls.Add(lblSubtitle);
            pnlLeft.CustomizableEdges = customizableEdges3;
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.FillColor = Color.FromArgb(99, 102, 241);
            pnlLeft.FillColor2 = Color.FromArgb(59, 130, 246);
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlLeft.Size = new Size(338, 500);
            pnlLeft.TabIndex = 4;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.CustomizableEdges = customizableEdges1;
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.ImageRotate = 0F;
            picLogo.Location = new Point(90, 60);
            picLogo.Name = "picLogo";
            picLogo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            picLogo.Size = new Size(100, 100);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // Logotxt
            // 
            Logotxt.BackColor = Color.Transparent;
            Logotxt.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            Logotxt.ForeColor = Color.White;
            Logotxt.Location = new Point(30, 180);
            Logotxt.Name = "Logotxt";
            Logotxt.Size = new Size(191, 47);
            Logotxt.TabIndex = 1;
            Logotxt.Text = "A-Sync Task";
            Logotxt.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(226, 232, 240);
            lblSubtitle.Location = new Point(20, 240);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(292, 23);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Gelişmiş Görev ve Proje\r\nYönetim Çözümü";
            lblSubtitle.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Headertxt
            // 
            Headertxt.BackColor = Color.Transparent;
            Headertxt.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            Headertxt.ForeColor = Color.White;
            Headertxt.Location = new Point(386, 60);
            Headertxt.Name = "Headertxt";
            Headertxt.Size = new Size(196, 47);
            Headertxt.TabIndex = 5;
            Headertxt.Text = "Hoş Geldiniz";
            // 
            // lblSubHeader
            // 
            lblSubHeader.BackColor = Color.Transparent;
            lblSubHeader.Font = new Font("Segoe UI", 9.5F);
            lblSubHeader.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubHeader.Location = new Point(386, 110);
            lblSubHeader.Name = "lblSubHeader";
            lblSubHeader.Size = new Size(154, 23);
            lblSubHeader.TabIndex = 6;
            lblSubHeader.Text = "Hesabınıza giriş yapın";
            // 
            // lnkRegister
            // 
            lnkRegister.AutoSize = true;
            lnkRegister.BackColor = Color.Transparent;
            lnkRegister.Cursor = Cursors.Hand;
            lnkRegister.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lnkRegister.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkRegister.LinkColor = Color.FromArgb(99, 102, 241);
            lnkRegister.Location = new Point(386, 385);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(66, 21);
            lnkRegister.TabIndex = 7;
            lnkRegister.TabStop = true;
            lnkRegister.Text = "Kayıt Ol";
            lnkRegister.Click += lnkRegister_Click;
            // 
            // lnkForgot
            // 
            lnkForgot.AutoSize = true;
            lnkForgot.BackColor = Color.Transparent;
            lnkForgot.Cursor = Cursors.Hand;
            lnkForgot.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lnkForgot.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkForgot.LinkColor = Color.FromArgb(244, 63, 94);
            lnkForgot.Location = new Point(596, 385);
            lnkForgot.Name = "lnkForgot";
            lnkForgot.Size = new Size(130, 21);
            lnkForgot.TabIndex = 8;
            lnkForgot.TabStop = true;
            lnkForgot.Text = "Şifremi Unuttum";
            lnkForgot.Click += lnkForgot_Click;
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(15, 17, 26);
            ClientSize = new Size(818, 500);
            Controls.Add(lnkForgot);
            Controls.Add(lnkRegister);
            Controls.Add(lblSubHeader);
            Controls.Add(Headertxt);
            Controls.Add(pnlLeft);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipseLoginForm;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowLoginForm;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel pnlLeft;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel Logotxt;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel Headertxt;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubHeader;
        private System.Windows.Forms.LinkLabel lnkRegister;
        private System.Windows.Forms.LinkLabel lnkForgot;
    }
}