using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copies_item : Base_Mono
{
    /// <summary>
    /// ��ʾͼƬ
    /// </summary>
    private Image icon;
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private Text info;

    public int index;
    private void Awake()
    {
        icon=Find<Image>("bg/icon");
        info=Find<Text>("info");
    }

    public void Init(int _index)
    {
        index = _index;
        info.text = "��" + index + "��";
    }
}
