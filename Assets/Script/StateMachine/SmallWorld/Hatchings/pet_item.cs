using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pet_item : Base_Mono
{
    /// <summary>
    /// 宠物数据
    /// </summary>
    private string data;

    private db_pet_vo crt_pet;

    private Text text;
    private void Awake()
    {
        text = Find<Text>("info");
    }

    public void Init(string db_pet_vo)
    {
        data = (db_pet_vo);
        text.text ="";
        string[] va = data.Split(',');
        text.text = va[0]+"lv:"+ va[3];
    }
    public void Init(db_pet_vo data)
    {
        crt_pet = (data);
        text.text = crt_pet.petName + "lv:" + crt_pet.level;
    }
    /// <summary>
    /// 获取宠物数据
    /// </summary>
    /// <returns></returns>
    public string Set()
    {
        return data;
    }
    public db_pet_vo SetPet()
    {
        return crt_pet;
    }
}
