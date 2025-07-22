using System;
using System.Collections.Generic;
using MVC;

namespace Common
{
    /// <summary>
    /// 中间存储结构
    /// </summary>
    public static class SumSave
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string uid;
        public static string Tapid ;
        /// <summary>
        /// 服务器几区
        /// </summary>
        public static int par = 1;
        /// <summary>
        /// 验证单次收益 0 灵珠 1历练 2魔丸 3材料获取量 4背包 5仓库 6至尊值（用于掉落绝世装备）
        /// </summary>
        public static List<int> base_setting = new List<int>() { 90000, 90000, 9000, 5000};
        /// <summary>
        /// 验证单次材料收益
        /// </summary>
        public static List<long> base_settin_uint = new List<long>();
        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime nowtime=DateTime.Now;
        /// <summary>
        /// 苹果账号 用来判断是否为苹果审核号
        /// </summary>
        public static string ios_account_number="";


        #region 玩家数据中转

        /// <summary>
        /// 版本控制信息
        /// </summary>
        public static user_versions crt_versions;
        /// <summary>
        /// 版本控制器
        /// </summary>
        public static bool OpenGame;
        /// <summary>
        /// 主角色数据
        /// </summary>
        public static user_base_vo crt_user;
        /// <summary>
        /// 当前玩家总属性
        /// </summary>
        public static crtMaxHeroVO crt_MaxHero;
        /// <summary>
        /// 玩家Buff
        /// </summary>
        public static user_player_Buff crt_player_buff;

        /// <summary>
        /// 玩家技能
        /// </summary>
        public static List<base_skill_vo> crt_skills;
        /// <summary>
        /// 角色基准值
        /// </summary>
        public static Hero_VO crt_hero;
        /// <summary>
        /// 货币库存
        /// </summary>
        public static user_vo crt_user_unit;
        /// <summary>
        /// 自身通行证
        /// </summary>
        public static user_pass_vo crt_pass;
        /// <summary>
        /// 资源项
        /// </summary>
        public static user_base_Resources_vo crt_resources;
        /// <summary>
        /// 设置
        /// </summary>
        public static user_base_setting_vo crt_setting;
        /// <summary>
        /// 自身神器
        /// 
        /// </summary>
        public static user_artifact_vo crt_artifact;
        /// <summary>
        /// 设置类型
        /// </summary>
        public static user_setting_type_vo crt_setting_type;
        /// <summary>
        /// 当前物品
        /// </summary>
        public static bag_Resources_vo crt_bag_resources;
        /// <summary>
        /// 背包
        /// </summary>
        public static List<Bag_Base_VO> crt_bag;
        /// <summary>
        /// 仓库
        /// </summary>
        public static List<Bag_Base_VO> crt_house;
        /// <summary>
        /// 穿戴
        /// </summary>
        public static List<Bag_Base_VO> crt_euqip;
        /// <summary>
        /// 种植数据
        /// </summary>
        public static user_plant_vo crt_plant;
        /// <summary>
        /// 自身孵化数据
        /// </summary>
        public static db_pet_vo crt_hatching;
        /// <summary>
        /// 自身探索数据
        /// </summary>
        public static user_explore_vo crt_explore;
        /// <summary>
        /// 自身宠物
        /// </summary>
        public static user_pet_vo crt_pet=new user_pet_vo();
        /// <summary>
        /// 自身宠物列表 
        /// </summary>
        public static List<db_pet_vo> crt_pet_list1=new List<db_pet_vo>();
        /// <summary>
        /// 炼丹数据
        /// </summary>
        public static bag_seed_vo crt_seeds;
        /// <summary>
        /// 自身小世界数据
        /// </summary>
        public static user_world_vo crt_world;
        /// <summary>
        /// 称号提供极品率
        /// </summary>
        public static int titleLucky = 0;
        /// <summary>
        /// 排行榜
        /// </summary>
        public static rank_vo user_ranks;
        /// <summary>
        /// 自身成就
        /// </summary>
        public static user_achievement_vo crt_achievement;

        /// <summary>
        /// 用户需求信息
        /// </summary>
        public static user_needlist_vo crt_needlist;

        ///<summary>
        ///收集
        ///</summary>
        public static user_collect_vo crt_collect;
        /// <summary>
        /// 自身签到数据
        /// </summary>
        public static user_signin_vo crt_signin;
        /// <summary>
        /// 自身任务引导
        /// </summary>
        public static user_greenhand_vo crt_greenhand;
        /// <summary>
        /// 滚动消息列表
        /// </summary>
        public static List<(int, string, string)> crt_message_window;
        /// <summary>
        /// 自身累计奖励
        /// </summary>
        public static user_Accumulatedrewards_vo crt_accumulatedrewards;
   
        /// <summary>
        /// 世界boss排行榜
        /// </summary>
        public static mo_world_boss_rank crt_world_boss_rank;
        /// <summary>
        /// 试练塔排行榜
        /// </summary>
        public static mo_world_boss_rank crt_Trial_Tower_rank;
        /// <summary>
        /// 世界boss伤害
        /// </summary>
        public static user_world_boss crt_world_boss_hurt;

