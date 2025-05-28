
using Common;


namespace MVC
{
    /// <summary>
    ///  物品数据结构
    /// </summary>
    public class Bag_Base_VO : Base_VO
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 物品类型
        /// </summary>
        public string StdMode;
        /// <summary>
        /// 需求等级
        /// </summary>
        public int need_lv;
        /// <summary>
        /// 物品等级
        /// </summary>
        public int equip_lv;
        /// <summary>
        /// 售价
        /// </summary>
        public int price;
        /// <summary>
        /// 属性 生命
        /// </summary>
        public int hp;
        /// <summary>
        /// 属性 魔法
        /// </summary>
        public int mp;
        /// <summary>
        /// 属性 防御
        /// </summary>
        public int defmin;
        /// <summary>
        /// 属性 防御
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
        /// 套装
        /// </summary>
        public int suit;
        /// <summary>
        /// 套装名称
        /// </summary>
        public string suit_name;
        /// <summary>
        public string suit_dec;
        /// <summary>
        /// 判断值 1 名称 2 强化等级 3品质 4附加值 5套装6锁定
        /// </summary>
        private string User_value;

        public Bag_Base_VO()
        {
        }

        public Bag_Base_VO(Bag_Base_VO item)
        {
            Name= item.Name;
            StdMode = item.StdMode;
            need_lv = item.need_lv;
            equip_lv = item.equip_lv;
            price = item.price;
            hp = item.hp;
            mp = item.mp;
            defmin = item.defmin;
            defmax = item.defmax;
            macdefmin = item.macdefmin;
            macdefmax = item.macdefmax;
            damgemin = item.damgemin;
            damagemax = item.damagemax;
            magicmin = item.magicmin;
            magicmax = item.magicmax;
            dec = item.dec;
            suit = item.suit;
            suit_name = item.suit_name;
            suit_dec = item.suit_dec;

        }

        public string user_value
        {
            get { return User_value; }

            set { User_value = value; }
        }
    }
}