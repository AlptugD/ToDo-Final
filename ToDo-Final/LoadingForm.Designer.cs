namespace ToDo_Final
{
    partial class LoadingForm
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
            elipseLoading = new Guna.UI2.WinForms.Guna2Elipse(components);
            shadowLoading = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            progressIndicator = new Guna.UI2.WinForms.Guna2WinProgressIndicator();
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            loadingTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // elipseLoading
            // 
            elipseLoading.BorderRadius = 25;
            elipseLoading.TargetControl = this;
            // 
            // shadowLoading
            // 
            shadowLoading.TargetForm = this;
            // 
            // progressIndicator
            // 
            progressIndicator.AutoStart = true;
            progressIndicator.BackColor = Color.Transparent;
            progressIndicator.Location = new Point(175, 80);
            progressIndicator.Name = "progressIndicator";
            progressIndicator.NumberOfCircles = 12;
            progressIndicator.ProgressColor = Color.FromArgb(99, 102, 241);
            progressIndicator.Size = new Size(100, 100);
            progressIndicator.TabIndex = 0;
            // 
            // progressBar
            // 
            progressBar.BorderRadius = 6;
            progressBar.CustomizableEdges = customizableEdges1;
            progressBar.FillColor = Color.FromArgb(31, 41, 55);
            progressBar.Location = new Point(50, 240);
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(99, 102, 241);
            progressBar.ProgressColor2 = Color.FromArgb(0, 229, 255);
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar.Size = new Size(350, 12);
            progressBar.TabIndex = 1;
            progressBar.Text = "guna2ProgressBar1";
            progressBar.Value = 0;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            lblStatus.ForeColor = Color.FromArgb(148, 163, 184);
            lblStatus.Location = new Point(50, 200);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(170, 23);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Sistem hazırlanıyor...";
            lblStatus.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(140, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 39);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "A-Sync Task";
            // 
            // loadingTimer
            // 
            loadingTimer.Interval = 100;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 15, 25);
            ClientSize = new Size(450, 300);
            Controls.Add(lblTitle);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(progressIndicator);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoadingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadingForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipseLoading;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowLoading;
        private Guna.UI2.WinForms.Guna2WinProgressIndicator progressIndicator;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private System.Windows.Forms.Timer loadingTimer;
    }
}
