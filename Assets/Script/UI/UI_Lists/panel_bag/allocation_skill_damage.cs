using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allocation_skill_damage : Base_Mono
{
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private Text info;
    /// <summary>
    /// ȷ��
    /// </summary>
    private Button confirm_btn;
    /// <summary>
    /// ��������
    /// </summary>
    private Slider slider;
    /// <summary>
    /// ��ǰ���似��
    /// </summary>
    private base_skill_vo user_skill;

    private Button close;
    private void Awake()
    {
        close = Find<Button>("close_button");
        close.onClick.AddListener(() => { gameObject.SetActive(false); });
        info = Find<Text>("base_info");
        confirm_btn = Find<Button>("confirm_btn");
        slider = Find<Slider>("Slider");
        confirm_btn.onClick.AddListener(Confirm);
        slider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnSliderChange(float arg0)
    {
        info.text = Show_Color.Red(user_skill.skillname)+ "�������� " + Show_Color.Yellow(arg0);
    }

    /// <summary>
    /// ȷ�Ϸ���
    /// </summary>
    private void Confirm()
    {
        user_skill.user_values[3]= ((int)slider.value).ToString();
        user_skill.user_value = ArrayHelper.Data_Encryption(user_skill.user_values);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.skill_value, SumSave.crt_skills);
        SendNotification(NotiList.Refresh_Max_Hero_Attribute);
    }

    public void Show(base_skill_vo skill)
    {
        user_skill= skill;
        info.text = "��������������" + skill.skillname;
        slider.value = 0;
        slider.maxValue = SumSave.crt_MaxHero.internalforceMP;
    }
}
