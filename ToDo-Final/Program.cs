using System;
using System.Windows.Forms;

namespace ToDo_Final
{
    /// <summary>
    /// Uygulamanın ana başlangıç noktasını içeren statik sınıf.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana giriş noktası (Main metodu).
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows Forms uygulamasının DPI farkındalığını, yazı tiplerini ve görsel stil ayarlarını başlatır
            ApplicationConfiguration.Initialize();
            
            // Uygulamayı başlatır ve ilk olarak Giriş Ekranını (LoginForm) açar
            Application.Run(new LoginForm());
        }
    }
}