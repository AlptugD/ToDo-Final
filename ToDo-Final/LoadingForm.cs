using System;
using System.Windows.Forms;

namespace ToDo_Final
{
    /// <summary>
    /// Uygulama açılırken veya veritabanı işlemleri yapılırken kullanıcıyı karşılayan premium yükleme ekranı.
    /// </summary>
    public partial class LoadingForm : Form
    {
        private int progressValue = 0;

        public LoadingForm()
        {
            InitializeComponent();
            this.Load += LoadingForm_Load;
            loadingTimer.Tick += LoadingTimer_Tick;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            // Yükleme animasyonunu ve zamanlayıcıyı başlatıyoruz
            progressIndicator.Start();
            loadingTimer.Start();
        }

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            // İlerlemeyi rastgele adımlarla artırıyoruz (SaaS yükleme hissi)
            Random rand = new Random();
            progressValue += rand.Next(5, 15);

            if (progressValue >= 100)
            {
                progressValue = 100;
                progressBar.Value = progressValue;
                loadingTimer.Stop();
                progressIndicator.Stop();

                // Yükleme tamamlandı, formu kapatıyoruz
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                progressBar.Value = progressValue;

                // İlerleme yüzdesine göre kullanıcıya dinamik durum mesajı göster
                if (progressValue < 25)
                {
                    lblStatus.Text = "Veritabanına bağlanılıyor...";
                }
                else if (progressValue < 55)
                {
                    lblStatus.Text = "Kullanıcı oturum verileri senkronize ediliyor...";
                }
                else if (progressValue < 85)
                {
                    lblStatus.Text = "Modern arayüz bileşenleri yükleniyor...";
                }
                else
                {
                    lblStatus.Text = "Güvenli bağlantı kuruluyor. Giriş yapılıyor...";
                }
            }
        }
    }
}
