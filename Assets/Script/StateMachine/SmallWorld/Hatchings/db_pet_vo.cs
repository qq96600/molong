using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_pet_vo : Base_VO
{
    /// <summary>
    /// ��������
    /// </summary>
    public string petName;
    /// <summary>
    /// ���ﵰ����
    /// </summary>
    public string petEggsName;
    /// <summary>
    /// ��Ҫ������ʱ��
    /// </summary>
    public int hatchingTime;
    /// <summary>
    /// ��ʼ������ʱ��
    /// </summary>
    private DateTime startHatchingTime;
    /// <summary>
    ///���ﵰ���ֺͿ�ʼ������ʱ��
    /// </summary>
    private (string name, DateTime time) crt_hatching;
    /// <summary>
    /// ������
    /// </summary>
    public int hero_type ;
    /// <summary>
    /// �����������
    /// </summary>
    public string crate_value;
    /// <summary>
    /// ���������������
    /// </summary>
    public List<string> crate_values=new List<string>();

    /// <summary>
    /// ������������
    /// </summary>
    public string up_value;
    /// <summary>
    /// ����������������
    /// </summary>
    public List<string> up_values=new List<string>();

    /// <summary>
    /// �����������
    /// </summary>
    public string up_base_value;
    /// <summary>
    /// �����츳
    /// </summary>
    public string hero_talent;
    /// <summary>
    /// ����ȼ�
    /// </summary>
    public int level=1;
    /// <summary>
    /// ���ﾭ��
    /// </summary>
    public int exp=0;


    



    /// <summary>
    /// ��������
    /// </summary>
    public void Init()
    {

        string[] str = user_value.Split('|');

        if (str.Length == 2)
        {
            crt_hatching = (str[0], (str[1] == "0") ? DateTime.Now : Convert.ToDateTime(str[1]));
        }
        else
        if (str == null)//û�����ݾͳ�ʼ��
        {
            crt_hatching = ("0", DateTime.Now);
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet_hatching,
            SumSave.crt_hatching.Set_Uptade_String(), SumSave.crt_hatching.Get_Update_Character());
        }

    }
    /// <summary>
    /// ��ó�����ֵ����
    /// </summary>
    public void GetNumerical()
    {
        string[] str1 = crate_value.Split(' ');
        for (int i = 0; i < str1.Length; i++)
        {
            crate_values.Add(str1[i]);
        }

        string[] str2 = up_value.Split(' ');
        for (int i = 0; i < str2.Length; i++)
        {
            up_values.Add(str2[i]);
        }
    }




    /// <summary>
    /// �������ݸ�ʽ
    /// </summary>
    /// <returns></returns>
    public string Set_data()
    {
        string dec = "";
        dec= crt_hatching.Item1+ "|"+ crt_hatching.Item2.ToString();
        return dec;

    }

    public void Set_data((string name, DateTime time) crt_plants)
    {
        crt_hatching = crt_plants;
    }


    /// <summary>
    /// ��ȡ��ǰ�����ĳ���
    /// </summary>
    /// <returns></returns>
    public (string, DateTime) Set()
    {
        return crt_hatching;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
           GetStr(0),
           GetStr(SumSave.crt_user.uid),
           GetStr(user_value), 
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "user_value",
        };
    }


    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
            GetStr(Set_data()),
        };
    }

}
