using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenS
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        public Form1()
        {
            InitializeComponent();
        }

        public void WallpapersStrings(String yol)
        {
            SystemParametersInfo(0x14, 0, yol, 0x01 | 0x02);
        }
        private void button1_Click(object sender, EventArgs e)
        {          
            OpenFileDialog wallPapers = new OpenFileDialog();
            wallPapers.Filter = "Resim Dosyası |*.jpg;*.jfif;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            wallPapers.ShowDialog();
            if (wallPapers.FileName == "")
            {
                MessageBox.Show("BOŞ", "WallpaperChanger v1.2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string FileString = wallPapers.FileName;
                Properties.Settings.Default.Path1 = FileString;
                Properties.Settings.Default.Save();
                pictureBox1.Image = Image.FromFile(Properties.Settings.Default.Path1);
                WpDegis();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog wp = new OpenFileDialog();
            wp.Filter = "Resim Dosyası |*.jpg;*.jfif;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            wp.ShowDialog();
            if (wp.FileName == "")
            {
                MessageBox.Show("BOŞ", "WallpaperChanger v1.2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string FileString1 = wp.FileName;
                Properties.Settings.Default.Path2 = FileString1;
                Properties.Settings.Default.Save();
                pictureBox2.Image = Image.FromFile(Properties.Settings.Default.Path2);
                WpDegis();
            }
        }

        public void WpDegis()
        {
            if (Properties.Settings.Default.Path1 != "" && Properties.Settings.Default.Path2 != "")
            {
                DateTime dei = new DateTime();
                DateTime dei1 = new DateTime();
                DateTime dei2 = new DateTime();
                DateTime dei3 = new DateTime();
                DateTime result = new DateTime();
                dei = Convert.ToDateTime(dateTimePicker1.Text);
                dei1 = Convert.ToDateTime(dateTimePicker2.Text);
                dei2 = Convert.ToDateTime(dateTimePicker3.Text);
                dei3 = Convert.ToDateTime(dateTimePicker4.Text);
                result = DateTime.Today;
                if (dei1.Hour == result.Hour || dei1.Hour <= dei.Hour)
                {
                    if (dei1 <= DateTime.Now ) { dei1 = dei1.AddDays(1); }
                    else { dei1 = dei1.AddDays(1); dei = dei.AddDays(-1); }                  
                }
                else { dei1 = Convert.ToDateTime(dateTimePicker2.Text); dei = Convert.ToDateTime(dateTimePicker1.Text); }
                if (dei <= DateTime.Now && dei1 >= DateTime.Now)
                {
                    WallpapersStrings(Properties.Settings.Default.Path1);
                    button1.FlatAppearance.BorderColor = Color.Yellow;
                    button1.FlatAppearance.BorderSize = 2;
                    button2.FlatAppearance.BorderSize = 0;
                }
                else if (dei2.AddDays(-1) <= DateTime.Now && dei3.AddDays(1) >= DateTime.Now)
                {
                    WallpapersStrings(Properties.Settings.Default.Path2);
                    button2.FlatAppearance.BorderColor = Color.Yellow;
                    button1.FlatAppearance.BorderSize = 0;
                    button2.FlatAppearance.BorderSize = 2;
                }
                else { MessageBox.Show("tarih şuanki saat dışında"); }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.Başlangıç;
            WpDegis();
            try
            {
                pictureBox1.Image = Image.FromFile(Properties.Settings.Default.Path1);
                pictureBox2.Image = Image.FromFile(Properties.Settings.Default.Path2);
            }
            catch (Exception)
            { }
            Properties.Settings.Default.tarih1 = dateTimePicker1.Value;
            Properties.Settings.Default.tarih2 = dateTimePicker2.Value;
            dateTimePicker3.Text = dateTimePicker2.Text;
            dateTimePicker4.Text = dateTimePicker1.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker2.Text == dateTimePicker1.Text)
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddMinutes(1);
                }
                dateTimePicker4.Value = dateTimePicker1.Value;
                Properties.Settings.Default.tarih1 = dateTimePicker1.Value;
                Properties.Settings.Default.Save();
                WpDegis();
            }
            catch (Exception)
            {
                MessageBox.Show("LÜTFEN GEÇERLİ BİR TARİH GİRİN...");
            }      
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker2.Text == dateTimePicker1.Text)
                {
                    dateTimePicker1.Value = dateTimePicker1.Value.AddMinutes(-1);
                }
                dateTimePicker3.Value = dateTimePicker2.Value;
                Properties.Settings.Default.tarih2 = dateTimePicker2.Value;
                Properties.Settings.Default.Save();
                WpDegis();
            }
            catch (Exception)
            {
                MessageBox.Show("LÜTFEN GEÇERLİ BİR TARİH GİRİN...");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            WpDegis();
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        public void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form mesj = new Form();
            mesj.StartPosition = FormStartPosition.Manual;
            mesj.Location = new Point(Cursor.Position.X, Cursor.Position.Y - 100);
            mesj.Size = new Size(210, 100);
            mesj.Text = "WallpaperChanger";
            mesj.FormBorderStyle = FormBorderStyle.FixedDialog;
            mesj.MaximizeBox = false;
            mesj.MinimizeBox = false;
            Label label = new Label();
            label.Top = 10;
            label.Left = 10;
            label.Width = 270;
            label.Height = 30;
            label.BackColor = Color.Transparent;
            label.Text = "WallpaperChanger © v1.2 - .02.2021 \n By İbrahim Ağlar";
            LinkLabel link = new LinkLabel();
            link.Text = "Hata Bildir";
            link.Top = 40;
            link.Left = 130;
            link.Width = 100;
            mesj.Controls.Add(label);
            mesj.Controls.Add(link);
            link.Click += new EventHandler(link_Click);
            mesj.Show();
        }
        void link_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.wallpaperchanger.com");
            //MessageBox.Show("ibrahimaglar@hotmail.com", "WallpaperChanger v1.2", MessageBoxButtons.OK);   
        }

        private void programıGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try { açılış(); }
            catch (Exception) { MessageBox.Show("Bir hata oluştu. \n Lütfen tekrar deneyin...", "WallpaperChanger v1.2", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        public void açılış()
        {
            if (checkBox1.Checked == true)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("WallpaperChanger", "\"" + Application.ExecutablePath + "\"");
                Properties.Settings.Default.Başlangıç = true;
                Properties.Settings.Default.Save();
            }
            else if (checkBox1.Checked == false)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("WallpaperChanger");
                Properties.Settings.Default.Başlangıç = false;
                Properties.Settings.Default.Save();
            }
        }
        int ss = 0;
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (ss == 0)
            {
                this.Hide();
                ss++;
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.location = Location;
            Properties.Settings.Default.Save();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            Form x = new Form();
            x.Width = 489;
            x.Height = 282;
            x.Text = "WALLPAPER-1";
            x.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            x.MinimizeBox = false;
            x.StartPosition = FormStartPosition.Manual;
            x.Location = new Point(Properties.Settings.Default.location.X - 25, Properties.Settings.Default.location.Y - 25);
            try
            {
                x.BackgroundImage = Image.FromFile(Properties.Settings.Default.Path1);
                x.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception)
            { }
            x.Click += new EventHandler(x_Click);
            x.Cursor = System.Windows.Forms.Cursors.Hand;
            x.Show();
        }
        void x_Click(object sender, EventArgs e)
        {
            Form x = Form.ActiveForm;
            x.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form y = new Form();
            y.Text = "WALLPAPER-2";
            y.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            y.MinimizeBox = false;
            y.Width = 489;
            y.Height = 282;
            y.StartPosition = FormStartPosition.Manual;
            y.Location = new Point(Properties.Settings.Default.location.X - 25, Properties.Settings.Default.location.Y - 25);
            try
            {
                y.BackgroundImage = Image.FromFile(Properties.Settings.Default.Path2);
                y.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception)
            { }
            y.Click += new EventHandler(y_Click);
            y.Cursor = System.Windows.Forms.Cursors.Hand;
            y.Show();
        }
        void y_Click(object sender, EventArgs e)
        {
            Form y = Form.ActiveForm;
            y.Hide();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Select();
        }
    }
}