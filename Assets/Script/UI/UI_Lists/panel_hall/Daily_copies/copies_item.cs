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
    private Text info;

    public user_map_vo index;
    private void Awake()
    {
        icon=Find<Image>("bg/icon");
        info=Find<Text>("info");
    }

    public void Init(user_map_vo map,int number)
    {
        index = map;
        info.text = map.map_name+"("+number+"/2)";
        //icon.sprite = Resources.Load<Sprite>("Image/Map/" + map.monster_list);
    }
}
