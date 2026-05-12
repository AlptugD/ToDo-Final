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

            // TODO: SQL Server bağlantısı eklenecek.
            if (username == "admin" && password == "123")
            {
                MessageBox.Show("Yönetici girişi başarılı. Tüm yetkilere sahipsiniz.", "Hoş Geldiniz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (username == "calisan" && password == "123")
            {
                MessageBox.Show("Çalışan girişi başarılı. Sınırlı yetki ile devam ediyorsunuz.", "Hoş Geldiniz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
