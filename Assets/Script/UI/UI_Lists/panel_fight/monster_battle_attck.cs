using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_battle_attck : BattleAttack
{

    public override void OnAuto()
    {
        base.OnAuto();
        //判断技能
        BaseAttack();
    }
}
