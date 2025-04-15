using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public enum Achieve_Type//�ɾ�����ö��
{
    ��ɱϵ�� = 1,
    �ռ�ϵ��,
    װ��ϵ��,
    ����ϵ��,
    ����ϵ��,
}

public class plant_achievement : Base_Mono
{
    /// <summary>
    /// ��ʾ����ֵ
    /// </summary>
    private Image show_offect;
    /// <summary>
    /// ��ʾ�ɾ�����
    /// </summary>
    private Text show_name, show_info;
    /// <summary>
    /// ��ȡ������ť
    /// </summary>
    private Button btn_receive;
   

    /// <summary>
    /// �ɾ�λ��
    /// </summary>
    public Transform crt;
    /// <summary>
    /// ����ɾ�
    /// </summary>
    public ach_item Achieve_Item_Prefab;
    /// <summary>
    /// ��ǰ�ɾ�
    /// </summary>
    private ach_item crt_achieve_Item;
    /// <summary>
    /// �ɾ�����Ԥ����
    /// </summary>
    public btn_item Achieve_Type_Prefab;
    /// <summary>
    /// ��ǰѡ��ɾ�����
    /// </summary>
    private btn_item crt_type;
    /// <summary>
    /// �洢�ֵ�
    /// </summary>
    private new Dictionary<btn_item, List<ach_item>> dic = new Dictionary<btn_item, List<ach_item>>();
    /// <summary>
    /// ������ֵ
    /// </summary>
    private Dictionary<string, int> dic_exp = new Dictionary<string, int>();
    private Dictionary<string, Dictionary<int, int>> dic_exchange = new Dictionary<string, Dictionary<int, int>>();
    /// <summary>
    /// ����ȼ�
    /// </summary>
    private Dictionary<string, int> dic_lv = new Dictionary<string, int>();
    
    /// <summary>
    /// �رհ�ť
    /// </summary>
    private Button close;



    private void Awake()
    {
        Achieve_Item_Prefab = Resources.Load<ach_item>("Prefabs/base_tool/ach_item"); 
        Achieve_Type_Prefab = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        crt= Find<Transform>("achievementList/Viewport/Content");
        show_offect= Find<Image>("show_offect");
        show_name= Find<Text>("show_offect/show_name/info");
        show_info=Find<Text>("show_offect/base_info/Viewport/Content/info");
        btn_receive = Find<Button>("show_offect/btn_list/btn_item");
        btn_receive.onClick.AddListener(() => { Receive(); });
        close= Find<Button>("close");
        close.onClick.AddListener(() => { Close(); });
        Insace_Base_Info();
    }

    /// <summary>
    /// �ɾ���Ϣ�������������ر�
    /// </summary>
    private void Close()
    {
        show_offect.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
    }

    /// <summary>
    /// �����ť��ȡ����
    /// </summary>
    private void Receive()
    {
        int lv = dic_lv[crt_achieve_Item.Data.achievement_value];
        if (true)
        {
            if (lv >= crt_achieve_Item.Data.achievement_needs.Count)
            {
                Alert_Dec.Show("��ǰ�׶�����");
                return;
            }
            if (crt_achieve_Item.Data.achievement_needs[lv] > dic_exp[crt_achieve_Item.Data.achievement_value])
            {
                Alert_Dec.Show("δ�ﵽ��ȡ����");
                return;
            }
        }
        dic_lv[crt_achieve_Item.Data.achievement_value]++;
        string[] temp = crt_achieve_Item.Data.achievement_rewards[lv].Split(' ');
        Battle_Tool.Obtain_Resources(temp[1], int .Parse( temp[2]));
        Alert_Dec.Show("��ȡ�ɹ�");
        show_offect.gameObject.SetActive(false);
        crt_achieve_Item.Init(); 
    }






