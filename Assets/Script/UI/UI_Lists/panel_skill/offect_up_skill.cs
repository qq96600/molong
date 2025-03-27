using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class offect_up_skill : Base_Mono
{
    /// <summary>
    /// 功能按键
    /// </summary>
    private Transform crt_attack_skill, crt_special_skill;

    private btn_item btn_Item_parfabs;

    private skill_offect_item skill_item_parfabs;
    /// <summary>
    /// 当前技能
    /// </summary>
    private base_skill_vo user_skill;

    private Button close;
    
    private void Awake()
    {
        crt_attack_skill=Find<Transform>("attack_skill");
        crt_special_skill = Find<Transform>("special_skill");
        btn_Item_parfabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        close = Find<Button>("close_button");
        close.onClick.AddListener(() => { On_Close(); });

    }
    /// <summary>
    /// 关闭
    /// </summary>
    private void On_Close()
    {
        gameObject.SetActive(false);
    }

    private void Init()
    {
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
            bool exist = true;
            for (int i = 0; i < SumSave.crt_skills.Count; i++)
            {
                string[] skill = SumSave.crt_skills[i].user_value.Split(' ');
                if (int.Parse(skill[2]) == attack_numbers[j])
                {
                    if ((skill_btn_list)SumSave.crt_skills[i].skill_type == skill_btn_list.战斗)
                    {
                        exist = false;
                        skill_offect_item item = Instantiate(skill_item_parfabs, crt_attack_skill);
                        item.Data = SumSave.crt_skills[i];
                        item.GetComponent<Button>().onClick.AddListener(() => { On_Click(item); });
                        continue;
                    }
                    

                }

            }
            if (exist)
            { 
                btn_item item = Instantiate(btn_Item_parfabs, crt_attack_skill);
                item.Show(attack_numbers[j], attack_numbers[j] + "战斗位");
                item.GetComponent<Button>().onClick.AddListener(() => { On_Attack_Click(item); });
            }
        }

        for (int j = 0; j < special_numbers.Count; j++)
        {
            bool exist = true;
            for (int i = 0; i < SumSave.crt_skills.Count; i++)
            {
                string[] skill = SumSave.crt_skills[i].user_value.Split(' ');
                if (int.Parse(skill[2]) == attack_numbers[j])
                {
                    if ((skill_btn_list)SumSave.crt_skills[i].skill_type == skill_btn_list.秘笈)
                    {
                        exist = false;
                        skill_offect_item item = Instantiate(skill_item_parfabs, crt_special_skill);
                        item.Data = SumSave.crt_skills[i];
                        item.GetComponent<Button>().onClick.AddListener(() => { On_Click(item); });
                        continue;
                    }


                }

            }
            if (exist)
            {
                btn_item item = Instantiate(btn_Item_parfabs, crt_special_skill);
                item.Show(attack_numbers[j], attack_numbers[j] + "秘笈位");
                item.GetComponent<Button>().onClick.AddListener(() => { On_Special_Click(item); });
            }
        }

    }
    /// <summary>
    /// 设置特殊技能
    /// </summary>
    /// <param name="item"></param>
    private void On_Special_Click(btn_item item)
    {

    }

    /// <summary>
    /// 切换战斗技能
    /// </summary>
    /// <param name="item"></param>
    private void On_Attack_Click(btn_item item)
    {

    }

    /// <summary>
    /// 切换技能
    /// </summary>
    /// <param name="item"></param>
    private void On_Click(skill_offect_item item)
    {

    }

    public void Show(base_skill_vo skill)
    {
        Init();
        user_skill = skill;
    }
}
