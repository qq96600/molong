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
    /// ��ťԤ����
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// ����
    /// </summary>
    private Image small_World_bg;
    /// <summary>
    /// ��ʾֵ
    /// </summary>
    private Text base_info;
    private string[] btn_list = new string[] { "����", "ũׯ", "���", "̽��" };
    /// <summary>
    /// ������ʾλ��
    /// </summary>
    private Transform pet_pos;
    /// <summary>
    /// ������Ϣ����
    /// </summary>
    private Text Pet_name;
    /// <summary>
    /// ����������Ϣ�ı�
    /// <summary>  
    private Text Pet_attribute;
    /// <summary>
    /// �رշ������水ť,�򿪷�������
    /// </summary>
    private Button but,displayPet;
    /// <summary>
    /// ��������
    /// </summary>
    private Transform hatching_progres;

    private hatching_progress hatching_progress;
    public override void Initialize()
    {
        base.Initialize();
        small_World_bg = Find<Image>("small_World");
        _plant=Find<panel_plant>("small_World/Panel_plant");
        _Hatching = Find<Pet_Hatching>("small_World/Pet_Hatching");
        _explore = Find<Pet_explore>("small_World/Pet_explore");
        pos_btn=Find<Transform>("bg_main/btn_list");
        base_info = Find<Text>("bg_main/base_info/info");
        btn_item = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        pet_pos= Find<Transform>("small_World/Pet_Hatching/Pet_list/Viewport/items");
        Pet_name= Find<Text>("small_World/Pet_Hatching/Pet_info/Pet_name/Text");
        Pet_attribute = Find<Text>("small_World/Pet_Hatching/Pet_info/Pet_attribute/Viewport/Content/Text"); 
         but = Find<Button>("small_World/Pet_Hatching/but");
        displayPet= Find<Button>("small_World/Pet_Hatching/displayPet");
        hatching_progres = Find<Transform>("small_World/Pet_Hatching/hatching_progress");
        hatching_progress = Find<hatching_progress>("small_World/hatching_progress");
        but.onClick.AddListener(delegate { CloseHatching(); });
        displayPet.onClick.AddListener(delegate { ShowHatching(); });
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item btn_items = Instantiate(btn_item, pos_btn);//ʵ��������װ��
            btn_items.Show(i, btn_list[i]);
            btn_items.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_items); });
        }
    }

   /// <summary>
   /// �򿪷�������
   /// </summary>
    private void ShowHatching()
    {
        hatching_progres.gameObject.SetActive(true);
        but.gameObject.SetActive(true);
    }
    /// <summary>
    /// �رշ�������
    /// </summary>
    private void CloseHatching()
    {
        hatching_progres.gameObject.SetActive(false);
        but.gameObject.SetActive(false);
    }

    public override void Hide()
    {
        if (small_World_bg.gameObject.activeInHierarchy)//�����ϲ�ر�
        {
            if (_plant.gameObject.activeInHierarchy) _plant.Hide();
            if (_Hatching.gameObject.activeInHierarchy) _Hatching.Hide();
            if (_explore.gameObject.activeInHierarchy) _explore.Hide();

            small_World_bg.gameObject.SetActive(false);
        }
        else
            base.Hide();
    }

    /// <summary>
    /// �򿪽���
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
                //_Hatching.Show();
                //HatchingInit();
                hatching_progress.gameObject.SetActive(true);
                hatching_progress.Show();
                break;
            case "̽��":
                _explore.Show();
                break;
        }
    }
    /// <summary>
    /// ����б��ʼ��
    /// </summary>
    private void HatchingInit()
    {
        ClearObject(pet_pos);
        if(SumSave.crt_pet_list.Count==0)
        {
            btn_item btn_items = Instantiate(btn_item, pet_pos);
            btn_items.Show(1, "�����б�Ϊ��");
        }
        for (int i= 0; i < SumSave.crt_pet_list.Count; i++)
        {
            db_pet_vo pet = SumSave.crt_pet_list[i];
            btn_item btn_items = Instantiate(btn_item, pet_pos);
            btn_items.Show(i, SumSave.crt_pet_list[i].petName+" Lv." + SumSave.crt_pet_list[i].level);
            btn_items.GetComponent<Button>().onClick.AddListener(delegate { Select_Pet(pet); });
        }
    }
    /// <summary>
    /// ��ʾ�����Ϣ
    /// </summary>
    /// <param name="pet"></param>
    private void Select_Pet(db_pet_vo pet)
    {
        Pet_name.text = pet.petName + " Lv." + pet.level;
        Pet_attribute.text="";

        Pet_attribute.text = DisplayPetAttribute(pet);
    }
    /// <summary>
    /// ��ʾ��������
    /// </summary>
    private string  DisplayPetAttribute(db_pet_vo pet)
    {
        string dec = "";
        List<string> value = SumSave.db_pet_dic[pet.petName].crate_values;//��ó����������
       
        for(int i = 0; i < value.Count; i++)
        { 
            enum_attribute_list attribute= (enum_attribute_list)i;
            dec += attribute.ToString() + "��" +(int.Parse( value[i])*pet.level ).ToString()+ "\n";
        }

        return dec;
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
            List<string> list = SumSave.crt_world.Get();
            int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
            SumSave.crt_world.Set(Obtain_Init(1, time, int.Parse(list[1])));
            SumSave.crt_world.World_Lv++;
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
        }
        else Alert_Dec.Show("���ϲ��� ��Ҫ" + dec.Item1 + " * " + dec.Item2);
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_world == null)
        {
            Alert_Dec.Show("С����δ����");
            Hide();
            return;
        }
        Base_Show();
    }

    private void Base_Show()
    {
        List<string> list = SumSave.crt_world.Get();
        int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
        string dec = "���飺Lv." + SumSave.crt_world.World_Lv + "\n";
        dec += "���� ��" + Obtain_Init(1,time,int.Parse(list[1])) + "(Max" + Obtain_Init(2) + ")\n";
        dec += "ÿ���ӿɻ�� ��" + SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv]+  "����\n";
        dec += "������� :" + (SumSave.crt_world.World_Lv * 10) + "%\n";
        if (SumSave.crt_world.World_Lv >= SumSave.db_lvs.world_lv_list.Count)
        {
            dec += "�Ѵ���ߵȼ�";
        }
        else
        {
            (string, int) item = SumSave.db_lvs.world_lv_list[SumSave.crt_world.World_Lv];
            dec += "�������� " + item.Item1 + " * " + item.Item2;
        }
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
                ArrayHelper.SafeGet(SumSave.db_lvs.world_offect_list, SumSave.crt_world.World_Lv, out int se);
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
