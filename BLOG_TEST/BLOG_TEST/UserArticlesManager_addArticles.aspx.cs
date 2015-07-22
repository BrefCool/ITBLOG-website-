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
    public partial class UserArticlesManager : System.Web.UI.Page
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
        }
        protected void publishBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();
            String articleInsertString = "INSERT INTO Articles (writerID,title,[content],publishtime,readtimes,description,categoryID) VALUES (@writerID,@title,@content,@publishtime,@readtimes,@description,@categoryID)";
            SqlCommand articleInsertCommand = new SqlCommand(articleInsertString, con);
            try
            {
                articleInsertCommand.Parameters.Add(new SqlParameter("writerID", SqlDbType.Int));
                articleInsertCommand.Parameters["writerID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);

                articleInsertCommand.Parameters.Add(new SqlParameter("title", SqlDbType.NVarChar, 50));
                articleInsertCommand.Parameters["title"].Value = (articleTitleTextBox.Text == "") ? null : articleTitleTextBox.Text;

                articleInsertCommand.Parameters.Add(new SqlParameter("content", SqlDbType.NVarChar));
                articleInsertCommand.Parameters["content"].Value = (Request.Form["ctl00$MainContent$blogTextarea"] == "") ? null : Request.Form["ctl00$MainContent$blogTextarea"];

                articleInsertCommand.Parameters.Add(new SqlParameter("publishtime", SqlDbType.DateTime));
                articleInsertCommand.Parameters["publishtime"].Value = DateTime.Now.ToString("yyyy-MM-dd");

                articleInsertCommand.Parameters.Add(new SqlParameter("readtimes", SqlDbType.Int));
                articleInsertCommand.Parameters["readtimes"].Value = 0;

                articleInsertCommand.Parameters.Add(new SqlParameter("description", SqlDbType.NVarChar, 255));
                articleInsertCommand.Parameters["description"].Value = (articleDescriptionTextBox.Text == "") ? null : articleDescriptionTextBox.Text;

                articleInsertCommand.Parameters.Add(new SqlParameter("categoryID", SqlDbType.Int));
                articleInsertCommand.Parameters["categoryID"].Value = Convert.ToInt32(CategoryRadioButtonList.SelectedItem.Value);

                if(articleInsertCommand.ExecuteNonQuery() > 0)
                {
                    Response.Write("<script>alert('发表成功');window.location = 'UserArticlesManager_addArticles.aspx';</script>");
                }
            }
            catch
            { 
                Response.Write("<script>alert(\"发表失败\");</script>");
            }
            

            
            con.Close();
        }
    }
}