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
    public partial class UserArticlesManager_myArticles : System.Web.UI.Page
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

            DataSet loadDataSet = loadMyArticles(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);
            if (loadDataSet.Tables["Articles"].Rows.Count == 0)
            {
                viewMyArticles.Controls.Add(new LiteralControl("<div style=\"width: 80%; margin-left: auto; margin-right: auto; text-align: center; background: url(images/tomyBlog_background.PNG) no-repeat; height:140px; \" >"));
                viewMyArticles.Controls.Add(new LiteralControl("<h2>还没有文章，赶快去写吧...</h2>"));
                viewMyArticles.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                for (int i = 0; i < loadDataSet.Tables["Articles"].Rows.Count; i++)
                {
                    Panel newArticlePanel = new Panel();
                    HyperLink ArticleTitle = new HyperLink();
                    Label ArticleContent = new Label();
                    Label publishTime = new Label();
                    Label readTimes = new Label();
                    Label commentsTimes = new Label();
                    Button deleteBtn = new Button();

                    newArticlePanel.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "_Panel";

                    ArticleTitle.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "Link";
                    ArticleTitle.Font.Bold = true;
                    ArticleTitle.Font.Size = 25;
                    ArticleTitle.Text = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString();
                    ArticleTitle.NavigateUrl = "UserBlog_ArticlesDetails.aspx?par_ArticleID=" + loadDataSet.Tables["Articles"].Rows[i]["ID"].ToString();

                    ArticleContent.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "_Content";
                    ArticleContent.Font.Size = 12;
                    ArticleContent.ForeColor = System.Drawing.Color.FromName("#999999");
                    ArticleContent.Text = loadDataSet.Tables["Articles"].Rows[i]["description"].ToString();

                    publishTime.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "PublishTimeLabel";
                    publishTime.Font.Size = 10;
                    publishTime.Text = Convert.ToDateTime(loadDataSet.Tables["Articles"].Rows[i]["publishtime"]).ToString("yyyy-MM-dd");

                    readTimes.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "ReadTimesLabel";
                    readTimes.Font.Size = 10;
                    readTimes.Text = loadDataSet.Tables["Articles"].Rows[i]["readtimes"].ToString();

                    commentsTimes.ID = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString() + "CommentsTimesLabel";
                    commentsTimes.Font.Size = 10;
                    commentsTimes.Text = "0";

                    deleteBtn.ID = loadDataSet.Tables["Articles"].Rows[i]["ID"].ToString();
                    deleteBtn.CssClass = "btn btn-danger";
                    deleteBtn.Text = "删除";
                    deleteBtn.OnClientClick = "return confirm('确认删除？')";
                    deleteBtn.Click += deleteBtn_Click;

                    newArticlePanel.Controls.Add(new LiteralControl("<div style=\"margin-left:10px;margin-top:20px;margin-right:10px;border-bottom:1px solid #CCCCCC ;\">"));
                    newArticlePanel.Controls.Add(ArticleTitle);
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
                    newArticlePanel.Controls.Add(new LiteralControl("<li>"));
                    newArticlePanel.Controls.Add(deleteBtn);
                    newArticlePanel.Controls.Add(new LiteralControl("</li>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</ul>"));
                    newArticlePanel.Controls.Add(new LiteralControl("</div>"));

                    viewMyArticles.Controls.Add(newArticlePanel);
                }
            }
        }

        public static DataSet loadMyArticles(string userID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Articles WHERE writerID=@userID";
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            loadCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            loadCommand.Parameters["userID"].Value = Convert.ToInt32(userID);
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            return loadDataSet;
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            Button tempBtn = (Button)sender;

            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();

            String deleteArticleString = "DELETE FROM Articles WHERE ID=@articleID";
            SqlCommand deleteArticleCommand = new SqlCommand(deleteArticleString, Connection);
            deleteArticleCommand.Transaction = trans;
            deleteArticleCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            deleteArticleCommand.Parameters["articleID"].Value = Convert.ToInt32(tempBtn.ID);

            String deleteCommentsString = "DELETE FROM Comments WHERE articleID=@articleID";
            SqlCommand deleteCommentsCommand = new SqlCommand(deleteCommentsString, Connection);
            deleteCommentsCommand.Transaction = trans;
            deleteCommentsCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            deleteCommentsCommand.Parameters["articleID"].Value = Convert.ToInt32(tempBtn.ID);

            try
            {
                deleteCommentsCommand.ExecuteNonQuery();
                deleteArticleCommand.ExecuteNonQuery();

                trans.Commit();
                Response.Write("<script>alert('删除成功');window.location = 'UserArticlesManager_myArticles.aspx';</script>");
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