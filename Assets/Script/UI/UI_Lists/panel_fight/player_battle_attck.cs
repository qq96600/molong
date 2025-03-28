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
        for (int i = 0; i < battle_skills.Count; i++)
        {
            if (battle_skills[i].IsState())
            {
                if (target.MP >= battle_skills[i].Data.skill_spell * target.maxMP / 100)
                { 
                    skill_offect_item skill = battle_skills[i];
                    target.MP -= battle_skills[i].Data.skill_spell * target.maxMP / 100;
                    //释放技能
                    BaseAttack(battle_skills[i].Data);
                    battle_skills[i].Battle();
                    battle_skills.RemoveAt(i);
                    battle_skills.Add(skill);
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
        StateMachine.stateAutoInit(data);
        //skill_damage(data);
    }

    protected void skill_damage(base_skill_vo skill)
    {
        Debug.Log("技能回调");
        float damage = 0f;
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
            monster.target.TakeDamage(1, DamageEnum.技能未命中, monster);
            return;
        }
        damage = damage * (skill.skill_damage + (skill.skill_power * int.Parse(skill.user_values[1]))) / 100;

        bool isCrit = false;
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            isCrit = true;
            damage = damage * data.crit_damage / 100;
        }
        damage = 100;

        monster.target.TakeDamage(damage, isCrit ? DamageEnum.暴击技能伤害 : DamageEnum.技能伤害, monster);
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
            monster.target.TakeDamage(1,DamageEnum.未命中, monster);
            return;
        }
        bool isCrit = false;
        if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
        {
            isCrit = true;
            damage = damage * data.crit_damage / 100;
        }
        damage = 100;

        monster.target.TakeDamage(damage, isCrit ? DamageEnum.暴击伤害 : DamageEnum.普通伤害, monster);
    }

}
