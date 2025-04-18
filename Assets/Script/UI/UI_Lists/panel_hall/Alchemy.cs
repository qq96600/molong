using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����
/// </summary>
public class Alchemy : Base_Mono
{
    /// <summary>
    /// Ȩ���б�
    /// </summary>
    private List<WeightedItem> eighteditems = new List<WeightedItem>();
    /// <summary>
    /// λ���б�
    /// </summary>
    private Dictionary<string, Transform> pos_list = new Dictionary<string, Transform>();
    /// <summary>
    /// ������ʾ��׼
    /// </summary>
    private Slider slider;
    /// <summary>
    /// ��ʾ��׼ֵ
    /// </summary>
    private Text slider_info;
    /// <summary>
    /// ѡ���䷽
    /// </summary>
    private int select_formula = 0;
    /// <summary>
    /// λ��
    /// </summary>
    private Transform pos_materials;
    /// <summary>
    /// ȷ�ϰ�ť
    /// </summary>
    private Button confirm;
    /// <summary>
    /// ��ȡ�б�
    /// </summary>
    private Dropdown lists;

    private List<string> Dropdown_lists = new List<string>();

    private material_item material_item_Prefabs;
    /// <summary>
    /// ѡ���䷽����
    /// </summary>
    private Dictionary<string,(string,int)> Select_Materials = new Dictionary<string, (string, int)>();

    private effect_gather effect_gather;
    /// <summary>
    /// ��ʾ�ɹ���
    /// </summary>
    private Button show_Success_rate;
    private void Awake()
    {
        pos_materials=Find<Transform>("Scroll View/Viewport/Content");
        material_item_Prefabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        lists = Find<Dropdown>("list");
        confirm = Find<Button>("confirm");
        confirm.onClick.AddListener(() => { btn_Confirm(); });
        effect_gather=Find<effect_gather>("effect_gather");
        effect_gather.gameObject.SetActive(false);
        slider=Find<Slider>("Slider");
        slider_info = Find<Text>("Slider/info");
        slider.onValueChanged.AddListener((value) => { Open_Slider(value); });
        slider.gameObject.SetActive(false);
        show_Success_rate = Find<Button>("offect/show_Success_rate");
        show_Success_rate.onClick.AddListener(() => { Show_Success_rate(); });
        show_Success_rate.gameObject.SetActive(false);
    }
    /// <summary>
    /// ��ʾ�ɹ���
    /// </summary>
    private void Show_Success_rate()
    {
        WeightedRandomPicker picker = new WeightedRandomPicker(eighteditems);
        string dec = picker.Show_Success_rate();
        Alert.Show("��������", dec);
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="value"></param>
    private void Open_Slider(float value)
    {
        slider_info.text = "��ע�����鵤 " + slider.value + "��Max:" + slider.maxValue + ")";
    }

    /// <summary>
    /// �ϳ�
    /// </summary>
    private void btn_Confirm()
    {
        foreach (var item in Select_Materials.Keys)
        {
            if (Select_Materials[item].Item2 == 0)
            {
                Alert_Dec.Show("��ѡ�����в���");
                return;
            }
        }
        foreach (var item in Select_Materials.Keys)
        {
            NeedConsumables(Select_Materials[item].Item1, Select_Materials[item].Item2);
            Debug.Log(Select_Materials[item].Item1 + " " + Select_Materials[item].Item2);
        }
        if (RefreshConsumables())
        {
            effect_gather.gameObject.SetActive(true);
            effect_gather.OpenEffect_Gather((int)(slider.value * 10 / (slider.maxValue + 1)) + 1);
        }
        else
            Alert_Dec.Show("���ϲ���");
    }

    protected void Confirm_Number(int number)
    {
        synthesis();
        base_Show();
    }
    /// <summary>
    /// �ϳ�
    /// </summary>
    private void synthesis()
    {
        // ����һ����Ȩ���ѡ����
        WeightedRandomPicker picker = new WeightedRandomPicker(eighteditems);
        // ��ȡһ�������Ʒ
        WeightedItem selectedItem = picker.GetRandomItem();
        List<string> split = new List<string>();
        db_seed_vo item = ArrayHelper.Find(SumSave.db_seeds, e => e.pill == selectedItem.prizedraw);
        split = crate_seed(item, split);
        SumSave.crt_seeds.Set(split);
        Alert_Dec.Show("��õ�ҩ " + split[0]);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_seed, SumSave.crt_seeds.Set_Uptade_String(), SumSave.crt_seeds.Get_Update_Character());
    }
    /// <summary>
    /// �����䷽
    /// </summary>
    /// <param name="item"></param>
    /// <param name="split"></param>
    private List<string> crate_seed(db_seed_vo item, List<string> split)
    {
        split.Add(item.pill);
        split.Add(SumSave.nowtime.ToString());
        string[] split1 = item.pill_effect.Split(' ');
        int random = UnityEngine.Random.Range(int.Parse(split1[0]), int.Parse(split1[1]));
        split.Add(random.ToString());

        return split;
    }

