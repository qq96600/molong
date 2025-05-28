using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_role_health : Base_Mono
{
    ///<summary>
    ///右上角健康栏Hp,Mp显示
    /// <summary>
    private Slider role_Hp, role_Mp, role_internalforceMP, role_EnergyMp, role_exp;

    private Text show_name, show_moeny, show_point;

    private Button btn_back;

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
        role_Hp = Find<Slider>("panel_Hp");
        role_Mp = Find<Slider>("panel_Mp");
        role_exp = Find<Slider>("panel_exp");
        role_internalforceMP = Find<Slider>("panel_internalforceMP");
        role_EnergyMp = Find<Slider>("panel_EnergyMp");
        show_name = Find<Text>("role_name/info");
        btn_back= Find<Button>("role_name");
        btn_back.onClick.AddListener(() => {
            UI_Manager.I.TogglePanel(Panel_List.panel_Buff, true);
        });
        show_moeny = Find<Text>("show_unit/moeny/info");
        show_point = Find<Text>("show_unit/Point/info");
        pos_health = Find<Transform>("profile_picture");
        Instance_Skin();
    }
    public void SetHealth(BattleHealth _health)
    {
        health = _health;
        role_Hp.maxValue = health.maxHP;
        role_Mp.maxValue = health.maxMP;
        role_internalforceMP.maxValue = health.internalforcemaxMP;
        role_EnergyMp.maxValue = health.EnergymaxMp;
        show_name.text = SumSave.crt_MaxHero.show_name;
        role_exp.maxValue=SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv];
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
     
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/within_" + SumSave.crt_hero.hero_pos);
        Instantiate(skin_prefabs, pos_health);
    }
    private void Update()
    {
        if (health != null)
        {
            if (int.Parse(SumSave.crt_hero.hero_index) != (int)skin_state)
            {
                Instance_Skin();
            }
            show_name.text = SumSave.crt_hero.hero_name + " Lv." + SumSave.crt_hero.hero_Lv +
               "(" + SumSave.crt_hero.hero_Exp * 100 / SumSave.db_lvs.hero_lv_list[SumSave.crt_hero.hero_Lv] + "%)";
            role_exp.maxValue = SumSave.db_lvs.hero_lv_list[SumSave.crt_hero.hero_Lv];
            role_exp.value = SumSave.crt_hero.hero_Exp;
            if (SumSave.crt_hero.hero_Exp >= SumSave.db_lvs.hero_lv_list[SumSave.crt_hero.hero_Lv])
            {
                SumSave.crt_MaxHero.Lv++;
                SumSave.crt_MaxHero.Exp -= SumSave.db_lvs.hero_lv_list[SumSave.crt_hero.hero_Lv];
                SumSave.crt_hero.hero_Exp -= SumSave.db_lvs.hero_lv_list[SumSave.crt_hero.hero_Lv];
                SumSave.crt_hero.hero_Lv++;
                role_exp.maxValue = SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv];
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto,Mysql_Table_Name.mo_user_hero,
                    SumSave.crt_hero.Set_Uptade_String(),SumSave.crt_hero.Get_Update_Character());
                //刷新数据
                SendNotification(NotiList.Refresh_Max_Hero_Attribute);
            }
            role_Hp.value = health.HP;
            role_Mp.value = health.MP;
            role_internalforceMP.value = health.internalforceMP;
            role_EnergyMp.value = health.EnergyMp;
            List<long> list = SumSave.crt_user_unit.Set();
            show_moeny.text = Battle_Tool.FormatNumberToChineseUnit(list[0]) + " " + currency_unit.灵珠;
            show_point.text = Battle_Tool.FormatNumberToChineseUnit(list[1]) + " " + currency_unit.历练;
        } 
    }



}
