
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
                NotiList.Execute_Write


            };

        }


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
            }
        }
    }

}
