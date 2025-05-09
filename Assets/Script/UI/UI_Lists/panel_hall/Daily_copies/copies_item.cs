using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copies_item : Base_Mono
{
    /// <summary>
    /// 显示图片
    /// </summary>
    private Image icon;
    /// <summary>
    /// 显示信息
    /// </summary>
    private Text info,base_name;

    public user_map_vo index;

    private int number, maxnumber;
    private void Awake()
    {
        icon=Find<Image>("bg/icon");
        base_name = Find<Text>("bg/icon/name");
        info =Find<Text>("info");
    }

    public void Init(user_map_vo map,int _number,int _maxnumber)
    {
        index = map;
        number = _number;
        maxnumber = _maxnumber;
        info.text = map.map_name + "(" + number + "/" + maxnumber + ")";
        icon.sprite = Resources.Load<Sprite>("base_bg/怪物/国风怪物/" + map.monster_list);
        base_name.text = "[Boss]" + map.monster_list;
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    /// <returns></returns>
    public string ShowInfo()
    {
        return "\n 挑战次数 " + number + "/" + maxnumber + " 次";
    }
    /// <summary>
    /// 是否可以挑战
    /// </summary>
    /// <returns></returns>
    public bool IsSate()
    {
        return number < maxnumber;
    }
    /// <summary>
    /// 刷新状态
    /// </summary>
    public void updatestate()
    {
        number++;
        info.text = index.map_name + "(" + number + "/" + maxnumber + ")";

    }
}
