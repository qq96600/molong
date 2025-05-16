using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class panel_fatePalace : Panel_Base
{
    /// <summary>
    /// 单抽，十连抽按钮
    /// </summary>
    private Button single_draw, ten_consecutive_draws;
    /// <summary>
    /// 具体期数列表 ，具体物品列表位置
    /// </summary>
    private Transform designatedTime_items, fale_items;

    /// <summary>
    /// 显示奖励预制件
    /// </summary>
    private fate_item fate_item_prefab;
    /// <summary>
    /// 具体期数预制件
    /// </summary>
    private btn_item btn_item_prefab;
    /// <summary>
    /// 当前点击的期数
    /// </summary>
    private btn_item current_designated;

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();

        single_draw = Find<Button>("Single_draw");
        single_draw.onClick.AddListener(()=> { Single_draw(); });
        ten_consecutive_draws=Find<Button>("Ten_consecutive_draws");
        ten_consecutive_draws.onClick.AddListener(()=> { Ten_consecutive_draws(); });

        designatedTime_items= Find<Transform>("designatedTime_items/Viewport/Content");
        fale_items = Find<Transform>("fale_items/Viewport/Content");

        fate_item_prefab=Battle_Tool.Find_Prefabs<fate_item>("fate_item");
        btn_item_prefab=Battle_Tool.Find_Prefabs<btn_item>("btn_item");

        ClearObject(designatedTime_items);

        for(int i = 0; i < SumSave.db_fate_list.Count; i++)//实例化期数列表
        {
            btn_item btn_item = Instantiate(btn_item_prefab, designatedTime_items);
            string text="第"+SumSave.db_fate_list[i].fate_id+"期";
            btn_item.Show(SumSave.db_fate_list[i].fate_id, text);
            btn_item.GetComponent<Button>().onClick.AddListener(()=> { On_btn_item_click(btn_item); });
            if(btn_item.index==1)
            {
                On_btn_item_click(btn_item);
            }
        }
    }




    /// <summary>
    /// 切换到对应的期数物品
    /// </summary>
    private void On_btn_item_click(btn_item btn_item)
    {
        int fate_id = btn_item.index;
        ClearObject(fale_items);
        current_designated = btn_item;
        for (int j = 1; j <= 3; j++)
        {
            for (int i = 0; i < SumSave.db_fate_list[fate_id-1].fate_value_list.Count; i++)//实例化第一期物品列表
            {
                (string, int, int, int, int) data = SumSave.db_fate_list[fate_id-1].fate_value_list[i];
                if (data.Item2 == j)//按物品类型排序
                {
                    fate_item fate_item = Instantiate(fate_item_prefab, fale_items);
                    int num= 0;
                    if (SumSave.crt_needlist.fate_value_dic[fate_id].ContainsKey((data.Item1, data.Item3)))
                    {
                        num = data.Item4 - SumSave.crt_needlist.fate_value_dic[fate_id][(data.Item1, data.Item3)];//获得剩余数量

                    }else
                    {
                        num= data.Item4;
                    }

                    fate_item.Init(data,num);
                }
            }
        }

    }

    /// <summary>
    /// 十连抽
    /// </summary>
    private void Ten_consecutive_draws()
    {
        NeedConsumables("命运币", 10);
        if (!RefreshConsumables())
        {
            Alert_Dec.Show("命运币不足");
            return;
        }
        int weight = 0;
        List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index-1].fate_value_list;
        for (int i = 0; i < data.Count; i++)//获取总权重
        {
            weight += data[i].Item5;
        }
        List<(string, int )> dic = new List<(string, int)>();//存储奖励
        if (isExhaust(10))
        {
            for (int i=0;i<10;i++)
            {
                while (true)
                {
                    int rand = Random.Range(0, data.Count);//随机抽取一个物品
                    if (Random.Range(0, weight) < data[rand].Item5)//判断是否抽中
                    {
                        GetRewards(data, rand);
                        dic.Add((data[rand].Item1, data[rand].Item3));
                        break;
                    }
                }
            }
           
        }
        Alert_Icon.Show(dic);
        On_btn_item_click(current_designated);

    }
    

    /// <summary>
    /// 单抽
    /// </summary>
    private void Single_draw()
    {

        NeedConsumables("命运币", 1);
        if (!RefreshConsumables())
        {
            Alert_Dec.Show("命运币不足");
            return;
        }

        int weight = 0;
        List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index - 1].fate_value_list;

        for (int i=0;i < data.Count;i++)//获取总权重
        {
            weight+= data[i].Item5;
        }
       if( isExhaust())
        {
            while (true)
            {
                int rand = Random.Range(0, data.Count);//随机抽取一个物品
                int count = data[rand].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(data[rand].Item1, data[rand].Item3)];//命运宝典该剩余抽取次数

                if (Random.Range(0, weight) < data[rand].Item5 && count > 0)//判断是否抽中
                {

                    GetRewards(data, rand);
                    List<(string, int)> dic = new List<(string, int)>();//存储奖励
                    dic.Add((data[rand].Item1, data[rand].Item3));
                    Alert_Icon.Show(dic);
                    break;
                }
            }
       
        }
        On_btn_item_click(current_designated);
    }
    /// <summary>
    /// 当期是否抽完
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool isExhaust(int num=1)
    {
        int SumCount = 0;
        List<(string, int, int, int, int)> data = SumSave.db_fate_list[current_designated.index - 1].fate_value_list;
        for (int i = 0; i < data.Count; i++)//判断该期数是否抽完
        {
            if (SumSave.crt_needlist.fate_value_dic[current_designated.index].ContainsKey((data[i].Item1, data[i].Item3)))
            {
                //if (data[i].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(data[i].Item1, data[i].Item3)] == 0)
                //{
                //    SumCount++;
                //}
                //if (SumCount == data.Count)
                //{
                //    Alert_Dec.Show("该期数已抽完");
                //    return false;
                //}

                SumCount += data[i].Item4 - SumSave.crt_needlist.fate_value_dic[current_designated.index][(data[i].Item1, data[i].Item3)];
            }
            else
            {
                SumCount += data[i].Item4;
            }
        }

        if (num > SumCount)
        {
            Alert_Dec.Show("该期数奖励不足");
            return false;
        }

            return true;
    }

    /// <summary>
    /// 获得奖励
    /// </summary>
    /// <param name="data">（名字，奖励数量，抽奖次数，抽奖次数上限，权重）奖励列表</param>
    /// <param name="rand">具体奖励序列</param>
    private void GetRewards(List<(string, int, int, int, int)> data, int rand)
    {

   

        if (SumSave.crt_needlist.fate_value_dic.ContainsKey(current_designated.index))//判断是否已经存在该期数
        {
            if(SumSave.crt_needlist.fate_value_dic[current_designated.index].ContainsKey((data[rand].Item1, data[rand].Item3)))
            {
                SumSave.crt_needlist.fate_value_dic[current_designated.index][(data[rand].Item1, data[rand].Item3)]++;//更具期数，物品，物品单抽获取的数量找到对应的字典，并增加数量
            }else
            {
                SumSave.crt_needlist.fate_value_dic[current_designated.index].Add((data[rand].Item1, data[rand].Item3), 1);//不存在则创建
            }

            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
        }
        else
        {
            Dictionary<(string, int), int> dic = new Dictionary<(string, int), int>();
            dic.Add((data[rand].Item1, data[rand].Item3), 1);
            SumSave.crt_needlist.fate_value_dic.Add(current_designated.index, dic);//不存在则创建
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
        }

        switch (data[rand].Item2)
        {
            case 1:
                //获得材料技能书神器
                Battle_Tool.Obtain_Resources(data[rand].Item1, data[rand].Item3);
                break;
            case 2:
                //获得货币
                Battle_Tool.Obtain_Unit((currency_unit)Enum.Parse(typeof(currency_unit), data[rand].Item1), data[rand].Item3);
                break;
            case 3:
                //获得皮肤
                SumSave.crt_hero.hero_value += (SumSave.crt_hero.hero_value == "" ? "" : ",") + data[rand].Item1;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero, new string[] { Battle_Tool.GetStr(SumSave.crt_hero.hero_value) },
                    new string[] { "hero_value" });
                break;
        }


       
    }
}
