using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // first time coming to page
            {
                buildBarChart("2018 - Thailand Trip", "Study Trip");
                //buildPieChart("All");
                //buildHorizontalChart("All");
                //buildLineChart("All", "All");
            }
        }

        private void buildBarChart(string nameOfTrip, string typeOfTrip)
        {
            String myConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            SqlConnection myConn = new SqlConnection(myConnect);

            DataSet ds = new DataSet();

            //String strSQL = "SELECT tripCost, country, tripType, tripName FROM overseasTrip";
            String strSQL = "SELECT tripCost, country, tripType, tripName, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType FROM overseasTrip";
            // Sorry, I named it as tripCost instead of cost.
            /*if (!country.Equals("All"))
            {
                //strSQL += "where location = @paraCountry ";
            }

            if (!type.Equals("All") && (!country.Equals("All")))
            {
                //strSQL += "and triptype = @paratype ";

            }*/
            //strSQL += "GROUP BY country";

            SqlDataAdapter da = new SqlDataAdapter(strSQL.ToString(), myConn);

            /*if (!country.Equals("All"))
            {
                //da.SelectCommand.Parameters.AddWithValue("@paraCountry", country);
            }

            if (!type.Equals("All"))
            {
                //da.SelectCommand.Parameters.AddWithValue("@paratype", type);
            }*/

            // NOT SURE ABOUT HOW THE BELOW CODE WORKS
            /*if (!dateStart.Equals(""))
            {
                //da.SelectCommand.Parameters.AddWithValue("@paradateStart", dateStart);
            }

            if (!dateEnd.Equals(""))
            {
                //da.SelectCommand.Parameters.AddWithValue("@paradateEnd", dateEnd);
            }*/

            da.Fill(ds, "resultTable");

            Chart2.DataSource = ds;
            Chart2.DataBind();
            CountryDropDown.SelectedItem.ToString();
        }

       
    }
}