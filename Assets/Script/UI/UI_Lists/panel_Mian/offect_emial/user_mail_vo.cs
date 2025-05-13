using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_mail_vo : Base_VO
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
    public string mail_recipient;

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(mail_par),
            GetStr(mail_recipient),
            GetStr(user_value)
        };
    }

}
