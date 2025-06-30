using Common;
using MVC;
using System.Collections.Generic;

public class db_lv_vo : Base_VO
{
    /// <summary>
    /// 玩家等级需求经验
    /// </summary>
    public List<long> hero_lv_list;
    /// <summary>
    /// 小世界升级需求
    /// </summary>
    public Dictionary<int, List<(string, int)> > world_lv_list_dic = new Dictionary<int, List<(string, int)>>();
    /// <summary>
    /// 小世界升级效果
    /// </summary>
    public List<int> world_offect_list ;
    /// <summary>
    /// 小世界最大存储值 更具小世界等级变化
    /// </summary>
    public List<int> word_lv_max_value;

    /// <summary>
    /// 荣耀殿堂添加灵气上限
    /// </summary>
    public void AddUpperLimit()
    {
        for (int i = 0; i < SumSave.db_vip_list.Count; i++)
        {
            if (SumSave.db_vip_list[i].vip_lv == SumSave.crt_accumulatedrewards.SetRecharge().Item1)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < word_lv_max_value.Count; j++)
                {
                    list.Add(word_lv_max_value[j] + SumSave.db_vip_list[i].upperLimitOfSpiritualEnergy);
                }
                word_lv_max_value = list;
            }
        }
    }
}
