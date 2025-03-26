using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setting_item : MonoBehaviour
{
    private Dropdown dropdown;

    private int index;
    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(OnValueChange);
    }


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="arg0"></param>
    private void OnValueChange(int arg0)
    {
         transform.parent.parent.parent.parent.parent.SendMessage("Select_Setting",(index,arg0));
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="type">����</param>
    /// <param name="list">�б�</param>
    /// <param name="index">��ǰѡ��</param>
    public void Init(int type,string[] list,int value)
    {
        index = type;

        for (int i = 0; i < list.Length; i++)
        {
            dropdown.AddOptions(new List<string>() { list[i] });
        }
        dropdown.value = value;
    }
}
