using Common;
using Components;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Pet_explore : Panel_Base
{

    /// <summary>
    /// 探索按钮组
    /// </summary>
    private Button[] button_map;



    /// <summary>
    /// 玩家选择的探索
    /// </summary>
    private string explore;

    /// <summary>
    /// 探索次数
    /// </summary>
    private int IsExploring = 5;

    /// <summary>
    /// 按钮预制体
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// 物品信息预制体
    /// </summary>
    private material_item info_item_Prefabs;
    /// <summary>
    /// 探索宠物父物体
    /// </summary>
    private Transform pos_btn;
    /// <summary>
    /// 探索奖励父物体
    /// </summary>
    private Transform pos_Items;
    /// <summary>
    /// 功能按键父物体
    /// </summary>
    private Transform function_pos_btn;
    /// <summary>
    /// 探索宠物列表
    /// </summary>
    private string[] pet_btn_list = new string[] { "狼", "虎", "豹" };
    /// <summary>
    /// 功能按键列表
    /// </summary>
    private string[] function_btn_list = new string[] { "收获", "返回", "探索" };
    /// <summary>
    /// 宠物探索收获列表
    /// </summary>
    private List<(string,int)> btn_item_list = new List<(string, int)>();


    public override void Show() 
    {
        base.Show();
        #region 组件初始化
        button_map = Find<Transform>("explore_map/Buttons_map").GetComponentsInChildren<Button>();
        pos_btn = Find<Transform>("explore/pet_pos_btn");
        pos_Items = Find<Transform>("Income/Items");
        function_pos_btn= Find<Transform>("explore/function_pos_btn");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        info_item_Prefabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        #endregion

        #region 各功能按键初始化
        ///宠物地图探索按钮初始化
        for (int i = 0; i < button_map.Length; i++)
        {
            int index = i;
            button_map[i].onClick.AddListener(() => { Obtain_Explore(index); });
        }

        ///探索宠物button
        ClearObject(pos_btn);
        for (int i = 0; i < pet_btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, pos_btn);
            btn_item.Show(i, pet_btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { UpSetMaterial(btn_item); });
        }

        btn_item_list.Add(("灵石", 100));
        btn_item_list.Add(("武器碎片", 100));
        ///探索材料image
        ClearObject(pos_Items);
        for (int i = 0; i < btn_item_list.Count; i++)
        {
            material_item item = Instantiate(info_item_Prefabs, pos_Items);
            item.Init(btn_item_list[i]);
            //item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(item); });
        }

        ///功能按键初始化
        ClearObject(function_pos_btn);
        for (int i = 0; i < function_btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, function_pos_btn);
            btn_item.Show(i, function_btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { FunctionButton(btn_item); });
        }
        Init();
        #endregion
    }






    /// <summary>
    /// 清空子物体
    /// </summary>
    private void ClearObject(Transform pos_btn)
    {
        for (int i = pos_btn.childCount - 1; i >= 0; i--)//清空区域内按钮
        {
            Destroy(pos_btn.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 按钮具体功能
    /// </summary>
    /// <param name="btn_item"></param>
    private void FunctionButton(btn_item btn_item)
    {
        switch (btn_item.name)
        {
            case "收获":
                break;
            case "返回":
                break;
            case "探索":
                break;
        }
    }


    /// <summary>
    /// 显示宠物探索奖励列表
    /// </summary>
    /// <param name="btn_item"></param>
    private void UpSetMaterial(btn_item btn_item)
    {
        Debug.Log("显示宠物探索奖励列表");
    }


    /// <summary>
    /// 初始化探索列表
    /// </summary>
    /// <param name="data"></param>
    public void Init()
    {
       for(int i=0;i< button_map.Length; i++)//随机添加地图名称
       {
            int r = Random.Range(0, SumSave.db_pet_explore.Count);
            button_map[i].GetComponentInChildren<Text>().text = SumSave.db_pet_explore[r].petExploreMapName;
       }
    }
    /// <summary>
    /// 点击探索
    /// </summary>
    private void Obtain_Explore(int index)
    {
        explore = button_map[index].GetComponentInChildren<Text>().text;//获得探索地图的名字

        if (IsExploring>=0 && SumSave.db_pet_explore_dic.TryGetValue(explore, out user_pet_explore_vo vo)) //判断次数并且更具名字找到该地图的信息
        {
            string[] Explore_list = vo.petEvent_reward.Split("&");//获取该地图的奖励列表

            int r = 0;
            while(true)
            {
                r++;
                string[] data = Explore_list[Random.Range(0, Explore_list.Length)].Split(" ");//根据空格拆分奖励列表
                if (data.Length == 3)//判断奖励格式
                {
                    string[] odds = data[2].Split("/");
                    if (Random.Range(0, int.Parse(odds[1])) < int.Parse(odds[0]))//判断是否获得奖励
                    {
                        GainRewards(data);
                        return;
                    }
                }

                if (r >= 1000)
                {
                    data = Explore_list[1].Split(" ");
                    GainRewards(data);
                    return;
                }
                  
            }
        }
        else Alert_Dec.Show("探索次数不足");
    }

    /// <summary>
    /// 获得奖励并发送消息
    /// </summary>
    /// <param name="data"></param>
    private void GainRewards(string[] data)
    {
        int i=Random.Range(1, int.Parse(data[1])+1);//随机获得奖励数量

        Battle_Tool.Obtain_Resources(data[0], i);//获取奖励
        Alert_Dec.Show("探索收益 " + data[0] + " x " + i);
        IsExploring--;
        Init();
    }
}
