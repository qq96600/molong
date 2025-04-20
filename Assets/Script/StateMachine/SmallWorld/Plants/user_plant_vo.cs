using Common;
using MVC;
using System;
using System.Collections.Generic;
using UnityEngine;


public class user_plant_vo : Base_VO
{


    //public PanltEnum plantType; // ֲ������
    //public int seedNumber;//��������

    /// <summary>
    /// ֲ������
    /// </summary>
    public string plantName;
    /// <summary>
    /// ������Ҫ�����ʱ��
    /// </summary>
    public int plantTime;
    /// <summary>
    /// �����ջ����
    /// </summary>
    public string HarvestMaterials;
    /// <summary>
    /// �����ջ�����
    /// </summary>
    public int harvestnumber;
    /// <summary>
    /// ��������
    /// </summary>
    public int lossnumber;



    /// <summary>
    /// ��ֲ�ȼ�
    /// </summary>
    public int plantLeve;
    private List<(string,DateTime)> user_plants;//��ֲ��ֲ�����ƺͳ���ʱ��
    /// <summary>
    /// ����ֲ����Ϣ
    /// </summary> 
    public void Init()
    {
        user_plants = new List<(string, DateTime)>();
        string[] str = user_value.Split('&');

        for (int i = 0; i < str.Length; i++)
        {
            if (str.Length > 0)
            {
                string[] str1 = str[i].Split('|');
                if (str1.Length == 2)
                    user_plants.Add((str1[0], (str1[0] == "0") ? DateTime.Now : Convert.ToDateTime(str[1])));
            }
        }
    }
   public void Up_user_plants(List<(string, DateTime)> _user)
    {
        user_plants = _user;
    }

    public void Set_data(List<(string, DateTime)> crt_plants)
    {
        user_plants= crt_plants;
    }

    public string Set_data()
    {
        string dec = "";
        foreach (var item in user_plants)
        {
            if (dec != "") dec += "&";
            dec += item.Item1 + "|" + item.Item2.ToString();
        }
        return dec;

    }


    /// <summary>
    /// ��ȡ��ǰ��ֲ��ֲ��
    /// </summary>
    /// <returns></returns>
    public List<(string, DateTime)> Set()
    {
        return user_plants;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
           GetStr(0),
           GetStr(SumSave.crt_user.uid),
           GetStr(plantLeve),
           GetStr(user_value),
        };
     }
    
    public override string[] Get_Update_Character()
    {
        return new string[]
        {
           "plantLeve",
           "user_value"
           
        };
    }


    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
           GetStr(plantLeve),
           GetStr(Set_data()),

        };
    }



}
