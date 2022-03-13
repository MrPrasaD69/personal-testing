using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class FormDisplay : System.Web.UI.Page
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        string strcon = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;

        string id;
        string FormID;
        protected void Page_Load()
        {
            //SqlConnection con = new SqlConnection(strcon);
            //SqlCommand cmd = new SqlCommand("SELECT FormID ,Label FROM FormFields", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();
            //con.Open();


            //int RowCount = ds.Tables[0].Rows.Count;
            //string TableHead = "";
            //string TableBody = "";
            //for (int z = 0; z < RowCount; z++)
            //{
            //    TableHead += @"<th><td>" + ds.Tables[0].["Label"] + "</td></th>";
            //}

            //divTable.InnerText = @"<table id='example' class='display' style='width:100%'>"
            //       + "<thead>" + TableHead + "</thead>" +
            //       "<tbody>" + TableBody + @"</tbody></table>";

            //using (SqlDataReader sdr = cmd.ExecuteReader())
            //{

            //}

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("SELECT FormID ,Label FROM FormFields where FormId=" + Request.QueryString["id"].ToString(), con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
                     
            string html = CreateNewDataTable(ds.Tables[0], "FormID");
            divTable.InnerHtml = html;

        }
        public string CreateNewDataTable(DataTable dt, string rowid)

        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table style='width: 100%;' id='example' class='table table-hover table - striped table - bordered dataTable dtr-inline' role='grid' >");

            //html.Append("<div class='container'><div class='table-responsive'><table id='productTable' class='table  table-striped table-condensed table-hovered ' cellspacing='0' width='100%'>");
            //Building the Header row.

            html.Append("<thead><tr>");
            int clm = 0;

            html.Append("</tr></thead><tbody>");
            //Building the Data rows.
            int noRows = 0;
            html.Append("<tr>");

            foreach (DataRow row in dt.Rows)
            {
                clm = 0;
                foreach (DataColumn column in dt.Columns)
                {                   
                    if (clm != 0)
                    {
                        html.Append("<td>");
                        if (column.ColumnName.Trim() == "Label")
                            html.Append(row[column.ColumnName].ToString().Replace("''", "'"));
                        else
                            html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    clm++;
                }
                //html.Append("<td><input type='button'  id='editlink' value='Edit'  onclick=editData('" + dt.Rows[noRows][rowid] + "')></td><td><input type='button'  id='dellink' value='Delete' onclick=delData('" + dt.Rows[noRows][rowid] + "')></td><td><input type='button'  id='viewlink'  value='View' onclick=viewData('" + dt.Rows[noRows][rowid] + "')></td></tr>");
                noRows++;
            }
            html.Append("</tr>");
 html.Append("<tr>");

            foreach (DataRow row in dt.Rows)
            {
                clm = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    int formId = Convert.ToInt32(dt.Rows[0]["FormId"]);
                   
                    if (clm != 0)
                    {
                        html.Append("<td>");
                        if (column.ColumnName.Trim() == "Label")
                            html.Append("<input ID='" + formId + "' type='text' name='txtbox1' runat='server' />");
                        else
                            html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    clm++;
                }
                //html.Append("<td><input type='button'  id='editlink' value='Edit'  onclick=editData('" + dt.Rows[noRows][rowid] + "')></td><td><input type='button'  id='dellink' value='Delete' onclick=delData('" + dt.Rows[noRows][rowid] + "')></td><td><input type='button'  id='viewlink'  value='View' onclick=viewData('" + dt.Rows[noRows][rowid] + "')></td></tr>");
                noRows++;
            }
            html.Append("</tr>");
            //Table end.
            html.Append("</tbody></table>");
            return html.ToString();
        }


     
        //test function to fetch column fields from database and display in gridview
        protected void datadisplay()
        {
            //SqlConnection con = new SqlConnection(strcon);
            //SqlCommand cmd = new SqlCommand("SELECT FormID ,Label FROM FormFields",con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();

        }




    }
}