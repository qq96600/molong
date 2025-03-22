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
    /// ��ʾ��Ϣ
    /// </summary>
    /// <param name="type">����</param>
    /// <param name="value">ֵ</param>
    public void Show(object type, object value)
    { 
        base_type.text = type.ToString();
        base_value.text = value.ToString();
    }
}
