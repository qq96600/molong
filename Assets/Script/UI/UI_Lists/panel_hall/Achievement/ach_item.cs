using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ach_item : Base_Mono
{
    private Text info;
    private bool active = false;
    private void Awake()
    {
        info = Find<Text>("info");
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="value"></param>
    public void Show_Info(object value)
    {
        info.text = value.ToString();
    }
    private db_achievement_VO data;
    /// <summary>
    /// Data
    /// </summary>
    public db_achievement_VO Data
    {
        set
        {
            data = value;
            Init();
        }
        get
        {
            return data;
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Dictionary<string, int> dic_exp = SumSave.crt_achievement.Set_Exp();
        Dictionary<string, int> dic_lv = SumSave.crt_achievement.Set_Lv();
        //Debug.Log("dic_lv[data.achieve_value]:" + data.achieve_need);
        if (dic_lv[data.achievement_value] >= data.achievement_needs.Count)
            info.text = data.achievement_show_lv[data.achievement_show_lv.Length - 1] + " (" + dic_exp[data.achievement_value] + "/Max)";
        else
            info.text = data.achievement_show_lv[dic_lv[data.achievement_value]] + " (" + dic_exp[data.achievement_value] + "/" + data.achievement_needs[dic_lv[data.achievement_value]] + ")";
    }
}
