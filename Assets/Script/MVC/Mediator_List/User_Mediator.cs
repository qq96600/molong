
using TarenaMVC;
using UI;
using Common;

namespace MVC
{
    /// <summary>
    ///  用户系统的中介者
    /// </summary>
    public class User_Mediator : Mediator
    {
        /// <summary>
        ///  NAME
        /// </summary>
        public new const string NAME = "User_Mediator";

        /// <summary>
        /// 数据库
        /// </summary>
        private User_Proxy user;

        public User_Mediator()
        {
            this.MediatorName = NAME;
            // 获取UserProxy
            user = AppFacade.I.RetrieveProxy(User_Proxy.NAME) as User_Proxy; 
 
        }
        /// <summary>
        ///  监听的消息列表
        /// </summary>
        /// <returns></returns>
        public override string[] ListNotificationInterests()
        {
            return new string[] {

            };
        }
        /// <summary>
        ///  处理消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public override void HandleNotification(string name, object data)
        {
            switch (name)
            {
                default:
                    break;
            }
        }
    }
}
