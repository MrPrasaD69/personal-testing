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
    public partial class FieldTable : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        string formID = "";
        string id;
        string newid;
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

                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    //formID = Request.QueryString["formid"].ToString();
                    //fid.Value = formID;
                    //uid.Value = userID.ToString();
                    //GetDataTable();
                    //EditID.Value = id;
                    id = Request.QueryString["id"];
                    form_id.Value = id;
                    PopulateGridview();


                    //show form title
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




                }

                //formfields();
            }
        }

        protected void FieldGridView_SelectedIndexChanged(object sender, EventArgs e)

        {

        }
        






        //FETCH DATA INTO TABLE
        public void PopulateGridview()
        {

            DataTable dtbl = new DataTable();
            DataSet ds = new DataSet();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM FormFields", sqlCon);
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT FormFields.* , Fieldtypes.Label as FieldTypeLabel FROM FormFields LEFT JOIN FieldTypes ON FormFields.FieldType = Fieldtypes.ID WHERE FormID = " + form_id.Value, sqlCon);

                //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT FormFields.*, FieldTypes.Label as FieldTypeLabel FROM FormFields LEFT JOIN FieldTypes ON FormFields.FieldType = FieldTypes.ID WHERE FormID = @id ORDER BY FieldOrder ", sqlCon);

                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                FieldGridView.DataSource = dtbl;
                FieldGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                FieldGridView.DataSource = dtbl;
                FieldGridView.DataBind();
                FieldGridView.Rows[0].Cells.Clear();
                FieldGridView.Rows[0].Cells.Add(new TableCell());
                FieldGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                FieldGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                FieldGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }


        //void FillGridView()
        //{

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT FormFields.*, FieldTypes.Label as FieldTypeLabel FROM FormFields LEFT JOIN FieldTypes ON FormFields.FieldType = FieldTypes.ID WHERE FormID = '1' ORDER BY FieldOrder ";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        con.Open();

        //        DataTable dt = new DataTable();
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while(sdr.Read())
        //            {
        //                DataRow row = dt.NewRow();

        //                form_id.Value = row["FormID"].ToString();

        //                newid = form_id.Value;
        //                dt.Rows.Add(row);

        //                //form_id.Value = sdr["FormID"].ToString();
        //            }
        //            FieldGridView.DataSource = dt;
        //            FieldGridView.DataBind();

        //        }
        //    }
        //}



        //INSERT FUNCTION
        protected void FieldGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO FormFields (FormID, FieldType, Label, isRequired, FieldOrder) values(@formid, @ftype, @lab, @isreq, @forder)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        //sqlCmd.Parameters.AddWithValue("@fid", (FieldGridView.FooterRow.FindControl("FooterFieldFormIDtxt") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@formid", form_id.Value );
                        sqlCmd.Parameters.AddWithValue("@lab", (FieldGridView.FooterRow.FindControl("FooterFieldNametxt") as TextBox).Text.Trim());

                        //normal working textbox 
                        //sqlCmd.Parameters.AddWithValue("@ftype", (FieldGridView.FooterRow.FindControl("FooterTypetxt") as TextBox).Text.Trim());

                        //testing dropdown for inserting types
                        sqlCmd.Parameters.AddWithValue("@ftype", (FieldGridView.FooterRow.FindControl("FooterTypetxt") as DropDownList).Text.Trim());
                        //dropdownList for inserting works

                        //sqlCmd.Parameters.AddWithValue("@val", (FieldGridView.FooterRow.FindControl("") as TextBox).Text.Trim());

                        //dropdownlist working for required field
                        sqlCmd.Parameters.AddWithValue("@isreq", (FieldGridView.FooterRow.FindControl("FooterRequiredTxt") as DropDownList).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@forder", (FieldGridView.FooterRow.FindControl("FooterOrderTxt") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        //FillGridView();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void FieldGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            FieldGridView.EditIndex = e.NewEditIndex;
            PopulateGridview();
            //FillGridView();
        }


        //CANCEL EDITING FUNCTOIN
        protected void FieldGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            FieldGridView.EditIndex = - 1;
            PopulateGridview();
            //FillGridView();
        }



        //UPDATE FUNCTION
        protected void FieldGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "UPDATE FormFields SET FieldType=@ftype, Label=@lab, isRequired=@isreq, FieldOrder=@forder WHERE Id =@id";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        //sqlCmd.Parameters.AddWithValue("@fid", (FieldGridView.FooterRow.FindControl("") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@lab", (FieldGridView.Rows[e.RowIndex].FindControl("Nametxt") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ftype", (FieldGridView.Rows[e.RowIndex].FindControl("DropDownList1") as DropDownList).Text.Trim());
                        //sqlCmd.Parameters.AddWithValue("@val", (FieldGridView.Rows[e.RowIndex].FindControl("") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@isreq", (FieldGridView.Rows[e.RowIndex].FindControl("reqDropDown") as DropDownList).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@forder", (FieldGridView.Rows[e.RowIndex].FindControl("fordertxt") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(FieldGridView.DataKeys[e.RowIndex].Value.ToString()));
                        sqlCmd.ExecuteNonQuery();
                        FieldGridView.EditIndex = -1;
                    PopulateGridview();
                    //FillGridView();
                    lblSuccessMessage.Text = "SELECTED Record UPDATED";
                        lblErrorMessage.Text = "";
                    }
             
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }



        //DELTE FUNCTION
        protected void FieldGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM FormFields WHERE Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(FieldGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    //FillGridView();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void dashbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

















        //public void formfields()
        //{
        //    SqlConnection con = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand("SELECT * FROM FormFields WHERE FormID = @fid ORDER BY FieldOrder", con) ;
        //    cmd.Parameters.AddWithValue("@fid", formID);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    con.Open();
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    GridView1.DataSource = ds.Tables[0];
        //    GridView1.DataBind();


        //}





    }
}