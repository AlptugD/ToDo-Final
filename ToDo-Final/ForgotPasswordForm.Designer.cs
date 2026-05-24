namespace ToDo_Final
{
    partial class ForgotPasswordForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            elipseForgot = new Guna.UI2.WinForms.Guna2Elipse(components);
            shadowForgot = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btnReset = new Guna.UI2.WinForms.Guna2Button();
            btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // elipseForgot
            // 
            elipseForgot.BorderRadius = 25;
            elipseForgot.TargetControl = this;
            // 
            // shadowForgot
            // 
            shadowForgot.TargetForm = this;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 12;
            txtUsername.BorderColor = Color.FromArgb(45, 50, 70);
            txtUsername.FillColor = Color.FromArgb(23, 25, 35);
            txtUsername.ForeColor = Color.White;
            txtUsername.CustomizableEdges = customizableEdges5;
            txtUsername.DefaultText = "";
            txtUsername.FocusedState.BorderColor = Color.FromArgb(244, 63, 94);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.HoverState.BorderColor = Color.FromArgb(244, 63, 94);
            txtUsername.Location = new Point(55, 120);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtUsername.PlaceholderText = "Kullanıcı Adınız";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUsername.Size = new Size(340, 50);
            txtUsername.TabIndex = 0;
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderRadius = 12;
            txtNewPassword.BorderColor = Color.FromArgb(45, 50, 70);
            txtNewPassword.FillColor = Color.FromArgb(23, 25, 35);
            txtNewPassword.ForeColor = Color.White;
            txtNewPassword.CustomizableEdges = customizableEdges3;
            txtNewPassword.DefaultText = "";
            txtNewPassword.FocusedState.BorderColor = Color.FromArgb(244, 63, 94);
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.HoverState.BorderColor = Color.FromArgb(244, 63, 94);
            txtNewPassword.Location = new Point(55, 190);
            txtNewPassword.Margin = new Padding(3, 4, 3, 4);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtNewPassword.PlaceholderText = "Yeni Şifreniz";
            txtNewPassword.SelectedText = "";
            txtNewPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtNewPassword.Size = new Size(340, 50);
            txtNewPassword.TabIndex = 1;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // btnReset
            // 
            btnReset.BorderRadius = 12;
            btnReset.CustomizableEdges = customizableEdges1;
            btnReset.FillColor = Color.FromArgb(244, 63, 94);
            btnReset.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(55, 270);
            btnReset.Name = "btnReset";
            btnReset.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnReset.Size = new Size(340, 50);
            btnReset.TabIndex = 2;
            btnReset.Text = "Şifreyi Güncelle";
            btnReset.Cursor = Cursors.Hand;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.CustomizableEdges = customizableEdges1;
            btnExit.FillColor = Color.Transparent;
            btnExit.IconColor = Color.FromArgb(148, 163, 184);
            btnExit.Location = new Point(405, 0);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnExit.Size = new Size(45, 30);
            btnExit.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(55, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(259, 47);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Şifremi Unuttum";
            // 
            // lblSubTitle
            // 
            lblSubTitle.BackColor = Color.Transparent;
            lblSubTitle.Font = new Font("Segoe UI", 9.5F);
            lblSubTitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubTitle.Location = new Point(55, 80);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(244, 23);
            lblSubTitle.TabIndex = 5;
            lblSubTitle.Text = "Hesabınızın şifresini sıfırlayın";
            // 
            // ForgotPasswordForm
            // 
            BackColor = Color.FromArgb(15, 17, 26);
            ClientSize = new Size(450, 360);
            Controls.Add(lblSubTitle);
            Controls.Add(lblTitle);
            Controls.Add(btnExit);
            Controls.Add(btnReset);
            Controls.Add(txtNewPassword);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ForgotPasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipseForgot;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForgot;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPassword;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTitle;
    }
}
