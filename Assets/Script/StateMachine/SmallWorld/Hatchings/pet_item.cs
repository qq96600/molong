using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_item : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    private string data;

    public void Init(db_pet_vo db_pet_vo)
    {
        data = JsonUtility.ToJson(db_pet_vo);
    }
    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <returns></returns>
    public string Set()
    {
        return data;
    }
}
