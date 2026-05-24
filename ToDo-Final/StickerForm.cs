using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace ToDo_Final
{
    public class StickerForm : Form
    {
        private FlowLayoutPanel flpStickers;
        private Guna2Elipse elipse;
        private Label lblTitle;
        private Guna2Button btnClose;
        private System.ComponentModel.IContainer components;
        private Guna2ShadowForm shadow;

        public event Action<string> StickerSelected;

        public StickerForm()
        {
            InitializeComponent();
            LoadStickers();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            elipse = new Guna2Elipse(components);
            shadow = new Guna2ShadowForm(components);
            lblTitle = new Label();
            btnClose = new Guna2Button();
            flpStickers = new FlowLayoutPanel();
            SuspendLayout();
            // elipse

            elipse.BorderRadius = 15;
            elipse.TargetControl = this;
            // shadow
            shadow.TargetForm = this;
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(115, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Sticker Seç";
            // btnClose 
            btnClose.BorderRadius = 10;
            btnClose.Cursor = Cursors.Hand;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.LightGray;
            btnClose.HoverState.FillColor = Color.FromArgb(233, 64, 87);
            btnClose.HoverState.ForeColor = Color.White;
            btnClose.Location = new Point(305, 12);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.Click += btnClose_Click;
            // flpStickers
            flpStickers.AutoScroll = true;
            flpStickers.BackColor = Color.Transparent;
            flpStickers.Location = new Point(15, 55);
            flpStickers.Name = "flpStickers";
            flpStickers.Padding = new Padding(5);
            flpStickers.Size = new Size(320, 330);
            flpStickers.TabIndex = 2;
            // StickerForm
            BackColor = Color.FromArgb(28, 28, 44);
            ClientSize = new Size(350, 400);
            Controls.Add(lblTitle);
            Controls.Add(btnClose);
            Controls.Add(flpStickers);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StickerForm";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadStickers()
        {
            string stickersPath = Path.Combine(Application.StartupPath, "Stickers");

            if (!Directory.Exists(stickersPath))
            {
                Directory.CreateDirectory(stickersPath);
            }

            string[] files = Directory.GetFiles(stickersPath, "*.*");
            bool hasImages = false;

            foreach (string file in files)
            {
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif")
                {
                    hasImages = true;
                    AddStickerToPanel(file);
                }
            }

            // Eğer klasörde hiç resim yoksa, geçici placeholder (renkli kutu) ekleyelim.
            // Kullanıcı görselleri daha sonra kendisi "Stickers" klasörüne ekleyecektir.
            if (!hasImages)
            {
                for (int i = 1; i <= 6; i++)
                {
                    AddPlaceholderSticker(i.ToString());
                }

                Label lblHint = new Label();
                lblHint.Text = "Lütfen görsellerinizi uygulamanın 'Stickers' klasörüne ekleyin.";
                lblHint.ForeColor = Color.Gray;
                lblHint.Font = new Font("Segoe UI", 9);
                lblHint.AutoSize = true;
                lblHint.MaximumSize = new Size(300, 0);
                flpStickers.Controls.Add(lblHint);
            }
        }

        private void AddStickerToPanel(string filePath)
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(64, 64);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Margin = new Padding(5);
            pb.Cursor = Cursors.Hand;
            pb.Image = Image.FromFile(filePath);
            pb.Tag = Path.GetFileName(filePath);

            pb.Click += (s, e) =>
            {
                StickerSelected?.Invoke(pb.Tag.ToString());
                this.Close();
            };

            flpStickers.Controls.Add(pb);
        }

        private void AddPlaceholderSticker(string id)
        {
            Guna2Button btnPlaceholder = new Guna2Button();
            btnPlaceholder.Size = new Size(64, 64);
            btnPlaceholder.Margin = new Padding(5);
            btnPlaceholder.BorderRadius = 10;
            btnPlaceholder.FillColor = Color.FromArgb(94, 148, 255);
            btnPlaceholder.Text = "Stk " + id;
            btnPlaceholder.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnPlaceholder.Cursor = Cursors.Hand;

            // Tıklandığında yine aynı mantıkla çalışsın
            btnPlaceholder.Click += (s, e) =>
            {
                StickerSelected?.Invoke("placeholder_" + id + ".png");
                this.Close();
            };

            flpStickers.Controls.Add(btnPlaceholder);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this .Close();
        }
    }
}
