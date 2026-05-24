using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ToDo_Final
{
    /// <summary>
    /// Kullanıcının profil bilgilerini görüntüleyebildiği ve profil görselini değiştirebildiği form.
    /// </summary>
    public partial class ProfileForm : Form
    {
        // Oturum açan kullanıcının bilgileri
        private string currentUser;
        private string currentRole;
        
        // Kullanıcının diskten seçtiği görselin yolu
        private string selectedImagePath = "";

        public ProfileForm(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentRole = role;

            // Form yüklenme ve tıklama olaylarını bağlıyoruz
            this.Load += ProfileForm_Load;
            btnSelectImage.Click += btnSelectImage_Click;
            btnSave.Click += btnSave_Click;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            // Kullanıcı adı ve rol bilgilerini yazdır
            lblUsername.Text = "Kullanıcı Adı: " + currentUser;
            lblRole.Text = "Rol: " + currentRole;

            // Mevcut profil resmini veritabanından çekip göster
            LoadCurrentProfilePicture();
        }

        /// <summary>
        /// Kullanıcının veritabanında kayıtlı profil resmini yükler.
        /// </summary>
        private void LoadCurrentProfilePicture()
        {
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
                            string picName = result.ToString();
                            string fullPath = Path.Combine(Application.StartupPath, "ProfilePictures", picName);

                            if (File.Exists(fullPath))
                            {
                                // Dosya kullanımda kilitlenmesin diye resim akışından (Stream) yükleme yapıyoruz
                                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                                {
                                    pbAvatar.Image = Image.FromStream(fs);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Profil resmi yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            // Dosya seçici diyalog penceresini açıyoruz
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Profil Resmi Seçin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    
                    // Seçilen görseli önizleme olarak göster
                    pbAvatar.Image = Image.FromFile(selectedImagePath);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Eğer yeni bir görsel seçilmediyse sadece formu kapat
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                this.Close();
                return;
            }

            try
            {
                // ProfilPictures klasörünü oluştur (Yoksa)
                string folderPath = Path.Combine(Application.StartupPath, "ProfilePictures");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Benzersiz dosya ismi oluştur (Örn: alptug_profile_1234567.jpg)
                string extension = Path.GetExtension(selectedImagePath);
                string newFileName = currentUser.ToLower() + "_profile_" + DateTime.Now.Ticks + extension;
                string destPath = Path.Combine(folderPath, newFileName);

                // Seçilen resmi ProfilePictures altına kopyala
                File.Copy(selectedImagePath, destPath, true);

                // Veritabanını güncelle
                UpdateProfilePictureInDatabase(newFileName);

                MessageBox.Show("Profil resmi başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Dashboard'a güncelleme bilgisini iletmek için OK döndür
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Profil resmi kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProfilePictureInDatabase(string fileName)
        {
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            string query = "UPDATE Users SET ProfilePicture = @pic WHERE Username = @user";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pic", fileName);
                    cmd.Parameters.AddWithValue("@user", currentUser);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
