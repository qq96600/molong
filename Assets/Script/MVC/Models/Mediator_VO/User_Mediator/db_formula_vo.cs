using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_formula_vo : Base_VO
{
    /// <summary>
    /// �ϳ����ͣ�1.�����ϳɣ�2.ѡ�������ϳɣ�
    /// </summary>
    public int formula_type;

    /// <summary>
    /// �ϳɵ���Ʒ
    /// </summary>
    public string formula_result;
    /// <summary>
    /// �ϳɵ���Ʒ�Լ����� (���֣�����������)
    /// </summary>
    public (string,int,int) formula_result_list;
    /// <summary>
    /// �ϳ���Ҫ�Ĳ���
    /// </summary>
    public string formula_need;
    /// <summary>
    /// �ϳ���Ҫ�Ĳ����б�
    /// </summary>
    public List<(string,int)> formula_need_list;

    public db_formula_vo(int formula_type, string formula_result, string formula_need)
    {
        this.formula_type = formula_type;
        this.formula_result = formula_result;
        this.formula_need = formula_need;
        Init();
    }

    internal void Init()
    {
        formula_need_list= new List<(string,int)>();
        string[] formula_need_arr = formula_need.Split('|');
        for (int i = 0; i < formula_need_arr.Length; i++)
        {
            string[] formula_need_arr2 = formula_need_arr[i].Split('*');
            formula_need_list.Add((formula_need_arr2[0], int.Parse(formula_need_arr2[1])));
        }

        string[] formula_result_arr = formula_result.Split('*');
        formula_result_list = (formula_result_arr[0], int.Parse(formula_result_arr[1]),int.Parse(formula_result_arr[2]));
    }
}

