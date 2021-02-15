using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Test
{
   
    public partial class Form1 : Form
    {
        enum Status
        {
            Main,
            Gallery
        }

        /// <summary>
        /// Промежуток времени между фотографиями
        /// </summary>
        int timeBetweenfoto = int.Parse(ConfigurationManager.AppSettings["timeBetweenfoto"]);
        int temp;

        public Label[] LinkMainForm = new Label[6];
        public Label[] LinkNavigation = new Label[4];
        public Label[] LinkSchedule = new Label[4];
        public Label[] LinkGallery = new Label[4];
        public Label TimeInfo;
        public PictureBox pictureBox;
        Gallery gallery;
        MediaPlayer mediaPlayer;

        #region Параметры объектов(позиция, размеры)
        string[] SizeMainLink = new string[] { "573 96", "573 100", "575 100", "575 100", "386 386", "386 386" };
        string[] LocationMainLink = new string[] {"254 198", "254 362", "254 526", "254 692", "117 1455", "582 1455"};

        string[] SizeNavigationLink = new string[] {"124 84","124 106","386 386","386 386"};
        string[] LocationNavigationLink = new string[] { "57 45", "883 31", "117 1455", "582 1455"};

        string[] SizeScheduleLink = new string[] { "124 84", "124 106", "124 84", "124 84" };
        string[] LocationScheduleLink = new string[] { "57 45", "883 31", "57 1800", "884 1800" };

        string[] SizeGalleryLink = new string[] { "124 84", "124 106", "124 84", "124 84" };
        string[] LocationGalleryLink = new string[] { "57 45", "883 31", "57 1800", "884 1800" };
        #endregion

        public Form1()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            this.Width = 1080;
            this.Height = 1920;
            MainScreen();
        }      

        private void MainScreen()
        {
            timerInfo.Enabled = true;
            this.BackgroundImage = Image.FromFile("Image\\ScreenMain.png");
            for (int i = 0; i < LinkMainForm.Length; i++)
            {
                LinkMainForm[i] = new Label();
                LinkMainForm[i].Name = $"Label{i}";               
                LinkMainForm[i].Size = new System.Drawing.Size(int.Parse(SizeMainLink[i].Split()[0]), 
                                                               int.Parse(SizeMainLink[i].Split()[1]));
                LinkMainForm[i].Location = new Point(int.Parse(LocationMainLink[i].Split()[0]), 
                                                     int.Parse(LocationMainLink[i].Split()[1]));
                LinkMainForm[i].BackColor = System.Drawing.Color.Transparent;
                Controls.Add(LinkMainForm[i]);            
            }
            TimeInfo = new Label();
            TimeInfo.AutoSize = true;
            TimeInfo.Location = new Point(250, 1100);
            TimeInfo.ForeColor = System.Drawing.Color.Gray;
            TimeInfo.Font = new Font("Calibri", 36, FontStyle.Bold | FontStyle.Bold);
            TimeInfo.BackColor = System.Drawing.Color.Transparent;        
            
            Controls.Add(TimeInfo);

            LinkMainForm[0].MouseClick += NavigationClick;
            LinkMainForm[1].MouseClick += ScheduleClick;
            LinkMainForm[2].MouseClick += GalleryClick;
            LinkMainForm[3].MouseClick += COPPOnlineClick;
            LinkMainForm[4].MouseClick += InstagramClick;
            LinkMainForm[5].MouseClick += VkontakteClick;

        }
        private void NavigationScreen()
        {
            this.BackgroundImage = Image.FromFile("Image\\ScreenNavigator.png");
            for (int i = 0; i < LinkNavigation.Length; i++)
            {
                LinkNavigation[i] = new Label();
                LinkNavigation[i].Name = $"Label{i}";
                LinkNavigation[i].Size = new System.Drawing.Size(int.Parse(SizeNavigationLink[i].Split()[0]),
                                                               int.Parse(SizeNavigationLink[i].Split()[1]));
                LinkNavigation[i].Location = new Point(int.Parse(LocationNavigationLink[i].Split()[0]),
                                                     int.Parse(LocationNavigationLink[i].Split()[1]));
                LinkNavigation[i].BackColor = System.Drawing.Color.Transparent;
                Controls.Add(LinkNavigation[i]);
            }
            LinkNavigation[0].MouseClick += BackArrowClick;
            LinkNavigation[1].MouseClick += BackArrowClick;
            LinkNavigation[2].MouseClick += InstagramClick;
            LinkNavigation[3].MouseClick += COPPOnlineClick;
        }
        private void ScheduleScreen()
        {
            this.BackgroundImage = Image.FromFile("Image\\ScreenSchedule.png");
            for (int i = 0; i < LinkSchedule.Length; i++)
            {
                LinkSchedule[i] = new Label();
                LinkSchedule[i].Name = $"Label{i}";
                LinkSchedule[i].Size = new System.Drawing.Size(int.Parse(SizeScheduleLink[i].Split()[0]),
                                                               int.Parse(SizeScheduleLink[i].Split()[1]));
                LinkSchedule[i].Location = new Point(int.Parse(LocationScheduleLink[i].Split()[0]),
                                                     int.Parse(LocationScheduleLink[i].Split()[1]));
                LinkSchedule[i].BackColor = System.Drawing.Color.Transparent;
                Controls.Add(LinkSchedule[i]);
            }
            LinkSchedule[0].MouseClick += BackArrowScheduleClick;
            LinkSchedule[1].MouseClick += BackArrowScheduleClick;
        }
        private void GalleryScreen()
        {
            this.BackgroundImage = Image.FromFile("Image\\ScreenGallery.png");
            for (int i = 0; i < LinkGallery.Length; i++)
            {
                LinkGallery[i] = new Label();
                LinkGallery[i].Name = $"Label{i}";
                LinkGallery[i].Size = new System.Drawing.Size(int.Parse(SizeGalleryLink[i].Split()[0]),
                                                               int.Parse(SizeGalleryLink[i].Split()[1]));
                LinkGallery[i].Location = new Point(int.Parse(LocationGalleryLink[i].Split()[0]),
                                                     int.Parse(LocationGalleryLink[i].Split()[1]));
                LinkGallery[i].BackColor = System.Drawing.Color.Transparent;
                Controls.Add(LinkGallery[i]);
            }
            LinkGallery[0].MouseClick += BackArrowGalleryClick;
            LinkGallery[1].MouseClick += BackArrowGalleryClick;
            LinkGallery[2].MouseClick += BackArowDownLeft;
            LinkGallery[3].MouseClick += BackArowDownRight;

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(0, 600);
            pictureBox.Size = new System.Drawing.Size(1080, 725);          
            Controls.Add(pictureBox);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            gallery = new Gallery("Photo", pictureBox);
            timer1.Enabled = true;
            timer1.Interval = timeBetweenfoto;
        }
        public void PlaySound(string sound)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(sound, UriKind.Relative));
            mediaPlayer.Play();
        }
        private void BackArowDownRight(object sender, MouseEventArgs e)
        {
            gallery.PhotoShowRight();
        }

        private void BackArowDownLeft(object sender, MouseEventArgs e)
        {
            gallery.PhotoShowLeft();
        }

        private void BackArrowGalleryClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkGallery.Length; i++)
            {
                LinkGallery[i].Dispose();
            }
            pictureBox.Dispose();
            timer1.Enabled = false;
            MainScreen();
        }

        private void BackArrowScheduleClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkSchedule.Length; i++)
            {
                LinkSchedule[i].Dispose();
            }
            MainScreen();
        }

        //Нажатие верхней левой стрели и иконки домой
        private void BackArrowClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkNavigation.Length; i++)
            {
                LinkNavigation[i].Dispose();
            }
            MainScreen();
        }
        private void NavigationClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkMainForm.Length; i++)
            {
                LinkMainForm[i].Dispose();
            }
            TimeInfo.Dispose();
            timerInfo.Enabled = false;
            NavigationScreen();
        }
        private void ScheduleClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkMainForm.Length; i++)
            {
                LinkMainForm[i].Dispose();
            }
            TimeInfo.Dispose();
            timerInfo.Enabled = false;
            ScheduleScreen();

        }
        private void VkontakteClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            System.Diagnostics.Process.Start("https://vk.com/tcopp36"); 
        }
        private void InstagramClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            System.Diagnostics.Process.Start("https://www.instagram.com/copp_vrn");
        }

        private void COPPOnlineClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            System.Diagnostics.Process.Start("https://copp36.ru");
        }
        private void GalleryClick(object sender, MouseEventArgs e)
        {
            PlaySound("Sound\\Next.mp3");
            for (int i = 0; i < LinkMainForm.Length; i++)
            {
                LinkMainForm[i].Dispose();
            }
            TimeInfo.Dispose();
            timerInfo.Enabled = false;
            GalleryScreen();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Weather();
        }

            private void timer1_Tick(object sender, EventArgs e)
        {
            gallery.PhotoShowRight();
        }

        private void timerInfo_Tick(object sender, EventArgs e)
        {
              TimeInfo.Text = DateTime.Now.ToString("dd MMMM yyyyг.  HH:mm:ss" + "\n" + "          Воронеж: " +  temp.ToString() + " °C");
        }

        private async void Weather()
        {
            WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=Voronezh&APPID=2d224dbf5c19d51aaefeaeaf773e14dc");
            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";
            WebResponse response = await request.GetResponseAsync();
            string answer = string.Empty;
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();
            temp = int.Parse(answer.Substring(answer.IndexOf("temp\":") + 6, 3)) - 273;
        }
    }
}
