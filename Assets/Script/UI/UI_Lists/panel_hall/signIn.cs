using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;
using System;
using Components;

public class signIn : Base_Mono
{
    /// <summary>
    /// ǩ���б�
    /// </summary>
    private Transform pos_list;
    /// <summary>
    /// ǩ���б�
    /// </summary>
    private signln_item signln_item_prafabs;
    /// <summary>
    /// ǩ����Ϣ
    /// </summary>
    private Text info;
    /// <summary>
    /// ǩ����ť
    /// </summary>
    private Button btn_signln;
    private void Awake()
    {
        pos_list=Find<Transform>("Scroll View/Viewport/Content");
        signln_item_prafabs = Resources.Load<signln_item>("prefabs/panel_hall/signIn/signln_item");
        info = Find<Text>("bg/info");
        btn_signln = Find<Button>("btn_signln");
        btn_signln.onClick.AddListener(OnClick_signln);
        ClearObject(pos_list);
        List<int> list = SumSave.crt_signin.Set();
        for (int i = 0; i < SumSave.db_Signins.Count; i++)
        {
            signln_item item = Instantiate(signln_item_prafabs, pos_list);
            while (i >= list.Count)
            {
                list.Add(0);
            }
            item.Init(i, SumSave.db_Signins[i], list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClick(item); });
        }
    }
    /// <summary>
    /// ǩ��
    /// </summary>
    private void OnClick_signln()
    {
        if ((SumSave.nowtime - SumSave.crt_signin.now_time).Days >= 1)
        {
            SumSave.crt_user_unit.verify_data(currency_unit.����, 1000000);
            SumSave.crt_signin.now_time = Convert.ToDateTime(SumSave.nowtime.ToString("yyyy-MM-dd"));
            SumSave.crt_signin.number++;
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_signin, SumSave.crt_signin.Set_Uptade_String(),
                SumSave.crt_signin.Get_Update_Character());
            Alert_Dec.Show("ǩ���ɹ�");
            base_show();
        }
        else Alert_Dec.Show("������ǩ��");
    }

    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="item"></param>
    private void OnClick(signln_item item)
    {
        int index = item.Set();
        List<int> list = SumSave.crt_signin.Set();
        if (index<= list.Count && list[index] == 1)
        {
            Alert_Dec.Show("����ȡ����");
            return;
        } 
        db_signin_vo vo = SumSave.db_Signins[index];
        if (SumSave.crt_signin.number < vo.index)
        {
            Alert_Dec.Show("δ������ȡ����") ;
            return;
        }
        string[] strs = vo.value.Split('*');
        string dec = strs[0] + "*" + strs[1];
        Alert.Show("��ȡ����", "ȷ���Ƿ���ȡ" + dec, Confirmreceipt, item);
    }
    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="arg0"></param>
    private void Confirmreceipt(object arg0)
    {
        signln_item item = arg0 as signln_item;
        int index = item.Set();
        Battle_Tool.Obtain_result(SumSave.db_Signins[index].value);
        Alert_Dec.Show("��ȡ�ɹ�");
        SumSave.crt_signin.Set(index);
        item.Init(index, SumSave.db_Signins[index], 1);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_signin, SumSave.crt_signin.Set_Uptade_String(),
    SumSave.crt_signin.Get_Update_Character());
    }

    public override void Show()
    {
        base.Show();
        base_show();
    }
    /// <summary>
    /// ��ʾ������Ϣ
    /// </summary>
    private void base_show()
    {
        string dec = "ǩ������";
        dec += "\n�ۻ�ǩ������ " + "* " + SumSave.crt_signin.number + " ��";
        dec += "\nǩ�����" + currency_unit.���� + "* " + 1000000;
        info.text = dec;
    }
}
