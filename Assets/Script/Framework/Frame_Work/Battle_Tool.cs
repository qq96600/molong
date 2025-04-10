using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Common;
/// <summary>
/// ս��������
/// </summary>
public static class Battle_Tool 
{
    /// <summary>
    /// ��Դ�洢��
    /// </summary>
    //private static List<(string, int)> resources_list = new List<(string, int)>();
    /// <summary>
    /// ��ȡ��Դ
    /// </summary>
    /// <param name="resources_name">����</param>
    /// <param name="number">����</param>
    public static void Obtain_Resources( object resources_name,int number)
    { 
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add(resources_name.ToString(), number);
        SumSave.crt_bag_resources.Get(dic);
        //д�����ݿ�
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="lv">1С��2��Ӣ3boss</param>
    public static void crate_monster(crtMaxHeroVO crt, int lv = 1)
    {


    }
    /// <summary>
    /// ��֤��ͼ�б�
    /// </summary>
    public static void tool_map()
    {
        for (int i = 0; i < SumSave.db_maps.Count; i++)
        {
            string value= SumSave.db_maps[i].ProfitList;
            string[] values = value.Split('&');
            if (values.Length > 1)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    string[] values1 = values[j].Split(' ');
                    if (values1.Length == 3)
                    {
                        if (values1[0] != values1[2])
                            Debug.Log("������ " + SumSave.db_maps[i].map_name + " " + values[j]);
                        else
                        {
                            Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == values1[0]);
                            if (bag == null) Debug.Log("���Ӵ��� �����ݿ��������" + SumSave.db_maps[i].map_name + " " + values[j]);
                        }
                    }
                    else Debug.Log(SumSave.db_maps[i].map_name + " " + values[j]);
                }
            }
        }
    }


}
