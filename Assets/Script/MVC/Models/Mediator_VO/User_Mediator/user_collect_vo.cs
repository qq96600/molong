using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class user_collect_vo : Base_VO
{
    /// <summary>
    /// �ɾ��Ƿ��ռ��б�
    /// </summary>
    public List<(string,int)> user_collect_list;
    /// <summary>
    /// �ɾ��Ƿ��ռ�dic
    /// </summary>
    public Dictionary<string, int> user_collect_dic= new Dictionary<string, int>();


    /// <summary>
    /// �ϲ��Ƿ��ռ��б�
    /// </summary>
    public string collect_Merge()
    {
        string str = "";
        

        for (int j= 0; j < user_collect_dic.Count; j++)
        {
            if (j > 0)
            {
                str += ",";
            }
            str += user_collect_dic.Keys.ToArray()[j] + " " + user_collect_dic.Values.ToArray()[j];
        }

        return str;
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
        return new string[] {
            "collect_value",
           
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
         {
            GetStr(collect_Merge()),
            
         };
    }
}
