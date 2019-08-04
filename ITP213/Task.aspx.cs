using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITP213.DAL;
using Blockchain_Text;
using Newtonsoft.Json;

namespace ITP213
{
    public partial class Task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_expire_Click(object sender, EventArgs e)
        {
            SecurityDAO securityMg = new SecurityDAO();
            securityMg.check_accounts_expired();

        }

        protected void btn_forceUpload_Click(object sender, EventArgs e)
        {
            //P2PClient.Connect("ws://127.0.0.1:6000/Blockchain");
            BlockchainManagerDAO bcManager = new BlockchainManagerDAO();
            string LogtoUpload = bcManager.GetDailyBlock();
            string adminLogtoUpload = bcManager.GetAdminBlock();
            Program.PhillyCoin.AddBlock(new Block(DateTime.Now, null, LogtoUpload));
            Program.PhillyCoin.AddBlock(new Block(DateTime.Now, null, adminLogtoUpload));
            P2PClient.Send(JsonConvert.SerializeObject(Program.PhillyCoin));
        }

        protected void btn_displayBC_Click(object sender, EventArgs e)
        {
            string bcJSON = JsonConvert.SerializeObject(Program.PhillyCoin, Formatting.Indented);
            BC_Panel.Visible = true;
            lb_BC.Text = bcJSON;
            
        }
    }
}