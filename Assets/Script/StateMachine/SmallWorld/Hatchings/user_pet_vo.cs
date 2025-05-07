using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_pet_vo : Base_VO
{
    /// <summary>
    /// 已孵化宠物信息
    /// </summary>
    public string pet_value;
   
    /// <summary>
    /// 宠物信息 0宠物名字 1孵化时间  2宠物品质 3宠物等级 4宠物经验 5宠物属性 6 pos 他在干什么0闲置1守护庄园2是探索
    /// </summary>
    public List<string> crt_pet_list = new List<string>();




    public void DataSet()
    {
        string value = "";
        pet_value = "";
        foreach (var pet in SumSave.crt_pet_list)
        {
            value += value == "" ? "" : "&";
            value += pet.petName + ",";
            value += pet.startHatchingTime + ",";
            value += pet.quality + ",";
            value += pet.level + ",";
            value += pet.exp + ",";
            value += pet.crate_value + "|" + pet.up_value + "|" + pet.up_base_value + ",";
            value += pet.pet_state;
        }
        pet_value = value;
    }


    public void Init()
    {
        crt_pet_list= new List<string>();
        string[] pet = pet_value.Split('&');
        for (int i = 0; i < pet.Length; i++)
        {
            crt_pet_list.Add(pet[i]);
        }
    }


    public string Set_pet_value()
    {
        string dec = "";
        for(int i = 0; i < crt_pet_list.Count; i++)
        {
            if(i>0)
            {
                dec += "&";
            }
            dec += crt_pet_list[i];
        }

        return dec;

    }


    public override string[] Set_Instace_String()
    {
        return new string[]
        {
           GetStr(0),
           GetStr(SumSave.crt_user.uid),
           GetStr(pet_value),
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "user_value",
        };
    }


    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
            GetStr(Set_pet_value()),
        };
    }

}
