using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class _Default : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            userID = 1;
            if (!Page.IsPostBack)
            {
                
                GetDataTable();
            }
            else
            {
                
                //userID = Convert.ToInt32(Session["userID"].ToString());
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
            }
        }
        public void GetDataTable()
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = "SELECT * FROM Forms WHERE userID = @uid";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@uid", userID);
                oCmd.Parameters.AddWithValue("@fdt", "");
                oCmd.Parameters.AddWithValue("@tdt", "");
                myConnection.Open();
                using (SqlDataReader row = oCmd.ExecuteReader())
                {
                    int i = 1;
                    string temp = "";
                    while (row.Read())
                    {
                        String combined = row["CreatedAt"].ToString(); //DateTime.ParseExact(row["CreatedAt"].ToString(), "dd-M-yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Add(new TimeSpan(5, 30, 0));

                        temp += @"<tr>
                            <td>" + (i++).ToString() + @"</td>
                            <td>" + row["Title"].ToString() + @"</td>
                            <td>" + row["SubTitle"].ToString() + @"</td>
                            <td>" + row["Description"].ToString() + @"</td>
                            <td><a href='FieldTable?id=" + row["Id"].ToString() + "' id='Button1' class='btn btn-primary'>Edit</a></td>" +
                            @"<td><a href='FormViewData?formid=" + row["Id"].ToString() + "' id='Button2' class='btn btn-primary'>View Data</a></td>" +
                            @"<td><a href='SendFormPage?formid=" + row["Id"].ToString()+ "'id='Button3' class='btn btn-info'>Send Form</td>"+
                            "</tr>";
                    }
                    myConnection.Close();
                    lblOutput.Text = @"<table id='example' class='display' style='width:100%'>
                    <thead><tr>
                        <th>SrNo</th>
                        <th>Title</th>
                        <th>Subtitle</th>
                        <th>Description</th>
                        <th>Action</th>
                        <th>Form Data</th>
                        <th>Send</th> 
                    </tr></thead>
                    <tbody>" + temp + @"</tbody>
                    </table>";
                }
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditForm.aspx");
            //Server.Transfer("AddEditForm.aspx");

        }
    }
}
