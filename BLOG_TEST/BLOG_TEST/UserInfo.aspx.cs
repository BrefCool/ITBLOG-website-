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
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet UserInfoDataSet = loadUserInfo();
            Page.Title = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"] + "的个人中心";
            if (!IsPostBack)
            {
                signTimeLabel.Text = Convert.ToDateTime(UserInfoDataSet.Tables["Users"].Rows[0]["rgtime"]).ToString("yyyy-MM-dd");
                Info_userNameLabel.Text = UserInfoDataSet.Tables["Users"].Rows[0]["username"].ToString();
                Info_userQQLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["QQ"].ToString();
                Info_userBirthLabel.Text = Convert.ToDateTime(UserInfoDataSet.Tables["Info"].Rows[0]["birthday"]).ToString("yyyy-MM");
                Info_userEmailLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["email"].ToString();
                if (Convert.ToInt32(UserInfoDataSet.Tables["Info"].Rows[0]["sex"]) == 1)
                {
                    Info_userSexLabel.Text = "男";
                }
                else
                {
                    Info_userSexLabel.Text = "女";
                }

                if (UserInfoDataSet.Tables["Info"].Rows[0]["introduce"].ToString() != "")
                {
                    Info_userIntroduceLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["introduce"].ToString();
                }
                else
                {
                    Info_userIntroduceLabel.Text = "暂无...";
                }
                Info_userBirthTextBox.Text = Info_userBirthLabel.Text;
                Info_userEmailTextBox.Text = Info_userEmailLabel.Text;
                Info_userQQTextBox.Text = Info_userQQLabel.Text;
                Info_userSexTextBox.Text = Info_userSexLabel.Text;
                Info_userIntroduceTextBox.Text = Info_userIntroduceLabel.Text;
            }
            else
            {
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "myscript", "<script>window.onload=function(){userInfoPrepare();}</script>");
            HeadImg.ImageUrl = UserInfoDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
            toMyBlog.HRef = "UserBlog_ArticlesList.aspx?par_userID=" + HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"].ToString();

            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();

            string loadString = "SELECT * FROM Collection WHERE hostID=@userID";
            SqlCommand loadCommand = new SqlCommand(loadString, con);
            loadCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            loadCommand.Parameters["userID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Collection");

            string getString = "SELECT * FROM Messages WHERE receiverID=@userID ORDER BY sendtime DESC";
            SqlCommand getCommand = new SqlCommand(getString, con);
            getCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            getCommand.Parameters["userID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
            SqlDataAdapter getAdapter = new SqlDataAdapter(getCommand);
            getAdapter.Fill(loadDataSet, "Messages");
            
            if(loadDataSet.Tables["Collection"].Rows.Count == 0)
            {
                myCollectHolder.Controls.Add(new LiteralControl("<div style=\"width: 80%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; \" >"));
                myCollectHolder.Controls.Add(new LiteralControl("<h2>暂无收藏</h2>"));
                myCollectHolder.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                for (int i = 0; i < loadDataSet.Tables["Collection"].Rows.Count; i++)
                {
                    HyperLink CollectionLink = new HyperLink();
                    Label CollectionContentLabel = new Label();
                    Button deleteBtn = new Button();

                    CollectionLink.ID = loadDataSet.Tables["Collection"].Rows[i]["ID"].ToString() + "_CollectionLink";
                    CollectionLink.NavigateUrl = loadDataSet.Tables["Collection"].Rows[i]["webpath"].ToString();
                    CollectionLink.Text = loadDataSet.Tables["Collection"].Rows[i]["title"].ToString();
                    CollectionLink.Font.Size = 25;
                    CollectionLink.Font.Bold = true;

                    CollectionContentLabel.ID = loadDataSet.Tables["Collection"].Rows[i]["ID"].ToString() + "_CollectionContent";
                    CollectionContentLabel.Text = loadDataSet.Tables["Collection"].Rows[i]["description"].ToString();
                    CollectionContentLabel.Font.Size = 12;
                    CollectionContentLabel.ForeColor = System.Drawing.Color.FromName("#999999");

                    deleteBtn.ID = loadDataSet.Tables["Collection"].Rows[i]["ID"].ToString();
                    deleteBtn.CssClass = "btn btn-danger";
                    deleteBtn.Text = "删除";
                    deleteBtn.OnClientClick = "return confirm('确认删除？')";
                    deleteBtn.Click += deleteBtn_Click;

                    myCollectHolder.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px dashed;\">"));
                    myCollectHolder.Controls.Add(new LiteralControl("<ul class=\"list-unstyled list-inline\" style=\"text-align:left;\">"));
                    myCollectHolder.Controls.Add(new LiteralControl("<li style=\"margin-right:5px;\">"));
                    myCollectHolder.Controls.Add(CollectionLink);
                    myCollectHolder.Controls.Add(new LiteralControl("</li>"));
                    myCollectHolder.Controls.Add(new LiteralControl("<li style=\"position:relative;bottom:6px;\">"));
                    myCollectHolder.Controls.Add(deleteBtn);
                    myCollectHolder.Controls.Add(new LiteralControl("</li>"));
                    myCollectHolder.Controls.Add(new LiteralControl("</ul>"));
                    myCollectHolder.Controls.Add(new LiteralControl("<br/>"));
                    myCollectHolder.Controls.Add(CollectionContentLabel);
                    myCollectHolder.Controls.Add(new LiteralControl("</div>"));
                }
            }
            
            if (loadDataSet.Tables["Messages"].Rows.Count == 0)
            {
                myMessageHolder.Controls.Add(new LiteralControl("<div style=\"width: 80%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; \" >"));
                myMessageHolder.Controls.Add(new LiteralControl("<h2>暂无消息</h2>"));
                myMessageHolder.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                for (int i = 0; i < loadDataSet.Tables["Messages"].Rows.Count; i++)
                {
                    Label senderNameLabel = new Label();
                    Label contentLabel = new Label();
                    Label timeLabel = new Label();
                    Button deleteButton = new Button();

                    string loadSenderString = "SELECT username FROM Users WHERE userID = @senderID";
                    SqlCommand loadSenderCom = new SqlCommand(loadSenderString, con);
                    loadSenderCom.Parameters.Add(new SqlParameter("senderID", SqlDbType.Int));
                    loadSenderCom.Parameters["senderID"].Value = loadDataSet.Tables["Messages"].Rows[i]["senderID"];

                    string senderName = loadSenderCom.ExecuteScalar().ToString();

                    senderNameLabel.Text = senderName + ": ";
                    senderNameLabel.ID = senderName + "Label";

                    contentLabel.ID = loadDataSet.Tables["Messages"].Rows[i]["ID"].ToString() + "_Content";
                    contentLabel.Text = loadDataSet.Tables["Messages"].Rows[i]["content"].ToString();

                    timeLabel.ID = loadDataSet.Tables["Messages"].Rows[i]["ID"].ToString() + "_Time";
                    timeLabel.Text = Convert.ToDateTime(loadDataSet.Tables["Messages"].Rows[i]["sendtime"]).ToString("yyyy-MM-dd");

                    deleteButton.ID = loadDataSet.Tables["Messages"].Rows[i]["ID"].ToString();
                    deleteButton.CssClass = "btn btn-danger";
                    deleteButton.Text = "删除";
                    deleteButton.OnClientClick = "return confirm('确认删除？')";
                    deleteButton.Click += deleteButton_Click;

                    myMessageHolder.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px dashed;\">"));
                    myMessageHolder.Controls.Add(senderNameLabel);
                    myMessageHolder.Controls.Add(contentLabel);
                    myMessageHolder.Controls.Add(new LiteralControl("<ul class=\"list-unstyled list-inline\" style=\"text-align:right;\">"));
                    myMessageHolder.Controls.Add(new LiteralControl("<li>"));
                    myMessageHolder.Controls.Add(timeLabel);
                    myMessageHolder.Controls.Add(new LiteralControl("</li>"));
                    myMessageHolder.Controls.Add(new LiteralControl("<li><a role=\"button\" class=\"btn btn-default\" href=\"UserSendMessages.aspx?par_receiver=" + senderName + "\">回复</a></li>"));
                    myMessageHolder.Controls.Add(new LiteralControl("<li>"));
                    myMessageHolder.Controls.Add(deleteButton);
                    myMessageHolder.Controls.Add(new LiteralControl("</li>"));
                    myMessageHolder.Controls.Add(new LiteralControl("</ul>"));
                    myMessageHolder.Controls.Add(new LiteralControl("</div>"));
                }
            }
        }

        public static DataSet loadUserInfo()
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadString = "SELECT username,headimgID,rgtime,infoID FROM Users WHERE userID=@userID";

            SqlCommand loadCommand1 = new SqlCommand(loadString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("userID", SqlDbType.NVarChar, 10));
            if(HttpContext.Current.Request.Cookies["UserInfo"] != null)
            {
                loadCommand1.Parameters["userID"].Value = HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"].ToString();
            }
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet,"Users");

            string loadHeadImgString = "SELECT * FROM HeadImg WHERE ID=@headimgID";

            SqlCommand loadCommand2 = new SqlCommand(loadHeadImgString, loadConnection);
            loadCommand2.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
            loadCommand2.Parameters["headimgID"].Value = loadDataSet.Tables["Users"].Rows[0]["headimgID"];
            SqlDataAdapter HeadimgAdapter = new SqlDataAdapter(loadCommand2);
            HeadimgAdapter.Fill(loadDataSet, "HeadImg");

            string loadUserInfoString = "SELECT * FROM Info WHERE ID=@infoID";

            SqlCommand loadCommand3 = new SqlCommand(loadUserInfoString, loadConnection);
            loadCommand3.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
            loadCommand3.Parameters["infoID"].Value = loadDataSet.Tables["Users"].Rows[0]["infoID"];
            SqlDataAdapter InfoAdapter = new SqlDataAdapter(loadCommand3);
            InfoAdapter.Fill(loadDataSet, "Info");

            return loadDataSet;
        }

        protected void myMessageBtn_Click(object sender, EventArgs e)
        {
            myCollectHolder.Visible = false;
            myBlogHolder.Visible = false;
            myMessageHolder.Visible = true;
        }

        protected void myBlogBtn_Click(object sender, EventArgs e)
        {
            myMessageHolder.Visible = false;
            myCollectHolder.Visible = false;
            myBlogHolder.Visible = true;
        }

        protected void myCollectBtn_Click(object sender, EventArgs e)
        {
            myMessageHolder.Visible = false;
            myBlogHolder.Visible = false;
            myCollectHolder.Visible = true;
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            DataSet UserInfoDataSet = loadUserInfo();

            if(Info_userBirthTextBox.Text != "")
            {
                DateTime dt1 = DateTime.ParseExact(Info_userBirthTextBox.Text, "yyyy-MM", System.Globalization.CultureInfo.CurrentCulture);
                UserInfoDataSet.Tables["Info"].Rows[0]["birthday"] = dt1;
            }

            if(Info_userEmailTextBox.Text != "")
            {
                UserInfoDataSet.Tables["Info"].Rows[0]["email"] = Info_userEmailTextBox.Text;
            }

            if(Info_userIntroduceTextBox.Text != "")
            {
                UserInfoDataSet.Tables["Info"].Rows[0]["introduce"] = Info_userIntroduceTextBox.Text;
            }

            if(Info_userQQTextBox.Text != "")
            {
                UserInfoDataSet.Tables["Info"].Rows[0]["QQ"] = Info_userQQTextBox.Text;
            }

            if(Info_userSexTextBox.Text == "男")
            {
                UserInfoDataSet.Tables["Info"].Rows[0]["sex"] = 1;
            }
            else if(Info_userSexTextBox.Text == "女")
            {
                UserInfoDataSet.Tables["Info"].Rows[0]["sex"] = 0;
            }

            if(UserInfoDataSet.HasChanges())
            {
                SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
                string UpdateCommandString = "SELECT * FROM Info";
                SqlCommand UpdateCommand = new SqlCommand(UpdateCommandString, con);
                SqlDataAdapter UpdateAdapter = new SqlDataAdapter(UpdateCommand);
                SqlCommandBuilder sb = new SqlCommandBuilder(UpdateAdapter);
                UpdateAdapter.Update(UserInfoDataSet, "Info");
            }

            signTimeLabel.Text = Convert.ToDateTime(UserInfoDataSet.Tables["Users"].Rows[0]["rgtime"]).ToString("yyyy-MM-dd");
            Info_userQQLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["QQ"].ToString();
            Info_userBirthLabel.Text = Convert.ToDateTime(UserInfoDataSet.Tables["Info"].Rows[0]["birthday"]).ToString("yyyy-MM");
            Info_userEmailLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["email"].ToString();
            if (Convert.ToInt32(UserInfoDataSet.Tables["Info"].Rows[0]["sex"]) == 1)
            {
                Info_userSexLabel.Text = "男";
            }
            else
            {
                Info_userSexLabel.Text = "女";
            }

            if (UserInfoDataSet.Tables["Info"].Rows[0]["introduce"].ToString() != "")
            {
                Info_userIntroduceLabel.Text = UserInfoDataSet.Tables["Info"].Rows[0]["introduce"].ToString();
            }
            else
            {
                Info_userIntroduceLabel.Text = "暂无...";
            }
            Info_userBirthTextBox.Text = Info_userBirthLabel.Text;
            Info_userEmailTextBox.Text = Info_userEmailLabel.Text;
            Info_userQQTextBox.Text = Info_userQQLabel.Text;
            Info_userSexTextBox.Text = Info_userSexLabel.Text;
            Info_userIntroduceTextBox.Text = Info_userIntroduceLabel.Text;
        }

        protected void HeadImgUploadBtn_Click(object sender, EventArgs e)
        {
            DataSet UserInfoDataSet = loadUserInfo();
            if(HeadImgUpload.HasFile)
            {
                string savePath = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"].ToString() + "_headImg" + HeadImgUpload.FileName ;
                HeadImgUpload.SaveAs(Server.MapPath("~/images/") + savePath);
                string lastPath = UserInfoDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
                if(lastPath != "images/default_user.png")
                {
                    System.IO.File.Delete(Server.MapPath("~/") + lastPath);
                }
                
                UserInfoDataSet.Tables["HeadImg"].Rows[0]["imgpath"] = "images/" + savePath;

                string UpdateCommandString = "SELECT * FROM HeadImg";
                SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
                SqlCommand UpdateCommand = new SqlCommand(UpdateCommandString, con);
                SqlDataAdapter UpdateAdapter = new SqlDataAdapter(UpdateCommand);
                SqlCommandBuilder sb = new SqlCommandBuilder(UpdateAdapter);
                if(UpdateAdapter.Update(UserInfoDataSet, "HeadImg") > 0)
                {
                    HeadImg.ImageUrl = UserInfoDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
                }
                
            }
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;

            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();

            String deleteString = "DELETE FROM Collection WHERE ID=@deleteID";
            SqlCommand deleteCommand = new SqlCommand(deleteString, Connection);
            deleteCommand.Transaction = trans;
            deleteCommand.Parameters.Add(new SqlParameter("deleteID", SqlDbType.Int));
            deleteCommand.Parameters["deleteID"].Value = Convert.ToInt32(tempBtn.ID);

            try
            {
                deleteCommand.ExecuteNonQuery();

                trans.Commit();
                Response.Write("<script>alert('删除成功');window.location = 'UserInfo.aspx';</script>");
            }
            catch (Exception ex)
            {
                trans.Rollback();//回滚事务

                Response.Write(ex.ToString());

                Response.Write("删除失败");
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;

            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();

            String deleteString = "DELETE FROM Messages WHERE ID=@deleteID";
            SqlCommand deleteCommand = new SqlCommand(deleteString, Connection);
            deleteCommand.Transaction = trans;
            deleteCommand.Parameters.Add(new SqlParameter("deleteID", SqlDbType.Int));
            deleteCommand.Parameters["deleteID"].Value = Convert.ToInt32(tempBtn.ID);

            try
            {
                deleteCommand.ExecuteNonQuery();

                trans.Commit();
                Response.Write("<script>alert('删除成功');window.location = 'UserInfo.aspx';</script>");
            }
            catch (Exception ex)
            {
                trans.Rollback();//回滚事务

                Response.Write(ex.ToString());

                Response.Write("删除失败");
            }
        }
    }
}