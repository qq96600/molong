using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_achievement_VO : Base_VO
{
    /// <summary>
    /// 成就类型
    /// </summary>
    public int achievement_type;
    /// <summary>
    /// 成就名称
    /// </summary>
    public string achievement_value;
    /// <summary>
    /// 成就达成条件
    /// </summary>
    public string achievement_need;
    /// <summary>
    /// 成就达成条件列表
    /// </summary>
    public List<long> achievement_needs = new List<long>();
    /// <summary>
    /// 奖励类型
    /// 1装备
    /// 1.boss点
    /*       
   2.装备 盾牌 
   3.金币
   4.材料
   5.生命 魔法
   6.攻击防御
   7.免伤
   8.暴击抗性
   9.幸运+1
   10.回复 1生命 2魔法
   11.攻击速度
   12.技能伤害
   13.技能攻击个数
    */
    /// </summary>
    public string achievement_reward;
    /// <summary>
    /// 收益列表
    /// </summary>
    public List<string> achievement_rewards = new List<string>();
    /// <summary>
    /// 显示等级介绍
    /// </summary>
    public string[] achievement_show_lv;
    /// <summary>
    /// 是否有兑换列表
    /// </summary>
    public string achievement_exchange_offect = "";
}
