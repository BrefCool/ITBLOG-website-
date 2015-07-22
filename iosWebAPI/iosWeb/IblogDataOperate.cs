using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iosWeb;

namespace iosWeb
{
    public interface IblogDataOperate
    {
        IEnumerable<Articles> getAllArticlesByCategory(int categoryID);
        Articles loadArticleByArticleID(int articleID);
        IEnumerable<Articles> loadArticlesBywriterID(int writerID);
        bool deleteArticle(int articleID);

        IEnumerable<Comments> getAllCommentsByArticleID(int articleID);
        bool addComment(Comments newComment);

        IEnumerable<Collection> getAllCollectionsByHostID(int hostID);
        bool deleteCollection(int ID);

        IEnumerable<Messages> getAllMessagesByUser(int userID);
        Messages loadMessagesByUser(int ID);
        bool deleteMessage(int ID);
        bool sendMessage(Messages newMessage);

        Info loadUserInfoByUser(int userID);
        bool updateUserInfo(int userID, Info newInfo);

        HeadImg loadHeadImgByUser(int userID);
        //bool updateHeadImg(int userID, HeadImg newHeadImg);

        userBasic tryLogIn(string username, string userpwd);
        bool trySignIn(Users newUser);

        string Getsendername(int senderID);
    }
}
