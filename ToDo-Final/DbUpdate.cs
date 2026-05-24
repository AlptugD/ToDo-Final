using System;
using Microsoft.Data.SqlClient;

namespace ToDo_Final
{
    /// <summary>
    /// Veritabanı şemasını güncellemek (kolon isimlerini değiştirmek veya yeni kolonlar eklemek) için tasarlanmış göç (migration) yardımcı sınıfı.
    /// </summary>
    internal static class DbUpdate
    {
        /// <summary>
        /// Şema güncellemelerini çalıştırır. Tasks tablosundaki kolonları düzenler ve yeni tarih alanları ekler.
        /// </summary>
        public static void Run()
        {
            // Yerel SQL Server Express veritabanı bağlantı dizesi
            string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                // 1. Adım: Eski 'TaskDate' kolonunu 'StartDate' (Başlangıç Tarihi) olarak yeniden adlandırır
                try
                {
                    using (SqlCommand cmd = new SqlCommand("EXEC sp_rename 'Tasks.TaskDate', 'StartDate', 'COLUMN';", conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Bilgi: 'TaskDate' kolonu başarıyla 'StartDate' olarak adlandırıldı.");
                    }
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("Bilgi: Kolon yeniden adlandırılamadı (daha önce çalıştırılmış olabilir): " + ex.Message); 
                }

                // 2. Adım: Görev bitiş saatlerinin tutulması için tabloya 'EndDate' kolonu ekler
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ALTER TABLE Tasks ADD EndDate DATETIME;", conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Bilgi: 'EndDate' kolonu başarıyla tabloya eklendi.");
                    }
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("Bilgi: 'EndDate' kolonu eklenemedi (zaten veritabanında mevcut olabilir): " + ex.Message); 
                }
            }
        }
    }
}
