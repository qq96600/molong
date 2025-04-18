using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_achievement_VO : Base_VO
{
    /// <summary>
    /// �ɾ�����
    /// </summary>
    public int achievement_type;
    /// <summary>
    /// �ɾ�����
    /// </summary>
    public string achievement_value;
    /// <summary>
    /// �ɾʹ������
    /// </summary>
    public string achievement_need;
    /// <summary>
    /// �ɾʹ�������б�
    /// </summary>
    public List<long> achievement_needs = new List<long>();
    /// <summary>
    /// ��������
    /// 
    /// 0��Ʒ 1����
   
    /// </summary>
    public string achievement_reward;
    /// <summary>
    /// �����б�
    /// </summary>
    public List<string> achievement_rewards = new List<string>();
    /// <summary>
    /// ��ʾ�ȼ�����
    /// </summary>
    public string[] achievement_show_lv;
    /// <summary>
    /// �Ƿ��жһ��б�
    /// </summary>
    public string achievement_exchange_offect = "";
}
