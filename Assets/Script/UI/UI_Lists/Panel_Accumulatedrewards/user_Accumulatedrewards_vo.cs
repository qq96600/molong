using Common;
using MVC;
using System.Collections.Generic;

public class user_Accumulatedrewards_vo : Base_VO
{ 
    /// <summary>
    /// 0通行证累积1签到累积2充值累积奖励
    /// </summary>
    private Dictionary<int,List<int>> accumulated_rewards;
    /// <summary>
    /// 真实充值
    /// </summary>
    private int Real_recharge;
    /// <summary>
    /// 累积充值
    /// </summary>
    private int sum_recharge;
    public void Init(int Realrecharge,int sumrecharge)
    {
        Real_recharge= Realrecharge;
        sum_recharge = sumrecharge;
        accumulated_rewards = new Dictionary<int, List<int>>();
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

    /// <summary>
    /// 取出累计充值的值 0等级 1经验 2名字
    /// </summary>
    /// <returns></returns>
    public List<string> SetSum_recharge()
    {
        List<string> list = new List<string>();
        for (int i = SumSave.db_vip_list.Count; i > 0; i--)//实例化vip信息
        {
            if (sum_recharge >= SumSave.db_vip_list[i-1].vip_exp)
            {
               list.Add(SumSave.db_vip_list[i-1].vip_lv.ToString());
               list.Add(sum_recharge.ToString());
               list.Add(SumSave.db_vip_list[i-1].vip_name);
               return list;
            }
        }
        return list;
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="index">1真实2累积</param>
    /// <returns></returns>
    public int Set(int index)
    {
        int value = 0;
        switch (index)
        {
            case 1:value= Real_recharge; break;
            case 2:value = sum_recharge; break;
            default:break;
        }
        return value;
    }
    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="index">1真实2累积</param>
    /// <param name="value"></param>
    public void Set(int index,int value)
    {
        switch (index)
        {
            case 1: 
                Real_recharge += value;
                sum_recharge += value;
                break;
            case 2: sum_recharge += value; break;
            default: break;
        }
        MysqlData();
    }

    public override void MysqlData()
    {
        base.MysqlData();
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_rewards_state,
SumSave.crt_accumulatedrewards.Set_Uptade_String(), SumSave.crt_accumulatedrewards.Get_Update_Character());
    }
    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(DataSet()),
            GetStr(Real_recharge),
            GetStr(sum_recharge)
        };
    }
    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="value"></param>
    public void Set(Dictionary<int, List<int>> value)
    {
        accumulated_rewards = value;
        MysqlData();
    }

    public override string[] Get_Update_Character()
    {

        return new string[] { "Real_recharge", "sum_recharge", "accumulated_rewards" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] 
        {
            GetStr(Real_recharge),
            GetStr(sum_recharge),
            GetStr(DataSet()) };
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
