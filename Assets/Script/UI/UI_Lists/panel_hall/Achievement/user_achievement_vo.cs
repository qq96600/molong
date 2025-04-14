using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_achievement_vo : Base_VO
{
    /// <summary>
    /// 自身uid
    /// </summary>
    public string uid;
    /// <summary>
    /// 区
    /// </summary>
    public int par;
    /// <summary>
    /// 经验值
    /// </summary>
    public string achievement_exp;

    /// <summary>
    /// 具体成就以及成就等级
    /// </summary>
    private Dictionary<string, int> user_achievements = new Dictionary<string, int>();
    private Dictionary<string, int> user_achievements_lv = new Dictionary<string, int>();

    private List<int> user_lvs = new List<int>();

}
