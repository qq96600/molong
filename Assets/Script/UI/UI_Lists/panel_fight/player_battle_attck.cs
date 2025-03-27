using MVC;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_battle_attck : BattleAttack
{

    /// <summary>
    /// 初始化战斗装备
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
        //判断技能
        Debug.Log(1);
        for (int i = 0; i < battle_skills.Count; i++)
        {
            if (battle_skills[i].IsState())
            {
                if (target.MP >= battle_skills[i].Data.skill_spell * target.maxMP / 100)
                { 
                    target.MP -= battle_skills[i].Data.skill_spell * target.maxMP / 100;
                    //释放技能
                    BaseAttack(battle_skills[i].Data);
                    battle_skills[i].Battle();
                    return;
                }
            }
        }
        //player_move(开天);
        BaseAttack();

    }
    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="data"></param>
    private void BaseAttack(base_skill_vo data)
    {
        //释放技能
    }

    private void BaseAttack()
    {
        float damage = 0f;
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
        if (Random.Range(0, 100) < 50) StateMachine.stateAutoInit(1);
        else StateMachine.stateAutoInit(1, "kai");
        
        damage = 1000000000000000;

        monster.target.TakeDamage(damage,monster);
    }

}
