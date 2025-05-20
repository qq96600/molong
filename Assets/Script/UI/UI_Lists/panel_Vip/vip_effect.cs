using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class vip_effect : Base_Mono
{
    /// <summary>
    /// 编号
    /// </summary>
    private int index;
    /// <summary>
    /// vip数据
    /// </summary>
    private db_vip vip_data;

    /// <summary>
    /// 单个信息显示预制体
    /// </summary>
    private btn_item btn_item;

    private void Awake()
    {
        btn_item = Battle_Tool.Find_Prefabs<btn_item>("btn_item");
    }


    public void Init(int index,db_vip vip_data)
    {
        this.index = index;
        this.vip_data = vip_data;
        ShowInit();
    }

    public void Init(int index, List<string> title)
    {
        this.index = index;
        for (int i = 0; i < title.Count; i++)
        {
            btn_item go = Instantiate(btn_item, transform);
            go.Show(i, title[i]);
            if(index==-1)
            {
                go.Selected=true;
            }
        }
    }


    /// <summary>
    /// 按排序显示信息
    /// </summary>
    private void ShowInit()
    {
        ClearObject(transform);
        List<object> title = CollectingInformation();
        for (int i = 0; i < title.Count; i++)
        {
            btn_item go = Instantiate(btn_item, transform);
            go.Show(i, title[i]);
        }
        
    }
    /// <summary>
    /// 收集信息
    /// </summary>
    /// <param name="title"></param>
    private List<object> CollectingInformation()
    {
        List<object> title = new List<object>();
        title.Add(vip_data.vip_name);
        title.Add(vip_data.vip_exp);
        title.Add(vip_data.experienceBonus + tool_Categoryt.Obtain_unit((int)enum_skill_attribute_list.经验加成));
        title.Add(vip_data.lingzhuIncome);
        title.Add(vip_data.equipmentExplosionRate);
        title.Add(vip_data.characterExperience);
        title.Add(vip_data.monsterHuntingInterval);
        title.Add(vip_data.hpRecovery);
        title.Add(vip_data.manaRegeneration);
        title.Add(vip_data.goodFortune);
        title.Add(vip_data.strengthenCosts);
        title.Add(vip_data.offlineInterval);
        title.Add(vip_data.signInIncome);
        title.Add(vip_data.whippingCorpses);
        title.Add(vip_data.upperLimitOfSpiritualEnergy);
        return title;
    }

}
