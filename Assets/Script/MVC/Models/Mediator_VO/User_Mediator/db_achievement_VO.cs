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
    /// 
    /// 0物品 1属性
   
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
