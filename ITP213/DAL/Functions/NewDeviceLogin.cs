using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace ITP213.DAL.Peishan_Function
{
    public class NewDeviceLogin
    {
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) // only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }

            }
            return sMacAddress;
        }

        public static string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        public static string IPRequestHelper(string url)
        {
            HttpWebRequest objrequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objresponse = (HttpWebResponse)objrequest.GetResponse();
            StreamReader responsereader = new StreamReader(objresponse.GetResponseStream());
            string responseread = responsereader.ReadToEnd();
            responsereader.Close();
            responsereader.Dispose();
            return responseread;
        }

        public static string GetCountrybyip()
        {
            string ipaddress = getExternalIp();
            string strreturnvalue = string.Empty;
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipaddress);
            XmlDocument ipInfixml = new XmlDocument();
            ipInfixml.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfixml.GetElementsByTagName("query");
            string returnvalue = responseXML.Item(0).ChildNodes[2].InnerText.ToString();

            return returnvalue;

        }

        public static Tuple<Boolean, string> checkDeviceLogin(string email)
        {
            string lblError = string.Empty;
            Boolean verdict = false;

            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(email);
            string UUID = loginObj.UUID; // retriving UUID from loginObj

            string country = GetCountrybyip();
            string macAddress = GetMACAddress();
            string publicIP = getExternalIp();

            Login obj = getMacAddressFromNewDeviceLogin(UUID, macAddress);
            if (obj != null) // macAddress exist in this user
            {
                // update
                int result = updateIntoNewDeviceLoginTable(UUID, GetMACAddress(), publicIP);
                if (result == 1)
                {
                    verdict = true;
                }
                else
                {
                    lblError = "Sorry! An error has occurred! Please try again later!";
                }
            }
            else
            {
                // insert
                int result = DAL.RegisterDAO.insertIntoNewDeviceLogin(GetMACAddress(), country, getExternalIp(), DateTime.Now, UUID);

                if (result == 1)
                {
                    // ***** send an Email to alert user
                    
                    string title = "Security Alert - New Device Login";

                    var htmlContent = "Hi, " + loginObj.name + ". An unknown device login is found. Country: "+ country + ", IP address: "+ getExternalIp()+".";

                    verdict = true;
                    Functions.Validations.EmailAndPhoneValidation.Execute(loginObj.name, email, "Hi", title, htmlContent);
                    //=========================================
                    verdict = true;
                }
                else
                {
                    lblError = "Sorry! An error has occurred! Please try again later!";
                }
            }

            return Tuple.Create(verdict, lblError);
           
        }

        public static Login getMacAddressFromNewDeviceLogin(string UUID, string macAddress) // to check if exiiting macAddress Exist, if it did, return macAddress to update the Last Login column
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            string sqlString = "SELECT macAddress FROM newDeviceLogin WHERE UUID = @UUID AND macAddress=@macAddress";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            da.SelectCommand.Parameters.AddWithValue("@macAddress", macAddress);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.macAddress = row["macAddress"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public static int updateIntoNewDeviceLoginTable(string UUID, string macAddress,string PublicIPAddress)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET mobile = '81234567', dateOfBirth='01-31-2000'
            FROM account
            INNER JOIN student
            ON account.UUID = student.UUID
            WHERE adminNo='171846z';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE newDeviceLogin ");
            sqlStr.AppendLine("SET LastLogin=GetDate(), PublicIPAddress=@PublicIPAddress");
            sqlStr.AppendLine("WHERE macAddress=@macAddress and UUID=@UUID");


            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@macAddress",macAddress);
            cmd.Parameters.AddWithValue("@PublicIPAddress", PublicIPAddress);

            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}