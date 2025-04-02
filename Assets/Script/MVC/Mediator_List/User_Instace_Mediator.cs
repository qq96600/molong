
using System.Collections.Generic;
using TarenaMVC;

namespace MVC
{
    public class User_Instace_Mediator : Mediator
    {
        /// <summary>
        ///  NAME
        /// </summary>
        public new const string NAME = "User_Instace_Mediator";

        private User_Instace_Proxy user;
        /// <summary>
        ///  构造函数
        /// </summary>
        public User_Instace_Mediator()
        {
            this.MediatorName = NAME;

            user = AppFacade.I.RetrieveProxy(User_Instace_Proxy.NAME) as User_Instace_Proxy;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                NotiList.User_Login,
                NotiList.Execute_Write,
                NotiList.Refresh_User_Setting,
                NotiList.Refresh_Max_Hero_Attribute,
                NotiList.Delete,
                NotiList.loglist


            };

        }

        /// <summary>
        /// 处理通知
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public override void HandleNotification(string name, object data)
        {
            switch (name)
            {
                    case NotiList.User_Login:
                    user.User_Login();
                    Game_Omphalos.i.activation();
                    break;
                    case NotiList.Execute_Write:
                    user.Execute_Write(data as List<Base_Wirte_VO>);
                    break;
                case NotiList.Refresh_User_Setting:
                    user.Refresh_User_Setting(data as user_base_setting_vo);
                    break;
                case NotiList.Refresh_Max_Hero_Attribute:
                    user.Refresh_Max_Hero_Attribute();
                    break;
                case NotiList.Delete:
                    user.Delete(data.ToString());
                    break;
                    case NotiList.loglist:
                    user.loglist(data.ToString());
                    break;
            }
        }
    }

}
