using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_world_vo : Base_VO
{
    /// <summary>
    /// �ȼ�
    /// </summary>
    public int World_Lv;
    /// <summary>
    /// 0 ���ˢ��ʱ�� 1����ֵ
    /// </summary>
    private List<string> value_lists = new List<string>();

    public void Init()
    {
        string[] spilts = user_value.Split('&');
        if (spilts.Length > 1)
        {
            for (int i = 0; i < spilts.Length; i++)
            {
                value_lists.Add(spilts[i]);
            }
        }
    }
    /// <summary>
    /// ��ȡֵ
    /// </summary>
    /// <returns></returns>
    public List<string> Get()
    { 
     return value_lists;
    }
    /// <summary>
    /// ��ȡֵ
    /// </summary>
    /// <param name="value"></param>
    public void Set(int value)
    {
        value_lists[0] = SumSave.nowtime.ToString();
        value_lists[1] = value.ToString();
    }
    public string Set_data()
    {
        string dec = "";
        for (int i = 0; i < value_lists.Count; i++)
        {
            if(i!=0)
            {
                dec += "&";
            }
            dec += value_lists[i] ;
        }
        return dec;
    }
    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <returns></returns>
    private string InitValue()
    {
        user_value = SumSave.nowtime + "&" + "0";
        Init();
        return user_value;
    }
    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <returns></returns>
    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(World_Lv),
            GetStr(InitValue())
        };
    }


    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "World_Lv",
            "user_value"
        };
    }


    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
            GetStr(World_Lv),
            GetStr(Set_data()),

        };
    }


}
