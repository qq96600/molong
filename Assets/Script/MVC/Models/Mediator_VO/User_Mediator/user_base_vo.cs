using System;
namespace MVC
{
    /// <summary>
    /// ���˻����ݴ洢�ṹ
    /// </summary>
    public class user_base_vo : Base_VO
    {

        public int par;//��Ϸ��
        public string uid;//��id
        public DateTime RegisterDate;//ע������
        public DateTime Nowdate;//��¼����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public override string[] Set_Instace_String()
        {
            return new string[] { 
            GetStr(0),
            GetStr(par),
            GetStr(uid),
            GetStr(RegisterDate),
            GetStr(Nowdate)
            };
              

        }

        public override string[] Set_Uptade_String()
        {
            return new string[] { GetStr(Nowdate)};
        }
        /// <summary>
        /// д���ַ���
        /// </summary>
        /// <returns></returns>
        public override string[] Get_Update_Character()
        {
            return new string[] { "Nowdate" };
        }
    }

}
