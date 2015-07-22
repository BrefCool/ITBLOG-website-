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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            //    ; Page.Title = Page.Header.Title + "--ITBLOG";
            
            //TitleTextBox.Text = Page.Header.Title;
            //TitleTextBox.Enabled = false;
            //webTextBox.Text = Request.Url.ToString();
            //webTextBox.Enabled = false;
            //}
            

            //if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
            //{
            //    welcomeUserLabel.Text = "欢迎，" + HttpContext.Current.Request.Cookies["UserInfo"].Values["username"].ToString();
            //}
            //else
            //{
            //    if(!IsPostBack)
            //    {
            //        string[] yearBooks = { "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989",
            //               "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999","2000"};
            //        string[] monthBooks = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

            //        for (int i = 0; i < yearBooks.GetLength(0); i++)
            //        {
            //            YearDropDownList.Items.Add(new ListItem(yearBooks[i]));
            //        }

            //        for (int i = 0; i < monthBooks.GetLength(0); i++)
            //        {
            //            MonthDropDownList.Items.Add(new ListItem(monthBooks[i]));
            //        }
            //    } 
            //    else
            //    {
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "myscript", "<script>window.onload=function(){showmodal();}</script>");
            //    }   
            //}
           
        }

        //public static bool trySignIn(string username,string password)
        //{
        //    SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

        //    con.Open();

        //    string sqlSel = "Select userID from Users where username=@username and password=@userpwd";
            

        //    SqlCommand com = new SqlCommand(sqlSel, con);

        //    com.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar, 50));
        //    com.Parameters["username"].Value = username;
        //    com.Parameters.Add(new SqlParameter("userpwd", SqlDbType.NVarChar, 10));
        //    com.Parameters["userpwd"].Value = password;

        //    SqlDataAdapter signAdapter = new SqlDataAdapter(com);
        //    DataSet signDataSet = new DataSet();
        //    signAdapter.Fill(signDataSet,"Users");

        //    if (signDataSet.Tables["Users"].Rows.Count > 0)
        //    {
        //        HttpContext.Current.Session.Abandon();
        //        FormsAuthenticationTicket t = new FormsAuthenticationTicket(1, username,
        //            DateTime.Now, DateTime.Now.AddMonths(3),
        //            false, username,
        //            FormsAuthentication.FormsCookiePath);
        //        string encTicket = FormsAuthentication.Encrypt(t);
        //        HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //        HttpContext.Current.Response.Cookies.Add(c);

        //        HttpCookie userCookie = new HttpCookie("UserInfo");
        //        userCookie.Values["username"] = username;
        //        userCookie.Values["userID"] =signDataSet.Tables["Users"].Rows[0]["userID"].ToString();
        //        HttpContext.Current.Response.Cookies.Add(userCookie);

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //protected void SignButton_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
        //    con.Open();
        //    SqlTransaction trans = con.BeginTransaction();

        //    SqlCommand command1 = new SqlCommand("addInfo", con);
        //    command1.Transaction = trans;
        //    command1.CommandType = CommandType.StoredProcedure;

        //    SqlCommand command2 = new SqlCommand("addHeadimg", con);
        //    command2.Transaction = trans;
        //    command2.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        command1.Parameters.Add(new SqlParameter("sex", SqlDbType.Bit));
        //        command1.Parameters["sex"].Value = Convert.ToInt32(SexRadioButtonList.SelectedItem.Value);
        //        command1.Parameters["sex"].Direction = ParameterDirection.Input;

        //        command1.Parameters.Add(new SqlParameter("birthday", SqlDbType.DateTime));
        //        string dateString = YearDropDownList.SelectedItem.Text + MonthDropDownList.SelectedItem.Text;
        //        DateTime dt1 = DateTime.ParseExact(dateString, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);
        //        command1.Parameters["birthday"].Value = dt1;
        //        command1.Parameters["birthday"].Direction = ParameterDirection.Input;

        //        command1.Parameters.Add(new SqlParameter("email", SqlDbType.NVarChar, 50));
        //        command1.Parameters["email"].Value = EmailTextBox.Text;
        //        command1.Parameters["email"].Direction = ParameterDirection.Input;

        //        command1.Parameters.Add(new SqlParameter("qq", SqlDbType.NVarChar, 50));
        //        command1.Parameters["qq"].Value = QQTextBox.Text;
        //        command1.Parameters["qq"].Direction = ParameterDirection.Input;

        //        command1.Parameters.Add(new SqlParameter("signtime", SqlDbType.DateTime));
        //        DateTime dt2 = DateTime.Now;
        //        command1.Parameters["signtime"].Value = dt2;
        //        command1.Parameters["signtime"].Direction = ParameterDirection.Input;

        //        command1.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
        //        command1.Parameters["infoID"].Direction = ParameterDirection.Output;

        //        command1.ExecuteNonQuery();

        //        command2.Parameters.Add(new SqlParameter("imgPath", SqlDbType.NVarChar, 255));
        //        command2.Parameters["imgPath"].Value = "images/default_user.png";
        //        command2.Parameters["imgPath"].Direction = ParameterDirection.Input;

        //        command2.Parameters.Add(new SqlParameter("imgID", SqlDbType.Int));
        //        command2.Parameters["imgID"].Direction = ParameterDirection.Output;

        //        command2.ExecuteNonQuery();

        //        string commandString = "INSERT INTO Users (username,password,headimgID,infoID,rgtime,bloghot) VALUES (@username,@password,@headimgID,@infoID,@rgtime,@bloghot)";
        //        SqlCommand command3 = new SqlCommand(commandString, con);
        //        command3.Transaction = trans;

        //        command3.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar, 10));
        //        command3.Parameters["username"].Value = UsernameTextBox.Text;

        //        command3.Parameters.Add(new SqlParameter("password", SqlDbType.NVarChar, 16));
        //        command3.Parameters["password"].Value = PasswordTextBox.Text;

        //        command3.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
        //        command3.Parameters["headimgID"].Value = command2.Parameters["imgID"].Value;

        //        command3.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
        //        command3.Parameters["infoID"].Value = command1.Parameters["infoID"].Value;

        //        command3.Parameters.Add(new SqlParameter("rgtime", SqlDbType.DateTime));
        //        DateTime dt3 = DateTime.Now;
        //        command3.Parameters["rgtime"].Value = dt3;

        //        command3.Parameters.Add(new SqlParameter("bloghot", SqlDbType.Int));
        //        command3.Parameters["bloghot"].Value = 0;

        //        command3.ExecuteNonQuery();

        //        trans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();//回滚事务

        //        Response.Write(ex.ToString());

        //        Response.Write("Neither record was written to database.注册失败");
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void LoginButton_Click(object sender, EventArgs e)
        //{
        //    string username = Username.Text;
        //    string password = Password.Text;

        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        return;
        //    }

        //    if (trySignIn(username, password))
        //    {
        //        string returnUrl = Request.Url.ToString();
        //        Response.Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        FailureText.Text = "用户名不存在或密码错误";
        //        FailureHolder.Visible = true;
        //    }
        //}

        //protected void collectButton_Click(object sender, EventArgs e)
        //{
        //    SqlConnection insertConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
        //    insertConnection.Open();

        //    string insertString = "INSERT INTO Collection (hostID,title,webpath,description) VALUES (@hostID,@title,@webpath,@description)";
        //    SqlCommand insertCommand = new SqlCommand(insertString, insertConnection);

        //    insertCommand.Parameters.Add(new SqlParameter("hostID", SqlDbType.Int));
        //    insertCommand.Parameters["hostID"].Value = Convert.ToInt32(HttpContext.Current.Request.Cookies["UserInfo"].Values["userID"]);

        //    insertCommand.Parameters.Add(new SqlParameter("title", SqlDbType.NVarChar, 255));
        //    insertCommand.Parameters["title"].Value = TitleTextBox.Text;

        //    insertCommand.Parameters.Add(new SqlParameter("webpath", SqlDbType.NVarChar, 255));
        //    insertCommand.Parameters["webpath"].Value = webTextBox.Text;

        //    insertCommand.Parameters.Add(new SqlParameter("description", SqlDbType.NVarChar, 255));
        //    insertCommand.Parameters["description"].Value =  descriptionTextBox.Text;

        //    if(insertCommand.ExecuteNonQuery() > 0)
        //    {
        //        Response.Write("<script>alert('收藏成功');window.location = '"+ Request.Url.ToString() + "';</script>");
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert(\"收藏失败\");</script>");
        //    }
        //}

    }
}