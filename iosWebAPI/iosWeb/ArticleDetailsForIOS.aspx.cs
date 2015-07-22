using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace iosWeb
{
    public partial class ArticleDetailsForIOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadArticleString = "SELECT * FROM Articles WHERE ID=@ArticleID";

            SqlCommand loadCommand1 = new SqlCommand(loadArticleString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("ArticleID", SqlDbType.Int));
            loadCommand1.Parameters["ArticleID"].Value = Request["ArticleID"];
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            ArticleTitleLabel.Text = loadDataSet.Tables["Articles"].Rows[0]["title"].ToString();
            ArticleContentLabel.Text = loadDataSet.Tables["Articles"].Rows[0]["content"].ToString();
        }
    }
}