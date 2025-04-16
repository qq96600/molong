using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_role_health : Base_Mono
{
    ///<summary>
    ///”“…œΩ«Ω°øµ¿∏Hp,Mpœ‘ æ
    /// <summary>
    private Slider role_Hp, role_Mp, role_internalforceMP, role_EnergyMp;

    private Text show_name, show_moeny, show_point;



    private BattleHealth health;
    private void Awake()
    {
        role_Hp = Find<Slider>("panel_Hp");
        role_Mp = Find<Slider>("panel_Mp");
        role_internalforceMP = Find<Slider>("panel_internalforceMP");
        role_EnergyMp = Find<Slider>("panel_EnergyMp");
        show_name = Find<Text>("role_name/info");
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
    }
    
    private void Update()
    {
        if (health != null)
        {
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
