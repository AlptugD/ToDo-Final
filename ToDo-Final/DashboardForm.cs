using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

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

        // Hatırlatıcı kontrolü için Timer ve tetiklenmiş alarm kümesi
        private System.Windows.Forms.Timer reminderTimer;
        private System.Windows.Forms.Timer clockTimer;
        private System.Collections.Generic.HashSet<int> activeAlarms = new System.Collections.Generic.HashSet<int>();

        // Giriş ekranından bilgileri alan yapıcı metot (Constructor)
        public DashboardForm(string username, string role, string themeHex)
        {
            InitializeComponent();

            // Veritabanı şemasını dinamik kontrol et ve eksik kolonları tamamla
            EnsureDatabaseSchema();

            // Formun her zaman ekranın tam ortasında açılmasını sağlar
            this.StartPosition = FormStartPosition.CenterScreen;

            // Giriş yapan kullanıcının bilgilerini içeri aktarıyoruz
            currentUser = username;
            currentRole = role;
            currentThemeHex = themeHex;

            // Form yüklenirken çalışacak yükleme olayını bağlıyoruz
            this.Load += DashboardForm_Load;

            // Hatırlatıcı alarm sistemini arka planda başlat
            InitializeReminderTimer();

            // Saat göstergesini başlat
            InitializeClock();

            // Buton tıklama olaylarını bağlıyoruz
            if (btnSendComment != null)
                btnSendComment.Click += btnSendComment_Click;

            if (btnAddSticker != null)
                btnAddSticker.Click += btnAddSticker_Click;

            if (btnProfile != null)
                btnProfile.Click += btnProfile_Click;
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
            // Modern Sleek Dark Mode Temel Rengi
            Color mainBgColor = Color.FromArgb(20, 20, 30);
            this.BackColor = mainBgColor;
            if (pnlMain != null) pnlMain.BackColor = mainBgColor;

            // Panelleri modern koyu cam (Glassmorphism) efektiyle kapla
            Color panelBgColor = Color.FromArgb(30, 30, 48);
            if (pnlAddTask != null)
            {
                pnlAddTask.FillColor = panelBgColor;
                pnlAddTask.BorderColor = themeColor;
                pnlAddTask.BorderThickness = 1;
                pnlAddTask.BorderRadius = 15;
            }

            if (pnlTaskDetails != null)
            {
                pnlTaskDetails.FillColor = panelBgColor;
                pnlTaskDetails.BorderColor = themeColor;
                pnlTaskDetails.BorderThickness = 1;
                pnlTaskDetails.BorderRadius = 15;
            }

            // Sol sidebar gradyan renkleri: Seçilen tema rengine göre şekillenir
            if (guna2CustomGradientPanel1 != null)
            {
                guna2CustomGradientPanel1.FillColor = ControlPaint.Dark(themeColor, 0.2f);
                guna2CustomGradientPanel1.FillColor2 = ControlPaint.Dark(themeColor, 0.5f);
            }

            // Saat Rengi Seçilen Temanın neon tonuyla uyumlu olsun
            if (lblClock != null)
            {
                lblClock.ForeColor = LightenColor(themeColor, 0.4);
            }

            Color buttonColor = themeColor;
            ApplyButtonModernStyle(btnAddTask, buttonColor);
            ApplyButtonModernStyle(btnSendComment, buttonColor);
            
            // Sol sidebar butonları şeffaf modern tasarım
            ApplyButtonModernStyle(guna2Button1, Color.Transparent);
            ApplyButtonModernStyle(guna2Button4, Color.Transparent);
            ApplyButtonModernStyle(btnChangeTheme, Color.Transparent);
            ApplyButtonModernStyle(btnProfile, Color.Transparent);

            if (taskCalendarDate != null)
            {
                taskCalendarDate.FillColor = buttonColor;
                taskCalendarDate.ForeColor = Color.White;
                taskCalendarDate.BorderRadius = 10;
            }
            if (cmbHour != null)
            {
                cmbHour.FillColor = Color.FromArgb(31, 41, 55);
                cmbHour.BorderColor = Color.FromArgb(55, 65, 81);
                cmbHour.ForeColor = Color.White;
            }
            if (cmbMinute != null)
            {
                cmbMinute.FillColor = Color.FromArgb(31, 41, 55);
                cmbMinute.BorderColor = Color.FromArgb(55, 65, 81);
                cmbMinute.ForeColor = Color.White;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // 1. Profil bilgilerini sol üst panele yazdırır ve resmi yükler
            if (lblUserName != null) lblUserName.Text = currentUser;
            if (lblUserRole != null) lblUserRole.Text = currentRole;
            LoadUserProfilePicture();

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
                if (taskCalendarDate != null) taskCalendarDate.Enabled = false;
                if (cmbHour != null) cmbHour.Enabled = false;
                if (cmbMinute != null) cmbMinute.Enabled = false;
            }
            else if (currentRole == "Yönetici" || currentRole == "Admin")
            {
                if (pnlAddTask != null) pnlAddTask.Visible = true;
                if (taskCalendarDate != null) taskCalendarDate.Enabled = true;
                if (cmbHour != null) cmbHour.Enabled = true;
                if (cmbMinute != null) cmbMinute.Enabled = true;
                if (guna2Button4 != null) guna2Button4.Visible = false;
            }

            // 4. Form açılır açılmaz veritabanındaki görevleri özel kartlarla (TaskItem) listele
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

        // --- VERİTABANI ŞEMA KONTROLÜ (Mevcut Veritabanına Kolon Ekleme) ---
        private void EnsureDatabaseSchema()
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Comments tablosunda StickerPath sütunu var mı kontrol et, yoksa ekle
                    string checkCommentsColumn = @"
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID('Comments') AND name = 'StickerPath'
                        )
                        BEGIN
                            ALTER TABLE Comments ADD StickerPath NVARCHAR(250) NULL;
                        END";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(checkCommentsColumn, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Tasks tablosunda ReminderTime ve HasReminder sütunları var mı kontrol et, yoksa ekle
                    string checkTasksColumns = @"
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID('Tasks') AND name = 'ReminderTime'
                        )
                        BEGIN
                            ALTER TABLE Tasks ADD ReminderTime DATETIME NULL;
                        END
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID('Tasks') AND name = 'HasReminder'
                        )
                        BEGIN
                            ALTER TABLE Tasks ADD HasReminder BIT DEFAULT 0;
                        END";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(checkTasksColumns, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Users tablosunda ProfilePicture sütunu var mı kontrol et, yoksa ekle
                    string checkUsersColumn = @"
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID('Users') AND name = 'ProfilePicture'
                        )
                        BEGIN
                            ALTER TABLE Users ADD ProfilePicture NVARCHAR(250) NULL;
                        END";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(checkUsersColumn, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Veritabanı şema doğrulama hatası: " + ex.Message);
                }
            }
        }

        // --- HATIRLATICI VE UYARI SİSTEMİ ---
        private void InitializeReminderTimer()
        {
            reminderTimer = new System.Windows.Forms.Timer();
            reminderTimer.Interval = 30000; // Her 30 saniyede bir kontrol et
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        // --- DİJİTAL SAAT SİSTEMİ ---
        private void InitializeClock()
        {
            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000; // Her saniye
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            // İlk anlık değerleri yazdır
            ClockTimer_Tick(null, null);
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            if (lblClock != null)
                lblClock.Text = DateTime.Now.ToString("HH:mm:ss");

            if (lblClockDate != null)
                lblClockDate.Text = DateTime.Now.ToString("dd MMMM yyyy dddd", new System.Globalization.CultureInfo("tr-TR"));
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            // Zamanı gelmiş veya geçmekte olan hatırlatıcıları bul (IsCompleted = 0)
            string query = "SELECT TaskID, Title, TaskDate FROM Tasks WHERE IsCompleted = 0 AND HasReminder = 1 AND ReminderTime <= GETDATE()";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int taskId = Convert.ToInt32(reader["TaskID"]);
                                string title = reader["Title"].ToString();
                                DateTime dueDate = Convert.ToDateTime(reader["TaskDate"]);

                                // Alarmlar kümesinde yoksa uyar
                                if (!activeAlarms.Contains(taskId))
                                {
                                    activeAlarms.Add(taskId);

                                    // Şık bir uyarı popup'ı
                                    MessageBox.Show($"⏰ GÖREV HATIRLATICI!\n\nBaşlık: {title}\nBitiş Tarihi: {dueDate.ToString("g")}\n\nLütfen görevi zamanında tamamlamayı unutmayın!", 
                                        "Görev Alarmı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Sessizce yoksay
                }
            }
        }

        // --- GÖREV EKLEME VE LİSTELEME SİSTEMİ ---

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Lütfen bir görev başlığı girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDesc.Text.Trim();

            // Başlangıç tarihi olarak "Şu An" otomatik kabul edilir
            DateTime startDate = DateTime.Now;

            // Bitiş tarihi ve saati seçilen kontrollerden alınır ve birleştirilir
            DateTime date = taskCalendarDate != null ? taskCalendarDate.Value.Date : DateTime.Today;
            int hour = 12;
            int minute = 0;
            if (cmbHour != null && cmbHour.SelectedItem != null)
                int.TryParse(cmbHour.SelectedItem.ToString(), out hour);
            if (cmbMinute != null && cmbMinute.SelectedItem != null)
                int.TryParse(cmbMinute.SelectedItem.ToString(), out minute);
            DateTime endDate = date.AddHours(hour).AddMinutes(minute);

            if (endDate.Date < startDate.Date)
            {
                MessageBox.Show("Bitiş tarihi bugünden önce olamaz!", "Tarih Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Varsayılan hatırlatıcı zamanı: Bitiş tarihinden 1 saat öncesidir.
            DateTime reminderTime = endDate.AddHours(-1);

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Tasks (Title, Description, StartDate, TaskDate, IsCompleted, ReminderTime, HasReminder) VALUES (@title, @desc, @start, @end, 0, @reminder, 1)";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@start", startDate);
                        cmd.Parameters.AddWithValue("@end", endDate);
                        cmd.Parameters.AddWithValue("@reminder", reminderTime);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Yeni görev başarıyla eklendi!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // İşlem bitince kutuları temizle ve listeyi yenile
                        txtTaskTitle.Clear();
                        txtTaskDesc.Clear();
                        LoadTasks();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Görev eklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTasks()
        {
            if (flpTasks != null) flpTasks.Controls.Clear();

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TaskID, Title, Description, StartDate, TaskDate, IsCompleted FROM Tasks ORDER BY TaskDate ASC";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int taskId = Convert.ToInt32(reader["TaskID"]);
                                string title = reader["Title"].ToString();
                                string desc = reader["Description"].ToString();
                                DateTime startDate = Convert.ToDateTime(reader["StartDate"]);
                                DateTime endDate = Convert.ToDateTime(reader["TaskDate"]);
                                bool isCompleted = Convert.ToBoolean(reader["IsCompleted"]);

                                // Tasarladığın özel Görev Kartı (UserControl) nesnesini yaratıyoruz
                                TaskItem taskItem = new TaskItem();

                                // Kartın içindeki metot sayesinde verileri içeri aktarıyoruz (Role bilgisini de ekledik!)
                                taskItem.SetTaskData(taskId, title, desc, startDate, endDate, isCompleted, currentRole);

                                // Görev seçilince sağdaki panelde yorum ve detayları gösteren olayı bağlıyoruz
                                taskItem.TaskSelected += (selId) =>
                                {
                                    selectedTaskId = selId;
                                    LoadComments(selId);
                                };

                                // Kartın genişliğini ortadaki panele tam uyacak şekilde ayarlıyoruz
                                taskItem.Width = flpTasks.Width - 25;
                                taskItem.Margin = new Padding(5, 5, 5, 10);

                                // Kartı ekrana ekliyoruz
                                flpTasks.Controls.Add(taskItem);
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

        // --- GÖREVE YORUM VE STICKER YAZMA SİSTEMİ ---

        private void LoadComments(int taskId)
        {
            if (flpComments != null) flpComments.Controls.Clear();

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT Username, CommentText, CreatedAt, StickerPath FROM Comments WHERE TaskID = @taskId ORDER BY CreatedAt ASC";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskId", taskId);
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string username = reader["Username"].ToString();
                                string text = reader["CommentText"].ToString();
                                DateTime date = Convert.ToDateTime(reader["CreatedAt"]);
                                string sticker = reader["StickerPath"] != DBNull.Value ? reader["StickerPath"].ToString() : "";

                                CommentItem commentItem = new CommentItem();
                                commentItem.SetCommentData(username, text, date, sticker);

                                // Genişlik ve stil ayarı
                                commentItem.Width = flpComments.Width - 25;
                                commentItem.Margin = new Padding(5, 5, 5, 10);

                                flpComments.Controls.Add(commentItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Yorumlar yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSendComment_Click(object sender, EventArgs e)
        {
            if (selectedTaskId == 0)
            {
                MessageBox.Show("Lütfen yorum yazmak için bir görev seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Lütfen boş bir yorum göndermeyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string text = txtComment.Text.Trim();
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Comments (TaskID, Username, CommentText, CreatedAt, StickerPath) VALUES (@taskId, @user, @text, GETDATE(), NULL)";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskId", selectedTaskId);
                        cmd.Parameters.AddWithValue("@user", currentUser);
                        cmd.Parameters.AddWithValue("@text", text);
                        cmd.ExecuteNonQuery();
                    }

                    txtComment.Clear();
                    LoadComments(selectedTaskId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Yorum gönderilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddSticker_Click(object sender, EventArgs e)
        {
            if (selectedTaskId == 0)
            {
                MessageBox.Show("Lütfen sticker eklemek için bir görev seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (StickerForm stickerPicker = new StickerForm())
            {
                stickerPicker.StickerSelected += (stickerFile) =>
                {
                    // Seçilen stickerı veritabanına yorum olarak kaydet
                    SaveStickerComment(selectedTaskId, stickerFile);
                };
                stickerPicker.ShowDialog(this);
            }
        }

        private void SaveStickerComment(int taskId, string stickerFile)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Comments (TaskID, Username, CommentText, CreatedAt, StickerPath) VALUES (@taskId, @user, 'Sticker gönderdi', GETDATE(), @sticker)";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskId", taskId);
                        cmd.Parameters.AddWithValue("@user", currentUser);
                        cmd.Parameters.AddWithValue("@sticker", stickerFile);
                        cmd.ExecuteNonQuery();
                    }

                    LoadComments(taskId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sticker kaydedilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        // --- PROFİL YÖNETİM SİSTEMİ ---

        /// <summary>
        /// Oturum açan kullanıcının profil resmini SQL veritabanından sorgular ve sol sidebar'daki PictureBox'a yükler.
        /// </summary>
        private void LoadUserProfilePicture()
        {
            if (guna2CirclePictureBox1 == null) return;

            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "SELECT ProfilePicture FROM Users WHERE Username = @user";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", currentUser);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string picPath = result.ToString();
                            string fullPath = Path.Combine(Application.StartupPath, "ProfilePictures", picPath);

                            if (File.Exists(fullPath))
                            {
                                // Dosyanın kilitlenmesini önlemek için akış kullanıyoruz
                                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                                {
                                    guna2CirclePictureBox1.Image = Image.FromStream(fs);
                                }
                                guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Profil resmi yükleme hatası: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Takvim kontrolünün herhangi bir yerine tıklandığında takvim açılır penceresini (tablo/takvim görünümü) tetikler.
        /// </summary>
        private void taskCalendarDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (taskCalendarDate != null && taskCalendarDate.Enabled)
            {
                taskCalendarDate.Focus();
                SendKeys.Send("%{DOWN}");
            }
        }

        /// <summary>
        /// Sol sidebar'daki Profilim butonuna tıklandığında profil düzenleme penceresini açar.
        /// </summary>
        private void btnProfile_Click(object sender, EventArgs e)
        {
            using (ProfileForm profileForm = new ProfileForm(currentUser, currentRole))
            {
                // Eğer profil resmi başarıyla güncellendiyse (DialogResult.OK döndüyse)
                if (profileForm.ShowDialog() == DialogResult.OK)
                {
                    // Sol sidebar profil görselini hemen yenile
                    LoadUserProfilePicture();
                }
            }
        }
    }
}