    public override void Show()
    {
        base.Show();
        base_Show();
    }
    /// <summary>
    /// ��ȡλ��
    /// </summary>
    /// <param name="pos"></param>
    protected void Init_Pos(Alchemy_button_pos_item pos)
    {
        pos_list.Add(pos.index.ToString(), pos.transform);
        Select_Materials.Add(pos.index.ToString(), (string.Empty, 0));
    }
    private void base_Show()
    {
        for (int i = pos_materials.childCount - 1; i >= 0; i--)//��������ڰ�ť
        {
            Destroy(pos_materials.GetChild(i).gameObject);
        }
        //����б�
        foreach (var item in pos_list.Keys)
        {
            for (int i = 0; i < pos_list[item].childCount; i++)
            {
                Destroy(pos_list[item].GetChild(i).gameObject);
            }
        }
        
        Dropdown_lists.Clear();
        lists.options.Clear();
        eighteditems.Clear();
        foreach (var item in pos_list.Keys)
        {
            Select_Materials[item]= (item, 0);
        }
        Dropdown_lists.Add("��ѡ���䷽");
        List<(string, int)> list = new List<(string, int)>();
        list=SumSave.crt_bag_resources.Set();
        for (int i = 0; i < list.Count; i++)
        { 
           Bag_Base_VO item=ArrayHelper.Find(SumSave.db_stditems,e=> e.Name== list[i].Item1);
            if (item != null)
            {
                if (item.StdMode == EquipConfigTypeList.��������.ToString())
                { 
                    material_item item1 = Instantiate(material_item_Prefabs, pos_materials);
                    item1.Init(list[i]);
                    item1.GetComponent<Button>().onClick.AddListener(() => { btn_Select_Material(item1); });
                }
            }
        }
        List<(string, List<string>)> formulalist = SumSave.crt_seeds.Getformulalist();
        if (formulalist.Count > 0)
        {
            for (int i = 0; i < formulalist.Count; i++)
            {
                Dropdown_lists.Add(formulalist[i].Item2[0] + "��" + formulalist[i].Item1);
            }
        }
        if (Dropdown_lists.Count > 0)
        {
            for (int i = 0; i < Dropdown_lists.Count; i++)
            { 
                lists.options.Add(new Dropdown.OptionData(Dropdown_lists[i]));
            }
        }
        lists.onValueChanged.AddListener(OnValueChange);
    }
    /// <summary>
    /// ѡ���䷽
    /// </summary>
    /// <param name="arg0"></param>
    private void OnValueChange(int arg0)
    {
        Alert_Dec.Show("��ǰѡ���䷽" + Dropdown_lists[arg0]);
        select_formula = arg0;
        Select_Formula();
    }
    /// <summary>
    /// �����䷽
    /// </summary>
    private void Select_Formula()
    {
        foreach (string pos in pos_list.Keys)
        {
            for (int i = 0; i < pos_list[pos].childCount; i++)
            {
                Destroy(pos_list[pos].GetChild(i).gameObject);
            }
            Select_Materials[pos] = (pos, 0);
        }
        if (select_formula == 0)
        {
            return;
        }
        (string,List<string>) item = SumSave.crt_seeds.Getformulalist()[select_formula-1];
        string[] split = item.Item2[1].Split(' '); 
        List<string> list = new List<string>();
        for (int i = 0; i < split.Length; i++)
        {
            db_seed_vo seed = ArrayHelper.Find(SumSave.db_seeds, e => e.sequence == int.Parse(split[i]));
            if (seed != null)
            {
                Select_Materials[i.ToString()] = (seed.seed_name, 1);
                material_item material_Item = Instantiate(material_item_Prefabs, pos_list[i.ToString()]);
                material_Item.Init(Select_Materials[i.ToString()]);
            }
        }
        Show_Slider();
    }
    /// <summary>
    /// Ȩ�ر�
    /// </summary>
    /// <param name="seed_name"></param>
    /// <param name="weight"></param>
    private void Obtain_Weight(string seed_name, int weight)
    {
        WeightedItem weightedItem = new WeightedItem();
        weightedItem.prizedraw = seed_name;
        weightedItem.Weight = weight;
        eighteditems.Add(weightedItem);
    }

