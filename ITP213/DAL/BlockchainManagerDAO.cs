using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Blockchain_Text;


namespace ITP213.DAL
{
    public class BlockchainManagerDAO
    {
        

        public string hashEventLog(string tobehashed)
        {
            SHA512Managed hashing = new SHA512Managed();
            byte[] hashingbyte = hashing.ComputeHash(Encoding.UTF8.GetBytes(tobehashed));
            return Convert.ToBase64String(hashingbyte);
        }

        public string GetDailyBlock()
        {
            SecurityEventLog SELog = new SecurityEventLog();
            List<SecurityEventLog> SELogList = SELog.GetSecurityEventLogsByDate(DateTime.Now, DateTime.Now);
            string SELogtoJSON = JsonConvert.SerializeObject(SELogList);
            string SELogtoJSONHashed = hashEventLog(SELogtoJSON);
            

            return SELogtoJSON + " Hash:" + SELogtoJSONHashed;
        }

        public string GetAdminBlock()
        {
            SecurityEventLog SELog = new SecurityEventLog();
            List<SecurityEventLog> SELogList = SELog.auditLog("admin");
            string SELogtoJSON = JsonConvert.SerializeObject(SELogList);
            string SELogtoJSONHashed = hashEventLog(SELogtoJSON);
            return SELogtoJSON + " Hash:" + SELogtoJSONHashed;
        }
    }
}