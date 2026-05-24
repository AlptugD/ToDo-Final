namespace ToDo_Final
{
    partial class CommentItem
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            lblUsername = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblCommentText = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pbSticker = new Guna.UI2.WinForms.Guna2PictureBox();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbSticker).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderRadius = 12;
            guna2Panel1.BorderColor = Color.FromArgb(37, 47, 72);
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.FillColor = Color.FromArgb(21, 27, 44);
            guna2Panel1.Controls.Add(pbSticker);
            guna2Panel1.Controls.Add(lblCommentText);
            guna2Panel1.Controls.Add(lblDate);
            guna2Panel1.Controls.Add(lblUsername);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(407, 150);
            guna2Panel1.TabIndex = 0;
            // 
            // lblUsername
            // 
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(99, 102, 241);
            lblUsername.Location = new Point(14, 10);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(150, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Isim";
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 8F);
            lblDate.ForeColor = Color.FromArgb(148, 163, 184);
            lblDate.Location = new Point(270, 10);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(120, 18);
            lblDate.TabIndex = 1;
            lblDate.Text = "Saat - Tarih";
            lblDate.TextAlignment = ContentAlignment.TopRight;
            // 
            // lblCommentText
            // 
            lblCommentText.BackColor = Color.Transparent;
            lblCommentText.Font = new Font("Segoe UI", 9.5F);
            lblCommentText.ForeColor = Color.FromArgb(243, 244, 246);
            lblCommentText.Location = new Point(14, 34);
            lblCommentText.Name = "lblCommentText";
            lblCommentText.Size = new Size(380, 22);
            lblCommentText.TabIndex = 2;
            lblCommentText.Text = "Yorum içeriği...";
            // 
            // pbSticker
            // 
            pbSticker.BackColor = Color.Transparent;
            pbSticker.Location = new Point(14, 65);
            pbSticker.Name = "pbSticker";
            pbSticker.Size = new Size(70, 70);
            pbSticker.SizeMode = PictureBoxSizeMode.Zoom;
            pbSticker.Visible = false;
            pbSticker.TabIndex = 3;
            pbSticker.TabStop = false;
            // 
            // CommentItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(guna2Panel1);
            Name = "CommentItem";
            Size = new Size(407, 150);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbSticker).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsername;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCommentText;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDate;
        private Guna.UI2.WinForms.Guna2PictureBox pbSticker;
    }
}