    /// <summary>
    /// ��ʼ���ɾ��б�
    /// </summary>
    private void Insace_Base_Info()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Achieve_Type)).Length; i++)//����ö�ٴ����ɾ�����
        {
            btn_item temp = Instantiate(Achieve_Type_Prefab, crt);//ʵ����
            temp.Show(i,(Achieve_Type)(i + 1));//��ʾ������Ϣ
            for (int j = 0; j < SumSave.db_Achievement_dic.Count; j++)//�ɾ����ݿ�
            {
                if (SumSave.db_Achievement_dic[j].achievement_type == i + 1)//�ж��Ƿ��ڵ�ǰ����
                {
                    ach_item item = Instantiate(Achieve_Item_Prefab, crt);//ʵ��������ɾ�
                    item.Data = SumSave.db_Achievement_dic[j];//��ȡ�ɾ���Ϣ
                    //��ȡ����
                    if (!dic.ContainsKey(temp)) dic.Add(temp, new List<ach_item>());//�ж��ֵ����Ƿ��Ѿ�����
                    dic[temp].Add(item);
                    item.GetComponent<Button>().onClick.AddListener(() => { Select_Achieve(item); });//����ɾ͵���¼�
                }
            }
            temp.GetComponent<Button>().onClick.AddListener(() => { Select_Type(temp); });//�ɾ����͵���¼�
        }
        foreach (btn_item item in dic.Keys)
        {
            foreach (ach_item crt_item in dic[item])//�ر����Ծ���ɾ�
            {
                crt_item.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ��ȡ�ɾ�
    /// </summary>
    /// <param name="item"></param>
    private void Select_Achieve(ach_item item)
    {
        show_offect.gameObject.SetActive(true);
        close.gameObject.SetActive(true);
        crt_achieve_Item = item;
        show_name.text = item.Data.achievement_value;
        Show_Dec();
    }
    /// <summary>
    /// ��ʾ�ɾ�����
    /// </summary>
    private void Show_Dec()
    {
        string dec = (Achieve_Type)crt_achieve_Item.Data.achievement_type + " " + crt_achieve_Item.Data.achievement_value;
        dic_exp = SumSave.crt_achievement.Set_Exp();
        dic_lv = SumSave.crt_achievement.Set_Lv();
        if (dic_lv[crt_achieve_Item.Data.achievement_value] > 0)
        {
            int max = (int)MathF.Min(dic_lv[crt_achieve_Item.Data.achievement_value], crt_achieve_Item.Data.achievement_needs.Count);
            for (int i = 0; i < max; i++)
            {
                dec += "\n" + Show_Color.Green(InSetInfo(i) + "(����ȡ)");
            }
        }
        if (dic_lv[crt_achieve_Item.Data.achievement_value] < crt_achieve_Item.Data.achievement_needs.Count)
        {
            int number = dic_lv[crt_achieve_Item.Data.achievement_value];
            dec += "\nLv" + (number + 1) + ".�ɾͽ׶� " + dic_exp[crt_achieve_Item.Data.achievement_value] + "/" + crt_achieve_Item.Data.achievement_needs[number];
            dec += "\n���� " + Show_Color.Yellow(InSetInfo(number));
        }
        show_info.text = dec;
    }
    /// <summary>
    /// ѡ��ɾ�����
    /// </summary>
    /// <param name="temp"></param>
    private void Select_Type(btn_item temp)
    {
        if (crt_type != null)
        {
            if (crt_type == temp)//�ж��Ƿ�Ϊ��ǰѡ������
            {
                if (!temp.Active())//�ж��Ƿ񼤻�
                {
                    crt_type.Selected = true;
                    foreach (ach_item item in dic[crt_type])
                    {
                        item.gameObject.SetActive(crt_type.Active());//�ж��Ƿ���Ҫ����
                        item.Init();//��ʼ��
                    }

                }
                else
                {
                    crt_type.Selected = false;
                    foreach (ach_item item in dic[crt_type])
                    {
                        item.gameObject.SetActive(crt_type.Active());
                    }
                }
                return;
            }

            crt_type.Selected = false;
            foreach (ach_item item in dic[crt_type])//���ǵ�ǰѡ�����͹ر�
            {
                item.gameObject.SetActive(crt_type.Active());
            }
        }
        crt_type = temp;//���µ�ǰѡ������
        crt_type.Selected = true;//ѡ��״̬
        foreach (ach_item item in dic[crt_type])//�򿪲��ҳ�ʼ��item
        {
            item.gameObject.SetActive(crt_type.Active());
            item.Init();
        }
    }
    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="i"></param>
    private string InSetInfo(int i)
    {
        string dec = "";
        //string[] temp = crt_achieve_Item.Data.achieve_rewards[i].Split(' ');
        //if (temp.Length < 1) return "";
        ////dec += (Achieve_Rewards_Type)int.Parse(temp[0]) + " ";
        //switch ((Achieve_Rewards_Type)int.Parse(temp[0]))
        //{
        //    case Achieve_Rewards_Type.Boss��: dec += "Boss�� +" + temp[1]; break;
        //    case Achieve_Rewards_Type.װ��: dec += "װ�� +" + temp[1]; break;
        //    case Achieve_Rewards_Type.���: dec += "��� +" + temp[1] + "w"; break;
        //    case Achieve_Rewards_Type.����: dec += temp[1] + " * " + temp[2]; break;
        //    case Achieve_Rewards_Type.�˺�����: dec += (temp[1] == "1" ? "���������˺�" : "����ħ���˺�") + " + " + temp[2] + "%"; break;
        //    case Achieve_Rewards_Type.����: dec += (temp[1] == "1" ? "����" : "ħ��") + " + " + temp[2]; break;
        //    case Achieve_Rewards_Type.Ԫ�ؼӳ�:
        //        {
        //            if (temp[1] == "1") dec += "�����˺� + " + temp[2] + "%";
        //            else if (temp[1] == "2") dec += "������ + " + temp[2] + "%";
        //            else if (temp[1] == "3") dec += "�����˺� + " + temp[2] + "%";
        //            else if (temp[1] == "4") dec += "�����ٶ� + " + temp[2] + "";
        //            else if (temp[1] == "5") dec += "���� + " + temp[2] + "";
        //        }
        //        break;
        //    case Achieve_Rewards_Type.ս��:
        //        if (temp[1] == "1") dec += "������� + " + temp[2] + "";
        //        else if (temp[1] == "2") dec += "ħ������ + " + temp[2] + "";
        //        else if (temp[1] == "3") dec += "�﹥ + " + temp[2] + "";
        //        else if (temp[1] == "4") dec += "ħ�� + " + temp[2] + "";
        //        else if (temp[1] == "5") dec += "���� + " + temp[2] + "";

        //        break;
        //    case Achieve_Rewards_Type.�쳣����:
        //        if (temp[1] == "1") dec += "�����ֿ� + " + temp[2] + "%";
        //        else if (temp[1] == "2") dec += "���˵ֿ� + " + temp[2] + "%";
        //        break;

        //    case Achieve_Rewards_Type.ף��:
        //        if (temp[1] == "1") dec += "���� + " + temp[2] + "";
        //        break;
        //    case Achieve_Rewards_Type.�ظ�����:
        //        if (temp[1] == "1") dec += "�����ظ� + " + temp[2] + "";
        //        else if (temp[1] == "2") dec += "ħ���ظ� + " + temp[2] + "";

        //        break;
        //    case Achieve_Rewards_Type.���ܼӳ�:
        //        dec += (Skill_List)(int.Parse(temp[1])) + " ����Ч�� + " + temp[2] + "%";
        //        break;

        //    case Achieve_Rewards_Type.����ӳ�:
        //        dec += (Skill_List)(int.Parse(temp[1])) + " �����˺� + " + temp[2] + "";
        //        break;

        //}

        return dec;
    }

}
