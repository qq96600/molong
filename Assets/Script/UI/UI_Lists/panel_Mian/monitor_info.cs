using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class monitor_info : Base_Mono
{
    ///<summary>
    ///右上角健康栏Hp,Mp显示
    /// <summary>
    private Slider role_exp;

    private Text show_name, show_moeny, show_point, show_diamond,show_exp;

    private BattleHealth health;
    /// <summary>
    /// 角色皮肤
    /// </summary>
    private enum_skin_state skin_state;
    /// <summary>
    /// 角色皮肤预制体
    /// </summary>
    private GameObject skin_prefabs;
    /// <summary>
    /// 角色头像
    /// </summary>
    private Transform pos_health;
    private void Awake()
    {
        role_exp = Find<Slider>("base_info/Slider");
        show_exp = Find<Text>("base_info/Slider/exp");
        show_name = Find<Text>("base_info/role_name/info");
        show_moeny = Find<Text>("show_unit/moeny/info");
        show_point = Find<Text>("show_unit/Point/info");
        show_diamond = Find<Text>("show_unit/diamond/info");
        pos_health = Find<Transform>("base_info/profile_picture");
        Instance_Skin();
    }
    /// <summary>
    /// 初始化角色皮肤
    /// </summary>
    private void Instance_Skin()
    {
        for (int i = pos_health.childCount - 1; i >= 0; i--)//清空区域内按钮
        {
            Destroy(pos_health.GetChild(i).gameObject);
        }
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/内观_" + SumSave.crt_hero.hero_pos);
        Instantiate(skin_prefabs, pos_health);
    }
    private void Update()
    {
        if (SumSave.crt_MaxHero != null)
        {
            if (int.Parse(SumSave.crt_hero.hero_index) != (int)skin_state)
            {
                Instance_Skin();
            }
            show_name.text = SumSave.crt_MaxHero.show_name;
            show_exp.text = " Lv." + SumSave.crt_MaxHero.Lv +
               "(" + SumSave.crt_MaxHero.Exp * 100 / SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv] + "%)";
            role_exp.value = SumSave.crt_MaxHero.Exp;
            List<long> list = SumSave.crt_user_unit.Set();
            show_moeny.text = Battle_Tool.FormatNumberToChineseUnit(list[0]) + " " + currency_unit.灵珠;
            show_point.text = Battle_Tool.FormatNumberToChineseUnit(list[1]) + " " + currency_unit.历练;
            show_diamond.text = Battle_Tool.FormatNumberToChineseUnit(list[2]) + " " + currency_unit.魔丸;

        }
    }
}
