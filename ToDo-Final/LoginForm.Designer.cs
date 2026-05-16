namespace ToDo_Final
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
            components = new System.ComponentModel.Container();
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
            picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            Logotxt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Headertxt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // elipseLoginForm
            // 
            elipseLoginForm.BorderRadius = 20;
            elipseLoginForm.TargetControl = this;
            // 
            // shadowLoginForm
            // 
            shadowLoginForm.TargetForm = this;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 15;
            txtUsername.CustomizableEdges = customizableEdges9;
            txtUsername.DefaultText = "";
            txtUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsername.Location = new Point(211, 185);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Kullanıcı Adı";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtUsername.Size = new Size(286, 60);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 15;
            txtPassword.CustomizableEdges = customizableEdges7;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPassword.Location = new Point(211, 274);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Şifre";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPassword.Size = new Size(286, 60);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BorderRadius = 15;
            btnLogin.CustomizableEdges = customizableEdges5;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.Font = new Font("Segoe UI", 9F);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(242, 363);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnLogin.Size = new Size(225, 56);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Giriş Yap";
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.CustomizableEdges = customizableEdges3;
            btnExit.FillColor = Color.FromArgb(139, 152, 166);
            btnExit.IconColor = Color.White;
            btnExit.Location = new Point(653, 0);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnExit.Size = new Size(56, 36);
            btnExit.TabIndex = 3;
            // 
            // picLogo
            // 
            picLogo.CustomizableEdges = customizableEdges1;
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.ImageRotate = 0F;
            picLogo.Location = new Point(12, 12);
            picLogo.Name = "picLogo";
            picLogo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            picLogo.Size = new Size(73, 72);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 4;
            picLogo.TabStop = false;
            // 
            // Logotxt
            // 
            Logotxt.BackColor = Color.Transparent;
            Logotxt.Font = new Font("Segoe UI", 10F);
            Logotxt.ForeColor = SystemColors.Control;
            Logotxt.Location = new Point(1, 90);
            Logotxt.Name = "Logotxt";
            Logotxt.Size = new Size(94, 25);
            Logotxt.TabIndex = 5;
            Logotxt.Text = "A-Sync Task";
            // 
            // Headertxt
            // 
            Headertxt.BackColor = Color.Transparent;
            Headertxt.Font = new Font("Segoe UI", 16F);
            Headertxt.ForeColor = SystemColors.Control;
            Headertxt.Location = new Point(278, 90);
            Headertxt.Name = "Headertxt";
            Headertxt.Size = new Size(153, 39);
            Headertxt.TabIndex = 6;
            Headertxt.Text = "Hoş Geldiniz";
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(36, 32, 55);
            ClientSize = new Size(709, 496);
            Controls.Add(Headertxt);
            Controls.Add(Logotxt);
            Controls.Add(picLogo);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private Guna.UI2.WinForms.Guna2Elipse elipseLoginForm;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowLoginForm;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel Headertxt;
        private Guna.UI2.WinForms.Guna2HtmlLabel Logotxt;
    }
}