using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_world_boos : Base_VO
{
    /// <summary>
    /// 当前世界boss名称
    /// </summary>
    public string name;
    /// <summary>
    /// 当前区服玩家总体的伤害
    /// </summary>
    private long maxHp;
    /// <summary>
    /// 期数
    /// </summary>
    public int number;


    /// <summary>
    /// 获取当前区服玩家总体的伤害
    /// </summary>
    /// <returns></returns>
    public long Get()
    {
        return maxHp;
    }
    /// <summary>
    /// 添加当前区服玩家总体的伤害
    /// </summary>
    /// <param name="finalDamage"></param>
    public void Set(long finalDamage)
    {
        maxHp -= finalDamage;
    }
    
    /// <summary>
    /// 初始化当前区服玩家总体的伤害
    /// </summary>
    /// <param name="_FinalDamage"></param>
    public void InitFinalDamage(long _FinalDamage)
    {
        maxHp = _FinalDamage;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.par),
            GetStr(name),
            GetStr(maxHp),
            GetStr(number)
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { 
            "FinalDamage",
            "number"
        };

    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
         {
             GetStr(maxHp),
             GetStr(number)
         };
    }

}
