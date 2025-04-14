using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_hall : Panel_Base
{
    /// <summary>
    /// ��Դ����������ͼ������
    /// </summary>
    private Transform pos_hero, pos_map, pos_otain;
    /// <summary>
    /// Ԥ�������
    /// </summary>
    private btn_item btn_item_Prefabs;
    private List<string> otainlist=new List<string>(){ "ǿ��", "�ϳ�", "����" };
    private List<string> maplist = new List<string>() { "��ͼ1", "��ͼ2", "��ͼ3" };
    private List<string> herolist = new List<string>() { "ǩ��", "ͨ��֤", "�ռ�", "�ɾ�"};
    /// <summary>
    /// �ɾ�ϵͳ
    /// </summary>
    private Achievement achievement;

    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_hero = Find<Transform>("bg_main/panel_list/otainlist/Scroll View/Viewport/Content");
        pos_map = Find<Transform>("bg_main/panel_list/maplist/Scroll View/Viewport/Content");
        pos_otain = Find<Transform>("bg_main/panel_list/herolist/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");

        achievement=Find<Achievement>("bg_main/achievement");

        ClearObject(pos_hero);
        ClearObject(pos_map);
        ClearObject(pos_otain);
        for (int i = 0; i < herolist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_hero);
            item.Show(i,herolist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickHeroItem(item); });
        }
        for (int i = 0; i < maplist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_map);
            item.Show(i,maplist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickMapItem(item); });
        }
        for (int i = 0; i < otainlist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_otain);
            item.Show(i,otainlist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickOtainItem(item); });
        }
    }
    /// <summary>
    /// ����Դ��������
    /// </summary>
    /// <param name="item"></param>
    private void OnClickOtainItem(btn_item item)
    {

    }
    /// <summary>
    /// �򿪵�ͼ
    /// </summary>
    /// <param name="item"></param>
    private void OnClickMapItem(btn_item item)
    {

    }
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="item"></param>
    private void OnClickHeroItem(btn_item item)
    {
        switch (item.index)//"ǩ��", "ͨ��֤", "�ռ�", "�ɾ�"
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                achievement.Show();
                break;


        }

    }

    public override void Show()
    {
        base.Show();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
