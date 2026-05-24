using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ToDo_Final
{
    /// <summary>
    /// Sisteme yeni bir kullanıcının (Yönetici veya Çalışan) kaydolmasını sağlayan modern arayüz formu.
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// Sınıfın kurucu metodu. Bileşenleri yükler ve Kaydet/İptal butonlarının tıklama olaylarını bağlar.
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
            
            // Buton tıklama olaylarını olay yöneticilerine bağlıyoruz
            if (btnRegister != null) btnRegister.Click += btnRegister_Click;
            if (btnExit != null) btnExit.Click += (s, e) => this.Close();
        }

        /// <summary>
        /// Kayıt Ol butonuna tıklandığında tetiklenen, metin kutusu doğrulayan ve veritabanına yeni kayıt ekleyen metot.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Kullanıcı girdilerindeki kenar boşluklarını kırparak alıyoruz
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            // Rol seçilmemişse varsayılan olarak "Çalışan" seçilir
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "Çalışan";

            // Zorunlu alan kontrolü
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Lütfen bütün alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL Server bağlantı dizesi
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            
            // Yeni kullanıcıyı varsayılan mor tema rengi (#8A2387) ile kaydeden SQL insert sorgusu
            string query = "INSERT INTO Users (Username, Password, Role, ThemeColor) VALUES (@user, @pass, @role, '#8A2387')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kullanıcı adının benzersiz (unique) olup olmadığını denetle
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @user";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", username);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Bu kullanıcı adı zaten alınmış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Benzersizlik doğrulandıktan sonra veritabanına ekle
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametrik sorgu kullanarak SQL Injection saldırılarının önüne geçiyoruz
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.Parameters.AddWithValue("@role", role);

                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Başarıyla kayıt olundu! Giriş ekranına dönebilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Başarılı kayıt sonrası pencereyi kapat
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt olunurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
