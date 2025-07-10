using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_tianmingTai : Base_Mono
{
    /// <summary>
    /// ������ʾλ��
    /// </summary>
    private Transform tianming_image;
    /// <summary>
    /// ������Ϣ��ʾλ��
    /// </summary>
    private Text tianming_Title, title_button;
    /// <summary>
    /// ˢ��������ť
    /// </summary>
    private Button UpButton;
    /// <summary>
    /// ����Ԥ����
    /// </summary>
    private GameObject tianming_item;
    /// <summary>
    /// ˢ������ħ������
    /// </summary>
    private int need = 20;
    
    private void Awake()
    {
        tianming_image = Find<Transform>("tianming_image/Viewport/Content");
        tianming_Title = Find<Text>("tianming_Title/Text");
        UpButton = Find<Button>("UpButton");
        UpButton.onClick.AddListener(UpButtonOnClick);
        title_button= Find<Text>("UpButton/title_button");
        title_button.text = "ħ��*" + need + "ˢ��һ��";
    }

    /// <summary>
    /// ˢ������̨��ʾ
    /// </summary>
    private void RefreshDisplay()
    {
        ClearObject(tianming_image);
        string str = "";
        str = "�������ԣ�";
        for (int i = 0; i < SumSave.crt_hero.tianming_Platform.Length; i++)
        {
            GameObject game = Resources.Load<GameObject>("Prefabs/halo/halo_" + SumSave.crt_hero.tianming_Platform[i]);
            Instantiate(game, tianming_image);
            str += SumSave.five_element_type[SumSave.crt_hero.tianming_Platform[i]-1]+" ";
        }
        tianming_Title.text = str;
    }

    /// <summary>
    /// ˢ����������
    /// </summary>
    private void UpButtonOnClick()
    {
        NeedConsumables(currency_unit.ħ��, need);
        if (RefreshConsumables())
        {
            SumSave.crt_hero.RefreshTianming();
            RefreshDisplay();
        }else
        {
            Alert_Dec.Show("ħ�費��");
        }
            
    }

    public override void Show()
    {
        base.Show();
        RefreshDisplay();
    }
}
