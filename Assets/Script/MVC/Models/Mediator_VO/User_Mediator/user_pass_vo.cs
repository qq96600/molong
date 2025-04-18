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

    /// <summary>
    /// �ۻ��������
    /// </summary>
    public int Max_task_number;
    /// <summary>
    /// ����״̬
    /// </summary>
    public string day_state_value;
    /// <summary>
    /// ÿ������״̬
    /// </summary>
    public List<int> day_state;
    /// <summary>
    /// �û���ȡ״̬
    /// </summary>
    private List<int> data_day_state = new List<int>();

    ///��ȡ״̬
    private Dictionary<int,List<int>> dic_user_values = new Dictionary<int, List<int>>();

    public void Init()
    {
        string[] str = user_value.Split('|');
        day_state = new List<int>();
        data_day_state = new List<int>();
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
        str = day_state_value.Split('|');
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != "")
            {
                data_day_state.Add(int.Parse(str[i]));
                day_state.Add(0);
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
    /// ��ȡ����״̬
    /// </summary>
    /// <returns></returns>
    public List<int> Get_day_state()
    { 
     return data_day_state;
    }
    /// <summary>
    /// �����û���ȡ״̬
    /// </summary>
    /// <param name="list"></param>
    public void Get(Dictionary<int, List<int>> list)
    {
        dic_user_values = list;
    }
    /// <summary>
    /// д��״̬
    /// </summary>
    /// <param name="list"></param>
    public void Get(int index)
    {
        data_day_state[index] = 1;
    }
    /// <summary>
    /// д����ȡ
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// д��ÿ������
    /// </summary>
    /// <returns></returns>
    private string Set_day_state_value()
    {
        string dec = "";

        for (int i = 0; i < data_day_state.Count; i++)
        {
            dec += (dec == "" ? "" : "|") + data_day_state[i];
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
        GetStr(Max_task_number),
        GetStr(Set_data()),
        GetStr(Set_day_state_value())
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
        "pass_lv",
        "pass_exp",
        "Max_task_number",
        "user_value",
        "day_state_value"
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
        GetStr(data_lv),
        GetStr(data_exp),
        GetStr(Max_task_number),
        GetStr(Set_data()),
        GetStr(Set_day_state_value())
        };
    }
}
