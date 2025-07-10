using Common;
using MVC;
using UnityEngine;
/// <summary>
/// 角色数据
/// </summary>
public class Hero_VO : Base_VO
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string hero_name;
    /// <summary>
    /// 角色职业
    /// </summary>
    public string hero_type;
    /// <summary>
    /// 角色编号
    /// </summary>
    public string hero_index="1";
    /// <summary>
    /// 已有英雄列表
    /// </summary>
    public string hero_list1;
    /// <summary>
    /// 角色等级
    /// </summary>
    public string hero_lv;
    /// <summary>
    /// 角色等级
    /// </summary>
    public int hero_Lv;
    /// <summary>
    /// 角色经验
    /// </summary>
    public string hero_exp;
    /// <summary>
    /// 角色经验
    /// </summary>
    public long hero_Exp;
    /// <summary>
    /// 角色VIP 等级 经验
    /// </summary>
    public string hero_vip_lv_exp1;

    /// <summary>
    /// 角色上阵选择
    /// </summary>
    public string hero_pos;
    /// <summary>
    /// 角色存储
    /// </summary>
    public string hero_value;
    /// <summary>
    /// 获取英雄资源
    /// </summary>
    public string hero_material;
    /// <summary>
    /// 英雄资源 转生 强化
    /// </summary>
    public int[] hero_material_list;
    /// <summary>
    /// 天命台
    /// </summary>
    public int[] tianming_Platform;


    /// <summary>
    /// 当前职业和天命台写入
    /// </summary>
    private string Get_hero_pos_vase()
    {
        string str = "";
        str = hero_pos + "|" + ArrayHelper.Data_Encryption(tianming_Platform);
        return str;
    }

    /// <summary>
    /// 刷新天命台属性并写入
    /// </summary>
    public void RefreshTianming()
    {
        for (int i = 0; i < tianming_Platform.Length; i++)
        {
            int index = Random.Range(1, 6);
            tianming_Platform[i] = index;
        }
        MysqlData();
    }


    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(hero_name),
            GetStr(hero_lv),
            GetStr(hero_exp),
            GetStr(Get_hero_pos_vase()),
            GetStr(hero_value),
            GetStr(ArrayHelper.Data_Encryption(hero_material_list)),
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "hero_name",
            "hero_lv",
            "hero_exp",
            "hero_pos",
            "hero_value",
            "hero_material",
        };
    }

    public override void MysqlData()
    {
        base.MysqlData();
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero,
                    SumSave.crt_hero.Set_Uptade_String(), SumSave.crt_hero.Get_Update_Character());
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
       {
            GetStr(hero_name),
            GetStr(hero_Lv),
            GetStr(hero_Exp),
            GetStr(Get_hero_pos_vase()),
            GetStr(hero_value),
            GetStr(ArrayHelper.Data_Encryption(hero_material_list)),

       };
    }

}
