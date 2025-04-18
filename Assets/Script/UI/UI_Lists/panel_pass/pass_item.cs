using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pass_item : Base_Mono
{
    /// <summary>
    /// ��Ʒͼ��
    /// </summary>
    private Image icon;
    /// <summary>
    /// ��Ʒ��Ϣ
    /// </summary>
    private Text info_lv, info_value, upinfo_value;
    /// <summary>
    /// ���װ�ť
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
    /// ��ȡ���׽���
    /// </summary>
    private void OnupLvReceive()
    {
        transform.parent.parent.parent.parent.parent.SendMessage("GetupLvReward", this);

    }
    /// <summary>
    /// �����ȡ
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
            //info.text = data.arrifact_name + "(δ����)";
            getList();
        }
        get
        {
            return data;
        }
    }

    private void getList()
    {
        info_lv.text = data.lv + "��";
        info_value.text = Show_Info(data.reward);
        upinfo_value.text = Show_Info(data.uplv_reward);
    }
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    /// <param name="reward"></param>
    /// <returns></returns>
    private string Show_Info(string reward)
    {
        string dec = "";
        string[] arr = reward.Split('*');
        dec+=arr[0] + "��" + arr[1] + "";
        return dec;
    }

    /// <summary>
    /// ��ȡ�ȼ�
    /// </summary>
    public void Set(int lv,int upLv)
    {
        receive.gameObject.SetActive(lv == 0);
        upLvreceive.gameObject.SetActive(upLv == 0);
    }
}
