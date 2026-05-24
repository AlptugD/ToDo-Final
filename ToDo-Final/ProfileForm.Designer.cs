namespace ToDo_Final
{
    partial class ProfileForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            elipseProfile = new Guna.UI2.WinForms.Guna2Elipse(components);
            shadowProfile = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblUsername = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblRole = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pbAvatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            btnSelectImage = new Guna.UI2.WinForms.Guna2Button();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            SuspendLayout();
            // 
            // elipseProfile
            // 
            elipseProfile.BorderRadius = 20;
            elipseProfile.TargetControl = this;
            // 
            // shadowProfile
            // 
            shadowProfile.TargetForm = this;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(35, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 43);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Profilim";
            // 
            // pbAvatar
            // 
            pbAvatar.BackColor = Color.Transparent;
            pbAvatar.ImageRotate = 0F;
            pbAvatar.Location = new Point(140, 100);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pbAvatar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbAvatar.Size = new Size(120, 120);
            pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            pbAvatar.TabIndex = 1;
            pbAvatar.TabStop = false;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BorderRadius = 10;
            btnSelectImage.CustomizableEdges = customizableEdges2;
            btnSelectImage.FillColor = Color.FromArgb(31, 41, 55);
            btnSelectImage.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(125, 235);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnSelectImage.Size = new Size(150, 36);
            btnSelectImage.TabIndex = 2;
            btnSelectImage.Text = "Görsel Seç";
            btnSelectImage.Cursor = Cursors.Hand;
            btnSelectImage.BorderColor = Color.FromArgb(55, 65, 81);
            btnSelectImage.BorderThickness = 1;
            // 
            // lblUsername
            // 
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(40, 290);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(118, 27);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Kullanıcı Adı: ";
            // 
            // lblRole
            // 
            lblRole.BackColor = Color.Transparent;
            lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblRole.ForeColor = Color.FromArgb(148, 163, 184);
            lblRole.Location = new Point(40, 325);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(39, 25);
            lblRole.TabIndex = 4;
            lblRole.Text = "Rol: ";
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 12;
            btnSave.CustomizableEdges = customizableEdges4;
            btnSave.FillColor = Color.FromArgb(99, 102, 241);
            btnSave.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(40, 380);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnSave.Size = new Size(320, 50);
            btnSave.TabIndex = 5;
            btnSave.Text = "Değişiklikleri Kaydet";
            btnSave.Cursor = Cursors.Hand;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.CustomizableEdges = customizableEdges6;
            btnExit.FillColor = Color.Transparent;
            btnExit.IconColor = Color.FromArgb(148, 163, 184);
            btnExit.Location = new Point(355, 0);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnExit.Size = new Size(45, 30);
            btnExit.TabIndex = 6;
            // 
            // ProfileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 15, 25);
            ClientSize = new Size(400, 470);
            Controls.Add(btnExit);
            Controls.Add(btnSave);
            Controls.Add(lblRole);
            Controls.Add(lblUsername);
            Controls.Add(btnSelectImage);
            Controls.Add(pbAvatar);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProfileForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ProfileForm";
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipseProfile;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowProfile;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbAvatar;
        private Guna.UI2.WinForms.Guna2Button btnSelectImage;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsername;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRole;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
    }
}
