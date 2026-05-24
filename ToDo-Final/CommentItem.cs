using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final
{
    /// <summary>
    /// Guna.UI2 kütüphanesi kullanılarak tasarlanmış, modern ve glassmorphic görünümlü yorum kartı bileşeni.
    /// Her bir yorumun kullanıcı adını, içeriğini, gönderim tarihini ve varsa eklenen görsel stickerı görüntüler.
    /// </summary>
    public partial class CommentItem : UserControl
    {
        /// <summary>
        /// Sınıfın kurucu metodu. Designer tarafından oluşturulan bileşenleri yükler.
        /// </summary>
        public CommentItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Yorum verilerini (Kullanıcı adı, yorum metni, tarih ve sticker görsel yolu) karta yükler ve kart boyutunu dinamik ayarlar.
        /// </summary>
        /// <param name="username">Yorumu yazan kullanıcının adı</param>
        /// <param name="commentText">Yorum içeriği metni</param>
        /// <param name="commentDate">Yorumun oluşturulduğu tarih</param>
        /// <param name="stickerPath">Seçilen sticker görselinin dosya adı (boş veya null olabilir)</param>
        public void SetCommentData(string username, string commentText, DateTime commentDate, string stickerPath)
        {
            // Kullanıcı adı, yorum metni ve tarih etiketlerini doldur
            lblUsername.Text = username;
            lblCommentText.Text = commentText;
            lblDate.Text = commentDate.ToString("g"); // Kısa tarih ve saat formatı (gg.AA.yyyy SS:dd)

            // Eğer yoruma eklenmiş bir sticker varsa görseli yükle ve boyutu büyüt
            if (!string.IsNullOrEmpty(stickerPath))
            {
                pbSticker.Visible = true;
                // Sticker görselinin çalışma dizinindeki (bin/Debug/net8.0-windows/Stickers) tam yolunu hesapla
                string fullPath = System.IO.Path.Combine(Application.StartupPath, "Stickers", stickerPath);
                
                if (System.IO.File.Exists(fullPath))
                {
                    // Dosya mevcutsa görseli yükle
                    pbSticker.Image = Image.FromFile(fullPath);
                }
                else
                {
                    // Eğer görsel dosyası bulunamazsa varsayılan mavi tema rengini dolgu olarak kullan
                    pbSticker.FillColor = Color.FromArgb(94, 148, 255);
                }
                
                // Sticker görseli olduğu için kart yüksekliğini 150px olarak büyüt
                this.Height = 150;
            }
            else
            {
                // Sticker yoksa PictureBox'ı gizle ve dikey alandan tasarruf etmek için yüksekliği 80px yap
                pbSticker.Visible = false;
                this.Height = 80;
            }
        }
    }
}
