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
    public partial class Index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (Request["par_cate"] != null)
                {
                    switch (Convert.ToInt32(Request["par_cate"]))
                    {
                        case 1:
                            {
                                articlesContainer.Attributes.Add("class", "panel panel-success");
                                panelTitle.InnerText = "移动开发";
                                loadArticles(articleCategoryHolder, 1);
                                break;
                            }
                        case 2:
                            {
                                articlesContainer.Attributes.Add("class", "panel panel-info");
                                panelTitle.InnerText = "编程语言";
                                loadArticles(articleCategoryHolder, 2);
                                break;
                            }
                        case 3:
                            {
                                articlesContainer.Attributes.Add("class", "panel panel-warning");
                                panelTitle.InnerText = "web开发";
                                loadArticles(articleCategoryHolder, 3);
                                break;
                            }
                        case 4:
                            {
                                articlesContainer.Attributes.Add("class", "panel panel-danger");
                                panelTitle.InnerText = "系统运维";
                                loadArticles(articleCategoryHolder, 4);
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    articlesContainer.Attributes.Add("class", "panel panel-primary");
                    panelTitle.InnerText = "全部分类";
                    loadArticles(articleCategoryHolder, 0);
                }
            
        }

        public static void loadArticles(PlaceHolder articlesHolder,int cate)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Articles WHERE categoryID=@cate ORDER BY readtimes DESC";
            if(cate == 0)
            {
                loadString = "SELECT * FROM Articles ORDER BY readtimes DESC";
            }
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            if(cate != 0)
            {
                loadCommand.Parameters.Add(new SqlParameter("cate", SqlDbType.Int));
                loadCommand.Parameters["cate"].Value = cate;
            }
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            for(int i = 0;i < loadDataSet.Tables["Articles"].Rows.Count;i++ )
            {
                string loadcomNumber = "SELECT COUNT(*) FROM Comments WHERE articleID=@articleID";
                SqlCommand Com = new SqlCommand(loadcomNumber, loadConnection);
                Com.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
                Com.Parameters["articleID"].Value = loadDataSet.Tables["Articles"].Rows[i]["ID"];

                Panel newArticlePanel = new Panel();
                HyperLink ArticleTitle = new HyperLink();
                Label publishTime = new Label();
                Label ArticleContent = new Label();
                Label readTimes = new Label();
                Label commentsTimes = new Label();

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
                commentsTimes.Text = Com.ExecuteScalar().ToString();

                string categoryString = "";
                switch(Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["categoryID"]))
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

                articlesHolder.Controls.Add(newArticlePanel);
            }
        }
    }
}