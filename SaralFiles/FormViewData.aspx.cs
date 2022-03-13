using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace saral
{
    public partial class FormViewData : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string formID = "";
        string excel;

        protected void Page_Load(object sender, EventArgs e)
        {
            formID = Request.QueryString["formid"].ToString();
            fid.Value = formID;


            userID = 1;
            if (!string.IsNullOrEmpty(Session["userID"] as string))
            {
                userID = Convert.ToInt32(Session["userID"].ToString());
            }
            else
            {
                //Response.Redirect("/Login.aspx");
            }
            if (userID == 0)
            {
                //Response.Redirect("/Login.aspx");
            }

            if (!Page.IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["formid"]))
                {
                    formID = Request.QueryString["formid"].ToString();
                    fid.Value = formID;
                    uid.Value = userID.ToString();
                    GetDataTable();
                    //ExportToExcel();
                }
            }
        }
        protected void Excelbtn_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void PDFbtn_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }


        public void GetDataTable()
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = "SELECT * FROM FormFields WHERE FormID = @fid ORDER BY FieldOrder";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                //oCmd.Parameters.AddWithValue("@uid", userID);
                oCmd.Parameters.AddWithValue("@fid", formID);
                //oCmd.Parameters.AddWithValue("@tdt", "");
                myConnection.Open();
                using (SqlDataReader row = oCmd.ExecuteReader())
                {
                    int i = 1;
                    string trBlock = "";
                    int[] FldType = new int[15];
                    string[] FldTypeInput = new string[15];
                    string temp = "<table id='example' class='display table table-bordered' style='width:100%'><thead><tr>";
                    trBlock = @"<tr>";
                    while (row.Read())
                    {
                        //String combined = row["CreatedAt"].ToString(); //DateTime.ParseExact(row["CreatedAt"].ToString(), "dd-M-yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Add(new TimeSpan(5, 30, 0));
                        temp += @"<th><b>" + row["Label"].ToString() + @"</th>";
                        FldType[i] = Convert.ToInt32(row["FieldType"].ToString());
                        switch (FldType[i])
                        {
                            case 1:
                                FldTypeInput[i] = "type='text'";
                                break;
                            case 2:
                                FldTypeInput[i] = "type='number'";
                                break;
                            case 3:
                                FldTypeInput[i] = "type='date'";
                                break;
                            case 4:
                                FldTypeInput[i] = "type='time'";
                                break;
                        }
                        trBlock += @"<td style='padding:0;'><input disabled " + FldTypeInput[i] + @" value='' style='border:0;width:100%;' onblur='saveField(0," + i + @", $(this), " + formID + @"," + userID + @");'/></td>";
                        i++;
                    }
                    myConnection.Close();
                    temp += @"</tr></thead>";
                    trBlock += @"</tr>";
                    //addBtn.OnClientClick = "$('#MainContent_lblOutput table tbody').append(\"" + trBlock + "\"); return false";

                    //tbody
                    string oString2 = "SELECT * FROM FormData WHERE FormID = @fid";
                    SqlCommand oCmd2 = new SqlCommand(oString2, myConnection);
                    oCmd2.Parameters.AddWithValue("@uid", userID);
                    oCmd2.Parameters.AddWithValue("@fid", formID);
                    myConnection.Open();
                    using (SqlDataReader row2 = oCmd2.ExecuteReader())
                    {
                        temp += @"<tbody>";
                        while (row2.Read())
                        {
                            temp += @"<tr>";
                            for (int j = 1; j < i; j++)
                            {
                                temp += @"<td style='padding:0;'>" + row2["F" + j].ToString() + @"</td>";
                            }
                            temp += @"</tr>";
                        }
                        temp += @"</tbody>";
                        myConnection.Close();
                    }
                    lblOutput.Text = temp + "</table>";
                    excel = temp.ToString();
                }
            }
        }

        protected void dashbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }



        //TESTING EXPORT EXCEL CODE       
        //protected void ExportExcel()
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.Charset = "";
        //    string FileName = "Test"+DateTime.Now+".xls";
        //    StringWriter strw = new StringWriter();
        //    HtmlTextWriter htx = new HtmlTextWriter(strw);
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "saral/test.ms-excel";

        //}



        //new code for excel
        public void ExportToExcel()
        {
            DataTable dt = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString2 = "SELECT * FROM FormData WHERE FormID = @fid";
                SqlCommand oCmd2 = new SqlCommand(oString2, myConnection);
                //oCmd2.Parameters.AddWithValue("@uid", userID);
                oCmd2.Parameters.AddWithValue("@fid", formID);
                myConnection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(oCmd2);
                sda.SelectCommand = oCmd2;
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
             "attachment;filename=SaralForm_Data.xls");
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


        protected void ExportToPDF()
        {
            DataTable dt = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString2 = "SELECT * FROM FormData WHERE FormID = @fid";
                SqlCommand oCmd2 = new SqlCommand(oString2, myConnection);
                //oCmd2.Parameters.AddWithValue("@uid", userID);
                oCmd2.Parameters.AddWithValue("@fid", formID);
                myConnection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(oCmd2);
                sda.SelectCommand = oCmd2;
                sda.Fill(dt);
            }

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
    }
}