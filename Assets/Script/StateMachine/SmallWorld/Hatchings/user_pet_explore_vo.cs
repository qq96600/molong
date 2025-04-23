using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_pet_explore_vo : Base_VO
{
   /// <summary>
   /// 宠物探索地图名字
   /// </summary>
    public string petExploreMapName;
    /// <summary>
    /// 宠物探索奖励
    /// </summary>
    public string petEvent_reward;
    public List<(string,int)> petExploreReward;

    public void Init()
    {
        petExploreReward = new List<(string, int)>();
        string[] str = petEvent_reward.Split('&'); 

        for (int i = 0; i < str.Length; i++)
        {
            if (str.Length > 0)
            {
                string[] str1 = str[i].Split(' ');
                if (str1.Length == 3)
                    petExploreReward.Add((str1[0],int.Parse(str1[1])));
            }
        }
    }
    
}
