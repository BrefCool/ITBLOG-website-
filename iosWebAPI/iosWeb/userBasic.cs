using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iosWeb
{
    public class userBasic
    {
        public int userID { get; set; }
        public string username { get; set; }

        public userBasic()
        {
            userID = 0;
            username = "";
        }
    }
}