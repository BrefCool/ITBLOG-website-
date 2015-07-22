using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace iosWeb
{
    public class blogDataOperate : IblogDataOperate
    {
        public IEnumerable<Articles> getAllArticlesByCategory(int categoryID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Articles WHERE categoryID=@cate ORDER BY readtimes DESC";
            if (categoryID == 0)
            {
                loadString = "SELECT * FROM Articles ORDER BY readtimes DESC";
            }
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            if (categoryID != 0)
            {
                loadCommand.Parameters.Add(new SqlParameter("cate", SqlDbType.Int));
                loadCommand.Parameters["cate"].Value = categoryID;
            }
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            Articles[] articles = new Articles[loadDataSet.Tables["Articles"].Rows.Count];

                for (int i = 0; i < loadDataSet.Tables["Articles"].Rows.Count; i++)
                {
                    articles[i] = new Articles()
                    {
                        categoryID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["categoryID"]),
                        ID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["ID"]),
                        publishtime = Convert.ToDateTime(loadDataSet.Tables["Articles"].Rows[i]["publishtime"]),
                        readtimes = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["readtimes"]),
                        title = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString(),
                        writerID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["writerID"]),
                        content = "",
                        description = loadDataSet.Tables["Articles"].Rows[i]["description"].ToString()
                    };
                }
            return articles;
        }

        public Articles loadArticleByArticleID(int articleID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadArticleString = "SELECT * FROM Articles WHERE ID=@ArticleID";

            SqlCommand loadCommand1 = new SqlCommand(loadArticleString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("ArticleID", SqlDbType.Int));
            loadCommand1.Parameters["ArticleID"].Value = articleID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            Articles article = new Articles()
            {
                categoryID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[0]["categoryID"]),
                ID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[0]["ID"]),
                publishtime = Convert.ToDateTime(loadDataSet.Tables["Articles"].Rows[0]["publishtime"]),
                readtimes = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[0]["readtimes"]),
                title = loadDataSet.Tables["Articles"].Rows[0]["title"].ToString(),
                writerID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[0]["writerID"]),
                content = loadDataSet.Tables["Articles"].Rows[0]["content"].ToString(),
                description = loadDataSet.Tables["Articles"].Rows[0]["description"].ToString()
            };

            return article;
        }

        public IEnumerable<Articles> loadArticlesBywriterID(int writerID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Articles WHERE writerID=@writerID ORDER BY readtimes DESC";
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            loadCommand.Parameters.Add(new SqlParameter("writerID", SqlDbType.Int));
            loadCommand.Parameters["writerID"].Value = writerID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Articles");

            Articles[] articles = new Articles[loadDataSet.Tables["Articles"].Rows.Count];

            for (int i = 0; i < loadDataSet.Tables["Articles"].Rows.Count; i++)
            {
                articles[i] = new Articles()
                {
                    categoryID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["categoryID"]),
                    ID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["ID"]),
                    publishtime = Convert.ToDateTime(loadDataSet.Tables["Articles"].Rows[i]["publishtime"]),
                    readtimes = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["readtimes"]),
                    title = loadDataSet.Tables["Articles"].Rows[i]["title"].ToString(),
                    writerID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[i]["writerID"]),
                    content = loadDataSet.Tables["Articles"].Rows[i]["content"].ToString(),
                    description = loadDataSet.Tables["Articles"].Rows[i]["description"].ToString()
                };
            }
            return articles;
        }

        public bool deleteArticle(int articleID)
        {
            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();
            bool flag = false;


            String deleteArticleString = "DELETE FROM Articles WHERE ID=@articleID";
            SqlCommand deleteArticleCommand = new SqlCommand(deleteArticleString, Connection);
            deleteArticleCommand.Transaction = trans;
            deleteArticleCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            deleteArticleCommand.Parameters["articleID"].Value = articleID;

            String deleteCommentsString = "DELETE FROM Comments WHERE articleID=@articleID";
            SqlCommand deleteCommentsCommand = new SqlCommand(deleteCommentsString, Connection);
            deleteCommentsCommand.Transaction = trans;
            deleteCommentsCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            deleteCommentsCommand.Parameters["articleID"].Value = articleID;

            try
            {
                if(deleteCommentsCommand.ExecuteNonQuery() > 0 && deleteArticleCommand.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }

                trans.Commit();
                return flag;
            }
            catch
            {
                trans.Rollback();//回滚事务
                return false;
            }
        }

        public IEnumerable<Comments> getAllCommentsByArticleID(int articleID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadArticlesCommentsString = "SELECT * FROM Comments WHERE articleID=@articleID";

            SqlCommand loadCommand5 = new SqlCommand(loadArticlesCommentsString, loadConnection);
            loadCommand5.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            loadCommand5.Parameters["articleID"].Value = articleID;
            SqlDataAdapter CommentAdapter = new SqlDataAdapter(loadCommand5);
            DataSet loadDataSet = new DataSet();
            CommentAdapter.Fill(loadDataSet, "Comments");
            
            Comments[] comments = new Comments[loadDataSet.Tables["Comments"].Rows.Count];

            for (int i = 0; i < loadDataSet.Tables["Comments"].Rows.Count; i++)
            {
                comments[i] = new Comments()
                {
                    ID = Convert.ToInt32(loadDataSet.Tables["Comments"].Rows[i]["ID"]),
                    articleID = Convert.ToInt32(loadDataSet.Tables["Comments"].Rows[i]["articleID"]),
                    commenterID = Convert.ToInt32(loadDataSet.Tables["Comments"].Rows[i]["commenterID"]),
                    commentTime = Convert.ToDateTime(loadDataSet.Tables["Comments"].Rows[i]["commentTime"]),
                    content = loadDataSet.Tables["Comments"].Rows[i]["content"].ToString()
                };
            }
            return comments;
        }

        public bool addComment(Comments newComment)
        {
            bool flag;
            SqlConnection addCommentConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            addCommentConnection.Open();

            string addString = "INSERT INTO Comments (commenterID,articleID,[content],commentTime) VALUES (@commenterID,@articleID,@content,@commentTime)";
            SqlCommand addCommand = new SqlCommand(addString, addCommentConnection);
            addCommand.Parameters.Add(new SqlParameter("commenterID", SqlDbType.Int));
            addCommand.Parameters["commenterID"].Value = newComment.commenterID;
            addCommand.Parameters.Add(new SqlParameter("articleID", SqlDbType.Int));
            addCommand.Parameters["articleID"].Value = newComment.articleID;
            addCommand.Parameters.Add(new SqlParameter("content", SqlDbType.NVarChar, 255));
            addCommand.Parameters["content"].Value = newComment.content;
            addCommand.Parameters.Add(new SqlParameter("commentTime", SqlDbType.DateTime));
            addCommand.Parameters["commentTime"].Value = newComment.commentTime;

            if (addCommand.ExecuteNonQuery() > 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            addCommentConnection.Close();
            return flag;
        }

        public IEnumerable<Collection> getAllCollectionsByHostID(int hostID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Collection WHERE hostID=@hostID ";
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            loadCommand.Parameters.Add(new SqlParameter("hostID", SqlDbType.Int));
            loadCommand.Parameters["hostID"].Value = hostID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Collection");

            Collection[] collection = new Collection[loadDataSet.Tables["Collection"].Rows.Count];

            for (int i = 0; i < loadDataSet.Tables["Collection"].Rows.Count; i++)
            {
                collection[i] = new Collection()
                {
                    ID = Convert.ToInt32(loadDataSet.Tables["Collection"].Rows[i]["ID"]),
                    description = loadDataSet.Tables["Collection"].Rows[i]["description"].ToString(),
                    webpath = loadDataSet.Tables["Collection"].Rows[i]["webpath"].ToString(),
                    hostID = Convert.ToInt32(loadDataSet.Tables["Collection"].Rows[i]["hostID"]),
                    articleID = Convert.ToInt32(loadDataSet.Tables["Collection"].Rows[i]["articleID"]),
                    title = loadDataSet.Tables["Collection"].Rows[i]["title"].ToString()
                };
            }
            return collection;
        }

        public bool deleteCollection(int ID)
        {
            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();
            bool flag = false;

            String deleteString = "DELETE FROM Collection WHERE ID=@deleteID";
            SqlCommand deleteCommand = new SqlCommand(deleteString, Connection);
            deleteCommand.Transaction = trans;
            deleteCommand.Parameters.Add(new SqlParameter("deleteID", SqlDbType.Int));
            deleteCommand.Parameters["deleteID"].Value = ID;

            try
            {
                if (deleteCommand.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }

                trans.Commit();
                return flag;
            }
            catch
            {
                trans.Rollback();//回滚事务

                return false;
            }
        }

        public IEnumerable<Messages> getAllMessagesByUser(int userID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            loadConnection.Open();

            string loadString = "SELECT * FROM Messages WHERE receiverID=@userID ";
            SqlCommand loadCommand = new SqlCommand(loadString, loadConnection);
            loadCommand.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            loadCommand.Parameters["userID"].Value = userID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Messages");

            Messages[] messages = new Messages[loadDataSet.Tables["Messages"].Rows.Count];

            for (int i = 0; i < loadDataSet.Tables["Messages"].Rows.Count; i++)
            {
                messages[i] = new Messages()
                {
                    ID = Convert.ToInt32(loadDataSet.Tables["Messages"].Rows[i]["ID"]),
                    receiverID = Convert.ToInt32(loadDataSet.Tables["Messages"].Rows[i]["receiverID"]),
                    senderID = Convert.ToInt32(loadDataSet.Tables["Messages"].Rows[i]["senderID"]),
                    content = loadDataSet.Tables["Messages"].Rows[i]["content"].ToString(),
                    sendtime = Convert.ToDateTime(loadDataSet.Tables["Messages"].Rows[i]["sendtime"])
                };
            }
            return messages;
        }

        public Messages loadMessagesByUser(int ID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadArticleString = "SELECT * FROM Messages WHERE ID=@msgID";

            SqlCommand loadCommand1 = new SqlCommand(loadArticleString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("msgID", SqlDbType.Int));
            loadCommand1.Parameters["msgID"].Value = ID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Messages");

            Messages message = new Messages()
            {
                ID = Convert.ToInt32(loadDataSet.Tables["Articles"].Rows[0]["ID"]),
                receiverID = Convert.ToInt32(loadDataSet.Tables["Messages"].Rows[0]["receiverID"]),
                senderID = Convert.ToInt32(loadDataSet.Tables["Messages"].Rows[0]["senderID"]),
                content = loadDataSet.Tables["Messages"].Rows[0]["content"].ToString(),
                sendtime = Convert.ToDateTime(loadDataSet.Tables["Messages"].Rows[0]["sendtime"])
            };

            return message;
        }

        public bool deleteMessage(int ID)
        {
            SqlConnection Connection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            Connection.Open();
            SqlTransaction trans = Connection.BeginTransaction();
            bool flag = false;

            String deletemsgString = "DELETE FROM Messages WHERE ID=@msgID";
            SqlCommand deletemsgCommand = new SqlCommand(deletemsgString, Connection);
            deletemsgCommand.Transaction = trans;
            deletemsgCommand.Parameters.Add(new SqlParameter("msgID", SqlDbType.Int));
            deletemsgCommand.Parameters["msgID"].Value = ID;
            try
            {
                if(deletemsgCommand.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }

                trans.Commit();
                return flag;
            }
            catch
            {
                trans.Rollback();//回滚事务
                return false;
            }
        }

        public bool sendMessage(Messages newMessage)
        {
            bool flag;
            SqlConnection insertConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            insertConnection.Open();
            SqlTransaction trans = insertConnection.BeginTransaction();

            string insertString = "INSERT INTO Messages (senderID,receiverID,[content],sendtime) VALUES (@senderID,@receiverID,@content,@sendtime)";

            SqlCommand insertCommand = new SqlCommand(insertString, insertConnection);
            insertCommand.Transaction = trans;

            try
            {
                insertCommand.Parameters.Add(new SqlParameter("senderID", SqlDbType.Int));
                insertCommand.Parameters["senderID"].Value = newMessage.senderID;
                insertCommand.Parameters.Add(new SqlParameter("receiverID", SqlDbType.Int));
                insertCommand.Parameters["receiverID"].Value = newMessage.receiverID;
                insertCommand.Parameters.Add(new SqlParameter("content", SqlDbType.NVarChar));
                insertCommand.Parameters["content"].Value = newMessage.content;
                insertCommand.Parameters.Add(new SqlParameter("sendtime", SqlDbType.DateTime));
                insertCommand.Parameters["sendtime"].Value = newMessage.sendtime;

                insertCommand.ExecuteNonQuery();
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();//回滚事务
                flag = false;
            }
            finally
            {
                insertConnection.Close();
            }
            return flag;
        }

        public Info loadUserInfoByUser(int userID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadString = "SELECT username,headimgID,rgtime,infoID FROM Users WHERE userID=@userID";

            SqlCommand loadCommand1 = new SqlCommand(loadString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("userID", SqlDbType.NVarChar, 10));
            loadCommand1.Parameters["userID"].Value = userID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Users");

            string loadUserInfoString = "SELECT * FROM Info WHERE ID=@infoID";

            SqlCommand loadCommand3 = new SqlCommand(loadUserInfoString, loadConnection);
            loadCommand3.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
            loadCommand3.Parameters["infoID"].Value = loadDataSet.Tables["Users"].Rows[0]["infoID"];
            SqlDataAdapter InfoAdapter = new SqlDataAdapter(loadCommand3);
            InfoAdapter.Fill(loadDataSet, "Info");

            Info info = new Info()
            {
                ID = Convert.ToInt32(loadDataSet.Tables["Info"].Rows[0]["ID"]),
                birthday = Convert.ToDateTime(loadDataSet.Tables["Info"].Rows[0]["birthday"]),
                QQ = loadDataSet.Tables["Info"].Rows[0]["QQ"].ToString(),
                email = loadDataSet.Tables["Info"].Rows[0]["email"].ToString(),
                sex = Convert.ToBoolean(loadDataSet.Tables["Info"].Rows[0]["sex"]),
                signtime = Convert.ToDateTime(loadDataSet.Tables["Info"].Rows[0]["signtime"]),
                introduce = (loadDataSet.Tables["Info"].Rows[0]["introduce"].ToString() == "") ? "暂无..." : loadDataSet.Tables["Info"].Rows[0]["introduce"].ToString()
            };

            return info;
        }

        public bool updateUserInfo(int userID, Info newInfo)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadString = "SELECT username,headimgID,rgtime,infoID FROM Users WHERE userID=@userID";

            SqlCommand loadCommand1 = new SqlCommand(loadString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("userID", SqlDbType.NVarChar, 10));
            loadCommand1.Parameters["userID"].Value = userID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Users");

            string loadUserInfoString = "SELECT * FROM Info WHERE ID=@infoID";

            SqlCommand loadCommand3 = new SqlCommand(loadUserInfoString, loadConnection);
            loadCommand3.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
            loadCommand3.Parameters["infoID"].Value = loadDataSet.Tables["Users"].Rows[0]["infoID"];
            SqlDataAdapter InfoAdapter = new SqlDataAdapter(loadCommand3);
            InfoAdapter.Fill(loadDataSet, "Info");

            loadDataSet.Tables["Info"].Rows[0]["birthday"] = newInfo.birthday;
            loadDataSet.Tables["Info"].Rows[0]["QQ"] = newInfo.QQ;
            loadDataSet.Tables["Info"].Rows[0]["email"] = newInfo.email;
            loadDataSet.Tables["Info"].Rows[0]["sex"] = newInfo.sex;
            loadDataSet.Tables["Info"].Rows[0]["signtime"] = newInfo.signtime;
            loadDataSet.Tables["Info"].Rows[0]["introduce"] = newInfo.introduce;

            if(loadDataSet.HasChanges())
            {
                try
                {
                    string UpdateCommandString = "SELECT * FROM Info";
                    SqlCommand UpdateCommand = new SqlCommand(UpdateCommandString, loadConnection);
                    SqlDataAdapter UpdateAdapter = new SqlDataAdapter(UpdateCommand);
                    SqlCommandBuilder sb = new SqlCommandBuilder(UpdateAdapter);
                    UpdateAdapter.Update(loadDataSet, "Info");
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public HeadImg loadHeadImgByUser(int userID)
        {
            SqlConnection loadConnection = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            loadConnection.Open();

            string loadString = "SELECT username,headimgID FROM Users WHERE userID=@userID";

            SqlCommand loadCommand1 = new SqlCommand(loadString, loadConnection);
            loadCommand1.Parameters.Add(new SqlParameter("userID", SqlDbType.NVarChar, 10));
            loadCommand1.Parameters["userID"].Value = userID;
            SqlDataAdapter loadAdapter = new SqlDataAdapter(loadCommand1);
            DataSet loadDataSet = new DataSet();
            loadAdapter.Fill(loadDataSet, "Users");

            string loadHeadImgString = "SELECT * FROM HeadImg WHERE ID=@headimgID";

            SqlCommand loadCommand2 = new SqlCommand(loadHeadImgString, loadConnection);
            loadCommand2.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
            loadCommand2.Parameters["headimgID"].Value = loadDataSet.Tables["Users"].Rows[0]["headimgID"];
            SqlDataAdapter HeadimgAdapter = new SqlDataAdapter(loadCommand2);
            HeadimgAdapter.Fill(loadDataSet, "HeadImg");

            HeadImg headImg = new HeadImg()
            {
                ID = Convert.ToInt32(loadDataSet.Tables["HeadImg"].Rows[0]["ID"]),
                imgpath = loadDataSet.Tables["HeadImg"].Rows[0]["imgpath"].ToString()
            };

            return headImg;
        }

        public userBasic tryLogIn(string username, string userpwd)
        {
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");

            con.Open();

            string sqlSel = "Select userID from Users where username=@username and password=@userpwd";


            SqlCommand com = new SqlCommand(sqlSel, con);

            com.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar, 50));
            com.Parameters["username"].Value = username;
            com.Parameters.Add(new SqlParameter("userpwd", SqlDbType.NVarChar, 10));
            com.Parameters["userpwd"].Value = userpwd;

            SqlDataAdapter signAdapter = new SqlDataAdapter(com);
            DataSet signDataSet = new DataSet();
            signAdapter.Fill(signDataSet, "Users");

            userBasic user = new userBasic();

            if (signDataSet.Tables["Users"].Rows.Count > 0)
            {
                user.userID = Convert.ToInt32(signDataSet.Tables["Users"].Rows[0]["userID"]);
                user.username = username;
            }
            return user;
        }

        public bool trySignIn(Users newUser)
        {
            bool flag;
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            SqlCommand command1 = new SqlCommand("addInfo", con);
            command1.Transaction = trans;
            command1.CommandType = CommandType.StoredProcedure;

            SqlCommand command2 = new SqlCommand("addHeadimg", con);
            command2.Transaction = trans;
            command2.CommandType = CommandType.StoredProcedure;

            try
            {
                command1.Parameters.Add(new SqlParameter("sex", SqlDbType.Bit));
                command1.Parameters["sex"].Value = newUser.Info.sex;
                command1.Parameters["sex"].Direction = ParameterDirection.Input;

                command1.Parameters.Add(new SqlParameter("birthday", SqlDbType.DateTime));
                command1.Parameters["birthday"].Value = newUser.Info.birthday;
                command1.Parameters["birthday"].Direction = ParameterDirection.Input;

                command1.Parameters.Add(new SqlParameter("email", SqlDbType.NVarChar, 50));
                command1.Parameters["email"].Value = newUser.Info.email;
                command1.Parameters["email"].Direction = ParameterDirection.Input;

                command1.Parameters.Add(new SqlParameter("qq", SqlDbType.NVarChar, 50));
                command1.Parameters["qq"].Value = newUser.Info.QQ;
                command1.Parameters["qq"].Direction = ParameterDirection.Input;

                command1.Parameters.Add(new SqlParameter("signtime", SqlDbType.DateTime));
                command1.Parameters["signtime"].Value = newUser.Info.signtime;
                command1.Parameters["signtime"].Direction = ParameterDirection.Input;

                command1.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
                command1.Parameters["infoID"].Direction = ParameterDirection.Output;

                command1.ExecuteNonQuery();

                command2.Parameters.Add(new SqlParameter("imgPath", SqlDbType.NVarChar, 255));
                command2.Parameters["imgPath"].Value = "images/default_user.png";
                command2.Parameters["imgPath"].Direction = ParameterDirection.Input;

                command2.Parameters.Add(new SqlParameter("imgID", SqlDbType.Int));
                command2.Parameters["imgID"].Direction = ParameterDirection.Output;

                command2.ExecuteNonQuery();

                string commandString = "INSERT INTO Users (username,password,headimgID,infoID,rgtime,bloghot) VALUES (@username,@password,@headimgID,@infoID,@rgtime,@bloghot)";
                SqlCommand command3 = new SqlCommand(commandString, con);
                command3.Transaction = trans;

                command3.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar, 10));
                command3.Parameters["username"].Value = newUser.username;

                command3.Parameters.Add(new SqlParameter("password", SqlDbType.NVarChar, 16));
                command3.Parameters["password"].Value = newUser.password;

                command3.Parameters.Add(new SqlParameter("headimgID", SqlDbType.Int));
                command3.Parameters["headimgID"].Value = command2.Parameters["imgID"].Value;

                command3.Parameters.Add(new SqlParameter("infoID", SqlDbType.Int));
                command3.Parameters["infoID"].Value = command1.Parameters["infoID"].Value;

                command3.Parameters.Add(new SqlParameter("rgtime", SqlDbType.DateTime));
                command3.Parameters["rgtime"].Value = newUser.rgtime;

                command3.Parameters.Add(new SqlParameter("bloghot", SqlDbType.Int));
                command3.Parameters["bloghot"].Value = newUser.bloghot;

                command3.ExecuteNonQuery();

                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();//回滚事务
                flag = false;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public string Getsendername(int senderID)
        {
            SqlConnection con = new SqlConnection("server=tf-PC\\SQLEXPRESS;database=blogData;uid=admin;pwd=s1y2x3");
            con.Open();
            string sqlSel = "Select username from Users where userID=@userID";

            SqlCommand com = new SqlCommand(sqlSel, con);

            com.Parameters.Add(new SqlParameter("userID", SqlDbType.Int));
            com.Parameters["userID"].Value = senderID;
            return com.ExecuteScalar().ToString();
        }
    }
}