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
    public partial class UserBlog_ArticlesDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet UserDataSet = loadUserInfo(Request["par_ArticleID"]);
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();
            if(!IsPostBack)
            {
                int readtimes = Convert.ToInt32(UserDataSet.Tables["Articles"].Rows[0]["readtimes"]);
                readtimes++;
                UserDataSet.Tables["Articles"].Rows[0]["readtimes"] = readtimes;
                if (UserDataSet.HasChanges())
                {
                    string UpdateCommandString = "SELECT * FROM Articles";
                    SqlCommand UpdateCommand = new SqlCommand(UpdateCommandString, con);
                    SqlDataAdapter UpdateAdapter = new SqlDataAdapter(UpdateCommand);
                    SqlCommandBuilder sb = new SqlCommandBuilder(UpdateAdapter);
                    UpdateAdapter.Update(UserDataSet, "Articles");
                }

                if (UserDataSet.Tables["Info"].Rows[0]["introduce"].ToString() != "")
                {
                    userIntroduceLabel.Text = UserDataSet.Tables["Info"].Rows[0]["introduce"].ToString();
                }
                else
                {
                    userIntroduceLabel.Text = "暂无...";
                }
                userNameLabel.Text = UserDataSet.Tables["Users"].Rows[0]["username"].ToString();
                sendMessages.HRef = "UserSendMessages.aspx?par_receiver=" + UserDataSet.Tables["Users"].Rows[0]["username"].ToString(); 
                emailLabel.Text = UserDataSet.Tables["Info"].Rows[0]["email"].ToString();
                qqLabel.Text = UserDataSet.Tables["Info"].Rows[0]["QQ"].ToString();
                userHeadImg.ImageUrl = UserDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
                blogNameLink.InnerText = UserDataSet.Tables["Users"].Rows[0]["username"].ToString() + "的博客";
                blogNameLink.HRef = "UserBlog_ArticlesList.aspx?par_userID=" + UserDataSet.Tables["Users"].Rows[0]["userID"].ToString();

                ArticleTitleLabel.Text = UserDataSet.Tables["Articles"].Rows[0]["title"].ToString();
                Page.Title = UserDataSet.Tables["Articles"].Rows[0]["title"].ToString();
                publishTimeLabel.Text = Convert.ToDateTime(UserDataSet.Tables["Articles"].Rows[0]["publishtime"]).ToString("yyyy-MM-dd");
                readTimesLabel.Text = UserDataSet.Tables["Articles"].Rows[0]["readtimes"].ToString();
                ArticleContentLabel.Text = UserDataSet.Tables["Articles"].Rows[0]["content"].ToString();

                string loadString = "SELECT * FROM Articles WHERE writerID=@userID";
                SqlCommand loadCommand = new SqlCommand(loadString, con);
                loadCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
                loadCommand.Parameters["userID"].Value = UserDataSet.Tables["Users"].Rows[0]["userID"].ToString();
                SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
                DataSet loadDataSet = new DataSet();
                loadAdapter.Fill(loadDataSet,"Articles");
                loadBlog(loadDataSet, hotBlogHolder, "readtimes DESC");
                loadBlog(loadDataSet, latestBlogHolder, "publishtime DESC");

                if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
                {
                    commentsProviderLabel.Text = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"].ToString();
                }
                else
                {
                    commentsProviderLabel.Text = "请先登录";
                    commentDetailBox.Enabled = false;
                }
            }
            if(UserDataSet.Tables["Comments"].Rows.Count != 0)
            {
                for(int i = 0;i < UserDataSet.Tables["Comments"].Rows.Count;i++)
                {
                    string loadString = "SELECT username,headimgID FROM Users WHERE userID=@commenterID";
                    SqlCommand loadCommand1 = new SqlCommand(loadString, con);
                    loadCommand1.Parameters.Add(new SqlParameter("commenterID", SqlDbType.Int));
                    loadCommand1.Parameters["commenterID"].Value = UserDataSet.Tables["Comments"].Rows[i]["commenterID"];
                    SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
                    DataSet tempDataSet = new DataSet();
                    loadAdapter.Fill(tempDataSet,"Users");

                    string loadHeadImgString = "SELECT * FROM HeadImg WHERE ID=@headimgID";
                    SqlCommand loadCommand2 = new SqlCommand(loadHeadImgString, con);
                    loadCommand2.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
                    loadCommand2.Parameters["headimgID"].Value = tempDataSet.Tables["Users"].Rows[0]["headimgID"];
                    SqlDataAdapter HeadimgAdapter = new SqlDataAdapter(loadCommand2);
                    HeadimgAdapter.Fill(tempDataSet, "HeadImg");

                    Label commentInfoLabel = new Label();
                    Image commenterHeadImg = new Image();
                    Label commentContentLabel = new Label();

                    commentInfoLabel.ID = "commentInfo";
                    commentInfoLabel.Text = (i+1).ToString() + "楼  用户" + tempDataSet.Tables["Users"].Rows[0]["username"] + "    " + 
                                            Convert.ToDateTime(UserDataSet.Tables["Comments"].Rows[i]["commentTime"]).ToString("yyyy-MM-dd") + "  ";
                    commentInfoLabel.Font.Size = 10;
                    commentInfoLabel.Height = 20;

                    commenterHeadImg.ID = "commenterHeadImg";
                    commenterHeadImg.ImageUrl = tempDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
                    commenterHeadImg.Height = 64;
                    commenterHeadImg.Width = 64;

                    commentContentLabel.Text = UserDataSet.Tables["Comments"].Rows[i]["content"].ToString();
                    commentContentLabel.ID = "commentContent";
                    commentContentLabel.Style.Add("word-break", "break-all");

                    commentsHolder.Controls.Add(new LiteralControl("<div style=\"width:100%;\">"));
                    commentsHolder.Controls.Add(new LiteralControl("<div class=\"panel\" style=\"width:100%;background-color:#CCCCCC;height:80px;\">"));
                    commentsHolder.Controls.Add(commentInfoLabel);
                    commentsHolder.Controls.Add(new LiteralControl("<a style=\"font-size:10px;\">回复</a>"));
                    commentsHolder.Controls.Add(new LiteralControl("<div style=\"height:70px;\">"));
                    commentsHolder.Controls.Add(new LiteralControl("<table style=\"text-align:left;width:100%;background-color:#ffffff\">"));
                    commentsHolder.Controls.Add(new LiteralControl("<tr>"));
                    commentsHolder.Controls.Add(new LiteralControl("<td style=\"width:70px;\">"));
                    commentsHolder.Controls.Add(commenterHeadImg);
                    commentsHolder.Controls.Add(new LiteralControl("</td>"));
                    commentsHolder.Controls.Add(new LiteralControl("<td>"));
                    commentsHolder.Controls.Add(commentContentLabel);
                    commentsHolder.Controls.Add(new LiteralControl("</td>"));
                    commentsHolder.Controls.Add(new LiteralControl("</tr>"));
                    commentsHolder.Controls.Add(new LiteralControl("</table>"));
                    commentsHolder.Controls.Add(new LiteralControl("</div>"));
                    commentsHolder.Controls.Add(new LiteralControl("</div>"));
                    commentsHolder.Controls.Add(new LiteralControl("</div>"));

                    con.Close();
                }
                 
            }
            else
            {
                Label noArticleLabel = new Label();
                noArticleLabel.ID = "noArticles";
                noArticleLabel.Text = "暂无评论";
                commentsHolder.Controls.Add(noArticleLabel);
            }
        }

        public static DataSet loadUserInfo(string ArticleID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadArticleString = "SELECT * FROM Articles WHERE ID=@ArticleID";

            SqlCommand loadCommand1 = new SqlCommand(loadArticleString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("ArticleID", SqlDbType.Int));
            loadCommand1.Parameters["ArticleID"].Value = Convert.ToInt32(ArticleID);
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            string loadUserString = "SELECT userID,username,headimgID,infoID FROM Users WHERE userID=@writerID";

            SqlCommand loadCommand2 = new SqlCommand(loadUserString, loadConnection);
            loadCommand2.Parameters.Add(new SqlParameter("writerID", SqlDbType.Int));
            loadCommand2.Parameters["writerID"].Value = loadDataSet.Tables["Articles"].Rows[0]["writerID"];
            SqlDataAdapter ArticlesAdapter = new SqlDataAdapter(loadCommand2);
            ArticlesAdapter.Fill(loadDataSet, "Users");

            string loadHeadImgString = "SELECT * FROM HeadImg WHERE ID=@headimgID";

            SqlCommand loadCommand3 = new SqlCommand(loadHeadImgString, loadConnection);
            loadCommand3.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
            loadCommand3.Parameters["headimgID"].Value = loadDataSet.Tables["Users"].Rows[0]["headimgID"];
            SqlDataAdapter HeadimgAdapter = new SqlDataAdapter(loadCommand3);
            HeadimgAdapter.Fill(loadDataSet, "HeadImg");

            string loadUserInfoString = "SELECT * FROM Info WHERE ID=@infoID";

            SqlCommand loadCommand4 = new SqlCommand(loadUserInfoString, loadConnection);
            loadCommand4.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
            loadCommand4.Parameters["infoID"].Value = loadDataSet.Tables["Users"].Rows[0]["infoID"];
            SqlDataAdapter InfoAdapter = new SqlDataAdapter(loadCommand4);
            InfoAdapter.Fill(loadDataSet, "Info");

            string loadArticlesCommentsString = "SELECT * FROM Comments WHERE articleID=@articleID";

            SqlCommand loadCommand5 = new SqlCommand(loadArticlesCommentsString, loadConnection);
            loadCommand5.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            loadCommand5.Parameters["articleID"].Value = Convert.ToInt32(ArticleID);
            SqlDataAdapter CommentAdapter = new SqlDataAdapter(loadCommand5);
            CommentAdapter.Fill(loadDataSet, "Comments");

            loadConnection.Close();
            return loadDataSet;
        }

        protected void PostBtn_Click(object sender, EventArgs e)
        {
            SqlConnection addCommentConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            addCommentConnection.Open();

            string addString = "INSERT INTO Comments (commenterID,articleID,[content],commentTime) VALUES (@commenterID,@articleID,@content,@commentTime)";
            SqlCommand addCommand = new SqlCommand(addString, addCommentConnection);
            addCommand.Parameters.Add(new SqlParameter("commenterID", SqlDbType.Int));
            addCommand.Parameters["commenterID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
            addCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            addCommand.Parameters["articleID"].Value = Convert.ToInt32(Request["par_ArticleID"]);
            addCommand.Parameters.Add(new SqlParameter("content",SqlDbType.NVarChar,255));
            addCommand.Parameters["content"].Value = commentDetailBox.Text;
            addCommand.Parameters.Add(new SqlParameter("commentTime", SqlDbType.DateTime));
            addCommand.Parameters["commentTime"].Value = DateTime.Now;

            addCommand.ExecuteNonQuery();
            addCommentConnection.Close();

            Response.Redirect(Request.Url.ToString());
        }

        public static void loadBlog(DataSet UserDataSet, PlaceHolder BlogHolder, string sort)
        {
            if (UserDataSet.Tables["Articles"].Rows.Count == 0)
            {
                BlogHolder.Controls.Add(new LiteralControl("<p>暂无文章</p>"));
            }
            else
            {
                DataRow[] dr = UserDataSet.Tables["Articles"].Select("", sort);
                for (int i = 0; i < dr.Length; i++)
                {
                    HyperLink articleTitle = new HyperLink();

                    articleTitle.ID = (i + 1).ToString() + "_Link";
                    articleTitle.Text = (i + 1).ToString() + "." + dr[i]["title"].ToString();
                    articleTitle.NavigateUrl = "UserBlog_ArticlesDetails.aspx?par_ArticleID=" + dr[i]["ID"].ToString();
                    articleTitle.Font.Size = 6;
                    articleTitle.Font.Bold = true;

                    BlogHolder.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px dashed;\">"));
                    BlogHolder.Controls.Add(articleTitle);
                    BlogHolder.Controls.Add(new LiteralControl("</div>"));
                }
            }
        }
    }
}