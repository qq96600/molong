using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_needlist_vo : Base_VO
{
    /// <summary>
    /// �̵��޹���Ʒ����Ĵ���
    /// </summary>
    public string store_value;
    /// <summary>
    /// �̵��޹���Ʒ�б�
    /// </summary>
    public List<string[]> store_value_list=new List<string[]>();
    /// <summary>
    /// ��ͼ�������
    /// </summary>
    public string map_value;
    /// <summary>
    /// ��ͼ��������б�
    /// </summary>
    private List<(string, int)> map_value_list=new List<(string, int)>();

    /// <summary>
    /// ��չ
    /// </summary>
    public string user_value;
    /// <summary>
    /// �����̵��޹���Ʒ
    /// </summary>
    public void store_Init()
    {
        string[] store_list = store_value.Split(',');
        for (int i = 0; i < store_list.Length; i++)
        {
            string[] store = store_list[i].Split(' '); 
            if(store.Length == 2)
            store_value_list.Add(store);
        }
     
    }



    /// <summary>
    /// ���������ͼ����
    /// </summary>
    public void map_Init()
    {
        string[] map_list = map_value.Split(',');
        for (int i = 0; i < map_list.Length; i++)
        {
            string[] map = map_list[i].Split(' ');
            if (map.Length == 2)
                map_value_list.Add((map[0], int.Parse(map[1])));
        }
    }
    /// <summary>
    /// �ϲ��̵��޹���Ʒ
    /// </summary>
    private string store_Merge()
    {
        string item="";
        for (int i = 0; i < store_value_list.Count; i++)
        {
            if(i>0)
            {
                item += ",";
            }
            item += store_value_list[i][0] + " " + store_value_list[i][1] ;
        }

        return item;
    }



    /// <summary>
    /// �ϲ���ͼ�������
    /// </summary>
    private string map_Merge()
    {
        string item = "";
        for (int i = 0; i < map_value_list.Count; i++)
        {
            if (i >0)
            {
                item += ",";
            }
            item += map_value_list[i].Item1 + " " + map_value_list[i].Item2;
        }
        return item;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(store_value),
            GetStr(map_value),
            GetStr(user_value),
        };

    }

    public override string[] Get_Update_Character()
    {
        return new string[] {
            "store_value",
            "map_value",
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
         {
            GetStr(store_Merge()),
            GetStr(map_Merge()),    
         };
    }
}
