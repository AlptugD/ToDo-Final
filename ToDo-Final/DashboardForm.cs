using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final
{
    public partial class DashboardForm : Form
    {
        // Gelen kullanıcı bilgilerini hafızada tutan değişkenler
        private string currentUser;
        private string currentRole;
        private string currentThemeHex;

        // Seçilen görevin ID'sini takip eden değişken
        private int selectedTaskId = 0;

        // Giriş ekranından bilgileri alan yapıcı metot (Constructor)
        public DashboardForm(string username, string role, string themeHex)
        {
            InitializeComponent();

            // Formun her zaman ekranın tam ortasında açılmasını sağlar
            this.StartPosition = FormStartPosition.CenterScreen;

            // Giriş yapan kullanıcının bilgilerini içeri aktarıyoruz
            currentUser = username;
            currentRole = role;
            currentThemeHex = themeHex;

            // Form yüklenirken çalışacak yükleme olayını bağlıyoruz
            this.Load += DashboardForm_Load;

            // Tasarım ekranının çökmemesi için buton tıklama olaylarını bağlı tutuyoruz
            if (btnSendComment != null)
                btnSendComment.Click += btnSendComment_Click;

            if (btnAddSticker != null)
                btnAddSticker.Click += btnAddSticker_Click;
        }

        // Seçilen tema rengini panellere uydurmak için rengi açan yardımcı metot
        private Color LightenColor(Color color, double amount)
        {
            int r = (int)((255 - color.R) * amount) + color.R;
            int g = (int)((255 - color.G) * amount) + color.G;
            int b = (int)((255 - color.B) * amount) + color.B;
            return Color.FromArgb(color.A, Math.Min(255, Math.Max(0, r)), Math.Min(255, Math.Max(0, g)), Math.Min(255, Math.Max(0, b)));
        }

        // Butonlara şık ve yumuşak gölge efekti veren yardımcı metot
        private void ApplyButtonModernStyle(Guna.UI2.WinForms.Guna2Button btn, Color fillColor)
        {
            if (btn == null) return;

            btn.FillColor = fillColor;

            if (fillColor != Color.Transparent)
            {
                btn.ShadowDecoration.Enabled = true;
                btn.ShadowDecoration.BorderRadius = btn.BorderRadius > 0 ? btn.BorderRadius : 10;
                btn.ShadowDecoration.Color = ControlPaint.Dark(fillColor, 0.2f);
            }
        }

        // Seçilen temayı tüm arayüze dinamik olarak yayan metot
        private void ApplyTheme(Color themeColor)
        {
            this.BackColor = themeColor;
            if (pnlMain != null) pnlMain.BackColor = themeColor;

            if (pnlAddTask != null)
                pnlAddTask.FillColor = LightenColor(themeColor, 0.60);

            if (guna2CustomGradientPanel1 != null)
            {
                guna2CustomGradientPanel1.FillColor = ControlPaint.Dark(themeColor, 0.05f);
                guna2CustomGradientPanel1.FillColor2 = ControlPaint.Dark(themeColor, 0.15f);
            }

            Color buttonColor = ControlPaint.Dark(themeColor, 0.1f);
            ApplyButtonModernStyle(btnAddTask, buttonColor);
            ApplyButtonModernStyle(btnSendComment, buttonColor);
            ApplyButtonModernStyle(guna2Button1, buttonColor);
            ApplyButtonModernStyle(guna2Button4, buttonColor);
            ApplyButtonModernStyle(btnChangeTheme, buttonColor);

            if (taskCalendar != null)
            {
                taskCalendar.FillColor = buttonColor;
                taskCalendar.ForeColor = Color.White;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // 1. Profil bilgilerini sol üst panele yazdırır
            if (lblUserName != null) lblUserName.Text = currentUser;
            if (lblUserRole != null) lblUserRole.Text = currentRole;

            // 2. Kullanıcının veritabanındaki renk tercihini arayüze uygular
            try
            {
                Color themeColor = ColorTranslator.FromHtml(currentThemeHex);
                ApplyTheme(themeColor);
            }
            catch
            {
                ApplyTheme(Color.FromArgb(138, 35, 135)); // Hata durumunda varsayılan tema
            }

            // 3. Yetkilendirme Kontrolü
            if (currentRole == "Çalışan")
            {
                if (pnlAddTask != null) pnlAddTask.Visible = false;
                if (taskCalendar != null) taskCalendar.Enabled = false;
            }
            else if (currentRole == "Yönetici" || currentRole == "Admin")
            {
                if (pnlAddTask != null) pnlAddTask.Visible = true;
                if (taskCalendar != null) taskCalendar.Enabled = true;
                if (guna2Button4 != null) guna2Button4.Visible = false;
            }

            // Not: Görev listeleme ve yorum listeleme kodları, yeni formlara taşınacağı için buradan kaldırıldı.
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            using (ThemePickerForm themePicker = new ThemePickerForm())
            {
                if (themePicker.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = themePicker.SelectedColor;
                    ApplyTheme(selectedColor);

                    // Rengi HEX formatına çevirip veritabanına kaydet
                    string hexColor = "#" + selectedColor.R.ToString("X2") + selectedColor.G.ToString("X2") + selectedColor.B.ToString("X2");
                    UpdateThemeInDatabase(hexColor);
                }
            }
        }

        private void UpdateThemeInDatabase(string newThemeHex)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET ThemeColor = @theme WHERE Username = @user";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@theme", newThemeHex);
                        cmd.Parameters.AddWithValue("@user", currentUser);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Çalışma alanı renginiz başarıyla kaydedildi!", "Tema Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            currentThemeHex = newThemeHex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Renk kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- İÇİ BOŞALTILMIŞ METOTLAR ---
        // Tasarım ekranının (Designer) çökmemesi için metotlar silinmedi, sadece içleri boşaltıldı.

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            // Görev ekleme işlemleri artık yeni formda yapılacak.
        }

        private void btnSendComment_Click(object sender, EventArgs e)
        {
            // Yorum gönderme işlemleri artık yeni formda yapılacak.
        }

        private void btnAddSticker_Click(object sender, EventArgs e)
        {
            // Sticker ekleme işlemleri artık yeni formda yapılacak.
        }

        // --- SİSTEM KONTROLLERİ ---

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hesabınızdan çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void DashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}