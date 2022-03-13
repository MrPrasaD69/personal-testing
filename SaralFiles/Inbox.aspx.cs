using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class Inbox : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "1";
            if (!string.IsNullOrEmpty(Session["userID"] as string))
            {
                userID = Convert.ToInt32(Session["userID"].ToString());
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
            if (userID == 0)
            {
                Response.Redirect("/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                //ExportToExcel();
                //Request.QueryString["id"] = "1";
                GetDataTable();
                //fid.Value = Request.QueryString["formid"].ToString();

            }
        }
        //public void GetDataTable()
        //{
            
        //    lblOutput.Text += @"<table class='table table-bordered'><tbody>";
        //    lblOutput.Text += @"<tr><td>SrNo</td><td>Form Name</td><td>Sub title</td><td>Description</td><td>Action</td></tr>";
        //    using (SqlConnection myConnection = new SqlConnection(connectionString))
        //    {
        //        string oString = @"SELECT Forms.* FROM FormInbox LEFT JOIN Forms ON FormInbox.FormId = Forms.Id where FormInbox.UserId = " + userID + "";
        //        SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //        //oCmd.Parameters.AddWithValue("@id", id);
        //        //oCmd.Parameters.AddWithValue("@uid", userID);
        //        myConnection.Open();
        //        using (SqlDataReader row = oCmd.ExecuteReader())
        //        {
        //            int i = 1;
        //            while (row.Read())
        //            {
        //                lblOutput.Text += @"<tr>";
        //                lblOutput.Text += @"<td>" + i++ + "</td>";
        //                lblOutput.Text += @"<td>" + row["Title"].ToString() + "</td>";
        //                lblOutput.Text += @"<td>" + row["SubTitle"].ToString() + "</td>";
        //                lblOutput.Text += @"<td>" + row["Description"].ToString() + "</td>";
        //                lblOutput.Text += @"<td><a href='FormView?formid=" + row["Id"].ToString() + "' class='btn btn-primary'>View</a></td>";
        //                lblOutput.Text += @"</tr>";
        //            }
        //            myConnection.Close();
        //        }
        //    }
        //    lblOutput.Text += @"</ tbody></table>";

        //}

        protected void btnCreateRoute_Click(object sender, EventArgs e)
        {

        }


      
        public void GetDataTable()
        {

            lblOutput.Text += @"<table class='table table-bordered'><tbody>";
            lblOutput.Text += @"<tr><td>SrNo</td><td>Form Name</td><td>Sub title</td><td>Description</td><td>Action</td></tr>";
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = @"SELECT Forms.*, FormInbox.AuthenticationStatus FROM FormInbox LEFT JOIN Forms ON FormInbox.FormId = Forms.Id where FormInbox.UserId = " + userID + "";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader row = oCmd.ExecuteReader())
                {
                    int i = 1;
                    while (row.Read())
                    {
                        lblOutput.Text += @"<tr>";
                        lblOutput.Text += @"<td>" + i++ + "</td>";
                        lblOutput.Text += @"<td>" + row["Title"].ToString() + "</td>";
                        lblOutput.Text += @"<td>" + row["SubTitle"].ToString() + "</td>";
                        lblOutput.Text += @"<td>" + row["Description"].ToString() + "</td>";
                        switch (row["AuthenticationStatus"].ToString()) {
                            case "0":
                                lblOutput.Text += @"<td><a href='FormView?formid=" + row["Id"].ToString() + "' class='btn btn-primary'>Update Form</a></td>";
                                break;
                            case "1":
                                lblOutput.Text += @"<td><a href='ViewUserForm?formid=" + row["Id"].ToString() + "' class='btn btn-primary' id='viewform'>View Form</a></td>";
                                break;
                            default:
                                lblOutput.Text += @"<td>" + row["AuthenticationStatus"].ToString() + "</td>";

                                break;
                        }
                       
                        lblOutput.Text += @"</tr>";
                    }
                    myConnection.Close();
                }
            }
            lblOutput.Text += @"</ tbody></table>";

        }



        /*
         *  /*dt.Columns.Remove("ID");
        //dt.Columns.Remove("PID");
        //dt.Columns.Remove("RoomId");
        dt.Columns.Remove("FurnitureId");
        //dt.Columns.Remove("IsActive");
        //dt.Columns.Remove("CreatedBy");
        //dt.Columns.Remove("CreatedDate");
        //dt.Columns.Remove("UpdatedBy");
        dt.Columns.Remove("RoomDetails");
        dt.Columns.Remove("Name");
        dt.Columns.Remove("QuarterName");
        dt.Columns.Remove("FloorName");
        dt.Columns.Remove("RoomNo");
        
        //dt.Columns.Remove("ID");
        DataView DV1 = new DataView(dt);
        DataTable Dt1 = DV1.Table;
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         *     protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        
            dt = GetData();
        
        //Create a dummy GridView
        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
         "attachment;filename=Roster_List.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            //Apply text style to each Row
            GridView1.Rows[i].Attributes.Add("class", "textmode");
        }
        GridView1.RenderControl(hw);

        //style to format numbers to string
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        
            dt = GetData();
       

        //Create a dummy GridView
        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();  
    }

         * */


        public void ExportToExcel()
        {
            DataTable dt = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = @"SELECT Forms.*, FormInbox.AuthenticationStatus FROM FormInbox LEFT JOIN Forms ON FormInbox.FormId = Forms.Id where FormInbox.UserId = " + userID + "";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(oCmd);
                sda.SelectCommand = oCmd;
                sda.Fill(dt);

            }
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Roster_List.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
}