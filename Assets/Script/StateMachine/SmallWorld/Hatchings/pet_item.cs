using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_item : MonoBehaviour
{
    /// <summary>
    /// 宠物数据
    /// </summary>
    private string data;

    public void Init(db_pet_vo db_pet_vo)
    {
        data = JsonUtility.ToJson(db_pet_vo);
    }
    /// <summary>
    /// 获取宠物数据
    /// </summary>
    /// <returns></returns>
    public string Set()
    {
        return data;
    }
}
