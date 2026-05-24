using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ToDo_Final
{
    /// <summary>
    /// Kullanıcıların sisteme güvenli bir şekilde giriş yapmasını sağlayan modern giriş ekranı formu.
    /// Guna.UI2 bileşenleri ile donatılmış şık ve duyarlı (responsive) bir arayüz sunar.
    /// </summary>
    public partial class LoginForm : Form
    {
        // Uygulamanın varsayılan tema rengi (Mavi tonu)
        private Color appThemeColor = Color.FromArgb(94, 148, 255);
        // Seçilebilir arka plan rengi
        private Color appBackgroundColor = Color.White;

        /// <summary>
        /// Sınıfın kurucu metodu. Arayüz bileşenlerini yükler ve olay dinleyicilerini (Event Handlers) bağlar.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // Form yüklenme (Load) ve yeniden boyutlanma (Resize) olaylarını bağla
            this.Load += LoginForm_Load;
            this.Resize += LoginForm_Resize;

            // Giriş ve çıkış butonlarının olaylarını bağla
            if (btnLogin != null) btnLogin.Click += btnLogin_Click;
            if (btnExit != null) btnExit.Click += btnExit_Click;
        }

        /// <summary>
        /// Form ilk yüklendiğinde tetiklenen olay. Kontrolleri ekrana göre ortalar.
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            CenterControls(); // Arayüz kontrollerini formun sağ panele göre ortalar
        }

        /// <summary>
        /// Form yeniden boyutlandırıldığında tetiklenen olay. Kontrolleri dinamik olarak tekrar ortalar.
        /// </summary>
        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CenterControls(); // Yeniden boyutlandırıldığında hizalamayı korur
        }

        /// <summary>
        /// Giriş formu bileşenlerini (Metin kutuları, butonlar, başlıklar) formun sağ yarısının yatay merkezine sabitleyen metot.
        /// Form boyutu değiştikçe kontrollerin ortalı ve dengeli görünmesini sağlar.
        /// </summary>
        private void CenterControls()
        {
            // Sol görsel panelin genişliği (Varsayılan olarak 280 piksel alınır)
            int leftPanelWidth = pnlLeft != null ? pnlLeft.Width : 280;
            // Sağ taraftaki boş alanın orta noktasının yatay koordinatını hesapla
            int rightAreaCenter = leftPanelWidth + (this.ClientSize.Width - leftPanelWidth) / 2;

            // Kullanıcı adı metin kutusunu ortala
            if (txtUsername != null)
                txtUsername.Left = rightAreaCenter - (txtUsername.Width / 2);

            // Şifre metin kutusunu ortala
            if (txtPassword != null)
                txtPassword.Left = rightAreaCenter - (txtPassword.Width / 2);

            // Giriş Yap butonunu ortala
            if (btnLogin != null)
                btnLogin.Left = rightAreaCenter - (btnLogin.Width / 2);

            // Ana başlık etiketini ortala
            if (Headertxt != null)
                Headertxt.Left = rightAreaCenter - (Headertxt.Width / 2);

            // Alt başlık etiketini ortala
            if (lblSubHeader != null)
                lblSubHeader.Left = rightAreaCenter - (lblSubHeader.Width / 2);

            // Kayıt Ol bağlantısını kullanıcı adı kutusunun sol kenarına hizala
            if (lnkRegister != null)
                lnkRegister.Left = rightAreaCenter - (txtUsername.Width / 2);

            // Şifremi Unuttum bağlantısını kullanıcı adı kutusunun sağ kenarına hizala
            if (lnkForgot != null)
                lnkForgot.Left = rightAreaCenter + (txtUsername.Width / 2) - lnkForgot.Width;
        }

        /// <summary>
        /// Giriş Yap butonuna tıklandığında tetiklenen ve kullanıcı kimlik bilgilerini SQL veritabanında sorgulayan metot.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // SQL Server Express veritabanı bağlantı dizesi
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // Veritabanı bağlantısını aç

                    // Kullanıcı adı ve şifreyi güvenli bir şekilde kontrol edip rolünü ve tema rengini alan SQL sorgusu
                    string query = "SELECT Role, ThemeColor FROM Users WHERE Username = @user AND Password = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // SQL Injection saldırılarını önlemek için parametrik sorgu yapısı kullanıyoruz
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Eşleşen kullanıcı kaydı bulunursa
                            {
                                string role = reader["Role"].ToString();
                                string themeColor = reader["ThemeColor"].ToString();

                                // SaaS görünümlü şık bir yükleme (Loading) modal penceresi göster
                                using (LoadingForm loading = new LoadingForm())
                                {
                                    if (loading.ShowDialog() == DialogResult.OK)
                                    {
                                        // Ana paneli (DashboardForm) yetki ve tema bilgileriyle aç
                                        DashboardForm dashboard = new DashboardForm(username, role, themeColor);
                                        dashboard.Show();
                                        this.Hide(); // Giriş formunu arka planda gizle
                                    }
                                }
                            }
                            else
                            {
                                // Hatalı giriş uyarısı
                                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Veritabanı bağlantı hatasını kullanıcıya göster
                    MessageBox.Show("Veritabanına bağlanılamadı. Hata: " + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Kayıt Ol linkine tıklandığında yeni kullanıcı kayıt formunu (RegisterForm) açan metot.
        /// </summary>
        private void lnkRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.ShowDialog(); // Kayıt formunu mod olarak açar
        }

        /// <summary>
        /// Şifremi Unuttum linkine tıklandığında şifre sıfırlama formunu (ForgotPasswordForm) açan metot.
        /// </summary>
        private void lnkForgot_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgot = new ForgotPasswordForm();
            forgot.ShowDialog(); // Şifre sıfırlama formunu modal açar
        }

        /// <summary>
        /// Çıkış butonuna tıklandığında uygulamayı tamamen sonlandıran metot.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