    /// <summary>
    /// ѡ�����
    /// </summary>
    /// <param name="item1"></param>
    private void btn_Select_Material(material_item item1)
    {
        if (select_formula == 0)
        {
            foreach (var item in Select_Materials.Keys)
            {
                if (Select_Materials[item].Item2 == 0)
                {
                    if (item1.GetItemData().Item2 > 1)
                    {
                        Select_Materials[item] = (item1.GetItemData().Item1, 1);
                        if (pos_list.ContainsKey(item))
                        {
                            material_item material_Item = Instantiate(material_item_Prefabs, pos_list[item]);
                            material_Item.Init(Select_Materials[item]);
                            material_Item.GetComponent<Button>().onClick.AddListener(() => { close_Select_Material(item); });
                        }
                        item1.Init((item1.GetItemData().Item1, item1.GetItemData().Item2 - 1));
                        Show_Slider();
                        return;
                    }
                    else Alert_Dec.Show("��ǰ���ϲ���");
                }
            }
            Alert_Dec.Show("��ǰ����λ����");
        }
        else Alert_Dec.Show("��ǰ�������䷽");

    }
    /// <summary>
    /// �رղ���
    /// </summary>
    /// <param name="material_Item"></param>
    private void close_Select_Material(string pos)
    {
        for (int i = 0; i < pos_list[pos].childCount; i++)
        { 
            Destroy(pos_list[pos].GetChild(i).gameObject);
        }
        for (int i = pos_materials.childCount - 1; i >= 0; i--)//��������ڰ�ť
        {
            material_item item = pos_materials.GetChild(i).GetComponent<material_item>();
            if (item != null)
            { 
                if(item.GetItemData().Item1 == Select_Materials[pos].Item1)
                {
                    item.Init((item.GetItemData().Item1, item.GetItemData().Item2 + 1));
                    break;
                }
            }
        }
        Select_Materials[pos] = (pos, 0);
        slider.gameObject.SetActive(false);
        show_Success_rate.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    private void Show_Slider()
    {
        bool exist = true;
        int number = 0;
        foreach (var item in Select_Materials.Keys)
        {
            if (Select_Materials[item].Item2 == 0)
            {
                exist = false;
            }
        }
        if (exist)
        {
            eighteditems.Clear();
            foreach (var item in Select_Materials.Keys)
            {
                db_seed_vo seed = ArrayHelper.Find(SumSave.db_seeds, e => e.seed_name == Select_Materials[item].Item1);
                number += seed.seed_number;
                Obtain_Weight(seed.pill, seed.Weight);
            }
            show_Success_rate.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
            slider.maxValue = number;
            slider.value = 0;
            slider_info.text="��ע�����鵤 "+slider.value+ "��Max:"+slider.maxValue+")";
        }
    }
}
