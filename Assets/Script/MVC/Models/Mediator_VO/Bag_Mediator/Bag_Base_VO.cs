
using Common;


namespace MVC
{
    /// <summary>
    ///  ��Ʒ���ݽṹ
    /// </summary>
    public class Bag_Base_VO : Base_VO
    {
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string Name;
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string StdMode;
        /// <summary>
        /// ����ȼ�
        /// </summary>
        public int need_lv;
        /// <summary>
        /// ��Ʒ�ȼ�
        /// </summary>
        public int equip_lv;
        /// <summary>
        /// �ۼ�
        /// </summary>
        public int price;
        /// <summary>
        /// ���� ����
        /// </summary>
        public int hp;
        /// <summary>
        /// ���� ħ��
        /// </summary>
        public int mp;
        /// <summary>
        /// ���� ����
        /// </summary>
        public int defmin;
        /// <summary>
        /// ���� ����
        /// </summary>
        public int defmax;
        public int macdefmin;
        public int macdefmax;
        public int damgemin;
        public int damagemax;
        public int magicmin;
        public int magicmax;
        public string dec;
        /// <summary>
        /// ��װ
        /// </summary>
        public int suit;
        /// <summary>
        /// ��װ����
        /// </summary>
        public string suit_name;
        /// <summary>
        public string suit_dec;
        /// <summary>
        /// �ж�ֵ 1 ���� 2 ǿ���ȼ� 3Ʒ�� 4����ֵ 5��װ
        /// </summary>
        public string user_value;
        
    }
}