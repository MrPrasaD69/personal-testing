using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class Outbox : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string id;
        string FormID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "1";
            if (!Page.IsPostBack)
            {
                id = Request.QueryString["id"];
                form_id.Value = id;
                GetDataTable();
            }
            
        }
        public void GetDataTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string oString = "select * from FormInbox left join forms on FormInbox.FormId = Forms.Id LEFT JOIN Users ON FormInbox.UserId = Users.Id WHERE SentBy = 1 AND FormInbox.FormId = 1";
                SqlCommand oCmd = new SqlCommand(oString, con);
                oCmd.Parameters.AddWithValue("@uid", userID);
                oCmd.Parameters.AddWithValue("@fdt", "");
                oCmd.Parameters.AddWithValue("@tdt", "");
                con.Open();
                using (SqlDataReader row = oCmd.ExecuteReader())
                {
                    int i = 1;
                    string temp = "";
                    while (row.Read())
                    {
                       // String combined = row["CreatedAt"].ToString();
                        //combined = Convert.ToBase64String(Byte);
                       // byte[] byteArray = Encoding.UTF8.GetBytes(combined);
                        //String date = System.Text.Encoding.UTF8.GetString(byteArray);

                        String status = row["AuthenticationStatus"].ToString();
                        String statusname = "";
                        switch(status)
                        {
                            case "0":
                                statusname = "Pending";
                                break;
                            case "1":
                                statusname = "Submitted";
                                break;
                            case "2":
                                statusname = "Approved";
                                break;
                            case "3":
                                statusname = "Approved with changes";
                                break;
                            case "4":
                                statusname = "Revert with Remarks";
                                break;

                            default :
                                statusname = "Pending";
                                break;
                        }

                        temp += @"<tr>
                                              <td>" + (i++).ToString() + @"</td>
                                              <td>" + row["FullName"].ToString() + @"</td>
                                                <td>" + row["PersonalNumber"].ToString() + @"</td>
                                                <td>" + row["Department"].ToString() + @"</td>
                                                <td>" + row["CreatedAt"].ToString() + @"</td>
                                                <td>" + statusname + @"</td>"+
                                "</tr>";
                    }
                    con.Close();
                    lblOutput.Text = @"<table id='example' class='display' style='width:100%'>
                                    <thead><tr>
                                       <th>SrNo</th>
                                       <th>SENT TO - Name</th>
                                        <th>Personal No</th>
                                        <th>Department</th>
                                        <th>Sent On</th>
                                        <th>Authentication Status</th>                        
                                    </tr></thead>
                                   <tbody>" + temp + @"</tbody>
                                    </table>";
                }
            }
        }

        /*         while (row.Read())
                    {
                        String combined = row["CreatedAt"].ToString(); 

                        temp += @"<tr>
                                              <td>" + (i++).ToString() + @"</td>
                                              <td>" + row["FullName"].ToString() + @"</td>
                                                <td>" + row["PersonalNumber"].ToString() + @"</td>
                                                <td>" + row["Department"].ToString() + @"</td>
                                                <td>" + row["CreatedAt"].ToString() + @"</td>
                                                <td>" + row["AuthenticationStatus"].ToString() +
                                "</tr>";
                    }      
                    
             
             */



        /*
          while (row.Read())
                {
                    lblOutput.Text += @"<tr>";
                    lblOutput.Text += @"<td>" + i++ + "</td>";
                    lblOutput.Text += @"<td>" + row["FullName"].ToString() + "</td>";
                    lblOutput.Text += @"<td>" + row["PersonalNumber"].ToString() + "</td>";
                    lblOutput.Text += @"<td>" + row["Department"].ToString() + "</td>";
                    lblOutput.Text += @"<td>" + row["CreatedAt"].ToString() + "</td>";
                    lblOutput.Text += @"<td>" + row["AuthenticationStatus"].ToString() + "</td>";
                    switch (row["AuthenticationStatus"].ToString())
                    {
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
                } */









        //public void GetDataTable()
        //{
        //    using (SqlConnection myConnection = new SqlConnection(connectionString))
        //    {
        //        string oString = "select * from FormInbox left join forms on FormInbox.FormId = Forms.Id LEFT JOIN Users ON FormInbox.UserId = Users.Id WHERE SentBy = 1 AND FormInbox.FormId = 1";
        //        SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //        oCmd.Parameters.AddWithValue("@uid", userID);
        //        oCmd.Parameters.AddWithValue("@fdt", "");
        //        oCmd.Parameters.AddWithValue("@tdt", "");
        //        myConnection.Open();
        //        using (SqlDataReader row = oCmd.ExecuteReader())
        //        {
        //            int i = 1;
        //            string temp = "";
        //            while (row.Read())
        //            {
        //                String combined = row["CreatedAt"].ToString(); //DateTime.ParseExact(row["CreatedAt"].ToString(), "dd-M-yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Add(new TimeSpan(5, 30, 0));

        //                temp += @"<tr>
        //                        <td>" + (i++).ToString() + @"</td>
        //                        <td>" + row["FullName"].ToString() + @"</td>
        //                        <td>" + row["PersonalNumber"].ToString() + @"</td>
        //                        <td>" + row["Department"].ToString() + @"</td>
        //                        <td>" + row["CreatedAt"].ToString() + @"</td>
        //                        <td>" + row["AuthenticationStatus"].ToString() +
        //                        "</tr>";
        //            }
        //            myConnection.Close();
        //            lblOutput.Text = @"<table id='example' class='display' style='width:100%'>
        //            <thead><tr>
        //                <th>SrNo</th>
        //                <th>SENT TO - Name</th>
        //                <th>Personal No</th>
        //                <th>Department</th>
        //                <th>Sent On</th>
        //                <th>Authentication Status</th>                        
        //            </tr></thead>
        //            <tbody>" + temp + @"</tbody>
        //            </table>";
        //        }
        //    }
        //}




        /* 
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



         */









        protected void ViewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAllData?formid="+form_id.Value);
        }
    }
}