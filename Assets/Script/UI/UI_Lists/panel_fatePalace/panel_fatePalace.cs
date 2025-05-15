using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class panel_fatePalace : Panel_Base
{
    /// <summary>
    /// ���飬ʮ���鰴ť
    /// </summary>
    private Button single_draw, ten_consecutive_draws;
    /// <summary>
    /// ���������б� ��������Ʒ�б�λ��
    /// </summary>
    private Transform designatedTime_items, fale_items;

    /// <summary>
    /// ��ʾ����Ԥ�Ƽ�
    /// </summary>
    private fate_item fate_item_prefab;
    /// <summary>
    /// ��������Ԥ�Ƽ�
    /// </summary>
    private btn_item btn_item_prefab;
    /// <summary>
    /// ��ǰ���������
    /// </summary>
    private btn_item current_designated;

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();

        single_draw = Find<Button>("Single_draw");
        single_draw.onClick.AddListener(()=> { Single_draw(); });
        ten_consecutive_draws=Find<Button>("Ten_consecutive_draws");
        ten_consecutive_draws.onClick.AddListener(()=> { Ten_consecutive_draws(); });

        designatedTime_items= Find<Transform>("designatedTime_items/Viewport/Content");
        fale_items = Find<Transform>("fale_items/Viewport/Content");

        fate_item_prefab=Battle_Tool.Find_Prefabs<fate_item>("fate_item");
        btn_item_prefab=Battle_Tool.Find_Prefabs<btn_item>("btn_item");

        ClearObject(designatedTime_items);

        for(int i = 0; i < SumSave.db_fate_list.Count; i++)//ʵ���������б�
        {
            btn_item btn_item = Instantiate(btn_item_prefab, designatedTime_items);
            string text="��"+SumSave.db_fate_list[i].fate_id+"��";
            btn_item.Show(SumSave.db_fate_list[i].fate_id, text);
            btn_item.GetComponent<Button>().onClick.AddListener(()=> { On_btn_item_click(btn_item); });
            if(btn_item.index==1)
            {
                On_btn_item_click(btn_item);
            }
        }
    }




        /// <summary>
        /// �л�����Ӧ��������Ʒ
        /// </summary>
    private void On_btn_item_click(btn_item btn_item)
    {
        int fate_id = btn_item.index - 1;
        ClearObject(fale_items);
        current_designated = btn_item;
        for (int j = 1; j <= 3; j++)
        {
            for (int i = 0; i < SumSave.db_fate_list[fate_id].fate_value_list.Count; i++)//ʵ������һ����Ʒ�б�
            {
                if (SumSave.db_fate_list[fate_id].fate_value_list[i].Item2 == j)
                {
                    fate_item fate_item = Instantiate(fate_item_prefab, fale_items);
                    fate_item.Init(SumSave.db_fate_list[fate_id].fate_value_list[i]);
                }
            }
        }

    }

    /// <summary>
    /// ʮ����
    /// </summary>
    private void Ten_consecutive_draws()
    {
        int weight = 0;
        List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index-1].fate_value_list;
        for (int i = 0; i < data.Count; i++)//��ȡ��Ȩ��
        {
            weight += data[i].Item5;
        }
        List<(string, int )> dic = new List<(string, int)>();//�洢����
        for (int i=0;i<10;i++)
        {
            while (true)
            {
                int rand = Random.Range(0, data.Count);//�����ȡһ����Ʒ
                if (Random.Range(0, weight) < data[rand].Item5)//�ж��Ƿ����
                {
                    GetRewards(data, rand);
                    dic.Add((data[rand].Item1, data[rand].Item3));
                    break;
                }
            }
        }
        Alert_Icon.Show(dic);

    }

    /// <summary>
    /// ����
    /// </summary>
    private void Single_draw()
    {
        int weight= 0;
        List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index - 1].fate_value_list;
       for (int i=0;i < data.Count;i++)//��ȡ��Ȩ��
        {
            weight+= data[i].Item5;
        }
        while (true)
        {
            int rand = Random.Range(0, data.Count);//�����ȡһ����Ʒ
            if (Random.Range(0, weight) < data[rand].Item5)//�ж��Ƿ����
            {
                GetRewards(data, rand);
                List<(string, int)> dic = new List<(string, int)>();//�洢����
                dic.Add((data[rand].Item1, data[rand].Item3));
                Alert_Icon.Show(dic);
                break;
            }
        }
        
    }
    /// <summary>
    /// ��ý���
    /// </summary>
    /// <param name="data">�����б�</param>
    /// <param name="rand">���影������</param>
    private static void GetRewards(List<(string, int, int, int, int)> data, int rand)
    {
        //if(SumSave.crt_needlist.fate_value_dic.ContainsKey(current_designated.index))
        //{

        //}

        switch (data[rand].Item2)
        {
            case 1:
                //��ò��ϼ���������
                Battle_Tool.Obtain_Resources(data[rand].Item1, data[rand].Item3);
                break;
            case 2:
                //��û���
                Battle_Tool.Obtain_Unit((currency_unit)Enum.Parse(typeof(currency_unit), data[rand].Item1), data[rand].Item3);
                break;
            case 3:
                //���Ƥ��
                SumSave.crt_hero.hero_value += (SumSave.crt_hero.hero_value == "" ? "" : ",") + data[rand].Item1;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero, new string[] { Battle_Tool.GetStr(SumSave.crt_hero.hero_value) },
                    new string[] { "hero_value" });
                break;
        }


       
    }
}
