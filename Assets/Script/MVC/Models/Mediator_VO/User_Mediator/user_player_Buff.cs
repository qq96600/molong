using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class user_player_Buff : Base_VO
{
    /// <summary>
    /// 玩家buff
    /// </summary>
    public string player_baff="";

    /// <summary>
    /// 玩家buff 1，buff名字 2，buff开始时间 3，buff持续时间
    /// </summary>
    //public  List<(string, DateTime,int)> player_Buffs = new List<(string, DateTime, int)>();
    public Dictionary<string ,(DateTime,int,float)> player_Buffs= new Dictionary<string, (DateTime, int,float)>();
    /// <summary>
    /// 整合Buff
    /// </summary>
    public string IntegrationBuff()
    {
        List<(string, DateTime, int,float)>buff= player_Buffs.Keys.Select(x => (x, player_Buffs[x].Item1, player_Buffs[x].Item2, player_Buffs[x].Item3)).ToList();
        player_baff = "";
        for (int i = 0; i < buff.Count; i++)
        {
            if(player_baff !="")
            {
                player_baff += "&";
            }
            player_baff += buff[i].Item1 + "," + buff[i].Item2.ToString("yyyy-MM-dd HH:mm:ss") + "," + buff[i].Item3+","+buff[i].Item4;
        }
        return player_baff;
    }


    public void SplitBuff()
    {
        string[] buff = player_baff.Split('&');
        
        for (int i = 0; i < buff.Length; i++)
        {
            if(buff[i] != "")
            {
                string[] buff_info = buff[i].Split(',');
                player_Buffs.Add(buff_info[0], (DateTime.Parse(buff_info[1]), int.Parse(buff_info[2]), int.Parse(buff_info[3])));
            }
           
        }

    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(player_baff),
        };

    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "player_Buff" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
         {
             GetStr(IntegrationBuff()),
         };
    }
}
