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

    public int index;
    private void Awake()
    {
        icon=Find<Image>("bg/icon");
        info=Find<Text>("info");
    }

    public void Init(int _index)
    {
        index = _index;
        info.text = "第" + index + "关";
    }
}
