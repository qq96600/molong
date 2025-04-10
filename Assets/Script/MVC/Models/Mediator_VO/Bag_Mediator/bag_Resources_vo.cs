using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_Resources_vo : Base_VO
{
    private List<(string,int)> list = new List<(string,int)>();
    private List<(string, int)> verify_list = new List<(string, int)>();
    private int index = -1;

    public void Init(string value)
    {
        index = Random.Range(1, 1000);
        list = new List<(string, int)>();
        string[] artifact_value_array = value.Split(',');
        if (artifact_value_array.Length > 1)
        {
            for (int i = 0; i < artifact_value_array.Length; i++)
            {
                string[] artifact_array = artifact_value_array[i].Split(' ');
                if (artifact_array.Length > 1)
                {
                    list.Add((artifact_array[0], int.Parse(artifact_array[1])));
                    verify_list.Add((artifact_array[0], int.Parse(artifact_array[1]) + index));
                }
            }
        }
    }
    /// <summary>
    /// ��ȡ��׼
    /// </summary>
    /// <returns></returns>
    public List<(string, int)> Set()
    {
        return list;
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="list"></param>
    public void Get(Dictionary<string, int> dec,bool exist = false)
    {
        bool crate_state = false;
        //��֤����
        for (int i = 0; i < list.Count; i++)
        {
            //ԭʼ���ݷ����ı�
            if (list[i].Item2 + index != verify_list[i].Item2)
            {
                //��֤����
                Game_Omphalos.i.Delete(list[i].Item1 + " ��ʾ���� " + list[i].Item2 + " ��ֵ֤ " + index + " " + verify_list[i].Item2);
            }
        }
        //д������
        foreach (var item in dec.Keys)
        {
            crate_state = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (item == list[i].Item1)
                {
                    crate_state = false;
                    if (exist)
                    {
                    }
                    else
                    {
                        if (dec[item] > SumSave.base_setting[3])
                        {
                            Game_Omphalos.i.Delete(item + " ���λ�ȡ " + list[i].Item2 + " ��ֵ֤ " + index + " " + verify_list[i].Item2);

                        }
                    }
                    list[i] = (item, dec[item] + list[i].Item2);
                    verify_list[i] = (item, verify_list[i].Item2 + dec[item]);
                }
            }
            if (crate_state)
            {
                list.Add((item, dec[item]));
                verify_list.Add((item, dec[item] + index));
            }
        }
    }
    /// <summary>
    /// ת����ʽ
    /// </summary>
    /// <returns></returns>
    public string GetData()
    {
        string data_list= "";
        for (int i = 0; i < list.Count; i++)
        {
            data_list += (i == 0 ? "" : ",") + (list[i].Item1 + " " + list[i].Item2);
        }
        return data_list;
    }
}
