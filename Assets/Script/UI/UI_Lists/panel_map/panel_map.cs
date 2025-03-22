using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_map : Panel_Base
{

    private btn_item btn_item_prefab;
    /// <summary>
    /// �ȼ�Ҫ��
    /// </summary>
    private Text need_lv;
    /// <summary>
    /// �����б�
    /// </summary>
    private Text monster_list;
    /// <summary>
    /// ��ƱҪ��
    /// </summary>
    private Text need_Required;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    private Text ProfitList;
    /// <summary>
    /// ��ͼ�б�
    /// </summary>
    private Dictionary<map_pos_item, user_map_vo> maplists = new Dictionary<map_pos_item, user_map_vo>();

    /// <summary>
    /// ս����ͼ
    /// </summary>
    private panel_fight fight_panel;
    /// <summary>
    /// ��ǰѡ���ͼ
    /// </summary>
    private map_pos_item crt_map;
    /// <summary>
    /// �����ͼ��ť
    /// </summary>
    private Button enter_map_button;
    private Image base_show_info;

    protected override void Awake()
    {
        base.Awake();
        need_lv = Find<Text>("bg_main/base_info/need_lv");

        monster_list = Find<Text>("bg_main/base_info/monster_list");
        need_Required = Find<Text>("bg_main/base_info/need_Required");
        ProfitList = Find<Text>("bg_main/base_info/ProfitList");
        enter_map_button = Find<Button>("bg_main/base_info/enter_map_button");
        enter_map_button.onClick.AddListener(Open_Map);
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        base_show_info = Find<Image>("bg_main/base_info");
    }


    public override void Initialize()
    {
        base.Initialize();

        btn_item_prefab = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
       
    }

    protected void Instance_Pos(map_pos_item item)
    {

        if (!maplists.ContainsKey(item))
        {
            foreach (var map in SumSave.db_maps)
            {
                if (map.map_index == item.index)
                { 
                    maplists.Add(item, map);
                    btn_item btn = Instantiate(btn_item_prefab, item.transform);
                    btn.Show(item.index, map.map_name);
                    btn.GetComponent<Button>().onClick.AddListener(delegate { Select_Map(item); });
                }
            }
        }
    }
    /// <summary>
    /// ѡ���ͼ
    /// </summary>
    /// <param name="item"></param>
    private void Select_Map(map_pos_item item)
    {
        crt_map = item;
        base_show_info.gameObject.SetActive(true);
        user_map_vo map = maplists[item];
        need_lv.text = "�ȼ�Ҫ�� "+ map.need_lv.ToString();
        monster_list.text = "�����б� "+map.monster_list.ToString();
        need_Required.text = "��ƱҪ�� "+map.need_Required.ToString();
        ProfitList.text = "��Ʒ���䣺 "+map.ProfitList.ToString();
       
    }

    public override void Show()
    {
        base.Show();
        base_show_info.gameObject.SetActive(false);
    }

    private void Open_Map()
    { 
        if(crt_map==null)
            return;
        //�򿪵�ͼ
        Debug.Log("�򿪵�ͼ");
        fight_panel.Open_Map(maplists[crt_map]);
        Hide();
    }
}
