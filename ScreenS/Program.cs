using System;
using System.Threading;
using System.Windows.Forms;

namespace ScreenS
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool kontrol;

            Mutex mutex = new Mutex(true, "Program", out kontrol);
            if (kontrol == false)
            {
                MessageBox.Show("Program zaten çalışıyor !", "WallpaperChanger v1.2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            GC.KeepAlive(mutex);
        }
    }
}
