using Common;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using MVC;
/// <summary>
/// 状态类列表
/// </summary>
public enum State_List
{
    经验丹 = 1,
    历练丹,
    月卡,
    天气,
    至尊卡,

}
/// <summary>
/// 验证状态
/// </summary>
public  static class Tool_State 
{
    private static Dictionary<State_List,bool> state_list = new Dictionary<State_List, bool>();

    /// <summary>
    /// 验证状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static bool IsState(State_List state)
    {
        if (state_list.ContainsKey(state))
        { 
            return state_list[state];
        }
        self_inspection();
        return state_list[state];
    }

    /// <summary>
    /// 激活状态
    /// </summary>
    /// <param name="state"></param>
    public static void activation_State(State_List state)
    {
        if (state_list.ContainsKey(state))
        {
             state_list[state]=true;
        }else state_list.Add(state, true);
    }
    /// <summary>
    /// 每次进入地图验证
    /// </summary>
    public static void self_inspection()
    {
        for (int i = 1; i < Enum.GetNames(typeof(State_List)).Length+1; i++)
        {
            if (SumSave.crt_player_buff.player_Buffs.Count > 0)
            {
                foreach (var item in SumSave.crt_player_buff.player_Buffs)
                {
                    (DateTime, int, float, int) time = item.Value;
                    if (time.Item4 == i)//
                    {
                        if (state_list.ContainsKey((State_List)(i)))
                        { 
                            state_list[(State_List)(i)] = false;
                        }else state_list.Add((State_List)(i), false);
                        int remainingTime = Battle_Tool.SettlementTransport((time.Item1).ToString("yyyy-MM-dd HH:mm:ss"), 2);
                        if (remainingTime < time.Item2 * 60)//有效期内
                            state_list[(State_List)(i)] = true;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 获取角色加成概率
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool Is_playerprobabilit(enum_skill_attribute_list value)
    {
        bool exist = false;
        if (SumSave.crt_MaxHero.bufflist[(int)value] > 0)
        {
            if (Random.Range(0, 100) < SumSave.crt_MaxHero.bufflist[(int)value])
            {
                exist = true;
            }
        }
        return exist;
    }

}
