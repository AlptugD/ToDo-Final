using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final
{
    public partial class TaskItem : UserControl
    {
        // Bu kartın veritabanındaki gerçek ID'sini hafızada tutuyoruz
        private int _taskId;

        public TaskItem()
        {
            InitializeComponent();
        }

        // Ana formdan verileri bu karta yüklemek için kullanacağımız metot
        public void SetTaskData(int taskId, string title, string desc, DateTime startDate, DateTime endDate, bool isCompleted)
        {
            _taskId = taskId;
            lblTitle.Text = title;
            lblDesc.Text = desc;

            // Tarihleri başlangıç ve bitiş olarak şık bir formatta yazdırıyoruz
            lblDate.Text = "📅 Başlangıç: " + startDate.ToShortDateString() + "  -  Bitiş: " + endDate.ToShortDateString();

            // Veritabanındaki duruma göre tiki işaretle veya kaldır
            chkIsCompleted.Checked = isCompleted;

            // Tamamlanmışsa yazının üstünü çizip rengini soluk yapıyoruz
            if (isCompleted)
            {
                lblTitle.Font = new Font(lblTitle.Font, FontStyle.Strikeout);
                lblTitle.ForeColor = Color.Gray;
            }
        }

        // 🗑️ Çöp Kutusuna Tıklanma Olayı (Veritabanından tamamen siler)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"'{lblTitle.Text}' görevini tamamen silmek istediğinize emin misiniz?", "Görevi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // Önce bu göreve ait yorumları, sonra görevin kendisini siliyoruz
                        string query = "DELETE FROM Comments WHERE TaskID = @id; DELETE FROM Tasks WHERE TaskID = @id;";
                        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", _taskId);
                            cmd.ExecuteNonQuery();
                        }

                        // Veritabanından silindikten sonra, kartı ekrandan anında gizliyoruz
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görev silinirken hata oluştu: " + ex.Message);
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
                        lblTitle.ForeColor = Color.Gray;
                    }
                    else
                    {
                        lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
                        lblTitle.ForeColor = Color.Black;
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