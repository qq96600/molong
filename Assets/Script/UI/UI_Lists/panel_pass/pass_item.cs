using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pass_item : Base_Mono
{
    /// <summary>
    /// 物品图标
    /// </summary>
    private Image icon;
    /// <summary>
    /// 物品信息
    /// </summary>
    private Text info_lv, info_value, upinfo_value;
    /// <summary>
    /// 进阶按钮
    /// </summary>
    private Button receive, upLvreceive;
    private void Awake()
    {
        //icon = Find<Image>("icon");
        info_lv =  Find<Text>("info_lv");
        info_value = Find<Text>("info_value");
        upinfo_value = Find<Text>("upinfo_value");
        receive = Find<Button>("receive");
        upLvreceive = Find<Button>("upLvreceive");
        receive.onClick.AddListener(OnReceive);
        upLvreceive.onClick.AddListener(OnupLvReceive);
    }
    /// <summary>
    /// 领取进阶奖励
    /// </summary>
    private void OnupLvReceive()
    {
        transform.parent.parent.parent.parent.parent.SendMessage("GetupLvReward", this);

    }
    /// <summary>
    /// 点击领取
    /// </summary>
    private void OnReceive()
    {
        transform.parent.parent.parent.parent.parent.SendMessage("GetReward", this);
    }

    private user_pass_vo data;
    /// <summary>
    /// Data
    /// </summary>
    public user_pass_vo Data
    {
        set
        {
            data = value;
            if (data == null) return;
            //info.text = data.arrifact_name + "(未激活)";
            getList();
        }
        get
        {
            return data;
        }
    }

    private void getList()
    {
        info_lv.text = data.lv + "级";
        info_value.text = data.reward;
        upinfo_value.text = data.reward;
    }

    /// <summary>
    /// 读取等级
    /// </summary>
    public void Set(int lv,int upLv)
    {
        receive.gameObject.SetActive(lv == 0);
        upLvreceive.gameObject.SetActive(upLv == 0);
    }
}
