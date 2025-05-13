using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_mail_vo : Base_VO
{
    /// <summary>
    /// 邮件id
    /// </summary>
    public int mail_id;
    /// <summary>
    /// 几区
    /// </summary>
    public int mail_par;
    /// <summary>
    /// 接受人 -1代表全区 uid代表个人
    /// </summary>
    public string uid;
    /// <summary>
    /// 获取时间
    /// </summary>
    public DateTime mail_time;
    /// <summary>
    /// 邮件内容
    /// </summary>
    public string dec;

    public Dictionary<int, Dictionary<string, int>> mail_dec = new Dictionary<int, Dictionary<string, int>>();
    public void Init()
    {
        string[] mail_dec_str = user_value.Split(',');
        foreach (var item in mail_dec_str)
        { 
            string[] mail_dec_str2 = item.Split(' ');
            if (!mail_dec.ContainsKey(int.Parse(mail_dec_str2[0])))
            { 
                mail_dec.Add(int.Parse(mail_dec_str2[0]), new Dictionary<string, int>());
            }
            mail_dec[int.Parse(mail_dec_str2[0])].Add(mail_dec_str2[1], int.Parse(mail_dec_str2[2]));
        }
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(mail_time),
            GetStr(mail_par),
            GetStr(uid),
            GetStr(user_value),
            GetStr(dec)
        };
    }

}
