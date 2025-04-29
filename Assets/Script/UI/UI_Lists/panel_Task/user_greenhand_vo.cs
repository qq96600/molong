using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_greenhand_vo : Base_VO
{
    public string crt_task;
    public int[] task_list;

    public void Init()
    {
        string[] parts = user_value?.Split(',') ?? Array.Empty<string>();
        task_list = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        { 
            task_list[i] = int.Parse(parts[i]);
        }
        //task_list = string.Joint(" ", crt_task);
    }

    public override string[] Set_Instace_String()
    {
        return new string[]

        {
        GetStr(0),
        GetStr(SumSave.crt_user.uid),
        GetStr(crt_task),
        GetStr(user_value)
        };
    }
}
