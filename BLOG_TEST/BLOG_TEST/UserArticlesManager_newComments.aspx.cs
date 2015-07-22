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
    public partial class UserArticlesManager_newComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userNameLabel.Text = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"];
            Page.Title = HttpContext.Current.Request.Cookies["UserInfo"].Values["username"] + "的文章管理";
            toMyBlog.HRef = "UserBlog_ArticlesList.aspx?par_userID=" + HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"];

            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();

            String imgIDLoadString = "SELECT headimgID FROM Users WHERE userID=@userID";
            SqlCommand imgIDLoadCommand = new SqlCommand(imgIDLoadString, con);
            imgIDLoadCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            imgIDLoadCommand.Parameters["userID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);

            int headimgID = 0;
            headimgID = Convert.ToInt32(imgIDLoadCommand.ExecuteScalar());

            String HeadImgLoadString = "SELECT imgpath FROM HeadImg WHERE ID=@headimgID";
            SqlCommand HeadImgLoadCommand = new SqlCommand(HeadImgLoadString, con);
            HeadImgLoadCommand.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
            HeadImgLoadCommand.Parameters["headimgID"].Value = headimgID;

            userHeadImg.ImageUrl = HeadImgLoadCommand.ExecuteScalar().ToString();

            DataSet loadDataSet = loadComments(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
            if (loadDataSet.Tables["Comments"] == null)
            {
                viewMyComments.Controls.Add(new LiteralControl("<div style=\"width: 80%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; \" >"));
                viewMyComments.Controls.Add(new LiteralControl("<h1>暂无评论</h1>"));
                viewMyComments.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                for (int i = 0; i < loadDataSet.Tables["Comments"].Rows.Count; i++)
                {
                    HyperLink commenterName = new HyperLink();
                    HyperLink articleTitle = new HyperLink();
                    Label commentContent = new Label();

                    String loadCommenter = "SELECT username FROM Users WHERE userID=@commenterID";
                    SqlCommand loadCommenterCom = new SqlCommand(loadCommenter,con);
                    loadCommenterCom.Parameters.Add(new SqlParameter("commenterID", SqlDbType.Int));
                    loadCommenterCom.Parameters["commenterID"].Value = Convert.ToInt32(loadDataSet.Tables["Comments"].Rows[i]["commenterID"]);

                    String loadarticleTitle = "SELECT title FROM Articles WHERE ID=@articleID";
                    SqlCommand loadarticleTitleCom = new SqlCommand(loadarticleTitle, con);
                    loadarticleTitleCom.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
                    loadarticleTitleCom.Parameters["articleID"].Value = Convert.ToInt32(loadDataSet.Tables["Comments"].Rows[i]["articleID"]);

                    commenterName.ID = loadDataSet.Tables["Comments"].Rows[i]["commenterID"].ToString();
                    commenterName.Text = loadCommenterCom.ExecuteScalar().ToString();
                    commenterName.NavigateUrl = "UserBlog_ArticlesList.aspx?par_userID=" + loadDataSet.Tables["Comments"].Rows[i]["commenterID"].ToString();

                    articleTitle.ID = loadDataSet.Tables["Comments"].Rows[i]["articleID"].ToString();
                    articleTitle.Text = loadarticleTitleCom.ExecuteScalar().ToString();
                    articleTitle.NavigateUrl = "UserBlog_ArticlesDetails.aspx?par_ArticleID=" + loadDataSet.Tables["Comments"].Rows[i]["articleID"].ToString();

                    commentContent.ID = loadDataSet.Tables["Comments"].Rows[i]["articleID"].ToString() + "_Content";
                    commentContent.Text = loadDataSet.Tables["Comments"].Rows[i]["content"].ToString();

                    viewMyComments.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px solid #CCCCCC ;\">"));
                    viewMyComments.Controls.Add(new LiteralControl("<ul class=\"list-unstyled list-inline\" style=\"text-align:left;\">"));
                    viewMyComments.Controls.Add(new LiteralControl("<li><p style=\"font-size:12px;\">用户</p></li>"));
                    viewMyComments.Controls.Add(new LiteralControl("<li>"));
                    viewMyComments.Controls.Add(commenterName);
                    viewMyComments.Controls.Add(new LiteralControl("</li>"));
                    viewMyComments.Controls.Add(new LiteralControl("<li><p style=\"font-size:12px;\"> 在文章</p></li>"));
                    viewMyComments.Controls.Add(new LiteralControl("<li>"));
                    viewMyComments.Controls.Add(articleTitle);
                    viewMyComments.Controls.Add(new LiteralControl("</li>"));
                    viewMyComments.Controls.Add(new LiteralControl("<li><p style=\"font-size:12px;\"> 中评论道</p></li>"));
                    viewMyComments.Controls.Add(new LiteralControl("</ul>"));
                    viewMyComments.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-right:10px;margin-bottom:3px;background-color:#eeeeee\">"));
                    viewMyComments.Controls.Add(commentContent);
                    viewMyComments.Controls.Add(new LiteralControl("</div>"));
                    viewMyComments.Controls.Add(new LiteralControl("</div>"));
            }
            }
            
        }

        public static DataSet loadComments(string userID)
        {
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();

            String getArticlesString = "SELECT ID,title FROM Articles WHERE writerID=@userID";
            SqlCommand getArticlesCommand = new SqlCommand(getArticlesString, con);
            getArticlesCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            getArticlesCommand.Parameters["userID"].Value = Convert.ToInt32(userID);
            SqlDataAdapter getArticlesAdapter = new SqlDataAdapter(getArticlesCommand);
            DataSet loadDataSet = new DataSet();
            getArticlesAdapter.Fill(loadDataSet, "Articles");
            
            String loadString = "SELECT * FROM Comments WHERE articleID=@articleID";

            for (int i = 0; i < loadDataSet.Tables["Articles"].Rows.Count; i++)
            {
                SqlCommand loadCommand = new SqlCommand(loadString, con);
                loadCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
                loadCommand.Parameters["articleID"].Value = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["ID"]);
                SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
                loadAdapter.Fill(loadDataSet, "Comments");
            }

            return loadDataSet;
        }
    }
}