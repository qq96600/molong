using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class task_item : Base_Mono
{
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private Text task_info;
    /// <summary>
    /// ״̬
    /// </summary>
    private Image state;
    /// <summary>
    /// ���
    /// </summary>
    public int index;
    private void Awake()
    {
        task_info=Find<Text>("info");
        state = Find<Image>("state");
    }

    public void Show(int i,int value, bool isFinish=false)
    {
        index = i;
        string dec = "";
        switch (index)
        {
            case 0:
                dec += Show_Color.Green("����120����") + "\n" + "����:" + Show_Color.Blue(value + "/120")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 1:
                dec += Show_Color.Green("����С������ֲ") + "\n" + "����:" + Show_Color.Blue(value + "/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 2:
                dec += Show_Color.Green("��ɱ10����ͨBoss") + "\n" + "����:" + Show_Color.Blue(value + "/10")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 3:
                dec += Show_Color.Green("��ɱ10������Boss") + "\n" + "����:" + Show_Color.Blue(value + "/10")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 4:
                dec += Show_Color.Green("��ɱ1����ԨBoss") + "\n" + "����:" + Show_Color.Blue(value + "/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 5:
                dec += Show_Color.Green("���Ƶ�ҩ") + "\n" + "����:" + Show_Color.Blue(value + "/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            default:
                break;
        }
        task_info.text = dec;
        state.gameObject.SetActive(isFinish);
    }

    public void progress(int value,bool isFinish)
    {
        string dec = "";
        switch (index)
        {
            case 0:
                dec += Show_Color.Green("����120����") + "\n" + "����:" + Show_Color.Blue( value+"/120")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 1:
                dec += Show_Color.Green("����С������ֲ") + "\n" + "����:" + Show_Color.Blue(value+"/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 2:
                dec += Show_Color.Green("��ɱ10����ͨBoss") + "\n" + "����:" + Show_Color.Blue(value+"/10")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 3:
                dec += Show_Color.Green("��ɱ10������Boss") + "\n" + "����:" + Show_Color.Blue(value+"/10")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 4:
                dec += Show_Color.Green("��ɱ1����ԨBoss") + "\n" + "����:" + Show_Color.Blue(value+"/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            case 5:
                dec += Show_Color.Green("���Ƶ�ҩ") + "\n" + "����:" + Show_Color.Blue(value+"/1")
                    + "\n" + "����:" + Show_Color.Red("���˽�� * 1");
                break;
            default:
                break;
        }
        task_info.text = dec;
        state.gameObject.SetActive(isFinish);
    }
    /// <summary>
    /// �Ƿ����
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool State(int value)
    {
        int MaxValue = 0;
        switch (index)
        {
            case 0:
                MaxValue = 120;
                break;
            case 1:
                MaxValue = 1;
                break;
            case 2:
                MaxValue = 10;
                break;
            case 3:

                MaxValue = 10;
                break;
            case 4:
                MaxValue = 1;
                break;
            case 5:
                MaxValue = 1;
                break;
            default:
                break;
        }
        return value >= MaxValue;

    }
}
