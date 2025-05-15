using Common;
using MVC;
using System.Collections.Generic;

public class user_Accumulatedrewards_vo : Base_VO
{ 
    /// <summary>
    /// 0通行证累积1签到累积2充值累积奖励
    /// </summary>
    private Dictionary<int,List<int>> accumulated_rewards;

    public void Init()
    {
        accumulated_rewards= new Dictionary<int, List<int>>();
        string[] str = user_value.Split('|');
        for (int i = 0; i < str.Length; i++)
        { 
            string[] str1 = str[i].Split(',');
            if (str1.Length > 1)
            {
                if (!accumulated_rewards.ContainsKey(int.Parse(str1[0])))
                {
                    accumulated_rewards.Add(int.Parse(str1[0]), new List<int>());
                }
                string[] str2 = str1[1].Split(';');
                for (int j = 0; j < str2.Length; j++)
                { 
                    accumulated_rewards[int.Parse(str1[0])].Add(int.Parse(str2[j]));
                }
            }
           
        }
    }
    /// <summary>
    /// 取出数据
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, List<int>> Set()
    {
        return accumulated_rewards;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(DataSet())
        };
    }
    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="value"></param>
    public void Set(Dictionary<int, List<int>> value)
    {
        accumulated_rewards = value;
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "accumulated_rewards" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] { GetStr(DataSet()) };
    }
    /// <summary>
    /// 写入数据
    /// </summary>
    /// <returns></returns>
    private string DataSet()
    {
        string value = "";
        foreach (int index in accumulated_rewards.Keys)
        {
            value += (value == "" ? "" : "|");
            value += index + ",";
            for (int i = 0; i < accumulated_rewards[index].Count; i++)
            {
                value+= (i == 0 ? "" : ";") + accumulated_rewards[index][i];
            }
        }
        return value;
    }
}
