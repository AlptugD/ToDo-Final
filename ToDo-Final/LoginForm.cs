using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final // Kendi projenin namespace adını buraya yazmalısın
{
    public partial class LoginForm : Form
    {
        // Uygulamanın ana tema rengi.
        private Color appThemeColor = Color.FromArgb(94, 148, 255); // Varsayılan mavi
        private Color appBackgroundColor = Color.White; // Seçilebilir arka plan rengi

        public LoginForm()
        {
            InitializeComponent();

            this.Load += LoginForm_Load;
            this.Resize += LoginForm_Resize;

            if (btnLogin != null) btnLogin.Click += btnLogin_Click;
            if (btnExit != null) btnExit.Click += btnExit_Click;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyThemeColors();
            CenterControls(); // Yüklenirken kontrolleri ortala
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CenterControls(); // Boyut değişirse kontrolleri tekrar ortala
        }

        // Bileşenleri formun yatay merkezine sabitleyen metot
        private void CenterControls()
        {
            int centerWidth = this.ClientSize.Width / 2;

            if (txtUsername != null)
                txtUsername.Left = centerWidth - (txtUsername.Width / 2);

            if (txtPassword != null)
                txtPassword.Left = centerWidth - (txtPassword.Width / 2);

            if (btnLogin != null)
                btnLogin.Left = centerWidth - (btnLogin.Width / 2);
        }

        // Dinamik renkleri uygulayan metot
        private void ApplyThemeColors()
        {
            this.BackColor = appBackgroundColor;

            if (btnLogin != null)
            {
                btnLogin.FillColor = appThemeColor;
                btnLogin.ForeColor = Color.White;
            }

            if (txtUsername != null)
            {
                txtUsername.FocusedState.BorderColor = appThemeColor;
                txtUsername.HoverState.BorderColor = appThemeColor;
            }

            if (txtPassword != null)
            {
                txtPassword.FocusedState.BorderColor = appThemeColor;
                txtPassword.HoverState.BorderColor = appThemeColor;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Sunucu adını (Server) kendi SQL Server ismine göre güncellemelisin. 
            // "." veya "localhost" genellikle yerel sunucuyu temsil eder.
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // Veritabanı kapısını aç

                    // Kullanıcı adı ve şifreyi güvenli bir şekilde kontrol eden SQL sorgusu
                    string query = "SELECT Role, ThemeColor FROM Users WHERE Username = @user AND Password = @pass";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        // Parametreleri ekle (SQL Injection saldırılarını önlemek için zorunludur)
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Eğer eşleşen bir kayıt bulunursa
                            {
                                string role = reader["Role"].ToString();
                                string themeColor = reader["ThemeColor"].ToString();

                                // Hoş geldin mesajını göster
                                MessageBox.Show($"{role} olarak giriş yapıldı!", "Hoş Geldiniz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Köprüyü kur: DashboardForm'a bilgileri göndererek oluştur
                                DashboardForm dashboard = new DashboardForm(username, role, themeColor);
                                dashboard.Show(); // Ana formu göster

                                this.Hide(); // Giriş formunu arka planda gizle
                            }
                            else
                            {
                                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Bağlantı başarısız olursa nedenini göster
                    MessageBox.Show("Veritabanına bağlanılamadı. Hata: " + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
