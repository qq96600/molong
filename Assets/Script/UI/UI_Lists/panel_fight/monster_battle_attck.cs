using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_battle_attck : BattleAttack
{

    public override void OnAuto()
    {
        base.OnAuto();
        //�жϼ���
        BaseAttack();
        
    }

    private void BaseAttack()
    {
        float damage = 0;
        BattleAttack monster = Terget.GetComponent<BattleAttack>();
        if (monster.target.HP <= 0) return;//��ս��
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
            //������Ϣ��δ����;
            monster.target.TakeDamage(1, DamageEnum.����δ����, monster);
            return;
        }
        bool crit_rate = false;
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            crit_rate = true;
            damage = damage * data.crit_damage / 100;
        }
        damage = 100;
        if (crit_rate)
        monster.target.TakeDamage(damage,DamageEnum.�����˺�, monster);
        else monster.target.TakeDamage(damage, DamageEnum.��ͨ�˺�, monster);


    }

  

}
