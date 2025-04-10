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
    /// 奖励以及个数
    /// </summary>
    private List<(string, int)> Explore_list = new List<(string, int)>();

    /// <summary>
    /// 玩家选择的探索
    /// </summary>
    private string explore;

    /// <summary>
    /// 是否可以探索
    /// </summary>
    private bool IsExploring = true;

    protected override void Awake()
    {
        #region 按钮初始化
        button_map= transform.Find("Buttons_map").GetComponentsInChildren<Button>();

        for (int i = 0; i < button_map.Length; i++)
        {
            int index = i;
            button_map[i].onClick.AddListener(() => { Obtain_Explore(index); });
        }
        Init();
        #endregion
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
        explore = button_map[index].GetComponentInChildren<Text>().text;

        if (IsExploring && SumSave.db_pet_explore_dic.TryGetValue(explore, out user_pet_explore_vo vo)) //判断是否可以探索并且在字典中找到该地图的信息
        {
            Explore_list = vo.petExploreReward;//获取该地图的奖励列表

            (string, int) data= Explore_list[Random.Range(0, Explore_list.Count)];//随机获取奖励

            Battle_Tool.Obtain_Resources(data.Item1, data.Item2);
            //Explore_list.Remove(data);
            Alert_Dec.Show("探索收益 " + data.Item1 + " x " + data.Item2);
            Init();
        }
        else Alert_Dec.Show("探索次数不足");


    }
}
