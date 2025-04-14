using Common;
using Components;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Pet_explore : Panel_Base
{

    /// <summary>
    /// ̽����ť��
    /// </summary>
    private Button[] button_map;



    /// <summary>
    /// ���ѡ���̽��
    /// </summary>
    private string explore;

    /// <summary>
    /// ̽������
    /// </summary>
    private int IsExploring = 5;

    /// <summary>
    /// ��ťԤ����
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// ��Ʒ��ϢԤ����
    /// </summary>
    private material_item info_item_Prefabs;
    /// <summary>
    /// ̽�����︸����
    /// </summary>
    private Transform pos_btn;
    /// <summary>
    /// ̽������������
    /// </summary>
    private Transform pos_Items;
    /// <summary>
    /// ���ܰ���������
    /// </summary>
    private Transform function_pos_btn;
    /// <summary>
    /// ̽�������б�
    /// </summary>
    private string[] pet_btn_list = new string[] { "��", "��", "��" };
    /// <summary>
    /// ���ܰ����б�
    /// </summary>
    private string[] function_btn_list = new string[] { "�ջ�", "����", "̽��" };
    /// <summary>
    /// ����̽���ջ��б�
    /// </summary>
    private List<(string,int)> btn_item_list = new List<(string, int)>();


    public override void Show() 
    {
        base.Show();
        #region �����ʼ��
        button_map = Find<Transform>("explore_map/Buttons_map").GetComponentsInChildren<Button>();
        pos_btn = Find<Transform>("explore/pet_pos_btn");
        pos_Items = Find<Transform>("Income/Items");
        function_pos_btn= Find<Transform>("explore/function_pos_btn");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        info_item_Prefabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        #endregion

        #region �����ܰ�����ʼ��
        ///�����ͼ̽����ť��ʼ��
        for (int i = 0; i < button_map.Length; i++)
        {
            int index = i;
            button_map[i].onClick.AddListener(() => { Obtain_Explore(index); });
        }

        ///̽������button
        ClearObject(pos_btn);
        for (int i = 0; i < pet_btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, pos_btn);
            btn_item.Show(i, pet_btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { UpSetMaterial(btn_item); });
        }

        btn_item_list.Add(("��ʯ", 100));
        btn_item_list.Add(("������Ƭ", 100));
        ///̽������image
        ClearObject(pos_Items);
        for (int i = 0; i < btn_item_list.Count; i++)
        {
            material_item item = Instantiate(info_item_Prefabs, pos_Items);
            item.Init(btn_item_list[i]);
            //item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(item); });
        }

        ///���ܰ�����ʼ��
        ClearObject(function_pos_btn);
        for (int i = 0; i < function_btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, function_pos_btn);
            btn_item.Show(i, function_btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { FunctionButton(btn_item); });
        }
        Init();
        #endregion
    }






    /// <summary>
    /// ���������
    /// </summary>
    private void ClearObject(Transform pos_btn)
    {
        for (int i = pos_btn.childCount - 1; i >= 0; i--)//��������ڰ�ť
        {
            Destroy(pos_btn.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// ��ť���幦��
    /// </summary>
    /// <param name="btn_item"></param>
    private void FunctionButton(btn_item btn_item)
    {
        switch (btn_item.name)
        {
            case "�ջ�":
                break;
            case "����":
                break;
            case "̽��":
                break;
        }
    }


    /// <summary>
    /// ��ʾ����̽�������б�
    /// </summary>
    /// <param name="btn_item"></param>
    private void UpSetMaterial(btn_item btn_item)
    {
        Debug.Log("��ʾ����̽�������б�");
    }


    /// <summary>
    /// ��ʼ��̽���б�
    /// </summary>
    /// <param name="data"></param>
    public void Init()
    {
       for(int i=0;i< button_map.Length; i++)//�����ӵ�ͼ����
       {
            int r = Random.Range(0, SumSave.db_pet_explore.Count);
            button_map[i].GetComponentInChildren<Text>().text = SumSave.db_pet_explore[r].petExploreMapName;
       }
    }
    /// <summary>
    /// ���̽��
    /// </summary>
    private void Obtain_Explore(int index)
    {
        explore = button_map[index].GetComponentInChildren<Text>().text;//���̽����ͼ������

        if (IsExploring>=0 && SumSave.db_pet_explore_dic.TryGetValue(explore, out user_pet_explore_vo vo)) //�жϴ������Ҹ��������ҵ��õ�ͼ����Ϣ
        {
            string[] Explore_list = vo.petEvent_reward.Split("&");//��ȡ�õ�ͼ�Ľ����б�

            int r = 0;
            while(true)
            {
                r++;
                string[] data = Explore_list[Random.Range(0, Explore_list.Length)].Split(" ");//���ݿո��ֽ����б�
                if (data.Length == 3)//�жϽ�����ʽ
                {
                    string[] odds = data[2].Split("/");
                    if (Random.Range(0, int.Parse(odds[1])) < int.Parse(odds[0]))//�ж��Ƿ��ý���
                    {
                        GainRewards(data);
                        return;
                    }
                }

                if (r >= 1000)
                {
                    data = Explore_list[1].Split(" ");
                    GainRewards(data);
                    return;
                }
                  
            }
        }
        else Alert_Dec.Show("̽����������");
    }

    /// <summary>
    /// ��ý�����������Ϣ
    /// </summary>
    /// <param name="data"></param>
    private void GainRewards(string[] data)
    {
        int i=Random.Range(1, int.Parse(data[1])+1);//�����ý�������

        Battle_Tool.Obtain_Resources(data[0], i);//��ȡ����
        Alert_Dec.Show("̽������ " + data[0] + " x " + i);
        IsExploring--;
        Init();
    }
}
