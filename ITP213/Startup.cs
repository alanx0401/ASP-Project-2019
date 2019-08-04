using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using System.Collections.Generic;
using ITP213.DAL;
using Blockchain_Text;
using System.Diagnostics;
using Newtonsoft.Json;



[assembly: OwinStartup(typeof(ITP213.Startup))]

namespace ITP213
{
    public class Startup
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

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();

            Blockchain AuditLogBC = new Blockchain();
            BlockchainManagerDAO bcManager = new BlockchainManagerDAO();
            Debug.Write("Test");
            //BackgroundJob.Enqueue(() => AuditLogBC.AddBlock(new Block(DateTime.Now, null, bcManager.GetDailyBlock())));
            //RecurringJob.AddOrUpdate(() => AuditLogBC.AddBlock(new Block(DateTime.Now, null,bcManager.GetDailyBlock())), Cron.Daily);
            RecurringJob.AddOrUpdate(() => UpdateDailyEventLogtoBlockchain(), Cron.Daily);
            BackgroundJob.Enqueue(() => UpdateDailyEventLogtoBlockchain());
        }

        public void UpdateDailyEventLogtoBlockchain()
        {
            P2PClient Client = new P2PClient();
            BlockchainManagerDAO bcManager = new BlockchainManagerDAO();
            string LogtoUpload = bcManager.GetDailyBlock();


            Program.PhillyCoin.AddBlock(new Block(DateTime.Now, null, LogtoUpload));
            P2PClient.Connect("127.0.0.1:6000/Blockchain");
            //Client.Send("127.0.0.1:6000/Blockchain",JsonConvert.SerializeObject(Program.PhillyCoin));


        }
    }
}
