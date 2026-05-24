namespace ToDo_Final
{
    partial class RegisterForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            elipseRegister = new Guna.UI2.WinForms.Guna2Elipse(components);
            shadowRegister = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            cmbRole = new Guna.UI2.WinForms.Guna2ComboBox();
            btnRegister = new Guna.UI2.WinForms.Guna2Button();
            btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // elipseRegister
            // 
            elipseRegister.BorderRadius = 25;
            elipseRegister.TargetControl = this;
            // 
            // shadowRegister
            // 
            shadowRegister.TargetForm = this;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 12;
            txtUsername.BorderColor = Color.FromArgb(45, 50, 70);
            txtUsername.FillColor = Color.FromArgb(23, 25, 35);
            txtUsername.ForeColor = Color.White;
            txtUsername.CustomizableEdges = customizableEdges9;
            txtUsername.DefaultText = "";
            txtUsername.FocusedState.BorderColor = Color.FromArgb(99, 102, 241);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.HoverState.BorderColor = Color.FromArgb(99, 102, 241);
            txtUsername.Location = new Point(55, 120);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtUsername.PlaceholderText = "Yeni Kullanıcı Adı";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtUsername.Size = new Size(340, 50);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 12;
            txtPassword.BorderColor = Color.FromArgb(45, 50, 70);
            txtPassword.FillColor = Color.FromArgb(23, 25, 35);
            txtPassword.ForeColor = Color.White;
            txtPassword.CustomizableEdges = customizableEdges7;
            txtPassword.DefaultText = "";
            txtPassword.FocusedState.BorderColor = Color.FromArgb(99, 102, 241);
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.HoverState.BorderColor = Color.FromArgb(99, 102, 241);
            txtPassword.Location = new Point(55, 190);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderForeColor = Color.FromArgb(100, 105, 125);
            txtPassword.PlaceholderText = "Yeni Şifre";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPassword.Size = new Size(340, 50);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // cmbRole
            // 
            cmbRole.BackColor = Color.Transparent;
            cmbRole.BorderRadius = 12;
            cmbRole.BorderColor = Color.FromArgb(45, 50, 70);
            cmbRole.FillColor = Color.FromArgb(23, 25, 35);
            cmbRole.ForeColor = Color.White;
            cmbRole.CustomizableEdges = customizableEdges5;
            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FocusedColor = Color.FromArgb(99, 102, 241);
            cmbRole.FocusedState.BorderColor = Color.FromArgb(99, 102, 241);
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.ItemHeight = 36;
            cmbRole.Items.AddRange(new object[] { "Çalışan", "Yönetici" });
            cmbRole.Location = new Point(55, 260);
            cmbRole.Name = "cmbRole";
            cmbRole.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbRole.Size = new Size(340, 42);
            cmbRole.StartIndex = 0;
            cmbRole.TabIndex = 2;
            // 
            // btnRegister
            // 
            btnRegister.BorderRadius = 12;
            btnRegister.CustomizableEdges = customizableEdges3;
            btnRegister.FillColor = Color.FromArgb(99, 102, 241);
            btnRegister.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(55, 335);
            btnRegister.Name = "btnRegister";
            btnRegister.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRegister.Size = new Size(340, 50);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Kayıt Ol";
            btnRegister.Cursor = Cursors.Hand;
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
            btnExit.TabIndex = 4;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(55, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 47);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Kayıt Ol";
            // 
            // lblSubTitle
            // 
            lblSubTitle.BackColor = Color.Transparent;
            lblSubTitle.Font = new Font("Segoe UI", 9.5F);
            lblSubTitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubTitle.Location = new Point(55, 80);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(201, 23);
            lblSubTitle.TabIndex = 6;
            lblSubTitle.Text = "Yeni bir hesap oluşturun";
            // 
            // RegisterForm
            // 
            BackColor = Color.FromArgb(15, 17, 26);
            ClientSize = new Size(450, 430);
            Controls.Add(lblSubTitle);
            Controls.Add(lblTitle);
            Controls.Add(btnExit);
            Controls.Add(btnRegister);
            Controls.Add(cmbRole);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipseRegister;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowRegister;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRole;
        private Guna.UI2.WinForms.Guna2Button btnRegister;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTitle;
    }
}
