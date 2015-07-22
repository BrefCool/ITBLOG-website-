using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;


namespace BLOG_TEST
{
    public partial class UserSendMessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                senderTextBox.Text = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"].ToString();
                senderTextBox.Enabled = true;

                if(Request["par_receiver"] != null)
                {
                    receiverTextBox.Text = Request["par_receiver"];
                }
            }
        }

        protected void sendBtn_Click(object sender, EventArgs e)
        {
            SqlConnection insertConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            insertConnection.Open();
            SqlTransaction trans = insertConnection.BeginTransaction();

            string insertString = "INSERT INTO Messages (senderID,receiverID,[content],sendtime) VALUES (@senderID,@receiverID,@content,@sendtime)";
            string loadIDString = "SELECT userID FROM Users WHERE username = @receivername";

            SqlCommand insertCommand = new SqlCommand(insertString, insertConnection);
            insertCommand.Transaction = trans;
            
            SqlCommand loadCommand = new SqlCommand(loadIDString, insertConnection);
            loadCommand.Transaction = trans;

            try
            {
                loadCommand.Parameters.Add(new SqlParameter("receivername", SqlDbType.NVarChar, 10));
                loadCommand.Parameters["receivername"].Value = receiverTextBox.Text;

                insertCommand.Parameters.Add(new SqlParameter("senderID", SqlDbType.Int));
                insertCommand.Parameters["senderID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
                insertCommand.Parameters.Add(new SqlParameter("receiverID", SqlDbType.Int));
                insertCommand.Parameters["receiverID"].Value = loadCommand.ExecuteScalar();
                insertCommand.Parameters.Add(new SqlParameter("content", SqlDbType.NVarChar));
                insertCommand.Parameters["content"].Value = messageTextBox.Text;
                insertCommand.Parameters.Add(new SqlParameter("sendtime", SqlDbType.DateTime));
                insertCommand.Parameters["sendtime"].Value = DateTime.Now;

                insertCommand.ExecuteNonQuery();
                trans.Commit();

                Response.Write("<script>alert('发送成功');window.location = '" + Request.Url.ToString() + "';</script>");
            }
            catch (Exception ex)
            {
                trans.Rollback();//回滚事务

                Response.Write(ex.ToString());

                Response.Write("<script>alert(\"发送失败\");</script>");
            }
            finally
            {
                insertConnection.Close();
            }


        }
    }
}