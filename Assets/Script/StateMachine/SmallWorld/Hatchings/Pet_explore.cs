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
    /// �����Լ�����
    /// </summary>
    private List<(string, int)> Explore_list = new List<(string, int)>();

    /// <summary>
    /// ���ѡ���̽��
    /// </summary>
    private string explore;

    /// <summary>
    /// �Ƿ����̽��
    /// </summary>
    private bool IsExploring = true;

    protected override void Awake()
    {
        #region ��ť��ʼ��
        button_map= transform.Find("Buttons_map").GetComponentsInChildren<Button>();

        for (int i = 0; i < button_map.Length; i++)
        {
            int index = i;
            button_map[i].onClick.AddListener(() => { Obtain_Explore(index); });
        }
        Init();
        #endregion
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
        explore = button_map[index].GetComponentInChildren<Text>().text;

        if (IsExploring && SumSave.db_pet_explore_dic.TryGetValue(explore, out user_pet_explore_vo vo)) //�ж��Ƿ����̽���������ֵ����ҵ��õ�ͼ����Ϣ
        {
            Explore_list = vo.petExploreReward;//��ȡ�õ�ͼ�Ľ����б�

            (string, int) data= Explore_list[Random.Range(0, Explore_list.Count)];//�����ȡ����

            Battle_Tool.Obtain_Resources(data.Item1, data.Item2);
            //Explore_list.Remove(data);
            Alert_Dec.Show("̽������ " + data.Item1 + " x " + data.Item2);
            Init();
        }
        else Alert_Dec.Show("̽����������");


    }
}
