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
    }
}
