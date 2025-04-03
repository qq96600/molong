using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_pass_vo : Base_VO
{
    /// <summary>
    /// �ȼ�
    /// </summary>
    public int lv;
    /// <summary>
    /// �ڼ���ͨ��֤
    /// </summary>
    public int pass_index = 0;
    /// <summary>
    /// ����
    /// </summary>
    public string reward;
    /// <summary>
    /// ���׽���
    /// </summary>
    public string uplv_reward;
    /// <summary>
    /// �ȼ�
    /// </summary>
    public int data_lv;
    /// <summary>
    /// ���׽���
    /// </summary>
    public int data_uplv;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int data_exp;
    ///��ȡ״̬
    private Dictionary<int,List<int>> dic_user_values = new Dictionary<int, List<int>>();

    public void Init()
    { 
        string[] str = user_value.Split('|');

        for (int i = 0; i < str.Length; i++)
        {
            string[] str1 = str[i].Split(',');
            if (str1.Length > 1)
            {
                if(!dic_user_values.ContainsKey(int.Parse(str1[0])))dic_user_values.Add(int.Parse(str1[0]), new List<int>());

                for (int j = 1; j < str1.Length; j++)
                { 
                    if(str1[j]!="")
                    dic_user_values[int.Parse(str1[0])].Add(int.Parse(str1[j]));
                }
            }
        }
    }
    /// <summary>
    /// �����û���ȡ״̬
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, List<int>> Set()
    {
        return dic_user_values;

    }
    /// <summary>
    /// �����û���ȡ״̬
    /// </summary>
    /// <param name="list"></param>
    public void Get(Dictionary<int, List<int>> list)
    {
        dic_user_values = list;
    }

    private string Set_data()
    {
        string dec = "";

        foreach (var item in dic_user_values.Keys)
        {
            if (dec != "") dec += "|";
            dec += item + ",";
            foreach (var list in dic_user_values[item])
            {
                dec+= list + ",";
            }
        }
         
        return dec;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
        GetStr(0),
        GetStr(SumSave.crt_user.uid),
        GetStr(data_lv),
        GetStr(data_exp),
        GetStr("")
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
        "pass_lv",
        "pass_exp",
        "user_value"
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
        GetStr(data_lv),
        GetStr(data_exp),
        GetStr(Set_data())
        };
    }
}
