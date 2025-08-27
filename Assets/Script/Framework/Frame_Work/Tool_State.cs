using Common;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using MVC;
using System.Text;
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
        self_inspection();
        if (state_list.ContainsKey(state))
        { 
            return state_list[state];
        }
        return state_list[state];
    }
    /// <summary>
    /// 获取临时状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static int GetSetMapState(string state)
    {
        
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        List<Bag_Base_VO> sell_list = new List<Bag_Base_VO>();
        int number = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == state)
            {
                number = list[i].Item2;
            }
        }
        return number;
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

        if(state_list.Count != Enum.GetNames(typeof(State_List)).Length)
        {
            for (int i = 1; i < Enum.GetNames(typeof(State_List)).Length + 1; i++)
            {
                state_list.Add((State_List)(i), false);
            }
        }

        for (int i = 1; i < state_list.Count + 1; i++)
        {
            if (SumSave.crt_player_buff.player_Buffs.Count > 0)
            {
                foreach (var item in SumSave.crt_player_buff.player_Buffs)
                {
                    (DateTime, int, float, int) time = item.Value;
                    if (time.Item4 == i)//
                    {
                        int remainingTime = Battle_Tool.SettlementTransport((time.Item1).ToString("yyyy-MM-dd HH:mm:ss"), 2);
                        if (remainingTime < time.Item2 * 60)//有效期内
                        {
                            state_list[(State_List)(i)] = true;
                        }
                        else
                        {
                            SumSave.crt_player_buff.player_Buffs.Remove(item.Key);
                            return;
                        }

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
        if (SumSave.crt_MaxHero.bufflist.Count > (int)value)
        {
            if (SumSave.crt_MaxHero.bufflist[(int)value] > 0)
            {
                if (Random.Range(0, 100) < SumSave.crt_MaxHero.bufflist[(int)value])
                {
                    exist = true;
                }
            }
        }
        
        return exist;
    }
    public static int Value_playerprobabilit(enum_skill_attribute_list value)
    {
        int number = 0;
        if (SumSave.crt_MaxHero.bufflist.Count > (int)value)
        {
            if (SumSave.crt_MaxHero.bufflist[(int)value] > 0)
            {
                number = SumSave.crt_MaxHero.bufflist[(int)value];
            }
        }
            
        return number;
    }
    /// <summary>
    /// 获取战斗属性buff
    /// </summary>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Value_playerprobabilit(List<int> list, enum_skill_attribute_list value)
    {
        int number = 0;
        if (list.Count > (int)value)
        {
            if (list[(int)value] > 0)
            {
                number = list[(int)value];
            }
        }
        return number;
    }

    private static void OnValueChanged(int arg0)
    {
        ///获得装备品质枚举
        var enumNames = Enum.GetNames(typeof(enum_equip_quality_list));
        ///获得下拉列表的值
        var selectedValue = arg0;
        ///判断是否为最后一个选项
        bool isLastOption = selectedValue == enumNames.Length - 1;
        ///创建一个StringBuilder对象来构建字符串
        var stringBuilder = new StringBuilder();

        for (int i = 0; i < enumNames.Length; i++)
        {
            //string percentage = GetPercentage(selectedValue, i, isLastOption);
        }
    }
    /// <summary>
    /// 获得对应概率
    /// </summary>
    /// <param name="selectedValue"></param>
    /// <param name="currentIndex"></param>
    /// <param name="isLastOption"></param>
    /// <returns></returns>
    private static int GetPercentage(int selectedValue, int currentIndex, bool isLastOption)
    {

        if (isLastOption)
        {
            return selectedValue == currentIndex ? 10 : 0;
        }

        if (selectedValue == currentIndex) return 50;
        if (selectedValue - 1 == currentIndex) return 45;
        if (selectedValue + 1 == currentIndex) return 5;

        return 0;
    }

}
