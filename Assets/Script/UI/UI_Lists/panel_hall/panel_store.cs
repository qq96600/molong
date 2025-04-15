using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class panel_store : Base_Mono
{
    /// <summary>
    /// �̵����͸�����
    /// </summary>
    private Transform store_type; 
    /// <summary>
    /// �̵���������
    /// </summary>
    private string[] type_name = { "����", "�޹�", "����" };
    /// <summary>
    /// �̵����Ͱ�ť
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// �̵�����Ʒ������
    /// </summary>
    private Transform store_item;
    /// <summary>
    /// �̵�����ƷԤ����
    /// </summary>
    private material_item material_item;
    /// <summary>
    /// ��ǰ�̵���Ʒ�ֵ�
    /// </summary>
    private Dictionary<string, db_store_vo> items_dic = new Dictionary<string, db_store_vo>();
    /// <summary>
    /// ��ǰ�̵���Ʒ�б�
    /// </summary>
    private List<db_store_vo> items_list = new List<db_store_vo>();

    private void Awake()
    {
        store_type = Find<Transform>("store_type");
        btn_item = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        store_item = Find<Transform>("store_item/Viewport/Content");
        material_item = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        ClearObject(store_type);
        for (int i = 0; i < type_name.Length; i++)
        {
            btn_item btn = Instantiate(btn_item, store_type);
            btn.Show(i, type_name[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { ShowItem(btn.index); });
        }
    }

    /// <summary>
    /// ��ʾ�̵�����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void ShowItem(int index)
    {
        items_dic.Clear();
        items_list.Clear();
        ClearObject(store_item);
        for (int i = 0; i < SumSave.db_stores_dic.Count; i++)//�ж��Ƿ�Ϊ��ǰ�̵�����
        {
            if (SumSave.db_stores_dic[i].store_Type == (index + 1))
            {
                if (!items_dic.ContainsKey(SumSave.db_stores_dic[i].ItemName))
                {
                    items_dic.Add(SumSave.db_stores_dic[i].ItemName, SumSave.db_stores_dic[i]);
                    items_list.Add(SumSave.db_stores_dic[i]);
                }
                else
                {
                    Debug.LogError("�̵���" + SumSave.db_stores_dic[i].ItemName + "��Ʒ�ظ�");
                }
            }
        }
        
        for (int i = 0; i < items_list.Count; i++)//��ʾ�̵�����Ʒ
        {
            material_item item = Instantiate(material_item, store_item);

            item.Init((items_list[i].ItemName, items_list[i].ItemPrice));

            item.GetComponent<Button>().onClick.AddListener(() => { ShowItemInfo(items_list[i]); });
        }
       
    }
    /// <summary>
    /// �����Ʒ��ʾ��Ʒ��Ϣ
    /// </summary>
    /// <param name="db_store_vo"></param>
    private void ShowItemInfo(db_store_vo db_store_vo)
    {
        Debug.Log("���"+db_store_vo.ItemName);
    }
}
