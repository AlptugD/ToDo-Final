using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo_Final // Eğer senin projenin namespace'i farklıysa burayı düzeltmeyi unutma
{
    public partial class ThemePickerForm : Form
    {
        // Dashboard formunun okuyabilmesi için seçilen rengi tutan özellik
        public Color SelectedColor { get; private set; }

        public ThemePickerForm()
        {
            InitializeComponent();
        }

        // Bütün renk butonlarının kullanacağı ORTAK tıklama metodu
        private void ColorButton_Click(object sender, EventArgs e)
        {
            // Pattern Matching: Eğer tıklanan 'sender' bir Guna2CircleButton ise, 
            // onu anında 'clickedButton' adında bir değişkene ata ve parantez içine gir.
            if (sender is Guna.UI2.WinForms.Guna2CircleButton clickedButton)
            {
                // Tıklanan butonun kendi rengini (FillColor) hafızaya al
                SelectedColor = clickedButton.FillColor;

                // İşlem başarılı sinyali verip bu küçük pencereyi kapat
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Formun arka planına veya kapatma ikonuna basılırsa iptal etme metodu (İsteğe bağlı)
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}