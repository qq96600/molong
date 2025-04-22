using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
/// <summary>
/// ��������
/// </summary>
public class hatching_progress : Base_Mono
{
    private Transform pos_list,pos_btn,pos_info,pos_pet_btn;

    /// <summary>
    /// ��ϢԤ����
    /// </summary>
    private info_item info_item_prefabs;

    private Dictionary<enum_attribute_list, info_item> info_item_dic = new Dictionary<enum_attribute_list, info_item>();

    private material_item material_item_Prefabs;
    private store_item store_item_Prefabs;
    private btn_item btn_item_Prefabs;
    private pet_item pet_item_Prefabs;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    private pet_item crt_pet;
    /// <summary>
    /// ��ǰ��
    /// </summary>
    private Bag_Base_VO crt_bag_egg;
    private string[] btn_list = new string[] { "����", "���ﵰ"};
    /// <summary>
    /// ѡ����﹦��
    /// </summary>
    private int crt_pet_index = 0;
    private string[] pet_list_btn = new string[] {"����","�ػ�", "����", "ι��", "̽��"};
    /// <summary>
    /// ѡ����� 0���� 1��
    /// </summary>
    private int index = 0;
    /// <summary>
    /// ��ʾ���ﵰ��Ϣ
    /// </summary>
    private Text pet_egg_info;
    /// <summary>
    /// ���ﵰ
    /// </summary>
    private (string, int) crt_egg;
    /// <summary>
    /// �������������
    /// </summary>
    private Slider hatching_Slider;
    /// <summary>
    /// ������ʱ��
    /// </summary>
    private int hatchingTimeCounter;
    /// <summary>
    /// ��������ʱ�ı�
    /// </summary>
    private Text countdown_text;
    /// <summary>
    /// ���������ʼʱ�� ��������+ʱ��
    /// </summary>
    private string incubate_Time;
    
