using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signln_item : Base_Mono
{
    private db_signin_vo crt_vo;
    private Text info;
    private Image icon;
    private int index;
    private void Awake()
    {
        info=Find<Text>("info");
        icon = Find<Image>("icon");
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="db_signin_vo"></param>
    /// <param name="state"></param>
    public void Init(int _index, db_signin_vo db_signin_vo,int state)
    {
        crt_vo=db_signin_vo;
        string[] strs = crt_vo.value.Split('*');
        string dec = strs[0] + "*" + strs[1];
        dec += "\n" + crt_vo.index + "天奖励";
        info.text = dec;
        icon.gameObject.SetActive(state == 1);
        index = _index;
    }
    /// <summary>
    /// 获取编号索引
    /// </summary>
    /// <returns></returns>
    public int Set()
    {
        return index;
    }
}
