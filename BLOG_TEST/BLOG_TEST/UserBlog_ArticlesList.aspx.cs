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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet UserDataSet = loadUserInfo(Request["par_userID"]);

            userNameLabel.Text = UserDataSet.Tables["Users"].Rows[0]["username"].ToString();
            sendMessages.HRef = "UserSendMessages.aspx?par_receiver=" + UserDataSet.Tables["Users"].Rows[0]["username"].ToString();
            Page.Title = UserDataSet.Tables["Users"].Rows[0]["username"].ToString() + "的博客";
            if (UserDataSet.Tables["Info"].Rows[0]["introduce"].ToString() != "")
            {
                userIntroduceLabel.Text = UserDataSet.Tables["Info"].Rows[0]["introduce"].ToString();
            }
            else
            {
                userIntroduceLabel.Text = "暂无...";
            }
            emailLabel.Text = UserDataSet.Tables["Info"].Rows[0]["email"].ToString();
            qqLabel.Text = UserDataSet.Tables["Info"].Rows[0]["QQ"].ToString();
            userHeadImg.ImageUrl = UserDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString();
            blogNameLink.InnerText = UserDataSet.Tables["Users"].Rows[0]["username"].ToString() + "的博客";
            blogNameLink.HRef = "UserBlog_ArticlesList.aspx?par_userID=" + Request["par_userID"];
            webTitle.InnerText = UserDataSet.Tables["Users"].Rows[0]["username"].ToString() + "的博客";

            loadArticles(UserDataSet, mainContentHolder);
            loadBlog(UserDataSet, hotBlogHolder, "readtimes DESC");
            loadBlog(UserDataSet, latestBlogHolder, "publishtime DESC");
        }

        public static DataSet loadUserInfo(string userID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadString = "SELECT username,headimgID,infoID FROM Users WHERE userID=@userID";

            SqlCommand loadCommand1 = new SqlCommand(loadString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            loadCommand1.Parameters["userID"].Value = Convert.ToInt32(userID);
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Users");

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

            string loadUserArticleString = "SELECT * FROM Articles WHERE writerID=@userID";

            SqlCommand loadCommand4 = new SqlCommand(loadUserArticleString, loadConnection);
            loadCommand4.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            loadCommand4.Parameters["userID"].Value = Convert.ToInt32(userID);
            SqlDataAdapter ArticlesAdapter = new SqlDataAdapter(loadCommand4);
            ArticlesAdapter.Fill(loadDataSet, "Articles");

            return loadDataSet;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "myscript", "<script>window.onload=function(){showmodal();}</script>");
        }

        public static void loadArticles(DataSet UserDataSet,PlaceHolder mainContentHolder)
        {
            if (UserDataSet.Tables["Articles"].Rows.Count == 0)
            {
                mainContentHolder.Controls.Add(new LiteralControl("<div style=\"width: 90%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; \" >"));
                mainContentHolder.Controls.Add(new LiteralControl("<h2>博主还没有写任何文章...</h2>"));
                mainContentHolder.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                for (int i = 0; i < UserDataSet.Tables["Articles"].Rows.Count; i++)
                {
                    SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
                    con.Open();

                    string loadcomNumber = "SELECT COUNT(*) FROM Comments WHERE articleID=@articleID";
                    SqlCommand Com = new SqlCommand(loadcomNumber, con);
                    Com.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
                    Com.Parameters["articleID"].Value = UserDataSet.Tables["Articles"].Rows[i]["ID"];

                    Panel newArticlePanel = new Panel();
                    HyperLink ArticleTitle = new HyperLink();
                    Label publishTime = new Label();
                    Label ArticleContent = new Label();
                    Label readTimes = new Label();
                    Label commentsTimes = new Label();

                    newArticlePanel.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "_Panel";

                    ArticleTitle.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "Link";
                    ArticleTitle.Font.Bold = true;
                    ArticleTitle.Font.Size = 25;
                    ArticleTitle.Text = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString();
                    ArticleTitle.NavigateUrl = "UserBlog_ArticlesDetails.aspx?par_ArticleID=" + UserDataSet.Tables["Articles"].Rows[i]["ID"].ToString();

                    ArticleContent.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "_Content";
                    ArticleContent.Font.Size = 12;
                    ArticleContent.ForeColor = System.Drawing.Color.FromName("#999999");
                    ArticleContent.Text = UserDataSet.Tables["Articles"].Rows[i]["description"].ToString();

                    publishTime.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "PublishTimeLabel";
                    publishTime.Font.Size = 10;
                    publishTime.Text = Convert.ToDateTime(UserDataSet.Tables["Articles"].Rows[i]["publishtime"]).ToString("yyyy-MM-dd");

                    readTimes.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "ReadTimesLabel";
                    readTimes.Font.Size = 10;
                    readTimes.Text = UserDataSet.Tables["Articles"].Rows[i]["readtimes"].ToString();

                    commentsTimes.ID = UserDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "CommentsTimesLabel";
                    commentsTimes.Font.Size = 10;
                    commentsTimes.Text = Com.ExecuteScalar().ToString();

                    string categoryString = "";
                    switch (Convert.ToInt32(UserDataSet.Tables["Articles"].Rows[i]["categoryID"]))
                    {
                        case 1:
                            {
                                categoryString = "<span style=\"margin-left:5px;\" class=\"label label-success\">移动开发</span>";
                                break;
                            }
                        case 2:
                            {
                                categoryString = "<span style=\"margin-left:5px;\" class=\"label label-info\">编程语言</span>";
                                break;
                            }
                        case 3:
                            {
                                categoryString = "<span style=\"margin-left:5px;\" class=\"label label-warning\">web开发</span>";
                                break;
                            }
                        case 4:
                            {
                                categoryString = "<span style=\"margin-left:5px;\" class=\"label label-danger\">系统运维</span>";
                                break;
                            }
                        default: break;
                    }

                    newArticlePanel.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px dashed;\">"));
                    newArticlePanel.Controls.Add(ArticleTitle);
                    newArticlePanel.Controls.Add(new LiteralControl(categoryString));
                    newArticlePanel.Controls.Add(new LiteralControl("<br/>"));
                    newArticlePanel.Controls.Add(ArticleContent);
                    newArticlePanel.Controls.Add(new LiteralControl("<ul class=\"list-unstyled list-inline\" style=\"text-align:right;\">"));
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(publishTime);
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<p style=\"font-size:10px;\">阅读</p>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(readTimes);
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<p style=\"font-size:10px;\">评论</p>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(commentsTimes);
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</ul>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</div>"));

                    mainContentHolder.Controls.Add(newArticlePanel);

                }
            }
        }

        public static void loadBlog(DataSet UserDataSet,PlaceHolder BlogHolder,string sort)
        {
            if(UserDataSet.Tables["Articles"].Rows.Count == 0)
            {
                BlogHolder.Controls.Add(new LiteralControl("<p>暂无文章</p>"));
            }
            else
            {
                DataRow[] dr = UserDataSet.Tables["Articles"].Select("",sort);
                for(int i = 0;i < dr.Length;i++)
                {
                    HyperLink articleTitle = new HyperLink();

                    articleTitle.ID = (i+1).ToString() + "_Link";
                    articleTitle.Text = (i+1).ToString() + "." + dr[i]["title"].ToString();
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