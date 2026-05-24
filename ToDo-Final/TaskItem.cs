using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final
{
    public partial class TaskItem : UserControl
    {
        // Bu kartın veritabanındaki gerçek ID'sini hafızada tutuyoruz
        private int _taskId;
        private DateTime _endDate;
        private System.Windows.Forms.Timer countdownTimer;
        private bool _isExpiredNotified = false;

        public int TaskId => _taskId;
        public event Action<int> TaskSelected;

        public TaskItem()
        {
            InitializeComponent();
        }

        // Ana formdan verileri bu karta yüklemek için kullanacağımız metot
        public void SetTaskData(int taskId, string title, string desc, DateTime startDate, DateTime endDate, bool isCompleted, string role)
        {
            _taskId = taskId;
            _endDate = endDate;
            lblTitle.Text = title;
            lblDesc.Text = desc;

            // Eğer açılışta süresi zaten dolmuşsa, uyarı popup'ı verilmesin
            if ((_endDate - DateTime.Now).TotalSeconds <= 0)
            {
                _isExpiredNotified = true;
            }
            else
            {
                _isExpiredNotified = false;
            }

            // Tarihleri başlangıç ve bitiş olarak şık bir formatta yazdırıyoruz
            lblDate.Text = "📅 Başlangıç: " + startDate.ToString("dd.MM.yyyy HH:mm") + "  -  Bitiş: " + endDate.ToString("dd.MM.yyyy HH:mm");

            // Veritabanındaki duruma göre tiki işaretle veya kaldır
            chkIsCompleted.Checked = isCompleted;

            // Sleek Koyu Tema Kart Tasarımı
            guna2Panel1.FillColor = isCompleted ? Color.FromArgb(15, 23, 42) : Color.FromArgb(21, 27, 44);
            guna2Panel1.BorderColor = Color.FromArgb(37, 47, 72);
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.BorderRadius = 12;

            lblDesc.ForeColor = Color.FromArgb(148, 163, 184);
            lblDate.ForeColor = Color.FromArgb(56, 189, 248);

            // Tamamlanmışsa yazının üstünü çizip rengini soluk yapıyoruz
            if (isCompleted)
            {
                lblTitle.Font = new Font(lblTitle.Font, FontStyle.Strikeout);
                lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
            }
            else
            {
                lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
                lblTitle.ForeColor = Color.White;
            }

            // ÇALIŞAN GÜVENLİK AYARI: Çalışanlar silemez!
            if (role == "Çalışan")
            {
                btnDelete.Visible = false;
            }
            else
            {
                btnDelete.Visible = true;
            }

            // Geri sayım sayacını başlat
            InitializeCountdown();

            // Kartın tıklanma olaylarını bağlama (Seçim işlemi için)
            BindClickEvent(guna2Panel1);
            BindClickEvent(lblTitle);
            BindClickEvent(lblDesc);
            BindClickEvent(lblDate);

            // Kartın üzerine gelindiğinde parlaması için Hover olaylarını bağlama
            BindHoverEvents(guna2Panel1);
            BindHoverEvents(lblTitle);
            BindHoverEvents(lblDesc);
            BindHoverEvents(lblDate);
        }

        private void BindClickEvent(Control control)
        {
            if (control == null) return;
            control.Cursor = Cursors.Hand;
            control.Click -= Control_Click;
            control.Click += Control_Click;
        }

        private void BindHoverEvents(Control control)
        {
            if (control == null) return;
            control.MouseEnter -= Control_MouseEnter;
            control.MouseEnter += Control_MouseEnter;
            control.MouseLeave -= Control_MouseLeave;
            control.MouseLeave += Control_MouseLeave;
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            guna2Panel1.BorderColor = Color.FromArgb(99, 102, 241); // Cyber Indigo hover!
            guna2Panel1.FillColor = Color.FromArgb(30, 41, 59); // Arka planı hafifçe aydınlat
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel1.BorderColor = Color.FromArgb(37, 47, 72); // Varsayılan mat sınırı geri getir
            guna2Panel1.FillColor = chkIsCompleted.Checked ? Color.FromArgb(15, 23, 42) : Color.FromArgb(21, 27, 44);
        }

        private void InitializeCountdown()
        {
            if (countdownTimer == null)
            {
                countdownTimer = new System.Windows.Forms.Timer();
                countdownTimer.Interval = 1000; // Her saniye
                countdownTimer.Tick += CountdownTimer_Tick;
            }
            countdownTimer.Start();
            CountdownTimer_Tick(null, null);
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan remaining = _endDate - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                lblCountdown.Text = "⏰ SÜRE BİTTİ!";
                lblCountdown.ForeColor = Color.FromArgb(255, 60, 60);
                if (countdownTimer != null) countdownTimer.Stop();

                // Eğer canlı çalışırken süre bittiyse ve daha önce uyarılmadıysa ve görev tamamlanmadıysa uyar
                if (!_isExpiredNotified && !chkIsCompleted.Checked)
                {
                    _isExpiredNotified = true;
                    MessageBox.Show($"⏰ '{lblTitle.Text}' görevinin süresi doldu!", "Süre Doldu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblCountdown.Text = string.Format("⏳ {0}g {1}s {2}d {3}sn", 
                    remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

                if (remaining.TotalHours < 24)
                {
                    lblCountdown.ForeColor = Color.FromArgb(255, 128, 128); // Kırmızımsı uyarı rengi
                }
                else
                {
                    lblCountdown.ForeColor = Color.FromArgb(128, 255, 128); // Güvenli yeşil renk
                }
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            TaskSelected?.Invoke(_taskId);
        }

        // 🗑️ Çöp Kutusuna Tıklanma Olayı (İSMİ DÜZELTİLDİ: btnDelete_Click_1)
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            // Kullanıcıya silme işlemi öncesinde onay kutusu gösteriyoruz
            DialogResult dialogResult = MessageBox.Show($"'{lblTitle.Text}' görevini tamamen silmek istediğinize emin misiniz?", "Görevi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Veritabanı ilişkileri (Foreign Key) nedeniyle önce bu göreve yazılmış yorumları siliyoruz, ardından görevin kendisini temizliyoruz
                        string query = "DELETE FROM Comments WHERE TaskID = @id; DELETE FROM Tasks WHERE TaskID = @id;";

                        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", _taskId);
                            cmd.ExecuteNonQuery();
                        }

                        // Veritabanından silme işlemi başarılı olduktan sonra kartı içinde bulunduğu panelden tamamen kaldırıyoruz
                        if (this.Parent != null)
                        {
                            this.Parent.Controls.Remove(this);
                        }

                        MessageBox.Show("Görev başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görev silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ✅ Özel Tik Kutusuna Tıklanma Olayı (Veritabanını günceller)
        private void chkIsCompleted_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Veritabanında görevin durumunu güncelliyoruz
                    string query = "UPDATE Tasks SET IsCompleted = @status WHERE TaskID = @id";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", chkIsCompleted.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@id", _taskId);
                        cmd.ExecuteNonQuery();
                    }

                    // Tiki işaretleme veya kaldırma durumuna göre başlığın görünümünü ayarlıyoruz
                    if (chkIsCompleted.Checked)
                    {
                        lblTitle.Font = new Font(lblTitle.Font, FontStyle.Strikeout);
                        lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
                        guna2Panel1.FillColor = Color.FromArgb(15, 23, 42);
                    }
                    else
                    {
                        lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
                        lblTitle.ForeColor = Color.White;
                        guna2Panel1.FillColor = Color.FromArgb(21, 27, 44);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Durum güncellenirken hata oluştu: " + ex.Message);
                }
            }
        }
    }
}