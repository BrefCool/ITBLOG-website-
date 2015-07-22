using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iosWeb;
using AttributeRouting;
using AttributeRouting.Web.Http;

namespace iosWeb.Controllers
{
    public class ValuesController : ApiController
    {
        //public IEnumerable<Articles> Get()
        //{
        //    blogDataOperate Operate = new blogDataOperate();
        //    return Operate.getAllArticlesByCategory(1);
        //}

        // POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
        [GET("GetAllArticlesByCategory/{ID}")]
        public IEnumerable<Articles> GetAllArticlesByCategory(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.getAllArticlesByCategory(ID);
            //blogDataOperate Operate = new blogDataOperate();
            //return Operate.loadArticlesBywriterID(categoryID);
        }

        [GET("GetArticleByArticleID/{ID}")]
        public Articles GetArticleByArticleID(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.loadArticleByArticleID(ID);
        }

        [GET("GetAllArticlesByWriter/{ID}")]
        public IEnumerable<Articles> GetAllArticlesBywriterID(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.loadArticlesBywriterID(ID);
        }

        [GET("GetdeleteArticle/{ID}")]
        public bool GetdeleteArticle(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.deleteArticle(ID);
        }

        [GET("GetAllCommentsByArticleID/{ID}")]
        public IEnumerable<Comments> GetAllCommentsByArticleID(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.getAllCommentsByArticleID(ID);
        }

        //[POST("api/Values/addComment")]
        //public bool addComment([FromBody]Comments newComment)
        //{
        //    blogDataOperate Operate = new blogDataOperate();
        //    return Operate.addComment(newComment);
        //}

        [GET("GetAllCollectionsByHostID/{ID}")]
        public IEnumerable<Collection> GetAllCollectionsByHostID(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.getAllCollectionsByHostID(ID);
        }

        [GET("GetdeleteCollection/{ID}")]
        public bool GetdeleteCollection(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.deleteCollection(ID);
        }

        [GET("GetAllMessagesByUser/{ID}")]
        public IEnumerable<Messages> GetAllMessagesByUser(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.getAllMessagesByUser(ID);
        }

        [GET("GetMessagesByUser/{ID}")]
        public Messages GetMessagesByUser(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.loadMessagesByUser(ID);
        }

        [GET("GetdeleteMessage/{ID}")]
        public bool deleteMessage(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.deleteMessage(ID);
        }

        //[POST("api/Values/sendMessage")]
        //public bool sendMessage([FromBody]Messages newMessage)
        //{
        //    blogDataOperate Operate = new blogDataOperate();
        //    return Operate.sendMessage(newMessage);
        //}

        [GET("GetUserInfoByUser/{ID}")]
        public Info GetUserInfoByUser(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.loadUserInfoByUser(ID);
        }

        //[POST("api/Values/updateUserInfo/{userID}")]
        //public bool updateUserInfo(int userID, [FromBody]Info newInfo)
        //{
        //    blogDataOperate Operate = new blogDataOperate();
        //    return Operate.updateUserInfo(userID, newInfo);
        //}

        [GET("GetHeadImgByUser/{ID}")]
        public HeadImg GetHeadImgByUser(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.loadHeadImgByUser(ID);
        }

        [GET("GettryLogIn/{username}/{userpwd}")]
        public userBasic GettryLogIn(string username, string userpwd)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.tryLogIn(username, userpwd);
        }

        //[POST("api/Values/trySignIn")]
        //public bool trySignIn([FromBody]Users newUser)
        //{
        //    blogDataOperate Operate = new blogDataOperate();
        //    return Operate.trySignIn(newUser);
        //}

        [GET("GetSenderName/{ID}")]
        public string GetSenderName(int ID)
        {
            blogDataOperate Operate = new blogDataOperate();
            return Operate.Getsendername(ID);
        }
    }
}