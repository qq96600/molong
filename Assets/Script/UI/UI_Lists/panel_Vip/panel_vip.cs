using Common;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_vip : Panel_Base
{
    /// <summary>
    /// VIP信息显示位置
    /// </summary>
    private Transform Information;
    /// <summary>
    /// vip信息预制体
    /// </summary>
    private vip_effect vip_effect_obj;
    /// <summary>
    /// vip信息标题
    /// </summary>
    private List<string> Title_list = new List<string>() { "VIP等级", "升级经验", "经验加成", "灵珠收益", "装备爆率", "人物历练", "寻怪间隔", "生命回复", "法力回复", "幸运", "强化费用", "离线间隔", "签到收益", "鞭尸", "灵气上限"};
    /// <summary>
    /// vip等级显示位置
    /// </summary>
    private Text show_vip_lv_text;
    private new void Awake()
    {
        Initialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        Information=Find<Transform>("Information/Viewport/Content");
        vip_effect_obj = Battle_Tool.Find_Prefabs<vip_effect>("vip_effect");
        show_vip_lv_text= Find<Text>("show_vip_lv/Text");
        Show_Vip_Info();
    }

    /// <summary>
    /// 显示vip信息
    /// </summary>
    public void Show_Vip_Info()
    {
        ClearObject(Information);

        //if(SumSave.crt_hero.hero_vip_lv_exp=="")
        //{
        //    SumSave.crt_hero.hero_vip_lv_exp= "0 0";
        //}
        //string[] lv_exp = SumSave.crt_hero.hero_vip_lv_exp.Split(' ');
        //show_vip_lv_text.text.Clone();
        //show_vip_lv_text.text = "VIP等级:" + lv_exp[0]+" 经验:"+lv_exp[1];//显示vip等级和经验

        vip_effect Title = Instantiate(vip_effect_obj, Information);//实例化vip信息标题
        Title.Init(-1, Title_list);

        for (int i = 0; i < SumSave.db_vip_list.Count; i++)//实例化vip信息
        {
            vip_effect vip_effect = Instantiate(vip_effect_obj, Information);
            vip_effect.Init(i, SumSave.db_vip_list[i]);
        }
    }



    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
}