        #endregion

        #region 配置db文件
        /// <summary>
        /// 套装属性
        /// </summary>
        public static List<db_suit_vo> db_suits;
        /// <summary>
        /// 具体功能消息
        /// </summary>
        public static List<db_dec> db_dec;
        /// <summary>
        /// 当前世界boss信息
        /// </summary>
        public static db_world_boos db_world_boos = new db_world_boos();
        /// <summary>
        /// 配置通行证
        /// </summary>
        public static List<user_pass_vo> db_pass;
        /// <summary>
        /// 服务器列表
        /// </summary>
        public static List<db_base_par> db_pars;
        /// <summary>
        /// 签到奖励列表
        /// </summary>
        public static List<db_signin_vo> db_Signins;
        /// <summary>
        /// 技能列表
        /// </summary>
        public static List<base_skill_vo> db_skills;
        ///<summary>
        /// 设置类型字典
        /// </summary>
        public static List<user_setting_type_vo> db_sttings;
        /// <summary>
        /// 神器列表
        /// </summary>
        public static List<db_artifact_vo> db_Artifacts;
        
        /// <summary>
        /// 全服玩家的boss伤害
        /// </summary>
        public static List<user_world_boss> db_world_boss_hurt;
        /// <summary>
        /// 天气列表
        /// </summary>
        public static List<db_weather> db_weather_list;

        /// <summary>
        /// 物品列表
        /// </summary>
        public static List<Bag_Base_VO> db_stditems;
        /// <summary>
        /// 激活角色列表
        /// </summary>
        public static List<db_hero_vo> db_heros;
     
        /// <summary>
        /// 读取怪物数据
        /// </summary>
        public static List<crtMaxHeroVO> db_monsters;
        /// <summary>
        /// 读取植物列表
        /// </summary>
        public static List<user_plant_vo> db_plants;
        /// <summary>
        /// 读取植物列表
        /// </summary>
        public static Dictionary<string, user_plant_vo> db_plants_dic;

        /// <summary>
        /// 宠物列表
        /// </summary>
        public static List<db_pet_vo> db_pet;
        /// <summary>
        /// 宠物字典
        /// </summary>
        public static Dictionary<string, db_pet_vo> db_pet_dic;

        /// <summary>
        /// 宠物探索列表
        /// </summary>
        public static List<user_pet_explore_vo> db_pet_explore;
        /// <summary>
        /// 宠物探索字典
        /// </summary>
        public static Dictionary<string, user_pet_explore_vo> db_pet_explore_dic;
        /// <summary>
        /// 种子炼丹列表
        /// </summary>
        public static List<db_seed_vo> db_seeds;
        /// <summary>
        /// 升级列表
        /// </summary>
        public static db_lv_vo db_lvs;
        /// <summary>
        /// 大厅列表
        /// </summary>
        public static db_hall_vo db_halls;
        /// <summary>
        /// 地图列表
        /// </summary>
        public static List<user_map_vo> db_maps;
        /// <summary>
        /// 商店物品列表
        /// </summary>
        public static List<db_store_vo> db_stores_list;
        /// <summary>
        /// 限定商店物品字典
        /// </summary>
        public static Dictionary<string, db_store_vo> db_stores_dic=new Dictionary<string, db_store_vo>();
        /// <summary>
        /// 成就物品字典
        /// </summary>
        public static List<db_achievement_VO> db_Achievement_dic;
        /// <summary>
        /// 收集物品列表
        /// </summary>
        public static List<db_collect_vo> db_collect_vo;
        /// <summary>
        /// 累积奖励列表
        /// </summary>
        public static db_Accumulatedrewards_vo db_Accumulatedrewards;
        /// 命运殿堂列表
        /// </summary>
        public static List<db_fate_vo> db_fate_list;
        /// <summary>
        /// VIP列表
        /// </summary>
        public static List<db_vip> db_vip_list;
        /// <summary>
        /// 造化炉合成列表
        /// </summary>
        public static List<db_formula_vo> db_formula_list;

        #endregion

        #region 功能文件
        /// <summary>
        /// 五行类型
        /// </summary>
        public static string[] five_element_type = { "土", "火", "水", "木", "金" };
        ///// <summary>
        ///// 怪物对象池
        ///// </summary>
        //public static List<BattleHealth> battleMonsterHealths1 = new List<BattleHealth>();
        ///// <summary>
        ///// 玩家对象池
        ///// </summary>
        //public static List<BattleHealth> battleHeroHealths1 = new List<BattleHealth>();
        /// <summary>
        /// 战斗刷新时间
        /// </summary>
        public static float WaitTime = 5f;
        /// <summary>
        /// 大世界新手引导
        /// </summary>
        public static Dictionary<int, GreenhandGuide_TotalTaskVO> GreenhandGuide_TotalTasks;
        /// <summary>
        /// 邮件列表
        /// </summary>
        public static List<db_mail_vo> Db_Mails;

        public static user_mail_vo CrtMail;
    
        #endregion
    }
}
