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

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // 1. Profil Bilgilerini Ekrana Yazdır
            if (lblUserName != null) lblUserName.Text = currentUser;
            if (lblUserRole != null) lblUserRole.Text = currentRole;

            // 2. Kullanıcının Seçtiği Tema Rengini Uygula
            try
            {
                Color themeColor = ColorTranslator.FromHtml(currentThemeHex);
                this.BackColor = themeColor; // Veya pnlSidebar.FillColor = themeColor; şeklinde özelleştirebilirsin
            }
            catch
            {
                this.BackColor = Color.White; // Renk kodu hatalıysa varsayılan renk
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

                    // Seçilen rengi ana forma uygula
                    this.BackColor = selectedColor;
                    // Eğer sol menüyü boyamak istersen: pnlSidebar.FillColor = selectedColor;

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

        }
    }
}
