namespace ToDo_Final
{
    partial class TaskItem
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            chkIsCompleted = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            btnDelete = new Guna.UI2.WinForms.Guna2ImageButton();
            lblDate = new Label();
            lblDesc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblCountdown = new Label();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BorderRadius = 12;
            guna2Panel1.Controls.Add(chkIsCompleted);
            guna2Panel1.Controls.Add(btnDelete);
            guna2Panel1.Controls.Add(lblDate);
            guna2Panel1.Controls.Add(lblDesc);
            guna2Panel1.Controls.Add(lblTitle);
            guna2Panel1.Controls.Add(lblCountdown);
            guna2Panel1.CustomizableEdges = customizableEdges4;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges5;
            guna2Panel1.Size = new Size(623, 157);
            guna2Panel1.TabIndex = 0;
            // 
            // chkIsCompleted
            // 
            chkIsCompleted.BackColor = Color.Transparent;
            chkIsCompleted.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            chkIsCompleted.CheckedState.BorderRadius = 4;
            chkIsCompleted.CheckedState.BorderThickness = 0;
            chkIsCompleted.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            chkIsCompleted.CustomizableEdges = customizableEdges1;
            chkIsCompleted.ForeColor = Color.Transparent;
            chkIsCompleted.Location = new Point(540, 45);
            chkIsCompleted.Name = "chkIsCompleted";
            chkIsCompleted.ShadowDecoration.CustomizableEdges = customizableEdges2;
            chkIsCompleted.Size = new Size(40, 40);
            chkIsCompleted.TabIndex = 5;
            chkIsCompleted.Text = "guna2CustomCheckBox1";
            chkIsCompleted.UncheckedState.BorderColor = Color.FromArgb(100, 100, 120);
            chkIsCompleted.UncheckedState.BorderRadius = 4;
            chkIsCompleted.UncheckedState.BorderThickness = 1;
            chkIsCompleted.UncheckedState.FillColor = Color.FromArgb(40, 40, 60);
            chkIsCompleted.Click += chkIsCompleted_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Transparent;
            btnDelete.CheckedState.ImageSize = new Size(64, 64);
            btnDelete.HoverState.ImageSize = new Size(64, 64);
            btnDelete.Image = Properties.Resources.trash_can;
            btnDelete.ImageOffset = new Point(0, 0);
            btnDelete.ImageRotate = 0F;
            btnDelete.Location = new Point(455, 27);
            btnDelete.Name = "btnDelete";
            btnDelete.PressedState.ImageSize = new Size(64, 64);
            btnDelete.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnDelete.Size = new Size(61, 77);
            btnDelete.TabIndex = 4;
            btnDelete.Click += btnDelete_Click_1;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Location = new Point(9, 118);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(40, 20);
            lblDate.TabIndex = 3;
            lblDate.Text = "Tarih";
            // 
            // lblDesc
            // 
            lblDesc.BackColor = Color.Transparent;
            lblDesc.Font = new Font("Segoe UI", 10F);
            lblDesc.Location = new Point(9, 57);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(132, 25);
            lblDesc.TabIndex = 2;
            lblDesc.Text = "Görev Açıklaması";
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblTitle.Location = new Point(9, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(128, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Görev Başlığı";
            // 
            // lblCountdown
            // 
            lblCountdown.AutoSize = true;
            lblCountdown.BackColor = Color.Transparent;
            lblCountdown.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCountdown.ForeColor = Color.FromArgb(255, 128, 128);
            lblCountdown.Location = new Point(320, 118);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(143, 21);
            lblCountdown.TabIndex = 6;
            lblCountdown.Text = "Kalan Süre: --:--:--";
            // 
            // TaskItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(guna2Panel1);
            Name = "TaskItem";
            Size = new Size(623, 157);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2ImageButton btnDelete;
        private Label lblDate;
        private Guna.UI2.WinForms.Guna2CustomCheckBox chkIsCompleted;
        private Label lblCountdown;
    }
}
