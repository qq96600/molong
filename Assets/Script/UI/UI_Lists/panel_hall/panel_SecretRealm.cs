using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_SecretRealm : Base_Mono
{
    
    private Transform pos_crtmap;
    private copies_item copies_item_Prefabs;
    /// <summary>
    /// ս����ͼ
    /// </summary>
    private panel_fight fight_panel;
    /// <summary>
    /// ��Ϣ��ʾ���
    /// </summary>
    private Transform base_show_info;

    /// <summary>
    /// ��ͼ����, ��Ҫ�ȼ�,�����б�,��Ʊ
    /// </summary>
    private Text  map_name,monster_list, need_Required;
    /// <summary>
    /// ��Ʒλ��
    /// </summary>
    private Transform pos_life;
    /// <summary>
    /// �����ͼ��ť,��Ϣ���ڹرհ�ť
    /// </summary>
    private Button enter_map_button,Close_button;
    /// <summary>
    /// ��ǰѡ���ͼ
    /// </summary>
    private copies_item item;
    /// <summary>
    /// ��Ʒ��ϢԤ����
    /// </summary>
    private material_item material_item_parfabs;
    /// <summary>
    /// �Ѷ�ѡ�������б�
    /// </summary>
    private Dropdown dropdown;
    /// <summary>
    /// ���������Ϣ��
    /// </summary>
    private Text drop_rate;
    /// <summary>
    /// Ʒ����ɫ
    /// </summary>
    private readonly string[] qualityColors = new string[]
   {
        "#FFFFFF", // ��ͨ����ɫ��
        "#00FF00", // ��������ɫ��
        "#00B4FF", // ������������
        "#800080", // ʷʫ����ɫ��
        "#FFA500", // ��˵����ɫ��
        "#FF0000", // �񻰣���ɫ��
        "#FFFF00"  // ��������ɫ��
   };

    private void Awake()
    {
        pos_crtmap = Find<Transform>("Scroll View/Viewport/Content");
        copies_item_Prefabs = Battle_Tool.Find_Prefabs<copies_item>("copies_item");
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        map_name= Find<Text>("Difficulty_info/map_name/map_name_text");

        base_show_info = Find<Transform>("Difficulty_info");

        monster_list = Find<Text>("Difficulty_info/monster_list");
        need_Required = Find<Text>("Difficulty_info/need_Required");
        pos_life = Find<Transform>("Difficulty_info/ProfitList/Scroll View/Viewport/Content");
        enter_map_button = Find<Button>("Difficulty_info/enter_map_button");
        material_item_parfabs = Battle_Tool.Find_Prefabs<material_item>("material_item");
        enter_map_button.onClick.AddListener(()=>confirm());

        dropdown= Find<Dropdown>("Difficulty_info/difficulty_selection/Dropdown");
        drop_rate = Find<Text>("Difficulty_info/difficulty_selection/Scroll View/Viewport/Content/Intr");
        Close_button= Find<Button>("Difficulty_info/Close");
        Close_button.onClick.AddListener(()=>base_show_info.gameObject.SetActive(false));
        // �������ѡ��
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < Enum.GetNames(typeof(enum_equip_quality_list)).Length; i++)
        {
            options.Add(Enum.GetNames(typeof(enum_equip_quality_list))[i]);
        }
        // �����ѡ��
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(OnValueChanged);
        //����Ϊ��ͼ��Ѷ�
        dropdown.value = 0;
        OnValueChanged(dropdown.value);
    }

    /// <summary>
    /// ѡ���Ѷ�
    /// </summary>
    /// <param name="arg0"></param>
    private void OnValueChanged(int arg0)
    {
        string str = "";
        for (int i = 0; i < Enum.GetNames(typeof(enum_equip_quality_list)).Length; i++)
        {
            if(dropdown.value != Enum.GetNames(typeof(enum_equip_quality_list)).Length-1)
            {
                if (dropdown.value == i)
                {
                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 50%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 50%\n";
                    }

                }
                else
                if (dropdown.value - 1 == i)
                {

                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 45%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 45%\n";
                    }

                }
                else if (dropdown.value + 1 == i)
                {

                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 5%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 5%\n";
                    }
                }
                else
                {
                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 0%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 0%\n";
                    }
                }
            }else
            {
                if (dropdown.value== i)
                {
                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 10%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 10%\n";
                    }
                }else
                {
                    if (qualityColors.Length > i)
                    {
                        str += "<color=" + qualityColors[i] + ">" + Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 0%</color>\n";
                    }
                    else
                    {
                        str += Enum.GetNames(typeof(enum_equip_quality_list))[i] + ": 0%\n";
                    }
                }

            }
            
        }
        drop_rate.text= str;
    }





    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="item"></param>
    private void OnClick(copies_item _item)
    {
        item=_item;
        base_show_info.transform.gameObject.SetActive(true);
        InitInfo(item.index);
    }


    /// <summary>
    /// ��ʼ��������Ϣ
    /// </summary>
    private void InitInfo(user_map_vo map)
    {
        monster_list.text = "�����б� " + map.monster_list.ToString();
        need_Required.text = "��ƱҪ�� " + map.need_Required.ToString();
        map_name.text = map.map_name;
        for (int i = pos_life.childCount - 1; i >= 0; i--)//��������ڰ�ť
        {
            Destroy(pos_life.GetChild(i).gameObject);
        }
        foreach (string str in map.ProfitList.Split('&'))
        {
            string[] str1 = str.Split(' ');
            material_item item = Instantiate(material_item_parfabs, pos_life);
            item.Init(((str1[0]), 0));
            item.GetComponent<Button>().onClick.AddListener(delegate { Alert.Show(str1[0], str1[0]); });
        }
    }

    /// <summary>
    /// ȷ�Ͻ���
    /// </summary>
    /// <param name="arg0"></param>
    private void confirm()
    {
        if(item== null)
        {
            return;
        }

        if (item.index.need_Required != "")
        {
            NeedConsumables(item.index.need_Required, 1);
            if (RefreshConsumables())
            {
                Open_Map(item);
                base_Show();
            }
            else
            {
                Alert_Dec.Show("��ս��Ʊ����");
            }
        }
        else
        {
            Alert_Dec.Show("������δ����");
        }

    }

    /// <summary>
    /// �����ͼ
    /// </summary>
    /// <param name="item"></param>
    private void Open_Map(copies_item item)
    {
        bool exist = true;
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == item.index.map_name)
            {
                exist = false;
                list[i] = (list[i].Item1, list[i].Item2 + 1);
                SumSave.crt_needlist.SetMap(list[i]);
                break;
            }
        }
        if (exist) SumSave.crt_needlist.SetMap((item.index.map_name, 1));
        //д������

        fight_panel.Show();
        fight_panel.Open_Map(item.index, true);
        item.updatestate();
    }

   
    public override void Show()
    {
        base.Show();
        if (SumSave.crt_MaxHero.Lv < 30 && SumSave.ios_account_number != "admin001")
        {
            Alert_Dec.Show("�ؾ������ȼ�Ϊ30��");
            gameObject.SetActive(false);
            return;
        }
        base_show_info.transform.gameObject.SetActive(false);
        base_Show();
    }
    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void base_Show()
    {
        ClearObject(pos_crtmap);
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        
        for (int i = SumSave.db_maps.Count - 1; i > 0; i--)
        {
            if (SumSave.db_maps[i].map_type == 8&&SumSave.crt_hero.hero_Lv >= SumSave.db_maps[i].need_lv)
            {
                int number = 0;
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].Item1 == SumSave.db_maps[i].map_name)
                    {
                        number = list[j].Item2;
                        break;
                    }
                }
                copies_item item = Instantiate(copies_item_Prefabs, pos_crtmap);


                List<(string, int)> _list = SumSave.crt_bag_resources.Set();
                int num = 0;
                 for (int j = 0; j < _list.Count; j++)
                {
                    if (_list[j].Item1 == SumSave.db_maps[i].need_Required)
                    {
                        num= _list[j].Item2;
                    }
                }

                item.InitSecretRealm(SumSave.db_maps[i],num);
                item.GetComponent<Button>().onClick.AddListener(() => { OnClick(item); });
            }
        }
    }
}
