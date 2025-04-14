using MVC;
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
    private List<string> herolist = new List<string>() { "ǩ��", "ͨ��֤", "�ռ�", "�ɾ�","���а�"};
    /// <summary>
    /// �ɾ�ϵͳ
    /// </summary>
    private Achievement achievement;
    /// <summary>
    /// ���а�
    /// </summary>
    private offect_rank offect_rank;

    private panel_pass pass;

    private Image offect_list;

    /// <summary>
    /// offect������
    /// </summary>
    private int index = -1;
    public override void Hide()
    {
        if (offect_list.gameObject.activeInHierarchy)
        {
            for (int i = offect_list.transform.childCount - 1; i >= 0; i--)//�ر������ڵ�UI
            {
                offect_list.transform.GetChild(i).gameObject.SetActive(false);
            }
            offect_list.gameObject.SetActive(false);
        }
        else
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_hero = Find<Transform>("bg_main/panel_list/otainlist/Scroll View/Viewport/Content");
        pos_map = Find<Transform>("bg_main/panel_list/maplist/Scroll View/Viewport/Content");
        pos_otain = Find<Transform>("bg_main/panel_list/herolist/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        offect_list=Find<Image>("bg_main/offect_list");
        achievement =Find<Achievement>("bg_main/achievement");
        offect_rank= Resources.Load<offect_rank>("Prefabs/panel_hall/offect_rank");
        pass = Resources.Load<panel_pass>("Prefabs/panel_hall/panel_pass");

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
        index = 200 + item.index;

    }
    /// <summary>
    /// �򿪵�ͼ
    /// </summary>
    /// <param name="item"></param>
    private void OnClickMapItem(btn_item item)
    {
        index =100+ item.index;

        //for (int i = offect_list.transform.childCount - 1; i >= 0; i--)//��������ڰ�ť
        //{
        //    Base_Mono offect = Resources.Load<Base_Mono>("Prefabs/panel_hall/"+(hall_offect_list)index);
        //    if (offect_list.transform.GetChild(i).GetComponent<>() != null)
        //    {
        //        offect_list.transform.GetChild(i).GetComponent<>().Show();
        //        return;
        //    }
        //}
        //if (exist)
        //{
        //    Instantiate(pass, offect_list.transform).GetComponent<panel_pass>().Show();
        //}

    }
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="item"></param>
    private void OnClickHeroItem(btn_item item)
    {
        index = item.index;
        offect_list.gameObject.SetActive(true);
        bool exist = true;
        switch (item.index)//"ǩ��", "ͨ��֤", "�ռ�", "�ɾ�"
        {
            case 0:
                break;
            case 1:
                for (int i = offect_list.transform.childCount - 1; i >= 0; i--)//��������ڰ�ť
                {
                    if (offect_list.transform.GetChild(i).GetComponent<panel_pass>() != null)
                    {
                        exist = false;
                        offect_list.transform.GetChild(i).GetComponent<panel_pass>().Show();
                        return;
                    }
                }
                if (exist)
                {
                     Instantiate(pass, offect_list.transform).GetComponent<panel_pass>().Show();
                }
                break;
            case 2:
                break;
            case 3:
                achievement.Show();
                break;
            case 4:
                for (int i = offect_list.transform.childCount - 1; i >= 0; i--)//��������ڰ�ť
                {
                    if (offect_list.transform.GetChild(i).GetComponent<offect_rank>() != null)
                    {
                        exist = false;
                        offect_rank offect = offect_list.transform.GetChild(i).GetComponent<offect_rank>();
                        offect.gameObject.SetActive(true);
                        offect.Show();
                        return;
                    }
                }
                if (exist)
                {
                    offect_rank offect = Instantiate(offect_rank, offect_list.transform);
                    offect.gameObject.SetActive(true);
                    offect.Show();
                }
                break;


        }

    }

    public override void Show()
    {
        base.Show();
        offect_list.gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
