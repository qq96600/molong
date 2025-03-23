using MVC;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_battle_attck : BattleAttack
{
    
    public override void OnAuto()
    {
        base.OnAuto();
        //判断技能
        //player_move(开天);
        BaseAttack();

    }

  

    private void BaseAttack()
    {
        float damage = 0;
        BattleAttack monster = Terget.GetComponent<BattleAttack>();
        if (monster.target.HP <= 0) return;//结战斗
        if (Data.Type == 1)
        {
            damage = Random.Range(Data.damageMin, Data.damageMax) - Random.Range(monster.Data.DefMin, monster.Data.DefMax);
        }else
        if (Data.Type == 2)
        {
            damage = Random.Range(Data.MagicdamageMin, Data.MagicdamageMax) - Random.Range(monster.Data.MagicDefMin, monster.Data.MagicDefMax);
        }

        if (Random.Range(0, 100) > Data.hit - monster.Data.dodge)
        {
            //传递消息，未命中;
            return;
        }
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            damage = damage * data.crit_damage / 100;
        }
        monster.target.TakeDamage(damage);
    }

}
