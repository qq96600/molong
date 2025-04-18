using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_pass : Base_Mono
{
    /// <summary>
    /// ͨ��֤�б�
    /// </summary>
    private List<string> btn_list = new List<string>() { "S1", "S2" }; 


    private Transform pos_btn, pos_item,pos_task;

    private btn_item btn_itm_prefabs;

    private pass_item pass_item_prefabs;

    private task_item task_item_prefabs;
    /// <summary>
    /// ��ʾ�����б�
    /// </summary>
    private Dictionary<int, task_item> dic_task = new Dictionary<int, task_item>();
    /// <summary>
    /// ��ʾ�ۻ�������Ϣ
    /// </summary>
    private Text task_info;
    /// <summary>
    /// �ڼ���ͨ��֤
    /// </summary>
    private int index=0;

    protected void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        pos_btn = Find<Transform>("bg_main/btn_list");
        pos_item = Find<Transform>("bg_main/Scroll View/Viewport/Content");
        pos_task = Find<Transform>("bg_main/task/task_list");
        task_info = Find<Text>("bg_main/task/info/info");
        btn_itm_prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
        pass_item_prefabs = Resources.Load<pass_item>("Prefabs/panel_hall/panel_pass/pass_item"); 
        task_item_prefabs = Resources.Load<task_item>("Prefabs/panel_hall/panel_pass/task_item");
        for (int i = 0; i < btn_list.Count; i++)
        {
            btn_item btn = Instantiate(btn_itm_prefabs, pos_btn);
            btn.Show(i + 1, btn_list[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { OnBtnClick(btn); });
        }
        List<int> list = SumSave.crt_pass.Get_day_state();
        //�̶�6������
        for (int i = 0; i < 6; i++)
        {
            task_item item = Instantiate(task_item_prefabs, pos_task);
            item.Show(i,SumSave.crt_pass.day_state[i], list[i] == 1);
            item.GetComponent<Button>().onClick.AddListener(() => { OnTaskClick(item); });
            dic_task.Add(i, item);
        }
    }
    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="item"></param>
    private void OnTaskClick(task_item item)
    {
        List<int> list = SumSave.crt_pass.Get_day_state();
        if (list[item.index] == 1)
        {
            Alert_Dec.Show("���������");
            return;
        }
        if (item.State(SumSave.crt_pass.day_state[item.index]))
        {
            //��ȡ����
            Battle_Tool.Obtain_Resources("���˽��", 1);

            SumSave.crt_pass.data_exp++;
            SumSave.crt_pass.Get(item.index);
            SumSave.crt_pass.Max_task_number++;
            if (SumSave.crt_pass.data_exp >= 10)
            {
                SumSave.crt_pass.data_lv++;
                SumSave.crt_pass.data_exp -= 10;
            }
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pass,
                SumSave.crt_pass.Set_Uptade_String(), SumSave.crt_pass.Get_Update_Character());
            Show_Pass_Progress();
        }
        else
        {
            Alert_Dec.Show("δ�����������");
        }

    }

    private void OnBtnClick(btn_item btn)
    {

    }

    public override void Show()
    {
        base.Show();
        Base_Show();
        Show_Pass_Progress();

    }
    /// <summary>
    /// ��ʾ�������
    /// </summary>
    private void Show_Pass_Progress()
    {
        task_info.text= "�ۼ��������" + SumSave.crt_pass.Max_task_number;
        List<int> list = SumSave.crt_pass.Get_day_state();
        foreach (int item in dic_task.Keys)
        {
            dic_task[item].progress(SumSave.crt_pass.day_state[item], list[item]==1);
        }
    }

    private void Base_Show()
    {
        for (int i = pos_item.childCount - 1; i >= 0; i--)
        {
            Destroy(pos_item.GetChild(i).gameObject);
        }
        //������ȡ״̬
        Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();

        for (int i = 0; i < SumSave.db_pass.Count; i++)
        {
            if (SumSave.db_pass[i].pass_index == index)
            { 
                pass_item item=Instantiate(pass_item_prefabs, pos_item);
                item.Data= SumSave.db_pass[i];
                if(!dic.ContainsKey(index))dic.Add(index,new List<int>(index));
                if (!dic[index].Contains(i))
                {
                    dic[index].Add(0);
                }
                if (dic.ContainsKey(index+1))
                {
                    if (!dic[index+1].Contains(i))
                    {
                        dic[index+1].Add(0);
                    }
                    item.Set(dic[index][i], dic[index + 1][i]);
                }
                else 
                 item.Set(dic[index][i], 1);

            }
        }

    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="item"></param>
    protected void GetReward(pass_item item)
    {
        if (SumSave.crt_pass.data_lv >= item.Data.lv)
        {
            Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();
            if (dic.ContainsKey(index))
            {
                if (dic[index][item.Data.lv] == 0)
                {
                    //��ȡ����
                    Obtain_result(item.Data.reward);
                    dic[index][item.Data.lv] = 1;
                    if (dic.ContainsKey(index + 1))
                        item.Set(dic[index][item.Data.lv], dic[index + 1][item.Data.lv]);
                    else item.Set(dic[index][item.Data.lv], 1);
                    SumSave.crt_pass.Get(dic);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pass, SumSave.crt_pass.Set_Uptade_String(), SumSave.crt_pass.Get_Update_Character());
                    Alert_Dec.Show("��ȡ�ɹ�");
                }
                else Alert_Dec.Show("�Ѿ���ȡ");
            }
        }else Alert_Dec.Show("�ȼ�����");
    }

    /// <summary>
    /// ������ȡ
    /// </summary>
    /// <param name="item"></param>
    protected void GetupLvReward(pass_item item)
    {
        if (SumSave.crt_pass.data_lv >= item.Data.lv)
        {
            Dictionary<int, List<int>> dic = SumSave.crt_pass.Set();
            if (dic.ContainsKey(index+1))
            {
                if (dic[index+1][item.Data.lv] == 0)
                {
                    //��ȡ����
                    Obtain_result(item.Data.uplv_reward);
                    dic[index+1][item.Data.lv] = 1;
                    item.Set(dic[index][item.Data.lv], dic[index + 1][item.Data.lv]);
                    SumSave.crt_pass.Get(dic);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pass, SumSave.crt_pass.Set_Uptade_String(), SumSave.crt_pass.Get_Update_Character());
                    Alert_Dec.Show("��ȡ�ɹ�");
                }
                else Alert_Dec.Show("�Ѿ���ȡ");
            }
        }
        else Alert_Dec.Show("�ȼ�����");
    }
    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="result"></param>
    private void Obtain_result(string result)
    {
        Battle_Tool.Obtain_result(result);
    }


}
