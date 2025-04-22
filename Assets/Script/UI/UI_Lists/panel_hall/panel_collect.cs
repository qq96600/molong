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
    /// <summary>
    /// �ռ���Ʒ����
    /// </summary>
    private db_collect_vo crt_collect;
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
        Put_but.onClick.AddListener(() => { PutItem(); });
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
    private void PutItem()
    {
        Debug.Log("������Ʒ");
        db_collect_vo coll = crt_collect;
        for (int i = 0; i < typeNames.Length; i++)
        {
            if(coll.StdMode==typeNames[i])
            {
                //���ұ����Ƿ��и���Ʒ 
                NeedConsumables(coll.Name, 1);
                if (RefreshConsumables())
                {
                    coll.isCollect = 1;
                    //������� ����user_collect_vo 
                    //AddAttribute(collect.bonuses_types[j], collect.bonuses_values[j]);
                    return;
                }  
            }
        }
        Alert_Dec.Show("����û��" + coll.Name);
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
       
        for (int j= 0; j < item_Type.Count; j++)
        {
            db_collect_vo collect_vo = new db_collect_vo();
            Bag_Base_VO data = new Bag_Base_VO();
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
        db_collect_vo coll =new db_collect_vo();
        coll= collect;
        collect_Title.text= coll.Name;

        Bag_Base_VO Dat =new Bag_Base_VO();
        Dat.Name = coll.Name;
        Dat.StdMode = coll.StdMode;
        item_image.Data = Dat;

        
        collect_info_text.text = "";
        for (int i = 0; i < coll.bonuses_types.Length; i++) 
        {
            string type =(Attribute_Type.GetValue(int.Parse(coll.bonuses_types[i]))).ToString();
            collect_info_text.text += type + "+" + coll.bonuses_values[i] + "\n";
        }

        if (coll.isCollect == 0)
        {
            crt_collect= coll;
            Put_but_text.text = "����";
        }
        else
        {
            Put_but.onClick.AddListener(() => { Alert_Dec.Show(coll.Name + " ���ռ�"); });
            Put_but_text.text = "���ռ�";
        }
    }
}
