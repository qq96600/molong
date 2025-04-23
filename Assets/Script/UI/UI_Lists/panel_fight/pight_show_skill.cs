using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pight_show_skill : Base_Mono
{
    /// <summary>
    /// 功能按键
    /// </summary>
    private Transform crt_attack_skill, crt_special_skill;
    private skill_offect_item skill_item_parfabs;
   
    private void Awake()
    {
        crt_attack_skill = Find<Transform>("attack_skill");
        crt_special_skill = Find<Transform>("special_skill");
        skill_item_parfabs = Resources.Load<skill_offect_item>("Prefabs/panel_skill/skill_offect_item");
    }
    public List<skill_offect_item> Init()
    {
        List<skill_offect_item> battle_skills = new List<skill_offect_item>();

        for (int i = crt_attack_skill.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_attack_skill.GetChild(i).gameObject);
        }
        for (int i = crt_special_skill.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_special_skill.GetChild(i).gameObject);
        }

        List<int> attack_numbers = new List<int>() { 1, 2, 3, 4 };
        List<int> special_numbers = new List<int>() { 1, 2 };

        for (int j = 0; j < attack_numbers.Count; j++)
        {
            for (int i = 0; i < SumSave.crt_skills.Count; i++)
            {
                if (int.Parse(SumSave.crt_skills[i].user_values[2]) == attack_numbers[j])
                {
                    if ((skill_btn_list)SumSave.crt_skills[i].skill_type == skill_btn_list.战斗)
                    {
                        skill_offect_item item = Instantiate(skill_item_parfabs, crt_attack_skill);
                        item.Data = SumSave.crt_skills[i];
                        item.GetComponent<Button>().onClick.AddListener(() => { On_Click(item); });
                        battle_skills.Add(item);
                        continue;
                    }
                }

            }
           
        }

        for (int j = 0; j < special_numbers.Count; j++)
        {
            for (int i = 0; i < SumSave.crt_skills.Count; i++)
            {
                if (int.Parse(SumSave.crt_skills[i].user_values[2]) == special_numbers[j])
                {
                    if ((skill_btn_list)SumSave.crt_skills[i].skill_type == skill_btn_list.秘笈)
                    {
                        skill_offect_item item = Instantiate(skill_item_parfabs, crt_special_skill);
                        item.Data = SumSave.crt_skills[i];
                        item.GetComponent<Button>().onClick.AddListener(() => { On_Click(item); });
                        continue;
                    }
                }
            }
        }
        return ArrayHelper.Ascending(battle_skills, e => int.Parse(e.Data.user_values[2]));
    }

    private void On_Click(skill_offect_item item)
    {

    }
}
