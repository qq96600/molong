using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_vo : Base_VO
{
    private List<int> list = new List<int>();

    private List<int> verify_list = new List<int>();

    private int index = -1;
    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="value"></param>
    public void Init(string value)
    {
        index = Random.Range(1, 1000);
        string[] str = value.Split(',');
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].Length > 0)
            {
                list.Add(int.Parse(str[i]));
                verify_list.Add(int.Parse(str[i]) + index);
            }
        }
    }
    public List<int> Set()
    { 
      return list;
    }

    private string Set_data()
    {

        string dec = "";

        for (int i = 0; i < list.Count; i++)
        {
            dec += list[i] + ",";
        }
        return dec;
    }

    /// <summary>
    /// ��֤����
    /// </summary>
    public void verify_data(currency_unit _index,int value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //ԭʼ����δ�����ı�
            if (list[i] + index == verify_list[i])
            {
            }
            else Game_Omphalos.i.Delete(_index + " ��ʾ���� " + list[i] + " ��ֵ֤ " + index + " " + verify_list[i]);
        }

        switch (_index)
        {
            case currency_unit.����:
                list[0] += value;
                verify_list[0] += value;
                break;
            case currency_unit.����:
                if (value >= SumSave.base_setting[0]) Game_Omphalos.i.Delete("���" + (currency_unit)_index + value);
                else
                {
                    list[1] += value;
                    verify_list[1] += value;
                }
                return;

            case currency_unit.Ԫ��:
                if (value >= SumSave.base_setting[1]) Game_Omphalos.i.Delete("���" + (currency_unit)_index + value);
                else
                {
                    list[2] += value;
                    verify_list[2] += value;
                }
                return;
        }
        
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "value" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] { GetStr(Set_data()) };
    }

    public override string[] Set_Instace_String()
    {
        return new string[] {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(Set_data())
        };
    }
}
