using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ToDo_Final
{
    /// <summary>
    /// Kullanıcıların şifrelerini güvenli bir şekilde sıfırlamalarını / güncellemelerini sağlayan arayüz formu.
    /// </summary>
    public partial class ForgotPasswordForm : Form
    {
        /// <summary>
        /// Sınıfın kurucu metodu. Arayüz elemanlarını hazırlar ve buton olaylarını bağlar.
        /// </summary>
        public ForgotPasswordForm()
        {
            InitializeComponent();
            
            // Olayları (Event Handlers) bağlama
            if (btnReset != null) btnReset.Click += btnReset_Click;
            if (btnExit != null) btnExit.Click += (s, e) => this.Close();
        }

        /// <summary>
        /// Şifreyi Sıfırla butonuna tıklandığında çalışan, kullanıcının varlığını doğrulayıp şifreyi güncelleyen metot.
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve yeni şifre boşluklarını kırparak alıyoruz
            string username = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            // Zorunlu alan doğrulamaları
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Lütfen bütün alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL Server Express bağlantı dizesi
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            
            // Kullanıcı şifresini güncelleyen SQL sorgusu
            string query = "UPDATE Users SET Password = @pass WHERE Username = @user";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // İlk olarak girilen kullanıcı adına sahip bir hesabın olup olmadığını denetle
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @user";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", username);
                        int count = (int)checkCmd.ExecuteScalar();
                        
                        if (count == 0)
                        {
                            MessageBox.Show("Bu kullanıcı adına kayıtlı hesap bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Hesap mevcutsa şifre alanını yeni şifreyle güncelle
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametrik sorgu ile SQL injection saldırılarının önüne geçilir
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", newPassword);

                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Şifreniz başarıyla sıfırlandı! Giriş ekranına dönebilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Başarılı sıfırlama sonrası ekranı kapat
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Şifre güncellenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
