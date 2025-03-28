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
    private Slider role_Hp, role_Mp;

    private BattleHealth health;
    private void Awake()
    {
        role_Hp = Find<Slider>("panel_Hp");
        role_Mp = Find<Slider>("panel_Mp");
    }

    public void SetHealth(BattleHealth _health)
    {
        health = _health;
        role_Hp.maxValue = health.maxHP;
        role_Mp.maxValue = health.maxMP;
    }
    
    private void Update()
    {
        if (health != null)
        {
            role_Hp.value = health.HP;
            role_Mp.value = health.MP;
        }

    }
}
