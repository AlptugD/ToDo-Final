using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final // Kendi projenin namespace'ini kontrol etmeyi unutma
{
    public partial class DashboardForm : Form
    {
        // Gelen bilgileri form içinde her yerden ulaşabilmek için hafızada tutuyoruz
        private string currentUser;
        private string currentRole;
        private string currentThemeHex;

        // Dışarıdan bilgi alan yeni yapıcı metot (Constructor)
        public DashboardForm(string username, string role, string themeHex)
        {
            InitializeComponent();

            // Bu satır, formun özelliklerini ezip kesinlikle ekranın ortasında açılmasını emreder.
            this.StartPosition = FormStartPosition.CenterScreen;

            // Gelen kargoyu (bilgileri) kendi değişkenlerimize atıyoruz
            currentUser = username;
            currentRole = role;
            currentThemeHex = themeHex;

            // Form yüklenirken çalışacak event'i bağlıyoruz
            this.Load += DashboardForm_Load;
        }

        // Rengi belirtilen oranda açan (beyaza yaklaştıran) yardımcı metot
        private Color LightenColor(Color color, double amount)
        {
            int r = (int)((255 - color.R) * amount) + color.R;
            int g = (int)((255 - color.G) * amount) + color.G;
            int b = (int)((255 - color.B) * amount) + color.B;
            return Color.FromArgb(color.A, Math.Min(255, Math.Max(0, r)), Math.Min(255, Math.Max(0, g)), Math.Min(255, Math.Max(0, b)));
        }

        // Butonlara renk ve modern gölge efekti uygulayan yardımcı metot
        private void ApplyButtonModernStyle(Guna.UI2.WinForms.Guna2Button btn, Color fillColor)
        {
            if (btn == null) return;
            
            btn.FillColor = fillColor;
            
            // Sadece arka planı olan (ana ekran) butonlara gölge veriyoruz
            if (fillColor != Color.Transparent)
            {
                btn.ShadowDecoration.Enabled = true;
                btn.ShadowDecoration.BorderRadius = btn.BorderRadius > 0 ? btn.BorderRadius : 10;
                btn.ShadowDecoration.Color = ControlPaint.Dark(fillColor, 0.2f); // Gölge rengini temanın koyusu yapıyoruz (daha doğal)
            }
        }

        private void ApplyTheme(Color themeColor)
        {
            // Kullanıcının seçtiği temanın rengini doğrudan form arka planı yapıyoruz
            this.BackColor = themeColor;
            if (pnlMain != null) pnlMain.BackColor = themeColor;
            
            // Görev ekleme paneli gibi iç panelleri seçilen renkten daha açık/farklı bir tona boyayarak kontrast oluşturuyoruz
            if (pnlAddTask != null) pnlAddTask.FillColor = LightenColor(themeColor, 0.60); // Arka planla uyumlu açık ton

            // Sol menüyü (sidebar) seçilen rengin daha koyu bir tonu yaparak modern bir kontrast ve derinlik sağlıyoruz
            if (guna2CustomGradientPanel1 != null)
            {
                guna2CustomGradientPanel1.FillColor = ControlPaint.Dark(themeColor, 0.05f);
                guna2CustomGradientPanel1.FillColor2 = ControlPaint.Dark(themeColor, 0.15f);
            }

            // Ana ekrandaki önemli butonların rengini kontrast sağlaması için koyulaştırıyoruz ve gölge ekliyoruz
            Color buttonColor = ControlPaint.Dark(themeColor, 0.1f);
            ApplyButtonModernStyle(btnAddTask, buttonColor);
            ApplyButtonModernStyle(btnSendComment, buttonColor);
            ApplyButtonModernStyle(guna2Button1, buttonColor);

            // Takvim arka planını da temaya uygun yapıyoruz
            if (taskCalendar != null)
            {
                taskCalendar.FillColor = buttonColor;
                taskCalendar.ForeColor = Color.White;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // 1. Profil Bilgilerini Ekrana Yazdır
            if (lblUserName != null) lblUserName.Text = currentUser;
            if (lblUserRole != null) lblUserRole.Text = currentRole;

            // 2. Kullanıcının Seçtiği Tema Rengini Uygula
            try
            {
                Color themeColor = ColorTranslator.FromHtml(currentThemeHex);
                ApplyTheme(themeColor);
            }
            catch
            {
                ApplyTheme(Color.FromArgb(138, 35, 135)); // Renk kodu hatalıysa varsayılan mor tema
            }

            // 3. Rol Kontrolü ve Yetkilendirme (En Önemli Kısım)
            if (currentRole == "Çalışan")
            {
                // Çalışanlar görev ekleyemez, ekleme panelini tamamen gizle
                if (pnlAddTask != null) pnlAddTask.Visible = false;

                // Takvimi görebilirler ama üzerinden işlem yapamazlar (Salt Okunur)
                if (taskCalendar != null) taskCalendar.Enabled = false;
            }
            else if (currentRole == "Yönetici")
            {
                // Yöneticiyse tüm paneller aktif kalır
                if (pnlAddTask != null) pnlAddTask.Visible = true;
                if (taskCalendar != null) taskCalendar.Enabled = true;
            }
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            using (ThemePickerForm themePicker = new ThemePickerForm())
            {
                if (themePicker.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = themePicker.SelectedColor;

                    // Seçilen rengi modern arayüz fonksiyonu ile uygula
                    ApplyTheme(selectedColor);

                    // Rengi HEX formatına çevirip veritabanına kaydet
                    string hexColor = "#" + selectedColor.R.ToString("X2") + selectedColor.G.ToString("X2") + selectedColor.B.ToString("X2");
                    UpdateThemeInDatabase(hexColor);
                }
            }
        }

        // Veritabanına bağlanıp kullanıcının renk tercihini güncelleyen özel metot
        private void UpdateThemeInDatabase(string newThemeHex)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // UPDATE sorgusu: Sadece o an giriş yapmış olan kişinin (currentUser) rengini değiştirir
                    string query = "UPDATE Users SET ThemeColor = @theme WHERE Username = @user";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        // Parametreleri ekleyerek güvenliği sağlıyoruz
                        cmd.Parameters.AddWithValue("@theme", newThemeHex);
                        cmd.Parameters.AddWithValue("@user", currentUser); // Form açılırken hafızaya aldığımız kullanıcı adı

                        int result = cmd.ExecuteNonQuery(); // Sorguyu çalıştır

                        if (result > 0)
                        {
                            MessageBox.Show("Çalışma alanı renginiz başarıyla kaydedildi!", "Tema Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Hafızadaki rengi de yenisiyle değiştiriyoruz ki program çalışırken uyumsuzluk olmasın
                            currentThemeHex = newThemeHex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Renk veritabanına kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hesabınızdan çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Eğer kullanıcı 'Evet' (Yes) butonuna basarsa
            if (result == DialogResult.Yes)
            {
                // Programı en baştan, temiz bir şekilde yeniden başlat (LoginForm açılır)
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
