using Common;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_vip : Panel_Base
{
    /// <summary>
    /// VIP��Ϣ��ʾλ��
    /// </summary>
    private Transform Information;
    /// <summary>
    /// vip��ϢԤ����
    /// </summary>
    private vip_effect vip_effect_obj;
    /// <summary>
    /// vip��Ϣ����
    /// </summary>
    private List<string> Title_list = new List<string>() { "VIP�ȼ�", "��������", "����ӳ�", "��������", "װ������", "��������", "Ѱ�ּ��", "�����ظ�", "�����ظ�", "����", "ǿ������", "���߼��", "ǩ������", "��ʬ", "��������"};
    /// <summary>
    /// vip�ȼ���ʾλ��
    /// </summary>
    private Text show_vip_lv_text;
    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        Information=Find<Transform>("Information/Viewport/Content");
        vip_effect_obj = Battle_Tool.Find_Prefabs<vip_effect>("vip_effect");
        show_vip_lv_text= Find<Text>("show_vip_lv/Text");
        Show_Vip_Info();
    }

    /// <summary>
    /// ��ʾvip��Ϣ
    /// </summary>
    public void Show_Vip_Info()
    {
        ClearObject(Information);
       
        if(SumSave.crt_hero.hero_vip_lv_exp=="")
        {
            SumSave.crt_hero.hero_vip_lv_exp= "0 0";
        }
        string[] lv_exp = SumSave.crt_hero.hero_vip_lv_exp.Split(' ');
        show_vip_lv_text.text.Clone();
        show_vip_lv_text.text = "VIP�ȼ�:" + lv_exp[0]+" ����:"+lv_exp[1];//��ʾvip�ȼ��;���

        vip_effect Title = Instantiate(vip_effect_obj, Information);//ʵ����vip��Ϣ����
        Title.Init(-1, Title_list);

        for (int i = 0; i < SumSave.db_vip_list.Count; i++)//ʵ����vip��Ϣ
        {
            vip_effect vip_effect = Instantiate(vip_effect_obj, Information);
            vip_effect.Init(i, SumSave.db_vip_list[i]);
        }
    }



    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
}
