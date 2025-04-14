using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_pass : Panel_Base
{
    /// <summary>
    /// 通行证列表
    /// </summary>
    private List<string> btn_list = new List<string>() { "通行证S1", "通行证S2" }; 

    private Transform pos_btn, pos_item;

    private btn_item btn_itm_prefabs;

    private pass_item pass_item_prefabs;
    /// <summary>
    /// 第几个通行证
    /// </summary>
    private int index=0;
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_btn = Find<Transform>("bg_main/btn_list");
        pos_item = Find<Transform>("bg_main/Scroll View/Viewport/Content");
        btn_itm_prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        pass_item_prefabs = Resources.Load<pass_item>("Prefabs/panel_hall/panel_pass/pass_item"); 
        for (int i = 0; i < btn_list.Count; i++)
        {
            btn_item btn = Instantiate(btn_itm_prefabs, pos_btn);
            btn.Show(i + 1, btn_list[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { OnBtnClick(btn); });
        }
    }

    private void OnBtnClick(btn_item btn)
    {

    }

    public override void Show()
    {
        base.Show();
        Base_Show();
    }

    private void Base_Show()
    {
        for (int i = pos_item.childCount - 1; i >= 0; i--)
        {
            Destroy(pos_item.GetChild(i).gameObject);
        }
        //自身领取状态
        Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();

        for (int i = 0; i < SumSave.db_pass.Count; i++)
        {
            if (SumSave.db_pass[i].pass_index == index)
            { 
                pass_item item=Instantiate(pass_item_prefabs, pos_item);
                item.Data= SumSave.db_pass[i];
                if(!dic.ContainsKey(index))dic.Add(index,new List<int>(index));
                if (!dic[index].Contains(i))
                {
                    dic[index].Add(0);
                }
                if (dic.ContainsKey(index+1))
                {
                    if (!dic[index+1].Contains(i))
                    {
                        dic[index+1].Add(0);
                    }
                    item.Set(dic[index][i], dic[index + 1][i]);
                }
                else 
                 item.Set(dic[index][i], 1);

            }
        }
    }

    /// <summary>
    /// 升级
    /// </summary>
    /// <param name="item"></param>
    protected void GetReward(pass_item item)
    {
        if (SumSave.crt_pass.data_lv >= item.Data.lv)
        {
            Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();
            if (dic.ContainsKey(index))
            {
                if (dic[index][item.Data.lv] == 0)
                {
                    //领取奖励
                    Obtain_result(item.Data.reward);
                    dic[index][item.Data.lv] = 1;
                    if (dic.ContainsKey(index + 1))
                        item.Set(dic[index][item.Data.lv], dic[index + 1][item.Data.lv]);
                    else item.Set(dic[index][item.Data.lv], 1);
                    SumSave.crt_pass.Get(dic);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pass, SumSave.crt_pass.Set_Uptade_String(), SumSave.crt_pass.Get_Update_Character());
                    Alert_Dec.Show("领取成功");
                }
                else Alert_Dec.Show("已经领取");
            }
        }else Alert_Dec.Show("等级不足");
    }

    /// <summary>
    /// 进阶领取
    /// </summary>
    /// <param name="item"></param>
    protected void GetupLvReward(pass_item item)
    {
        if (SumSave.crt_pass.data_lv >= item.Data.lv)
        {
            Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();
            if (dic.ContainsKey(index+1))
            {
                if (dic[index+1][item.Data.lv] == 0)
                {
                    //领取奖励
                    Obtain_result(item.Data.uplv_reward);
                    dic[index+1][item.Data.lv] = 1;
                    item.Set(dic[index][item.Data.lv], dic[index + 1][item.Data.lv]);
                    SumSave.crt_pass.Get(dic);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pass, SumSave.crt_pass.Set_Uptade_String(), SumSave.crt_pass.Get_Update_Character());
                    Alert_Dec.Show("领取成功");
                }
                else Alert_Dec.Show("已经领取");
            }
        }
        else Alert_Dec.Show("等级不足");
    }
    /// <summary>
    /// 获取奖励
    /// </summary>
    /// <param name="result"></param>
    private void Obtain_result(string result)
    { 
    
        string[] list = result.Split('*');
        /*
        switch ( int.Parse( list[2]))
        {
            case 1:
                foreach (string item in crtItem.Data.results.Keys)
                {
                    Addition.DropBag(item, crtItem.Data.results[item] * number, 3);
                }
                break;

            case 2:
                return;
                foreach (string item in crtItem.Data.results.Keys)
                {
                    Addition.DropBag(item, crtItem.Data.results[item] * number, 1);
                }
                break;

            case 3:
                SendNotification(NotiList.Add_Money, 10000000 * number);
                break;

            case 4:
                return;
                foreach (string item in crtItem.Data.results.Keys)
                {
                    SendNotification(NotiList.CrateWing, CrateStditems.CrateWing());
                }

                break;

            case 5:
                foreach (string item in crtItem.Data.results.Keys)
                {
                    SumSave.UserBase.MilitaryExploits += crtItem.Data.results[item] * number;
                }
                SendNotification(NotiList.MilitaryExploits, 0);
                break;
            case 6:
                string[] list = crtItem.Data.resultvalue.Split('*');
                SendNotification(NotiList.CrateEquip, CrateStditems.CrateStditem(list[0], -1, false));
                break;
            case 7:
                string[] Split = crtItem.Data.resultvalue.Split('*');
                if (称号中心.Instance.CheckTitle(Split[0])) { AlertDec.Show("已拥有此称号"); return; }
                foreach (var item in SumSave.AllDbTitles)
                {
                    if (item.show_name == Split[0]) { item.uid = SumSave.UserBase.uid; item.par = SumSave.Tap_server; SumSave.AllHeroTitles.Add(item); break; }
                }
                称号中心.Instance.AddDBTitle(Split[0]);
                AudioManager.Instance.playAudio(ClipEnum.获得称号);
                SendNotification(NotiList.Refresh);//刷新数据
                break;
            case 8:
                称号中心.Instance.AddSkin(crtItem.Data.resultvalue.Split("*")[0]);
                break;
            default:
                break;
        }
        */

    }

    protected override void Awake()
    {
        base.Awake();

    }

}
