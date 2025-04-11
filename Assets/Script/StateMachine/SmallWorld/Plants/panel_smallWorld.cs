using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_smallWorld : Panel_Base
{
    /// <summary>
    /// ��ֲ����
    /// </summary>
    private panel_plant _plant;
    /// <summary>
    /// ��������
    /// </summary>
    private Pet_Hatching _Hatching;
    /// <summary>
    /// ����̽��λ��
    /// </summary>
    private Pet_explore _explore;

    /// <summary>
    /// ��ťλ��
    /// </summary>
    private Transform pos_btn;
    /// <summary>
    /// ���ܰ�ť
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// ����
    /// </summary>
    private Image small_World_bg;
    /// <summary>
    /// ��ʾֵ
    /// </summary>
    private Text base_info;
    private string[] btn_list = new string[] { "����", "ũׯ", "���", "̽��" };
    public override void Hide()
    {
        if (small_World_bg.gameObject.activeInHierarchy)//�����ϲ�ر�
        {
            if (_plant.gameObject.activeInHierarchy) _plant.Hide();
            if(_Hatching.gameObject.activeInHierarchy) _Hatching.Hide();
            if (_explore.gameObject.activeInHierarchy) _explore.Hide();

            small_World_bg.gameObject.SetActive(false);
        }else
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        small_World_bg = Find<Image>("small_World");
        _plant=Find<panel_plant>("small_World/Panel_plant");
        _Hatching = Find<Pet_Hatching>("small_World/Pet_Hatching");
        _explore = Find<Pet_explore>("small_World/Pet_explore");
        pos_btn=Find<Transform>("bg_main/btn_list");
        base_info = Find<Text>("bg_main/base_info/info");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, pos_btn);//ʵ��������װ��
            btn_item.Show(i, btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        small_World_bg.gameObject.SetActive(true);
        switch (btn_list[btn_item.index])
        { 
            case "����":
                small_World_bg.gameObject.SetActive(false);
                uplv();
                break;
            case "ũׯ":
                _plant.Show();
                break;
            case "���":
                _Hatching.Show();
                break;
            case "̽��":
                _explore.Show();
                break;
        
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    private void uplv()
    {
        if (SumSave.crt_world.World_Lv >= SumSave.db_lvs.world_lv_list.Count)
        { 
            Alert_Dec.Show("�Ѵ���ߵȼ�");
            return;
        }
        (string,int) dec = SumSave.db_lvs.world_lv_list[SumSave.crt_world.World_Lv];
        NeedConsumables(dec.Item1, dec.Item2);
        if (RefreshConsumables())
        { 
            SumSave.crt_world.World_Lv++;
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
        }
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_world == null)
        {
            Alert_Dec.Show("С����δ����");
            Hide();
        }
        Base_Show();
    }

    private void Base_Show()
    {
        List<string> list = new List<string>();
        list = SumSave.crt_world.Get();
        int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
        string dec = "���飺Lv." + SumSave.crt_world.World_Lv + "\n";
        dec += "���� ��" + Obtain_Init(1,time,int.Parse(list[1])) + "(Max" + Obtain_Init(2) + ")\n";
        dec += "ÿ���ӿɻ�� ��" + SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv]+  "����\n";
        dec += "������� :" + (SumSave.crt_world.World_Lv * 10) + "%";
        base_info.text = dec;
    }
    /// <summary>
    /// ��ȡ����ֵ
    /// </summary>
    /// <param name="time"></param>
    private int Obtain_Init(int type,int time=0,int crt_value=0)
    {
       
        int value = 0;
        switch (type)
        {
            case 1:
                ///�ж�Խ��
                ArrayHelper.SafeGet(SumSave.db_lvs.world_offect_list, SumSave.crt_world.World_Lv, out value);
                value = time * SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv];
                value+= crt_value;
                value = Mathf.Min(value, SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv]);
                break;
            case 2:
                value = SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv];
                break;
            default:
                break;
        }

        return value;
    }
    
    protected override void Awake()
    {
        base.Awake();
    }


    

}
