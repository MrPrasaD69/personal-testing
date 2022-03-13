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
    public partial class AddEditFormField : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string id;
        string FormID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "1";
            //int UserId = Convert.ToInt32(Session["userID"].ToString());
            id = Request.QueryString["id"];
            FormID = Request.QueryString["FormId"];
            //Session["formID"] = id;

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
                using (SqlConnection con1 = new SqlConnection(connectionString))
                {   
                    con1.Open();
                    SqlCommand com = new SqlCommand("SELECT Id, Label FROM FieldTypes", con1);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    ddlFieldType.DataTextField = ds.Tables[0].Columns["Label"].ToString();
                    ddlFieldType.DataValueField = ds.Tables[0].Columns["Id"].ToString();
                    ddlFieldType.DataSource = ds.Tables[0];
                    ddlFieldType.DataBind();
                    con1.Close();
                }

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    //EDIT MODE
                   // id = Request.QueryString["id"];
                    pageTitleDisplay.Text = "Edit Field";
                    EditID.Value = id;
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
                                fieldName.Text = row["Label"].ToString();
                                ddlFieldType.SelectedValue = row["FieldType"].ToString();
                                if (Convert.ToBoolean(row["isRequired"]))
                                {
                                    requiredField.SelectedValue = "1";
                                }
                                fieldOrder.Text = row["FieldOrder"].ToString();
                                //pageTitleDisplay.Text = row["Title"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    //ADD MODE
                    pageTitleDisplay.Text = "Add New Field";

                }
            }



        }

        protected void submitField_Click(object sender, EventArgs e)
        {
            //Session["formID"] = "0";
            //string id = Request.QueryString["id"];
            //Session["formID"] = id;
            //string formID = Session["formID"].ToString();
            

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "";
                if (Convert.ToInt32(EditID.Value) > 0)
                {
                    query = @"UPDATE FormFields SET FieldType = @ftype, Label = @lab, Validations = @val, isRequired=@isreq, FieldOrder=@forder WHERE Id=" + EditID.Value;
                }
                else
                {
                    query = @"insert into FormFields (FormID, FieldType, Label, Validations, isRequired, FieldOrder) 
                            values(@fid,@ftype,@lab,@val,@isreq,@forder)";
                }
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@fid", FormID);
                    command.Parameters.AddWithValue("@ftype", ddlFieldType.SelectedValue);
                    command.Parameters.AddWithValue("@lab", fieldName.Text);
                    command.Parameters.AddWithValue("@val", "");
                    command.Parameters.AddWithValue("@isreq", requiredField.SelectedValue);
                    command.Parameters.AddWithValue("@forder", fieldOrder.Text);
                    command.ExecuteNonQuery();
                    con.Close();
                    //Response.Redirect("CallLogDetails?id=" + CurrLogID);
                    Response.Redirect("AddEditForm?id=" + FormID);
                    Response.Redirect("Default");
                }
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditForm?id=" + FormID);
        }
    }
}