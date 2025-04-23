using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using UnityEngine.UI;

public class info_item : Base_Mono
{
    private Text base_type, base_value;
    private void Awake()
    {
        base_value = Find<Text>("base_value");
        base_type = Find<Text>("base_type");
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="value">值</param>
    public void Show(object type, object value)
    { 
        base_type.text = type.ToString();
        base_value.text = value.ToString();
    }
}
