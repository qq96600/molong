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
        public static string uid = "DSFSDFSDFSDF";
        /// <summary>
        /// 服务器几区
        /// </summary>
        public static int par = 1;
        /// <summary>
        /// 主角色数据
        /// </summary>
        public static user_base_vo crt_user;
        /// <summary>
        /// 当前玩家总属性
        /// </summary>
        public static crtMaxHeroVO crt_MaxHero;
        /// <summary>
        /// 技能列表
        /// </summary>
        public static List<base_skill_vo> db_skills;
        /// <summary>
        /// 装备列表
        /// </summary>
        public static List<Bag_Base_VO> db_stditems;
        /// <summary>
        /// 激活角色列表
        /// </summary>
        public static List<db_hero_vo> db_heros;
        /// <summary>
        /// 玩家技能
        /// </summary>
        public static List<base_skill_vo> crt_skills;
        /// <summary>
        /// 读取怪物数据
        /// </summary>
        public static List<crtMaxHeroVO> db_monsters;
        /// <summary>
        /// 地图列表
        /// </summary>
        public static List<user_map_vo> db_maps;

        /// <summary>
        /// 怪物对象池
        /// </summary>
        public static List<BattleHealth> battleMonsterHealths = new List<BattleHealth>();
        /// <summary>
        /// 玩家对象池
        /// </summary>
        public static List<BattleHealth> battleHeroHealths = new List<BattleHealth>();
        /// <summary>
        /// 战斗刷新时间
        /// </summary>
        public static float WaitTime = 5f;
    }
}
