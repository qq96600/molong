using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_achievement_vo : Base_VO
{
    /// <summary>
    /// ����uid
    /// </summary>
    public string uid;
    /// <summary>
    /// ��
    /// </summary>
    public int par;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public string achievement_exp;

    /// <summary>
    /// ����ɾ��Լ��ɾ͵ȼ�
    /// </summary>
    private Dictionary<string, int> user_achievements = new Dictionary<string, int>();
    private Dictionary<string, int> user_achievements_lv = new Dictionary<string, int>();

    private List<int> user_lvs = new List<int>();

}
