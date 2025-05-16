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
    /// <summary>
    /// ѡ��������Ʒ
    /// </summary>
    private List<(string, int, int, int, int)> CurrentItems;

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

        ClearObject(fale_items);
        current_designated = btn_item;
        CurrentItems = SumSave.db_fate_list[current_designated.index - 1].fate_value_list;
        for (int j = 1; j <= 3; j++)
        {
            for (int i = 0; i < SumSave.db_fate_list[current_designated .index- 1].fate_value_list.Count; i++)//ʵ������һ����Ʒ�б�
            {
                if (CurrentItems[i].Item2 == j)//����Ʒ��������
                {
                    fate_item fate_item = Instantiate(fate_item_prefab, fale_items);
                    int num = 0;
                    if (isDic(i))
                    {
              
                        num = CurrentItems[i].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[i].Item1, CurrentItems[i].Item3)];//���ʣ������
                    }
                    else
                    {
                        num = CurrentItems[i].Item4;
                    }


                    fate_item.Init(CurrentItems[i], num);
                }
            }
        }

    }
    /// <summary>
    /// �ж��ֵ����Ƿ��и��ں͸���Ʒ
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private  bool isDic(int i)
    {
        return (SumSave.crt_needlist.fate_value_dic.ContainsKey(current_designated.index) && SumSave.crt_needlist.fate_value_dic[current_designated.index].ContainsKey((CurrentItems[i].Item1, CurrentItems[i].Item3)));
    }

    /// <summary>
    /// ʮ����
    /// </summary>
    private void Ten_consecutive_draws()
    {
        //NeedConsumables("���˱�", 10);
        //if (!RefreshConsumables())
        //{
        //    Alert_Dec.Show("���˱Ҳ���");
        //    return;
        //}
        int weight = 0;
       // List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index-1].fate_value_list;
        for (int i = 0; i < CurrentItems.Count; i++)//��ȡ��Ȩ��
        {
            weight += CurrentItems[i].Item5;
        }
        List<(string, int )> dic = new List<(string, int)>();//�洢����
        if (isExhaust(10))
        {
            for (int i=0;i<10;i++)
            {
                while (true)
                {
                    int rand = Random.Range(0, CurrentItems.Count);//�����ȡһ����Ʒ

                    if (Random.Range(0, weight) < CurrentItems[rand].Item5 && isCount(rand))//�ж��Ƿ����
                    {
                        GetRewards(rand);
                        dic.Add((CurrentItems[rand].Item1, CurrentItems[rand].Item3));
                        break;
                    }


                }
            }
            Alert_Icon.Show(dic);
            On_btn_item_click(current_designated);
        }
     


    }
    /// <summary>
    /// �жϵ�ǰ��Ʒ�Ƿ����
    /// </summary>
    /// <param name="rand">��һ����Ʒ</param>
    /// <returns></returns>
    private bool isCount(int rand)
    {
        bool isCount = true;
        if (isDic(rand))
        {
            //Debug.Log(CurrentItems[rand].Item1+ "��ǰ��ȡ������"+ SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[rand].Item1, CurrentItems[rand].Item3)]+"����Ʒ�ɳ�ȡ��������"+ CurrentItems[rand].Item4);
            if (CurrentItems[rand].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[rand].Item1, CurrentItems[rand].Item3)] <= 0)
            {
                isCount = false;
            }

        }
        else
        {
            isCount = true;
        }

        return isCount;
    }


    /// <summary>
    /// ����
    /// </summary>
    private void Single_draw()
    {

        //NeedConsumables("���˱�", 1);
        //if (!RefreshConsumables())
        //{
        //    Alert_Dec.Show("���˱Ҳ���");
        //    return;
        //}

        int weight = 0;

        for (int i=0;i < CurrentItems.Count;i++)//��ȡ��Ȩ��
        {
            weight+= CurrentItems[i].Item5;
        }
       if( isExhaust())
        {
            while (true)//�����֣����ͣ���Ʒ����������齱�������ޣ�Ȩ�أ�
            {
                int rand = Random.Range(0, CurrentItems.Count);//�����ȡһ����Ʒ
                int count = CurrentItems[rand].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[rand].Item1, CurrentItems[rand].Item3)];//���˱����ʣ���ȡ����

                if (Random.Range(0, weight) < CurrentItems[rand].Item5 &&isCount(rand))//�ж��Ƿ����
                {

                    GetRewards(rand);
                    List<(string, int)> dic = new List<(string, int)>();//�洢����
                    dic.Add((CurrentItems[rand].Item1, CurrentItems[rand].Item3));
                    Alert_Icon.Show(dic);
                    break;
                }
            }
            On_btn_item_click(current_designated);
        }
   
    }
    /// <summary>
    /// �жϵ����Ƿ��ж��ٳ齱����
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool isExhaust(int num=1)
    {
        int SumCount = 0;
        for (int i = 0; i < CurrentItems.Count; i++)//�жϸ������Ƿ����
        {
            if (isDic(i))
            {
                SumCount += CurrentItems[i].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[i].Item1, CurrentItems[i].Item3)];
            }
            else
            {
                SumCount += CurrentItems[i].Item4;
            }
        }

        if (num > SumCount)
        {
            Alert_Dec.Show("��������������");
            return false;
        }

            return true;
    }

    /// <summary>
    /// ��ý���
    /// </summary>
    /// <param name="data">�����֣����ͣ���Ʒ����������齱�������ޣ�Ȩ�أ������б�</param>
    /// <param name="rand">���影������</param>
    private void GetRewards(int rand)
    {

        if (SumSave.crt_needlist.fate_value_dic.ContainsKey(current_designated.index))//�ж��Ƿ��Ѿ����ڸ�����
        {
            if(SumSave.crt_needlist.fate_value_dic[current_designated.index].ContainsKey((CurrentItems[rand].Item1, CurrentItems[rand].Item3)))
            {
                SumSave.crt_needlist.fate_value_dic[current_designated.index][(CurrentItems[rand].Item1, CurrentItems[rand].Item3)]++;//������������Ʒ����Ʒ�����ȡ�������ҵ���Ӧ���ֵ䣬����������
            }else
            {
                SumSave.crt_needlist.fate_value_dic[current_designated.index].Add((CurrentItems[rand].Item1, CurrentItems[rand].Item3), 1);//�������򴴽�
            }

            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
        }
        else
        {
            Dictionary<(string, int), int> dic = new Dictionary<(string, int), int>();
            dic.Add((CurrentItems[rand].Item1, CurrentItems[rand].Item3), 1);
            SumSave.crt_needlist.fate_value_dic.Add(current_designated.index, dic);//�������򴴽�
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
        }

        switch (CurrentItems[rand].Item2)
        {
            case 1:
                //��ò��ϼ���������
                Battle_Tool.Obtain_Resources(CurrentItems[rand].Item1, CurrentItems[rand].Item3);
                break;
            case 2:
                //��û���
                Battle_Tool.Obtain_Unit((currency_unit)Enum.Parse(typeof(currency_unit), CurrentItems[rand].Item1), CurrentItems[rand].Item3);
                break;
            case 3:
                //���Ƥ��
                SumSave.crt_hero.hero_value += (SumSave.crt_hero.hero_value == "" ? "" : ",") + CurrentItems[rand].Item1;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero, new string[] { Battle_Tool.GetStr(SumSave.crt_hero.hero_value) },
                    new string[] { "hero_value" });
                break;
        }


       
    }
}
