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
        #region 玩家数据中转
        /// <summary>
        /// 主角色数据
        /// </summary>
        public static user_base_vo crt_user;
        /// <summary>
        /// 当前玩家总属性
        /// </summary>
        public static crtMaxHeroVO crt_MaxHero;
        /// <summary>
        /// 玩家技能
        /// </summary>
        public static List<base_skill_vo> crt_skills;
        /// <summary>
        /// 角色基准值
        /// </summary>
        public static Hero_VO crt_hero;
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
        /// 称号提供极品率
        /// </summary>
        public static int titleLucky = 0;
        #endregion

        #region 配置db文件
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
        /// 装备列表
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
        /// 地图列表
        /// </summary>
        public static List<user_map_vo> db_maps;
        #endregion

        #region 功能文件

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
        public static float WaitTime = 1f;

        #endregion
    }
}