    private void Awake()
    {
        pos_list = Find<Transform>("Pet_list/Viewport/items");
        pos_btn = Find<Transform>("btn_list");
        pos_info = Find<Transform>("Pet_info/base_info/Scroll View/Viewport/Content");
        pos_pet_btn = Find<Transform>("Pet_info/btn_list");
        material_item_Prefabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        pet_item_Prefabs = Resources.Load<pet_item>("Prefabs/panel_smallWorld/pets/pet_item");
        store_item_Prefabs = Resources.Load<store_item>("Prefabs/panel_hall/panel_store/store_item");
        info_item_prefabs = Resources.Load<info_item>("Prefabs/base_tool/info_item");
        //pet_egg_info= Find<Text>("Pet_info/btn_list/info");
        hatching_Slider= Find<Slider>("Pet_info/hatching_Slider");
        countdown_text=Find<Text>("Pet_info/hatching_Slider/countdown_text/info");
        hatching_Slider.gameObject.SetActive(false);
        //pet_egg_info.text = "";
        ClearObject(pos_btn);
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, pos_btn);
            item.Show(i, btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { onClick(item); });
        }
        ClearObject(pos_info);
        for (int i = 0; i < Enum.GetNames(typeof(enum_attribute_list)).Length; i++)//��ʾ����
        {
            info_item item = Instantiate(info_item_prefabs, pos_info);
            item.Show((enum_attribute_list)i, UnityEngine.Random.Range(1, 1000));
            info_item_dic.Add((enum_attribute_list)i, item);
        }
        ClearObject(pos_pet_btn);
        for (int i = 0; i < pet_list_btn.Length; i++)
        {
            btn_item btn = Instantiate(btn_item_Prefabs, pos_pet_btn);
            btn.Show(i, pet_list_btn[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { onPetClick(btn); });
        }
    }
    /// <summary>
    /// ���﹦��
    /// </summary>
    /// <param name="btn"></param>
    private void onPetClick(btn_item btn)
    {
        switch (pet_list_btn[btn.index])
        {   
            case "����":
                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add(crt_egg.Item1, -crt_egg.Item2);
                string EggsName = SumSave.db_pet_dic[crt_egg.Item1].petEggsName;//���ݳ����ҵ����ﵰ����
                SumSave.crt_bag_resources.Get(dic);
                Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());
                db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == EggsName);
                if (pet != null)
                {
                    incubate_Time = "";
                    incubate_Time = pet.petName + "," + SumSave.nowtime;
                     SumSave.crt_pet.crt_pet_list.Add(incubate_Time);
                    Alert_Dec.Show("����" + pet.petName + " ������ʼ");

                    pos_pet_btn.gameObject.SetActive(false);

                    hatchingTimeCounter = pet.hatchingTime;
                    hatching_Slider.gameObject.SetActive(true);
                    hatching_Slider.maxValue = pet.hatchingTime;
                    StartCoroutine(ShowPlant(pet));

                }
                break;
            case "�ػ�":
                break;
            case "����":
                break;
            case "ι��":
                break;
            case "̽��":
                break;
            default:
                break;
        }
    }

    private IEnumerator ShowPlant(db_pet_vo pet)
    {

        Fixed_Update(1,pet);
        yield return new WaitForSeconds(1f);
        if(hatchingTimeCounter>=0)
        {
            StartCoroutine(ShowPlant(pet));
        }
        else
        {
            StopCoroutine(ShowPlant(pet));
        }
    }
    /// <summary>
    /// ����ʱ
    /// </summary>
    /// <param name="time"></param>
    private void Fixed_Update(int time,db_pet_vo pet)
    {
        if (hatchingTimeCounter > 0)
        {
            hatchingTimeCounter -= time;
            countdown_text.text = ConvertSecondsToHHMMSS(hatchingTimeCounter);
            hatching_Slider.value = pet.hatchingTime - hatchingTimeCounter;
            Debug.Log("����ʱ" + hatchingTimeCounter + "��������" + hatching_Slider.value);
        }
        else if (hatchingTimeCounter <= 0)//�������
        {
            hatching_Slider.gameObject.SetActive(false);
            countdown_text.text = "";
            hatchingTimeCounter = -1;
            hatching_Slider.value = 0;
            Debug.Log("�������");
            string data = incubate_Time;
            SumSave.crt_pet.crt_pet_list.Remove(data);//��������ֻ����һ�����Ϳ���ֱ���ҵ�ɾ��
            db_pet_vo pet_init = SumSave.db_pet_dic[crt_egg.Item1];

            string value_data = " ";
            value_data += pet_init.petName + ",";
            value_data += SumSave.nowtime + ",";
            value_data += (SumSave.crt_world.World_Lv / 5 + 1) + ",";
            value_data += pet_init.level + ",";
            value_data += pet_init.exp + ",";
            value_data += crate_value(pet_init, (SumSave.crt_world.World_Lv / 5 + 1))+",";
            value_data += 0;

            SumSave.crt_pet.crt_pet_list.Add(value_data);

            SumSave.crt_pet_list.Add(pet);
            //Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
            //SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());

        }
    }
    /// <summary>
    /// ��ӳ�������
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="lv"></param>
    /// <returns></returns>
    private string crate_value(db_pet_vo pet, int lv)
    { 
        string data = "";
        for (int i = 0; i < pet.crate_values.Count; i++)
        {
            data += Random.Range(int.Parse(pet.crate_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.crate_values[i]) * (lv * 20 + 100) / 100) + " ";
        }
        data += "|";
        for (int i = 0; i < pet.up_values.Count; i++)
        {
            data += Random.Range(int.Parse(pet.up_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.up_values[i]) * (lv * 20 + 100) / 100) + " ";
        }
        data += "|";
        for (int i = 0; i < pet.up_base_values.Count; i++)
        {
            data+= Random.Range(int.Parse(pet.up_base_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.up_base_values[i]) * (lv * 20 + 100) / 100) + " ";
        }

        return data;
    }
    /// <summary>
    /// ѡ����� 0���� 1��
    /// </summary>
    /// <param name="item"></param>
    private void onClick(btn_item item)
    {
        index = item.index;
        Show();
    }

    public override void Show()
    {
        base.Show();
        Base_Show();
    }

    private void Base_Show()
    {

        ClearObject(pos_list);
        pos_pet_btn.gameObject.SetActive(true);
        if (index == 0)
        {
            for (int i = 0; i < SumSave.crt_pet_list.Count; i++)//��ʾ����
            {
                pet_item item= Instantiate(pet_item_Prefabs, pos_list);
                item.Init(SumSave.crt_pet_list[i]);
                item.GetComponent<Button>().onClick.AddListener(() => { Select_Pet(item); });
                if (crt_pet == null) Select_Pet(item);
                else
                { 
                    if(item==crt_pet) Select_Pet(item);
                }
            }
        }
        if (index == 1)
        {
            List<(string, int)> list = SumSave.crt_bag_resources.Set();
            for (int i = 0; i < list.Count; i++)
            {
                Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == list[i].Item1);
                if (bag != null)
                {
                    switch ((EquipConfigTypeList)Enum.Parse(typeof(EquipConfigTypeList), bag.StdMode))
                    {
                        case EquipConfigTypeList.���ﵰ:
                            (string, int) lists = list[i];
                            store_item item = Instantiate(store_item_Prefabs, pos_list); 
                            item.PetInit(list[i], "");
                            item.GetComponent<Button>().onClick.AddListener(() => { Select_Egg(lists); });
                            if (crt_bag_egg == null) Select_Egg(lists);
                            break;
                        default:
                            break;
                    }
                }
                
               
            }
        }
    }

    private void Show_Info(crtMaxHeroVO crt_MaxHero)
    {
        foreach (var item in info_item_dic.Keys)
        {
            switch (item)
            {
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, crt_MaxHero.MaxHP);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, crt_MaxHero.MaxMp);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, crt_MaxHero.internalforceMP);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, crt_MaxHero.EnergyMp);
                    break;
                case enum_attribute_list.�������:
                    info_item_dic[item].Show(item, crt_MaxHero.DefMin + " - " + crt_MaxHero.DefMax);
                    break;
                case enum_attribute_list.ħ������:
                    info_item_dic[item].Show(item, crt_MaxHero.MagicDefMin + " - " + crt_MaxHero.MagicDefMax);
                    break;
                case enum_attribute_list.������:
                    info_item_dic[item].Show(item, crt_MaxHero.damageMin + " - " + crt_MaxHero.damageMax);
                    break;
                case enum_attribute_list.ħ������:
                    info_item_dic[item].Show(item, crt_MaxHero.MagicdamageMin + " - " + crt_MaxHero.MagicdamageMax);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, crt_MaxHero.hit);
                    break;
                case enum_attribute_list.���:
                    info_item_dic[item].Show(item, crt_MaxHero.dodge);
                    break;
                case enum_attribute_list.��͸:
                    info_item_dic[item].Show(item, crt_MaxHero.penetrate);
                    break;
                case enum_attribute_list.��:
                    info_item_dic[item].Show(item, crt_MaxHero.block);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, crt_MaxHero.crit_rate);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, crt_MaxHero.Lucky);
                    break;
                case enum_attribute_list.�����˺�:
                    info_item_dic[item].Show(item, crt_MaxHero.crit_damage);
                    break;
                case enum_attribute_list.�˺��ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.double_damage);
                    break;
                case enum_attribute_list.��ʵ�˺�:
                    info_item_dic[item].Show(item, crt_MaxHero.Real_harm);
                    break;
                case enum_attribute_list.�˺�����:
                    info_item_dic[item].Show(item, crt_MaxHero.Damage_Reduction);
                    break;
                case enum_attribute_list.�˺�����:
                    info_item_dic[item].Show(item, crt_MaxHero.Damage_absorption);
                    break;
                case enum_attribute_list.�쳣����:
                    info_item_dic[item].Show(item, crt_MaxHero.resistance);
                    break;
                case enum_attribute_list.�����ٶ�:
                    info_item_dic[item].Show(item, crt_MaxHero.attack_speed);
                    break;
                case enum_attribute_list.�ƶ��ٶ�:
                    info_item_dic[item].Show(item, crt_MaxHero.move_speed);
                    break;
                case enum_attribute_list.�����ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Hp);
                    break;
                case enum_attribute_list.�����ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Mp);
                    break;
                case enum_attribute_list.�����ظ�:
                    info_item_dic[item].Show(item, crt_MaxHero.Heal_Hp);
                    break;
                case enum_attribute_list.�����ظ�:
                    info_item_dic[item].Show(item, crt_MaxHero.Heal_Mp);
                    break;
                case enum_attribute_list.�﹥�ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Damage);
                    break;
                case enum_attribute_list.ħ���ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_MagicDamage);
                    break;
                case enum_attribute_list.����ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Def);
                    break;
                case enum_attribute_list.ħ���ӳ�:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_MagicDef);
                    break;
                default:
                    break;
            }
        }

    }

    /// <summary>
    /// ��ʾ���ﵰ
    /// </summary>
    /// <param name="bag"></param>
    private void Select_Egg((string,int) bag)
    {
        Show_Btn(false,0);
        crt_egg = bag;
        db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == bag.Item1);
        Obtain_Pet(pet, 1);
    }
    /// <summary>
    /// ��ȡ����״̬
    /// </summary>
    private void Obtain_Egg_State()
    {
        string[] spilits= crt_pet.Set().Split(',');
    }

    /// <summary>
    /// չʾ����
    /// </summary>
    /// <param name="item"></param>
    private void Select_Pet(pet_item item)
    {
        //pet_egg_info.text = "";
        crt_pet = item;

        Show_Btn(true,0);
        Obtain_Egg_State();
        db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petName == item.name);
        Obtain_Pet(pet,1);
    }

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="lv"></param>
    private void Obtain_Pet(db_pet_vo pet,int lv)
    {
        if (pet != null)
        {
            crtMaxHeroVO crt = new crtMaxHeroVO();
            List<string> v = pet.crate_values;//�����������
            List<string> va = pet.up_values;//����ɳ�����
            for (int i = 0; i < v.Count; i++)
            {
                int value = int.Parse(v[i]) + (int.Parse(va[i]) * lv);
                Battle_Tool.Enum_Value(crt, i, value);
            }
            Show_Info(crt);
        }
    }
    /// <summary>
    /// ������ʾ
    /// </summary>
    /// <param name="state"></param>
    private void Show_Btn(bool state,int pos = -1)
    {
        for (int i = 0; i < pos_pet_btn.childCount; i++)
        {
            pos_pet_btn.GetChild(i).gameObject.SetActive(i == pos ? !state : state);
        }
    }
}
