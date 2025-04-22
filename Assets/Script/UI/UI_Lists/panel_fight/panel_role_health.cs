using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_role_health : Base_Mono
{
    ///<summary>
    ///”“…œΩ«Ω°øµ¿∏Hp,Mpœ‘ æ
    /// <summary>
    private Slider role_Hp, role_Mp, role_internalforceMP, role_EnergyMp, role_exp;

    private Text show_name, show_moeny, show_point;

    private Button btn_back;

    private BattleHealth health;
    private void Awake()
    {
        role_Hp = Find<Slider>("panel_Hp");
        role_Mp = Find<Slider>("panel_Mp");
        role_exp = Find<Slider>("panel_exp");
        role_internalforceMP = Find<Slider>("panel_internalforceMP");
        role_EnergyMp = Find<Slider>("panel_EnergyMp");
        show_name = Find<Text>("role_name/info");
        btn_back= Find<Button>("role_name");
        btn_back.onClick.AddListener(() => {
            UI_Manager.I.TogglePanel(Panel_List.panel_Buff, true);
        });
        show_moeny = Find<Text>("show_unit/moeny/info");
        show_point = Find<Text>("show_unit/Point/info");
    }
    public void SetHealth(BattleHealth _health)
    {
        health = _health;
        role_Hp.maxValue = health.maxHP;
        role_Mp.maxValue = health.maxMP;
        role_internalforceMP.maxValue = health.internalforcemaxMP;
        role_EnergyMp.maxValue = health.EnergymaxMp;
        show_name.text = SumSave.crt_MaxHero.show_name;
        role_exp.maxValue=SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv];
    }
    
    private void Update()
    {
        if (health != null)
        {
            show_name.text = SumSave.crt_MaxHero.show_name + " Lv." + SumSave.crt_MaxHero.Lv +
               "(" + SumSave.crt_MaxHero.Exp * 100 / SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv] + "%)";
            role_exp.value = SumSave.crt_MaxHero.Exp;
            if (SumSave.crt_MaxHero.Exp >= SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv])
            { 
                SumSave.crt_MaxHero.Lv++;
                SumSave.crt_MaxHero.Exp = 0;
                SumSave.crt_hero.hero_Exp = 0;
                SumSave.crt_hero.hero_Lv++;
                role_exp.maxValue = SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv];
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto,Mysql_Table_Name.mo_user_hero,
                    SumSave.crt_hero.Set_Uptade_String(),SumSave.crt_hero.Get_Update_Character());
                //À¢–¬ ˝æ›
                SendNotification(NotiList.Refresh_Max_Hero_Attribute);
            }
            role_Hp.value = health.HP;
            role_Mp.value = health.MP;
            role_internalforceMP.value = health.internalforceMP;
            role_EnergyMp.value = health.EnergyMp;
            List<long> list = SumSave.crt_user_unit.Set();
            show_moeny.text = Battle_Tool.FormatNumberToChineseUnit(list[0]) + " " + currency_unit.¡È÷È;
            show_point.text = Battle_Tool.FormatNumberToChineseUnit(list[1]) + " " + currency_unit.¿˙¡∑;
        } 
    }
}
