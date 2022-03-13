using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saral
{
    public partial class Login : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["ADConnection"].ToString();
        string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        int userID = 0;
        private string _path;
        private string _filterAttribute;
        private string _filterAttributeMail;
        private string _filterAttributeDisplayName;
        private string _errorMsg;
        private string _filterAttributeUnit;



        protected void Page_Load(object sender, EventArgs e)
        {
            /*
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
            Request.QueryString["id"] = "1";
            GetDataTable();
            }
            */
           
             
        }

        protected void searchPno_Click(object sender, EventArgs e)
        {
            string _uname = pNo.Text.Trim().ToString();
            string _pwd = password.Text.Trim().ToString();

            /* DirectoryEntry _dEntry = new DirectoryEntry();
             DirectorySearcher _dSearcher = new DirectorySearcher();
             _dEntry = new DirectoryEntry(conn, _uname.Trim(), _pwd.Trim());
             _dSearcher = new DirectorySearcher(_dEntry);
             _dSearcher.SearchRoot = _dEntry;
             _dSearcher.Filter = "(sAMAccountName=" + _uname + ")";
             SearchResult _sResult = _dSearcher.FindOne();
             _dEntry = _sResult.GetDirectoryEntry();
             int i = _sResult.Properties.Count;
             */

            //DataTable dt = GetDsg_AD("PASCHIM", _uname, _pwd);
            bool auth = IsAuthenticated("PASCHIM", _uname, _pwd);
            if (auth)
            {
                bool userExists = false;
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                {
                    string oString = "SELECT * FROM Users WHERE PersonalNumber = @pno";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@pno", _uname);
                    myConnection.Open();
                    using (SqlDataReader row = oCmd.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            Session["userID"] = 1;
                            userExists = true;
                            //Session["userID"] = row["Id"].ToString();
                            Session["fullName"] = row["FullName"].ToString();
                            Session["designation"] = row["Designation"].ToString();
                            //sesTitle.Text = "fullName";
                        }
                    }
                }
                if (!userExists) {
                    //INSERT THE USER TO OUR DB & ASSIGN THE SESSION DETAILS
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        string oString = "INSERT INTO Users (FullName, PersonalNumber, Department) Values(@fn,@pno,@dsg)";
                        using (SqlCommand command = new SqlCommand(oString, myConnection))
                        {
                            myConnection.Open();
                            command.Parameters.AddWithValue("@fn" , _filterAttributeDisplayName);
                            command.Parameters.AddWithValue("@pno", _uname);
                            command.Parameters.AddWithValue("@dsg", _filterAttributeUnit);
                            
                            int a= command.ExecuteNonQuery();
                            //command.ExecuteNonQuery();

                            myConnection.Close();

                            Session["userID"] = a;
                            //Session["userID"] = "1";

                            Session["fullName"] = _filterAttributeDisplayName;
                            Session["unit"] = _filterAttributeUnit;
                            //Response.Redirect("AddEditForm?id=" + formID);
                        }
                    }
                }
                Response.Redirect("Default.aspx");
                //Response.Redirect("Default.aspx", false);
                //Server.Transfer("Default.aspx");
            }
            else
            {
                regMsg.Text = _errorMsg;
            }
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(conn, domainAndUsername, pwd);
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("displayname");
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("empunit");
                search.PropertiesToLoad.Add("empdesignation");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    _errorMsg = "Error Fetching user.";
                    return false;
                }
                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
                try
                {
                    _filterAttributeMail = (string)result.Properties["mail"][0];
                    _filterAttributeDisplayName = (string)result.Properties["displayname"][0];
                    _filterAttributeUnit= (string)result.Properties["empunit"][0];
                    //_filterAttributeDesignation = (string)result.Properties["empdesignation"][0];
                }
                catch (Exception ex)
                { }
            }
            catch (Exception ex)
            {
                _errorMsg = "Error: " + ex.Message;
                //throw new Exception("Error authenticating user. " + ex.Message);
                return false;
            }

            return true;
        }

        public bool sendOutMail(string HtmlTemplate, string Subject, string FromMail, string ToMail)
        {
            string strBody = string.Empty;
            bool Res = false;
            //StreamReader str = new StreamReader(HtmlTemplate);
            //strBody = str.ReadToEnd().ToString();
            //str.Close();
            byte[] a = null;
            //string Body = strBody.Replace("~SNO~", SNO).Replace("~ReceivedDate~", RecDate).Replace("~Type~", Type).Replace("~FromUnit~", FromUnit).Replace("~LetterNo~", LetterNo).Replace("~LetterDate~", LetterDate).Replace("~Subject~", LtrSubject).Replace("~IssueType~", IssueType).Replace("~UnitRemarks~", Remarks).Replace("~Desg~", Desgn).Replace("~FileNo~", FileNo).Replace("~Reference~", Reference).ToString().Replace("~byDate~", bydate).ToString();
            string Body = strBody.Replace(" ", "").ToString();
            Res = SendMailWithAttachmentBytesAndDeliveryNotification(Subject, HtmlTemplate, ToMail + "@hq.indiannavy.mil", (FromMail + "@hq.indiannavy.mil"), "", a, (ToMail + "@hq.indiannavy.mil"));
            return Res;
        }
        public bool SendMailWithAttachmentBytesAndDeliveryNotification(string Subject, string Body, string To, string From, string File1, byte[] attach1, string strBcc)
        {
            try
            {
                string strBody = string.Empty;
                MailMessage Msg = new MailMessage();
                Msg.To.Add(To);
                if (strBcc != string.Empty)
                    Msg.Bcc.Add(strBcc);
                Msg.From = new MailAddress(From);
                Msg.Subject = Subject;
                Msg.Body = Body;
                if (attach1 != null && attach1.Length > 0)
                    Msg.Attachments.Add(new Attachment(new MemoryStream(attach1), File1));

                Msg.IsBodyHtml = true;
                Msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.Delay;
                Msg.Headers.Add("Disposition-Notification-To", From);
                Msg.ReplyTo = new MailAddress(From);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "webmail.Paschim.hq.indiannavy.mil";
                smtp.Port = 25;
                smtp.Timeout = 100000;
                smtp.Send(Msg);
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            sendOutMail("Test", "SBB", "218454N", "218454N");

            //sendMail sm = new sendMail();
            //MailSend(string from_MailID, string to_MailID, string sub, string mail_Body)
            //sm.MailSend("wnc-ebpsupport@hq.indiannavy.mil", pppp + "@hq.indiannavy.mil", "eBP Board Nomination", BMmailBody);

        }

        /*
        public DataTable GetDsg_AD(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            // DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            DataTable dtMyTable1 = new DataTable("WFstep");
            try
            {
                DirectorySearcher search = new DirectorySearcher(conn);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("displayname");
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("empdesignation");
                search.PropertiesToLoad.Add("empunit");
                //search.PropertiesToLoad.Add("emprank");
                SearchResult result = search.FindOne();
                SearchResultCollection resultCol = search.FindAll();
                try
                {
                    dtMyTable1.Columns.Add("DESG", typeof(string));
                    dtMyTable1.Columns.Add("UNITNAME", typeof(string));
                    dtMyTable1.Columns.Add("DSPNAME", typeof(string));
                    //dtMyTable1.Columns.Add("RANK", typeof(string));
                    dtMyTable1.Columns.Add("MAIL", typeof(string));
                    if (resultCol != null)
                    {
                        for (int i = 0; i < result.Properties["displayname"].Count; i++)
                        {
                            DataRow RRow;
                            RRow = dtMyTable1.NewRow();
                            RRow["DSPNAME"] = result.Properties["displayname"][i].ToString().ToUpper();
                            //RRow["RANK"] = result.Properties["emprank"][i].ToString().ToUpper();
                            RRow["MAIL"] = result.Properties["mail"][i].ToString().ToUpper();
                            //RRow["DESG"] = result.Properties["empdesignation"][i].ToString().ToUpper();
                            RRow["UNITNAME"] = result.Properties["empunit"][i].ToString();
                            searchDiv.Visible = false;
                            CNFdiv1.Visible = true;
                            chName.Value = RRow["DSPNAME"].ToString();
                            chUnit.Value = RRow["UNITNAME"].ToString();
                            chPno.Value = username;
                            cnfName.Text = "<b>Name: </b>" + RRow["DSPNAME"].ToString();
                            cnfUnit.Text = "<b>Unit: </b>" + RRow["UNITNAME"].ToString();
                            cnfPno.Text = "<b>Personal Number: </b>" + username;
                            dtMyTable1.Rows.Add(RRow);
                        }
                    }
                }
                catch (Exception ex)
                { }
                return dtMyTable1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
        }
        */
    }
}