using MVC;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_battle_attck : BattleAttack
{

    /// <summary>
    /// ��ʼ��ս��װ��
    /// </summary>
    /// <param name="skills"></param>
    public override void Refresh_Skill(List<skill_offect_item> skills)
    {
        base.Refresh_Skill(skills);
        battle_skills = skills;
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].Battle();
        }
    }

    public override void OnAuto()
    {
        base.OnAuto();
        //�жϼ���
        Debug.Log(1);
        for (int i = 0; i < battle_skills.Count; i++)
        {
            if (battle_skills[i].IsState())
            {
                if (target.MP >= battle_skills[i].Data.skill_spell * target.maxMP / 100)
                { 
                    target.MP -= battle_skills[i].Data.skill_spell * target.maxMP / 100;
                    //�ͷż���
                    BaseAttack(battle_skills[i].Data);
                    battle_skills[i].Battle();
                    return;
                }
            }
        }
        //player_move(����);
        BaseAttack();

    }
    /// <summary>
    /// �ͷż���
    /// </summary>
    /// <param name="data"></param>
    private void BaseAttack(base_skill_vo data)
    {
        //�ͷż���
    }

    private void BaseAttack()
    {
        float damage = 0f;
        BattleAttack monster = Terget.GetComponent<BattleAttack>();
        if (monster.target.HP <= 0) return;//��ս��
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
            //������Ϣ��δ����;
            return;
        }
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            damage = damage * data.crit_damage / 100;
        }
        if (Random.Range(0, 100) < 50) StateMachine.stateAutoInit(1);
        else StateMachine.stateAutoInit(1, "kai");
        
        damage = 1000000000000000;

        monster.target.TakeDamage(damage,monster);
    }

}
