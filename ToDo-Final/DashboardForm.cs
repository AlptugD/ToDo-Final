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

        // Sağ taraftaki panelde hangi görevin detaylarını ve yorumlarını gösterdiğimizi takip eden değişken
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

            // Buton tıklama olayını kod tarafında güvene alıyoruz
            if (btnSendComment != null)
                btnSendComment.Click += btnSendComment_Click;
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
                btn.ShadowDecoration.Color = ControlPaint.Dark(fillColor, 0.2f); // Doğal bir gölge rengi tonu
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
                ApplyTheme(Color.FromArgb(138, 35, 135)); // Hata durumunda varsayılan modern mor tema
            }

            // 3. Yetkilendirme Kontrolü
            if (currentRole == "Çalışan")
            {
                if (pnlAddTask != null) pnlAddTask.Visible = false; // Çalışanlar yeni görev ekleyemez
                if (taskCalendar != null) taskCalendar.Enabled = false;
            }
            else if (currentRole == "Yönetici")
            {
                if (pnlAddTask != null) pnlAddTask.Visible = true;
                if (taskCalendar != null) taskCalendar.Enabled = true;
            }

            // 4. Form açıldığı an mevcut görevleri veritabanından çekip listeler
            LoadTasks();
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            using (ThemePickerForm themePicker = new ThemePickerForm())
            {
                if (themePicker.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = themePicker.SelectedColor;
                    ApplyTheme(selectedColor);

                    // Rengi HEX koduna çevirip veritabanına kaydeder
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

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            // 1. Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Lütfen bir görev başlığı girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDesc.Text.Trim();
            DateTime dueDate = taskCalendar.Value;

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // DÜZELTME: CreatedBy sütununu sorgudan çıkardık, veritabanınla birebir eşitledik
                    string query = "INSERT INTO Tasks (Title, Description, TaskDate, IsCompleted) VALUES (@title, @desc, @date, 0)";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@date", dueDate);
                        // @creator parametresini de tablonuzda olmadığı için sildik

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Yeni görev başarıyla eklendi!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // İşlem bitince kutuları temizle
                        txtTaskTitle.Clear();
                        txtTaskDesc.Clear();

                        // Liste otomatik güncellensin
                        LoadTasks();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Görev eklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Veritabanındaki görevleri çekip orta alandaki akış paneline şık kartlar halinde basan metot
        private void LoadTasks()
        {
            if (flpTasks != null) flpTasks.Controls.Clear();

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Kolon isimleri veritabanındaki gerçek hallerine (TaskDate, IsCompleted) uyarlandı
                    string query = "SELECT TaskID, Title, Description, TaskDate, IsCompleted FROM Tasks ORDER BY TaskDate ASC";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int taskId = Convert.ToInt32(reader["TaskID"]);
                                string title = reader["Title"].ToString();
                                string desc = reader["Description"].ToString();
                                DateTime dueDate = Convert.ToDateTime(reader["TaskDate"]);
                                bool isCompleted = Convert.ToBoolean(reader["IsCompleted"]);

                                string status = isCompleted ? "Tamamlandı" : "Bekliyor";

                                // --- Dinamik Görev Kartı Tasarımı ---
                                Guna.UI2.WinForms.Guna2Panel taskCard = new Guna.UI2.WinForms.Guna2Panel();
                                taskCard.Size = new Size(flpTasks.Width - 25, 90);
                                taskCard.BorderRadius = 12;
                                taskCard.FillColor = Color.White;
                                taskCard.Margin = new Padding(5, 5, 5, 10);
                                taskCard.ShadowDecoration.Enabled = true;
                                taskCard.ShadowDecoration.Depth = 8;

                                // Görev Başlığı (Kalın Yazı)
                                Label lblTitle = new Label();
                                lblTitle.Text = title;
                                lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                                lblTitle.ForeColor = Color.FromArgb(47, 53, 66);
                                lblTitle.Location = new Point(15, 12);
                                lblTitle.AutoSize = true;

                                // Görev Açıklama Özeti
                                Label lblDesc = new Label();
                                lblDesc.Text = desc.Length > 45 ? desc.Substring(0, 45) + "..." : desc;
                                lblDesc.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                                lblDesc.ForeColor = Color.FromArgb(120, 120, 120);
                                lblDesc.Location = new Point(15, 38);
                                lblDesc.AutoSize = true;

                                // Tarih ve Durum Etiketi
                                Label lblDate = new Label();
                                lblDate.Text = "📅 " + dueDate.ToShortDateString() + "  |  📌 " + status;
                                lblDate.Font = new Font("Segoe UI", 8, FontStyle.Italic);
                                lblDate.ForeColor = Color.DimGray;
                                lblDate.Location = new Point(15, 62);
                                lblDate.AutoSize = true;

                                // Detay / Seçim Butonu
                                Guna.UI2.WinForms.Guna2Button btnDetail = new Guna.UI2.WinForms.Guna2Button();
                                btnDetail.Text = "Seç / Yorumla";
                                btnDetail.Size = new Size(120, 32);
                                btnDetail.Location = new Point(taskCard.Width - 135, 28);
                                btnDetail.BorderRadius = 8;
                                btnDetail.FillColor = Color.FromArgb(94, 148, 255);
                                btnDetail.Tag = taskId; // Görevin benzersiz anahtarını buton hafızasına alıyoruz

                                // Karta tıklanınca sağ tarafa yorumları yükleyen tetikleyici olay
                                btnDetail.Click += (s, ev) =>
                                {
                                    selectedTaskId = Convert.ToInt32(btnDetail.Tag);
                                    LoadComments(selectedTaskId);
                                };

                                taskCard.Controls.Add(lblTitle);
                                taskCard.Controls.Add(lblDesc);
                                taskCard.Controls.Add(lblDate);
                                taskCard.Controls.Add(btnDetail);

                                flpTasks.Controls.Add(taskCard);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Görevler listelenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Seçilen göreve ait eski tüm konuşmaları veritabanından çekip sağ panele basan metot
        private void LoadComments(int taskId)
        {
            if (flpComments != null) flpComments.Controls.Clear();

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Username, CommentText, CreatedAt FROM Comments WHERE TaskID = @taskId ORDER BY CreatedAt ASC";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskId", taskId);

                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string user = reader["Username"].ToString();
                                string text = reader["CommentText"].ToString();
                                DateTime date = Convert.ToDateTime(reader["CreatedAt"]);

                                // --- Yorum Balonu Tasarımı ---
                                Guna.UI2.WinForms.Guna2Panel commentBubble = new Guna.UI2.WinForms.Guna2Panel();
                                commentBubble.Size = new Size(flpComments.Width - 25, 65);
                                commentBubble.BorderRadius = 8;

                                // Giriş yapan kullanıcının mesajı ise mavi, başkasının ise gri balon olur (Discord mantığı)
                                commentBubble.FillColor = (user == currentUser) ? Color.FromArgb(230, 242, 255) : Color.FromArgb(241, 242, 246);
                                commentBubble.Margin = new Padding(5, 5, 5, 8);

                                // Yorum Yapan Kullanıcı Başlığı
                                Label lblUser = new Label();
                                lblUser.Text = user + " • " + date.ToShortTimeString();
                                lblUser.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                                lblUser.ForeColor = Color.DimGray;
                                lblUser.Location = new Point(10, 8);
                                lblUser.AutoSize = true;

                                // Mesaj İçeriği
                                Label lblText = new Label();
                                lblText.Text = text;
                                lblText.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                                lblText.ForeColor = Color.Black;
                                lblText.Location = new Point(10, 28);
                                lblText.AutoSize = true;

                                commentBubble.Controls.Add(lblUser);
                                commentBubble.Controls.Add(lblText);

                                flpComments.Controls.Add(commentBubble);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Yorumlar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSendComment_Click(object sender, EventArgs e)
        {
            if (selectedTaskId == 0)
            {
                MessageBox.Show("Lütfen önce yorum yapmak istediğiniz görevi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text)) return;

            string commentText = txtComment.Text.Trim();
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Comments (TaskID, Username, CommentText, CreatedAt) VALUES (@taskId, @user, @text, GETDATE())";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskId", selectedTaskId);
                        cmd.Parameters.AddWithValue("@user", currentUser);
                        cmd.Parameters.AddWithValue("@text", commentText);

                        cmd.ExecuteNonQuery();

                        // Yorum gönderildikten sonra metin alanı temizlenir ve liste tazece yenilenir
                        txtComment.Clear();
                        LoadComments(selectedTaskId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Yorum gönderilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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