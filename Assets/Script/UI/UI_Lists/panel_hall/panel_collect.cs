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
    地脉套装=0,
    祈愿套装,
    回忆套装,
    血夜套装, 
    虚空套装, 
    冥噬套装,
    天启套装,
    灭魂套装,
    奥术套装, 
    雷狱套装,
    风息套装,
    岩兽套装

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
    private List<string> typeNames;
    /// <summary>
    /// 当前选择的套装
    /// </summary>
    private List<db_collect_vo> suitItem;
    /// <summary>
    /// 收集物品类型位置
    /// </summary>
    private Transform pos_collect_type;
    /// <summary>
    /// 收集物品类型预制体
    /// </summary>
    private btn_item btn_Item;
    /// <summary>
    /// 收集物品
    /// </summary>
    private db_collect_vo crt_collect;
    private void Awake()
    {
        collect_item = Find<Transform>("collect_Scroll/Viewport/collect_items");
        bag_Item= Battle_Tool.Find_Prefabs<bag_item>("bag_item"); //Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        btn_Item= Battle_Tool.Find_Prefabs<btn_item>("btn_item"); //Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        pos_collect_type = Find<Transform>("Type_but/Viewport/Type");
        Attribute_Type = Enum.GetValues(typeof(enum_skill_attribute_list));       
        but=Find<Button>("but"); 
        but.onClick.AddListener(() =>{ CloseInfo();});
        

        #region 收集物品信息窗口
        collect_info = Find<Transform>("collect_info");
        collect_Title = Find<Text>("collect_info/collect_Title/Title");
        item_image =Find<bag_item>("collect_info/item_image/bag_item");
        collect_info_text = Find<Text>("collect_info/collect_info_text/info_text");
        Put_but = Find<Button>("collect_info/Put_but");
        Put_but_text= Find<Text>("collect_info/Put_but/Item_state");
        Put_but.onClick.AddListener(() => { PutItem(); });
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
    private void PutItem()
    {
        //if (SumSave.crt_collect.user_collect_dic[crt_collect.Name]==0)//是否为已收集
        if(!SumSave.crt_collect.user_collect_dic.ContainsKey(crt_collect.Name))
        {

            if ( !SumSave.crt_collect.user_collect_dic.ContainsKey(crt_collect.Name)|| SumSave.crt_collect.user_collect_dic[crt_collect.Name] == 0)
            {
                List<Bag_Base_VO> sell_list = new List<Bag_Base_VO>();
                foreach (Bag_Base_VO item in SumSave.crt_bag)
                {
                    if (item.user_value != null)
                    {
                        string[] info_str = item.user_value.Split(' ');
                        if (info_str.Length >= 6)
                        {
                            if (info_str[5] == "0")
                            {
                                sell_list.Add(item);

                            }
                        }
                        else
                        {
                            sell_list.Add(item);
                        }
                    }
                }

                foreach (Bag_Base_VO item in sell_list)
                {
                    string[] info = item.user_value.Split(' ');

                    if (item.Name == crt_collect.Name&&int.Parse( info[2])>=7)
                    {
                        SumSave.crt_bag.Remove(item);
                        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
                        SumSave.crt_collect.collect_complete(crt_collect.Name);//收集完成
                        Alert_Dec.Show(crt_collect.Name + " 收集成功");
                        SuitCollect(crt_collect);
                        CollectTasks();
                        return;
                    }
                  
                }
                Alert.Show("收集失败", "该装备被锁定，或背包没有该绝世品阶装备：" + crt_collect.Name);
 
            }
        }        
        else
        {
            Alert_Dec.Show("该物品已收集" );
        }

    }
    /// <summary>
    /// 完成收集任务
    /// </summary>
    private void CollectTasks()
    {
        tool_Categoryt.Base_Task(1023);
        tool_Categoryt.Base_Task(1043);
    }


    /// <summary>
    /// 判断套装是否收集完成
    /// </summary>
    /// <param name="coll"></param>
    private void SuitCollect(db_collect_vo coll)
    {
        for (int j = 0; j < suit_Type.GetNames(typeof(suit_Type)).Length; j++)
        {
            if (coll.StdMode == suit_Type.GetNames(typeof(suit_Type))[j])//是否为套装
            {
                List<db_collect_vo> suit = new List<db_collect_vo>();
                for (int z = 0; z < SumSave.db_collect_vo.Count; z++)//获得该套装未收集的装备
                {
                    if (SumSave.db_collect_vo[z].StdMode == coll.StdMode && SumSave.crt_collect.user_collect_dic[SumSave.db_collect_vo[z].Name] == 0)
                    {
                        suit.Add(SumSave.db_collect_vo[z]);
                    }
                }
                if (suit.Count == 0)
                {
                    string str = "";
                    for (int i = 0; i < crt_collect.bonuses_types.Length; i++)
                    {
                        string type = (enum_skill_attribute_list)(int.Parse(coll.bonuses_types[i])) + "";
                        str += type + "+" + coll.bonuses_values[i] + ",";
                    }

                    SetCollectionTask();
                    Alert_Dec.Show("该套装已收集,增加的属性为：" + str);
                }
                else
                {
                    string str = "";
                    for (int i = 0; i < suit.Count; i++)
                    {
                        str += suit[i].Name + " ";
                    }
                    Alert_Dec.Show("该套装中的" + str + "未收集");
                }
            }

        }
    }

    /// <summary>
    /// 完成套装收集任务
    /// </summary>
    private void  SetCollectionTask()
    {
        tool_Categoryt.Base_Task(1068);

    }

    public void Init()
    {
       
        ClearObject(pos_collect_type);
        typeNames= new List<string>();
        string[] type= Enum.GetNames(typeof(EquipTypeList));
        string[] type2= Enum.GetNames(typeof(suit_Type));
        for (int i = 0; i < type.Length; i++)
        {
            typeNames.Add(type[i]);
        }
        for (int i = 0; i < type2.Length; i++)
        {
            typeNames.Add(type2[i]);
        }

        
        //显示单个物品
        for (int i = 0; i < typeNames.Count; i++)
        {
            string typeName = typeNames[i];
            btn_item but_type = Instantiate(btn_Item, pos_collect_type);
            but_type.Show(i, typeName);
            but_type.GetComponent<Button>().onClick.AddListener(() => { ShowCollectItem(typeName); });
        }
        


        //foreach (var item in SumSave.crt_collect.user_collect_suit_dic)
        //{
        //    for(int i=0;i<SumSave.db_collect_vo.Count;i++)
        //    {
        //        if (item.Key == SumSave.db_collect_vo[i].Name)
        //        {
        //            string typeName = item.Value.ToString();
        //            btn_item but_type = Instantiate(btn_Item, pos_collect_type);
        //            but_type.Show(i, typeName);
        //        }
        //    }

        //}


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
            if (SumSave.db_collect_vo[i].StdMode == Type)//找到同类型的物品
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

            if (!SumSave.crt_collect.user_collect_dic.ContainsKey(item_Type[j].Name) || SumSave.crt_collect.user_collect_dic[item_Type[j].Name] == 0)
            {
                item.Transparent();
            }

            

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

        
        crt_collect = collect;
        collect_Title.text= crt_collect.Name;

        Bag_Base_VO Dat =new Bag_Base_VO();
        Dat.Name = crt_collect.Name;
        Dat.StdMode = crt_collect.StdMode;
        item_image.Data = Dat;

        
        collect_info_text.text = "";

        string[] typ = Enum.GetNames(typeof(suit_Type));
        for (int i = 0; i < typ.Length; i++)
        {
            if (crt_collect.StdMode == typ[i])
            {
                collect_info_text.text += "套装收集完成后获得:\n";
            }
        }

        for (int i = 0; i < crt_collect.bonuses_types.Length; i++) 
        {
            string type =(Attribute_Type.GetValue(int.Parse(crt_collect.bonuses_types[i]))).ToString();
            collect_info_text.text += type + "+" + crt_collect.bonuses_values[i] + "\n";
        }

        //if (SumSave.crt_collect.user_collect_dic[crt_collect.Name] == 0)
        //{
        //    Put_but_text.text = "放入";
        //}
        //else
        //{
        //    Put_but_text.text = "已收集";
        //}

        if(!SumSave.crt_collect.user_collect_dic.ContainsKey(crt_collect.Name)|| SumSave.crt_collect.user_collect_dic[crt_collect.Name] == 0)
        {
            Put_but_text.text = "放入";
        }else
        {
            Put_but_text.text = "已收集";
        }
    }
}
