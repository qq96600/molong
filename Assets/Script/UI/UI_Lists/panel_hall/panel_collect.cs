using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum suit_Type//��װ����
{
    ������װ,
    ��Ը��װ,
    ������װ,
    Ѫҹ��װ, 
    �����װ, 
    ڤ����װ
}





public class panel_collect : Base_Mono
{   
    /// <summary>
    /// �ռ���Ʒλ��
    /// </summary>
    private Transform collect_item;
    /// <summary>
    /// ��ƷԤ����
    /// </summary>
    private bag_item bag_Item;
    /// <summary>
    /// ��Ʒ������Ϣ
    /// </summary>
    private Transform collect_info;
    /// <summary>
    /// ��Ʒ������Ϣ����
    /// </summary>
    private Text collect_Title;
    /// <summary>
    /// ��Ʒ������ϢͼƬ
    /// </summary>
    private bag_item item_image;
    /// <summary>
    /// ��Ʒ������Ϣ����
    /// </summary>
    private Text collect_info_text;
    /// <summary>
    /// ��Ʒ���밴ť
    /// </summary>
    private Button Put_but;
    /// <summary>
    /// ���밴ť����
    /// </summary>
    private Text Put_but_text;
    /// <summary>
    /// �����Ƿ��ռ� 0��δ�ռ� 1�����ռ�
    /// </summary>
    //private int isCollect=0;
    /// <summary>
    /// ��������
    /// </summary>
    private Array Attribute_Type;
    /// <summary>
    /// ��Ϣ����collect_info�رհ�ť
    /// </summary>
    private Button but;
    /// <summary>
    /// װ������
    /// </summary>
    private string[] typeNames;
    /// <summary>
    /// �ռ���Ʒ����λ��
    /// </summary>
    private Transform pos_collect_type;
    /// <summary>
    /// �ռ���Ʒ����Ԥ����
    /// </summary>
    private btn_item btn_Item;
    private void Awake()
    {
        collect_item = Find<Transform>("collect_Scroll/Viewport/collect_items");
        bag_Item=Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        btn_Item=Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        pos_collect_type = Find<Transform>("Type_but/Viewport/Type");
        Attribute_Type = Enum.GetValues(typeof(enum_skill_attribute_list));       
        but=Find<Button>("but"); 
        but.onClick.AddListener(() =>{ CloseInfo();});
        
        #region �ռ���Ʒ��Ϣ����
        collect_info = Find<Transform>("collect_info");
        collect_Title = Find<Text>("collect_info/collect_Title/Title");
        item_image = Find<bag_item>("collect_info/item_image/bag_item");
        collect_info_text = Find<Text>("collect_info/collect_info_text/info_text");
        Put_but = Find<Button>("collect_info/Put_but");
        Put_but_text= Find<Text>("collect_info/Put_but/Item_state");

        #endregion
        

        CloseInfo();
        Init();
    }

    /// <summary>
    /// �ر��ռ���Ʒ��Ϣ
    /// </summary>
    private void CloseInfo()
    {
        collect_info.gameObject.SetActive(false);
        but.gameObject.SetActive(false);
    }

    /// <summary>
    /// ������Ʒ 
    /// </summary>
    private void PutItem(db_collect_vo collect)
    {
        Debug.Log("������Ʒ");

        collect.isCollect = 1;

        for (int i = 0; i < typeNames.Length; i++)
        {
            if(collect.StdMode==typeNames[i])
            {
                //���ұ����Ƿ��и���Ʒ 
                NeedConsumables(collect.Name, 1);
                if (RefreshConsumables())
                {
                    for(int j = 0; j < collect.bonuses_types.Length; j++)
                    {
                        //������� ����user_collect_vo 
                        //AddAttribute(collect.bonuses_types[j], collect.bonuses_values[j]);

                    }
                }
                else
                {
                    Alert_Dec.Show("����û��"+ collect.Name);
                }  
            }
        }

            

        }

    public void Init()
    {
       
        ClearObject(pos_collect_type);
        typeNames = Enum.GetNames(typeof(EquipTypeList));
        //��ʾ������Ʒ
        for (int i = 0; i < typeNames.Length; i++)
        {
            string typeName = typeNames[i];
            btn_item but_type = Instantiate(btn_Item, pos_collect_type);
            but_type.Show(i, typeName);
            but_type.GetComponent<Button>().onClick.AddListener(() => { ShowCollectItem(typeName); });
        }

        ShowCollectItem(typeNames[0]);



    }
    /// <summary>
    /// ��ʾ�ռ���Ʒ
    /// </summary>
    /// <param name="index"></param>
    private void ShowCollectItem(string Type)
    {
        ClearObject(collect_item);
        List<db_collect_vo> item_Type = new List<db_collect_vo>();
        for (int i = 0; i < SumSave.db_collect_vo.Count; i++)
        {
            if (SumSave.db_collect_vo[i].StdMode == Type)
            {
                item_Type.Add(SumSave.db_collect_vo[i]);
            }
            
        }

        for(int j= 0; j < item_Type.Count; j++)
        {
            Bag_Base_VO data = new Bag_Base_VO();
            db_collect_vo collect_vo = new db_collect_vo();
            collect_vo = item_Type[j];
            data.Name = item_Type[j].Name;
            bag_item item = Instantiate(bag_Item, collect_item);
            item.Data = data;
            item.GetComponent<Button>().onClick.AddListener(() => { OpenCollectInfo(collect_vo); });
        }

    }


    /// <summary>
    /// �����ռ���Ʒ����
    /// </summary>
    /// <param name="data"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private Bag_Base_VO SetDate(Bag_Base_VO data,int idex)
    {
        data.Name = SumSave.db_collect_vo[idex].Name;
        data.StdMode= SumSave.db_collect_vo[idex].StdMode;

        
         
        return data;
    }
    /// <summary>
    /// ���ռ���Ʒ��Ϣ
    /// </summary>
    /// <param name="data"></param>
    private void OpenCollectInfo(db_collect_vo collect)
    {

        but.gameObject.SetActive(true);
        collect_info.gameObject.SetActive(true);
        collect_Title.text= collect.Name;

        Bag_Base_VO Dat =new Bag_Base_VO();
        Dat.Name = collect.Name;
        Dat.StdMode = collect.StdMode;
        item_image.Data = Dat;

        
        collect_info_text.text = "";
        for (int i = 0; i < collect.bonuses_types.Length; i++) 
        {
            collect_info_text.text += collect.bonuses_types[i] + "+" + collect.bonuses_values[i] + "\n";
        }
       
       

        if (collect.isCollect == 0)
        {
            Put_but.onClick.AddListener(() => { PutItem(collect); });
            Put_but_text.text = "����";
        }
        else
        {
            Put_but.onClick.AddListener(() => { Alert_Dec.Show(collect.Name + " ���ռ�"); });
            Put_but_text.text = "���ռ�";
        }
        

    }
}
