using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using Blockchain_Text;
using ITP213.DAL;
using Newtonsoft.Json;
using WebSocketSharp;

namespace ITP213
{
    public class Global : System.Web.HttpApplication
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\ITP213.mdf;Integrated Security=True", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }
        protected void Application_Start(object sender, EventArgs e)
        {

            HangfireAspNet.Use(GetHangfireServers);
            BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));

            Program.PhillyCoin.InitializeChain();
            P2PClient.Connect("ws://127.0.0.1:6000/Blockchain");

            Blockchain AuditLogBC = new Blockchain();
            BlockchainManagerDAO bcManager = new BlockchainManagerDAO();
            SecurityDAO secMng = new SecurityDAO();
            //BackgroundJob.Enqueue(() => AuditLogBC.AddBlock(new Block(DateTime.Now, null, bcManager.GetDailyBlock())));
            //RecurringJob.AddOrUpdate(() => AuditLogBC.AddBlock(new Block(DateTime.Now, null,bcManager.GetDailyBlock())), Cron.Daily);
            RecurringJob.AddOrUpdate(() => UpdateDailyEventLogtoBlockchain(), Cron.Daily);
            RecurringJob.AddOrUpdate(() => secMng.check_accounts_expired(), Cron.Yearly);
            BackgroundJob.Enqueue(() => UpdateDailyEventLogtoBlockchain());
        }

        public void UpdateDailyEventLogtoBlockchain()
        {
            BlockchainManagerDAO bcManager = new BlockchainManagerDAO();
            string LogtoUpload = bcManager.GetDailyBlock();
            Program.PhillyCoin.AddBlock(new Block(DateTime.Now, null, LogtoUpload));
            //Client.Send("ws://127.0.0.1:6000/Blockchain", JsonConvert.SerializeObject(Program.PhillyCoin));

        }

            protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}