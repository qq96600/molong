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

    private void BaseAttack()
    {
        float damage = 0;
        BattleAttack monster = Terget.GetComponent<BattleAttack>();
        if (monster.target.HP <= 0) return;//结战斗
        if (Data.Type == 1)
        {
            damage = Random.Range(Data.damageMin, Data.damageMax) - Random.Range(monster.Data.DefMin, monster.Data.DefMax);
        }
        else
        if (Data.Type == 2)
        {
            damage = Random.Range(Data.MagicdamageMin, Data.MagicdamageMax) - Random.Range(monster.Data.MagicDefMin, monster.Data.MagicDefMax);
        }

        if (Random.Range(0, 100) > Data.hit - monster.Data.dodge)
        {
            //传递消息，未命中;
            monster.target.TakeDamage(1, DamageEnum.技能未命中);
            return;
        }
        bool crit_rate = false;
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            crit_rate = true;
            damage = damage * data.crit_damage / 100;
        }
        damage = 1;
        if (crit_rate)
        monster.target.TakeDamage(damage,DamageEnum.暴击伤害);
        else monster.target.TakeDamage(damage, DamageEnum.普通伤害);


    }

  

}
