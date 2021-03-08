using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Form2 : Form
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Tasks API .NET Quickstart";
        Label labelInfo = new Label();
        Font myFont;
        public Form2()
        {
            InitializeComponent();
            this.Width = 1080;
            this.Height = 1920;
            LoadFont();
            label1.Font = myFont;
            labelInfo.Font = label1.Font;
            LabelAdd();
            Init();
        }

        private void Init()
        {
            if(IsOnline())
               GoogleApi();
        }

        /// <summary>
        /// Проверяем подключение к интерну
        /// </summary>
        /// <returns>true если есть подключение и false если нет</returns>
        static bool IsOnline()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void GoogleApi()
        {
            BoardSchedule boardSchedule = new BoardSchedule();
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Tasks API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            #region Ключи
            // Главная - coppvrn@gmail.com
            // 201 - cocevnmr8qbblpgf865m02r4co@group.calendar.google.com
            // 202 - fnimt6s7hdjsqk7oocfl18io1o@group.calendar.google.com
            // 212 ауд-трансф 1 - mmim1oo225jfs1q6ff4nf41a58@group.calendar.google.com
            // 214 кабинет - 42dmv6sajckd9k72u0sglno2eo@group.calendar.google.com
            // 215 тренинговая - lvgaocr3nm6idu8477q1gv4iks@group.calendar.google.com
            // 401 ауд - трансф 2 - ro0201a3ijf3iidagrdt2mrs50@group.calendar.google.com
            // 402 ауд-трансф 3 - lhn0ad0h2v4blirk4014064n10@group.calendar.google.com
            // 404 ЗПД скрипториум - phok3bjbj7diuvd877ed5it4co@group.calendar.google.com
            // 405 ЗПД - hqj7acgk44ag4evnfoej46f56s@group.calendar.google.com
            // Jalinga - 5mr1852dfh274m4s5h98agnhb4@group.calendar.google.com
            // Коворкинг - nacn2qrifmo6eqfigpe9o166ac@group.calendar.google.com
            // Лекторий - ik999trodh6970fomi2k52t79c@group.calendar.google.com
            #endregion
            List<string> calendarId = new List<string>
            {
                "coppvrn@gmail.com",
                "cocevnmr8qbblpgf865m02r4co@group.calendar.google.com",
                "fnimt6s7hdjsqk7oocfl18io1o@group.calendar.google.com",
                "mmim1oo225jfs1q6ff4nf41a58@group.calendar.google.com",
                "42dmv6sajckd9k72u0sglno2eo@group.calendar.google.com",
                "lvgaocr3nm6idu8477q1gv4iks@group.calendar.google.com",
                "ro0201a3ijf3iidagrdt2mrs50@group.calendar.google.com",
                "lhn0ad0h2v4blirk4014064n10@group.calendar.google.com",
                "phok3bjbj7diuvd877ed5it4co@group.calendar.google.com",
                "hqj7acgk44ag4evnfoej46f56s@group.calendar.google.com",
                "5mr1852dfh274m4s5h98agnhb4@group.calendar.google.com",
                "nacn2qrifmo6eqfigpe9o166ac@group.calendar.google.com",
                "ik999trodh6970fomi2k52t79c@group.calendar.google.com"

            };
            foreach (var calend in calendarId)
            {
                EventsResource.ListRequest request = service.Events.List(calend);
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                Events events = request.Execute();
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        Schedule schedule;
                        if (eventItem.Start.DateTime == null)
                        {
                            continue;
                        }
                        else
                        {
                            schedule = new Schedule(eventItem.Start.DateTime.Value, eventItem.End.DateTime.Value,
                                                             eventItem.Summary, eventItem.Description, eventItem.Organizer.DisplayName);
                        }
                        boardSchedule.AddShedules(schedule);
                    }
                }
            }
            labelInfo.Text = "";
            boardSchedule.PrintCount(labelInfo);
        }

        private void LabelAdd()
        {
            labelInfo.Location = new System.Drawing.Point(40,230);
            labelInfo.AutoSize = false;
            labelInfo.Size = new System.Drawing.Size(1000, 1600);
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            labelInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#52565e");
            this.Controls.Add(labelInfo);
        }

        private void LoadFont()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile("Nova.ttf");
            myFont = new Font(fontCollection.Families[0], 18, FontStyle.Bold);
        }

        private void labelExit1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public class Schedule
        {
            public DateTime DataEventStart;
            public DateTime DataEventEnd;
            public string TitleEvent;
            public string DescriptionEvent;
            public string DisplayNameEvent;

            public Schedule(DateTime dataEventStart, DateTime dataEventEnd, string titleEvent,
                            string descriptionEvent, string displayNameEvent)
            {
                DataEventStart = dataEventStart;
                DataEventEnd = dataEventEnd;
                TitleEvent = titleEvent;
                DescriptionEvent = descriptionEvent;
                DisplayNameEvent = displayNameEvent;
            }
            public Schedule(DateTime dataEvent, string titleEvent,
                            string descriptionEvent, string displayNameEvent)
            {
                DataEventStart = dataEvent;
                //DataEventEnd = dataEventEnd;
                TitleEvent = titleEvent;
                DescriptionEvent = descriptionEvent;
                DisplayNameEvent = displayNameEvent;
            }

            public void Print(Label labelInfo)
            {

                labelInfo.Text += ($"Дата: {DataEventStart.Date.Day.ToString().PadLeft(2, '0')}." +
                                $"{DataEventStart.Date.Month.ToString().PadLeft(2, '0')}.{DataEventStart.Date.Year}\n" +
              $"Место проведения: {DisplayNameEvent}\n" +
                      $"Название: {TitleEvent}\n" +
                      $"Описание: {DescriptionEvent}\n" +
                         $"Время: {DataEventStart.Hour.ToString().PadLeft(2, '0')}" +
                               $":{DataEventStart.Minute.ToString().PadLeft(2, '0')} - " +
                                $"{DataEventEnd.Hour.ToString().PadLeft(2, '0')}:" +
                                $"{DataEventEnd.Minute.ToString().PadLeft(2, '0')}" + $"\n\n\n");
            }
        }
        public class BoardSchedule
        {
            List<Schedule> SchedulesList;

            public BoardSchedule()
            {
                SchedulesList = new List<Schedule>();
            }

            public void AddShedules(Schedule schedule)
            {
                SchedulesList.Add(schedule);
            }
            public void PrintCount(Label labelInfo)
            {
                DateTime dt = DateTime.Now;
                double daySchedule = dt.Day;
                int countDay = 10;
                for (int i = 0; i < countDay; i++)
                {
                    foreach (var s in SchedulesList)
                    {
                        if (s.DataEventStart.Day == daySchedule)
                            s.Print(labelInfo);
                    }
                    daySchedule = dt.AddDays(i + 1).Day;
                }

            }
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void GetEvents_Tick(object sender, EventArgs e)
        {
            if(IsOnline())
              GoogleApi();
        }
    }
}
    
