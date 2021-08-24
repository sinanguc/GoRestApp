using GorestApp.WinService.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GorestApp.WinService
{
    public partial class GorestAppSrv : ServiceBase
    {
        private static string AppName = ConfigurationSettings.AppSettings["AppName"].ToString();
        private static string GorestAppWebAPIUrl = ConfigurationSettings.AppSettings["GorestAppWebAPIUrl"].ToString();
        public GorestAppSrv()
        {
            InitializeComponent();
        }

        Timer _timerGetLatestRecordFromGorestService = new Timer();

        protected override void OnStart(string[] args)
        {
            var culture = CultureInfo.CreateSpecificCulture("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = culture;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            _timerGetLatestRecordFromGorestService.Interval = 1000 * 60 * 60 * 24; // Hergün bir defa başlar
            _timerGetLatestRecordFromGorestService.Enabled = true;
            _timerGetLatestRecordFromGorestService.Elapsed += GetLatestRecordFromGorestService_Elapsed;
            _timerGetLatestRecordFromGorestService.Start();

        }

        void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            if (!EventLog.SourceExists(AppName))
                EventLog.CreateEventSource(AppName, AppName);

            Exception ex = (Exception)args.ExceptionObject;
            EventLog.WriteEntry(AppName, ex.Message, EventLogEntryType.Error);
        }

        void GetLatestRecordFromGorestService_Elapsed(object sender, ElapsedEventArgs e)
        {
            PullData();
        }

        public void PullData()
        {
            var client = new RestClient(GorestAppWebAPIUrl);
            var request = new RestRequest("api/User", Method.GET);
            var result = client.Get<GenericResult>(request);
            if (result.Data == null || !(bool)result.Data.Data)
                throw new Exception("GorestApp.WebAPI: user store dan alınan veriler işlenemedi!" + result.StatusDescription);
        }

        protected override void OnStop()
        {
        }
    }
}
