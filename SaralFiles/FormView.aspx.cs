using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class FormView : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string formID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
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
                }

                /*
                using (SqlConnection con1 = new SqlConnection(connectionString))
                {
                    con1.Open();
                    SqlCommand com = new SqlCommand("SELECT Id, Label FROM FieldTypes", con1);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    //ddlFieldType.DataTextField = ds.Tables[0].Columns["Label"].ToString();
                    //ddlFieldType.DataValueField = ds.Tables[0].Columns["Id"].ToString();
                    //ddlFieldType.DataSource = ds.Tables[0];
                    //ddlFieldType.DataBind();
                    con1.Close();
                }

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    //EDIT MODE
                    string id = Request.QueryString["id"];
                    //pageTitleDisplay.Text = "Edit Field";
                    //EditID.Value = id;
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        string oString = "SELECT * FROM FormFields WHERE Id = @id";
                        SqlCommand oCmd = new SqlCommand(oString, myConnection);
                        oCmd.Parameters.AddWithValue("@id", id);
                        myConnection.Open();
                        using (SqlDataReader row = oCmd.ExecuteReader())
                        {
                            while (row.Read())
                            {
                                //fieldName.Text = row["Label"].ToString();
                                //ddlFieldType.SelectedValue = row["FieldType"].ToString();
                                //if (Convert.ToBoolean(row["isRequired"]))
                                //{
                                //    requiredField.SelectedValue = "1";
                                //}
                                //fieldOrder.Text = row["FieldOrder"].ToString();
                                //pageTitleDisplay.Text = row["Title"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    //ADD MODE
                    //pageTitleDisplay.Text = "Add New Field";

                }
                */
            }
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
                        temp += @"<th>" + row["Label"].ToString() + @"</th>";
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
                        trBlock += @"<td style='padding:0;'><input " + FldTypeInput[i] + @" value='' style='border:0;width:100%;' onblur='saveField(0," + i + @", $(this), " + formID + @"," + userID + @");'/></td>";
                        i++;
                    }
                    myConnection.Close();
                    temp += @"</tr></thead>";
                    trBlock += @"</tr>";
                    addBtn.OnClientClick = "$('#MainContent_lblOutput table tbody').append(\"" + trBlock + "\"); return false";

                    //tbody
                    string oString2 = "SELECT * FROM FormData WHERE UserID = @uid AND FormID = @fid";
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
                                temp += @"<td style='padding:0;'><input " + FldTypeInput[j] +
                                    @" value='" + row2["F" + j].ToString() +
                                    @"' name='as' style='border:0;width:100%;' 
                                    onblur='saveField(" + row2["id"].ToString() + @"," + j + @", $(this), " + fid.Value + @", " + userID + @");'/></td>";
                            }
                            temp += @"</tr>";

                        }
                        temp += @"</tbody>";
                        myConnection.Close();
                    }

                    lblOutput.Text = temp + "</table>";
                }
            }
        }


        [WebMethod]
        public static int saveField(string id, string field, string fldVal, string form, string user)
        {
            int returnVal = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString))
            {
                if (fldVal.Length > 0)
                {
                    string query = "";
                    if (Convert.ToInt32(id) > 0)
                    {
                        //EDIT Existing field
                        if (Convert.ToInt32(field) > 0)
                        {
                            query = @"UPDATE FormData SET F" + field + @"= @FieldVal WHERE Id=@id";
                            using (SqlCommand command = new SqlCommand(query, con))
                            {
                                con.Open();
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@FieldVal", fldVal);
                                command.ExecuteNonQuery();
                                con.Close();
                                returnVal = 0;
                            }
                        }
                    }
                    else
                    {
                        //ADD NEW ROW
                        query = @"insert into FormData (FormId, UserId, F" + field + @") values(@fid,@uid,@FieldVal); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            con.Open();
                            command.Parameters.AddWithValue("@fid", form);
                            command.Parameters.AddWithValue("@uid", user);
                            command.Parameters.AddWithValue("@FieldVal", fldVal);
                            //command.ExecuteNonQuery();
                            returnVal = Convert.ToInt32(command.ExecuteScalar());
                            con.Close();
                        }
                    }
                }
            }
            return returnVal;
        }

        protected void forwardBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForwardForm?formid=" + fid.Value);
        }



        [WebMethod]
        //Submit for auth button code : 0-Pending/ 1-submitted/ 2-Approved/3-Approved w Changes/4-reverted w remarks
        public static void SubmitForAuthbtn_Click(string form, string user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
            //ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "submitalert()", true);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE FormInbox SET AuthenticationStatus = '1' WHERE UserId = " + user + " AND FormID = " + form, con);
            con.Open();
            
            cmd.ExecuteNonQuery();
            con.Close();
            //Response.Redirect("FormView?formid="+fid.Value);

            return;

        }


       
    }
}
/*
  Auth Status:
  0 => Pending Form Filling
  1 => Form Submitted for Auth
  2 => Approved 
  3 => Approved with Changes
  4 => Revert with Remarks
 */