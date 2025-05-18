using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// 邮件列表
/// </summary>
public class offect_emial : Base_Mono
{
    /// <summary>
    /// 邮件位置
    /// </summary>
    private Transform pos_list, pos_receive;
    /// <summary>
    /// 邮件列表
    /// </summary>
    private emial_item emial_Item_Prefab;
    /// <summary>
    /// 当前邮件
    /// </summary>
    private emial_item crtMail;

    private material_item material_Item_Prefab;
    /// <summary>
    /// 显示信息
    /// </summary>
    private Text title, content;
    /// <summary>
    /// 接收按钮
    /// </summary>
    private Button btn_receive;
    private void Awake()
    {
        emial_Item_Prefab = Battle_Tool.Find_Prefabs<emial_item>("emial_item");
        pos_list = Find<Transform>("offect/Scroll View/Viewport/Content");
        content = Find<Text>("offect/show_offect/content");
        pos_receive= Find<Transform>("offect/show_offect/Scroll View/Viewport/Content");
        material_Item_Prefab= Battle_Tool.Find_Prefabs<material_item>("material_item");
        title = Find<Text>("offect/show_offect/info");
        content= Find<Text>("offect/show_offect/content");
        btn_receive= Find<Button>("offect/show_offect/receive");
        btn_receive.onClick.AddListener(Receive);
    }
    /// <summary>
    /// 领取奖励
    /// </summary>
    private void Receive()
    {
        if(crtMail==null)return;
        if (crtMail.crt_mail.mail_par == -1)
        {
            foreach (var item in SumSave.CrtMail.lists)
            {
                if (item == crtMail.crt_mail.mail_id)
                {
                    Alert_Dec.Show("当前邮件已领取");
                    return;
                }
            }
            SumSave.CrtMail.lists.Add(crtMail.crt_mail.mail_id);
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.user_emial, 
                SumSave.CrtMail.Set_Uptade_String(),SumSave.CrtMail.Get_Update_Character());
            Receive_Resources();
        }
        else
        {
            SumSave.crt_accumulatedrewards.Set(1, crtMail.crt_mail.moeny);

            Game_Omphalos.i.GetQueue(Mysql_Type.Delete, Mysql_Table_Name.server_mail,
                new string[] { Battle_Tool.GetStr(crtMail.crt_mail.uid) }, new string[] { "mail_recipient" });

            Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.history_server_mail,
              crtMail.crt_mail.Set_Instace_String());
            Receive_Resources();
        }
        
    }
    /// <summary>
    /// 领取资源
    /// </summary>
    private void Receive_Resources()
    {
        Dictionary<int, Dictionary<string, int>> str = crtMail.crt_mail.mail_dec;
        foreach (var index in str.Keys)
        {
            foreach (var material in str[index].Keys)
            {
                switch (index)
                {
                    case 1://货币
                        Battle_Tool.Obtain_Unit
                        ((currency_unit)Enum.Parse(typeof(currency_unit), material), str[index][material]);
                        break;
                    case 2://道具
                        Battle_Tool.Obtain_Resources(material, str[index][material],true);
                        break;
                    case 3://通行证
                        break;
                    case 4://皮肤
                        SumSave.crt_hero.hero_value += (SumSave.crt_hero.hero_value == "" ? "" : ",") + material;
                        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero, new string[] { Battle_Tool.GetStr(SumSave.crt_hero.hero_value) },
                            new string[] { "hero_value" });
                        break;
                    default:
                        break;
                }
            }
        }
        Alert_Dec.Show("领取成功");
    }

    public override void Show()
    {
        base.Show();
        SendNotification(NotiList.Read_Mail);
        Base_Show();
    }

    private void Base_Show()
    {
        ClearObject(pos_list);
        for (int i = 0; i < SumSave.Db_Mails.Count; i++)
        {
            if (SumSave.Db_Mails[i].mail_par == -1 || SumSave.Db_Mails[i].uid == SumSave.crt_user.uid)
            { 
                emial_item item = Instantiate(emial_Item_Prefab, pos_list);
                bool exist = false;
                foreach (var index in SumSave.CrtMail.lists)
                {
                    if (index == SumSave.Db_Mails[i].mail_id) exist = true;
                }
                item.SetInfo(SumSave.Db_Mails[i],exist);
                item.GetComponent<Button>().onClick.AddListener(() => { ShowMail(item); });
                if(crtMail==null)ShowMail(item);
            }
        }
    }

    private void ShowMail(emial_item item)
    {
        crtMail= item;
        title.text = item.crt_mail.uid == "-1" ? "系统邮件" : "玩家邮件";
        content.text = item.crt_mail.dec;
        ClearObject(pos_receive);
        Dictionary<int, Dictionary<string, int>> str = item.crt_mail.mail_dec;
        foreach (var index in str.Keys)
        {
            foreach (var material in str[index].Keys)
            {
                Instantiate(material_Item_Prefab, pos_receive).Init((material, str[index][material]));
            }
        }
    }
}
