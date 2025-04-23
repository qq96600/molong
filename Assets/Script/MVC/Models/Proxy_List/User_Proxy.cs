using UnityEngine;
using Common;
using System;
using System.Collections.Generic;

namespace MVC
{
    /// <summary>
    ///  处理用户相关数据:登录,注册和注销
    /// </summary>
    public class User_Proxy : Base_Proxy
    {
        /// <summary>
        /// NAME
        /// </summary>
        public new const string NAME = "User_Proxy";

        public User_Proxy()
        {
            this.ProxyName = NAME;
            OpenMySqlDB();
        }
    }
}