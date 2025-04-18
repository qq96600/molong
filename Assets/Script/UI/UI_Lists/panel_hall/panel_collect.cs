using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum suit_Type//套装类型
{
    地脉套装,
    祈愿套装,
    回忆套装,
    血夜套装, 
    虚空套装, 
    冥噬套装
}





public class panel_collect : Base_Mono
{   
    /// <summary>
    /// 收集物品位置
    /// </summary>
    private Transform collect_item;
    /// <summary>
    /// 物品预制体
    /// </summary>
    private bag_item bag_Item;
    /// <summary>
    /// 物品具体信息
    /// </summary>
    private Transform collect_info;
    /// <summary>
    /// 物品具体信息标题
    /// </summary>
    private Text collect_Title;
    /// <summary>
    /// 物品具体信息图片
    /// </summary>
    private bag_item item_image;
    /// <summary>
    /// 物品具体信息描述
    /// </summary>
    private Text collect_info_text;
    /// <summary>
    /// 物品放入按钮
    /// </summary>
    private Button Put_but;
    /// <summary>
    /// 放入按钮描述
    /// </summary>
    private Text Put_but_text;
    /// <summary>
    /// 物体是否被收集 0：未收集 1：已收集
    /// </summary>
    //private int isCollect=0;
    /// <summary>
    /// 属性类型
    /// </summary>
    private Array Attribute_Type;
    /// <summary>
    /// 信息界面collect_info关闭按钮
    /// </summary>
    private Button but;
    /// <summary>
    /// 装备类型
    /// </summary>
    private string[] typeNames;
    /// <summary>
    /// 收集物品类型位置
    /// </summary>
    private Transform pos_collect_type;
    /// <summary>
    /// 收集物品类型预制体
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
        
        #region 收集物品信息窗口
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
    /// 关闭收集物品信息
    /// </summary>
    private void CloseInfo()
    {
        collect_info.gameObject.SetActive(false);
        but.gameObject.SetActive(false);
    }

    /// <summary>
    /// 放入物品 
    /// </summary>
    private void PutItem(db_collect_vo collect)
    {
        Debug.Log("放入物品");

        collect.isCollect = 1;

        for (int i = 0; i < typeNames.Length; i++)
        {
            if(collect.StdMode==typeNames[i])
            {
                //查找背包是否有该物品 
                NeedConsumables(collect.Name, 1);
                if (RefreshConsumables())
                {
                    for(int j = 0; j < collect.bonuses_types.Length; j++)
                    {
                        //添加属性 创建user_collect_vo 
                        //AddAttribute(collect.bonuses_types[j], collect.bonuses_values[j]);

                    }
                }
                else
                {
                    Alert_Dec.Show("背包没有"+ collect.Name);
                }  
            }
        }

            

        }

    public void Init()
    {
       
        ClearObject(pos_collect_type);
        typeNames = Enum.GetNames(typeof(EquipTypeList));
        //显示单个物品
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
    /// 显示收集物品
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
    /// 设置收集物品数据
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
    /// 打开收集物品信息
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
            Put_but_text.text = "放入";
        }
        else
        {
            Put_but.onClick.AddListener(() => { Alert_Dec.Show(collect.Name + " 已收集"); });
            Put_but_text.text = "已收集";
        }
        

    }
}
