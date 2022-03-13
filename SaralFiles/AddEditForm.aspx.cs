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
    public partial class AddEditForm : System.Web.UI.Page
    {
        string FormId;
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string id;
        string FormID;
        protected void Page_Load(object sender, EventArgs e)
        {
             Session["userID"] = "1";
             //int UserId = Convert.ToInt32(Session["userID"].ToString());
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
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {



                    //EDIT MODE
                     id = Request.QueryString["id"];
                    form_id.Value = id;
                    FormID = id;
                    

                    //Session["formID"] = id;
                    ViewState["FormId"] = id;
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        string oString = "SELECT Title FROM Forms WHERE Id = @id";
                        SqlCommand oCmd = new SqlCommand(oString, myConnection);
                        oCmd.Parameters.AddWithValue("@id", id);
                        myConnection.Open();
                        using (SqlDataReader row = oCmd.ExecuteReader())
                        {
                            while (row.Read())
                            {
                                formTitle.Text = row["Title"].ToString(); 
                            }
                        }
                    }

                    //GetFields
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        string oString = @"SELECT FormFields.*, FieldTypes.Label as FieldTypeLabel FROM FormFields
                                    LEFT JOIN FieldTypes ON FormFields.FieldType = FieldTypes.ID
                                    WHERE FormID = @id ORDER BY FieldOrder";
                        SqlCommand oCmd = new SqlCommand(oString, myConnection);
                        oCmd.Parameters.AddWithValue("@id", id);
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
                            <td>" + row["Label"].ToString() + @"</td>
                            <td>" + row["FieldTypeLabel"].ToString() + @"</td>
                            <td>" + row["FieldOrder"].ToString() + @"</td>
                            <td><a href='AddEditFormField?id=" + row["Id"].ToString() + "&FormId="+ FormID + "' id='Button1' class='btn btn-primary'>Edit</a></td>" +

                                @"</tr>";
                            }
                            myConnection.Close();
                            lblOutput.Text = @"<table id='example' class='display' style='width:100%'>
                    <thead><tr>
                        <th>SrNo</th>
                        <th>Field Name</th>
                        <th>Type</th>
                        <th>Order</th>
                        <th>Action</th>
                    </tr></thead>
                    <tbody>" + temp + @"</tbody>
                    </table>";
                        }
                    }
                }
                else
                {
                    //ADD MODE
                    addDiv.Visible = true;
                    AddFieldBtn.Visible = false;
                    formTitle.Text = "New Form";
                }
            }
        }







        protected void submitForm_Click(object sender, EventArgs e)
        {
            
            //string formID = Session["formID"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "";
                query = @"insert into Forms (UserID, Title, SubTitle, Description) 
                            values(@uid,@title,@subt,@descr)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@uid", userID);
                    command.Parameters.AddWithValue("@title", FTitle.Text);
                    command.Parameters.AddWithValue("@subt", FSubtitle.Text);
                    command.Parameters.AddWithValue("@descr", FDescription.Text);
                    command.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect("CallLogDetails?id=" + CurrLogID);
                    Response.Redirect("Default.aspx");
                    //Response.Redirect("Default");
                }
            }
        }

        protected void AddFieldBtn_Click(object sender, EventArgs e)
        {

            Response.Redirect("AddEditFormField?FormId=" + form_id.Value);
            //Response.Redirect("AddFieldTabular?FormId=" + form_id.Value);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void dashbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}