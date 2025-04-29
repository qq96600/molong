using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public enum Achieve_Type//成就类型枚举
{
    收集系列 = 1,
    击杀系列 ,

}

public class plant_achievement : Base_Mono
{
    /// <summary>
    /// 显示具体值
    /// </summary>
    private Image show_offect;
    /// <summary>
    /// 显示成就名称
    /// </summary>
    private Text show_name, show_info;
    /// <summary>
    /// 领取奖励按钮
    /// </summary>
    private Button btn_receive;
   

    /// <summary>
    /// 成就位置
    /// </summary>
    public Transform crt;
    /// <summary>
    /// 具体成就
    /// </summary>
    public ach_item Achieve_Item_Prefab;
    /// <summary>
    /// 当前成就
    /// </summary>
    private ach_item crt_achieve_Item;
    /// <summary>
    /// 成就类型预制体
    /// </summary>
    public btn_item Achieve_Type_Prefab;
    /// <summary>
    /// 当前选择成就类型
    /// </summary>
    private btn_item crt_type;
    /// <summary>
    /// 存储字典
    /// </summary>
    private new Dictionary<btn_item, List<ach_item>> dic = new Dictionary<btn_item, List<ach_item>>();
    /// <summary>
    /// 自身经验值
    /// </summary>
    private Dictionary<string, int> dic_exp = new Dictionary<string, int>();
    private Dictionary<string, Dictionary<int, int>> dic_exchange = new Dictionary<string, Dictionary<int, int>>();
    /// <summary>
    /// 自身等级
    /// </summary>
    private Dictionary<string, int> dic_lv = new Dictionary<string, int>();
    
    /// <summary>
    /// 关闭按钮
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
    /// 成就信息栏点击其他区域关闭
    /// </summary>
    private void Close()
    {
        show_offect.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击按钮领取奖励
    /// </summary>
    private void Receive()
    {
        int lv = dic_lv[crt_achieve_Item.Data.achievement_value];
        if (true)
        {
            if (lv >= crt_achieve_Item.Data.achievement_needs.Count)
            {
                Alert_Dec.Show("当前阶段已满");
                return;
            }
            if (crt_achieve_Item.Data.achievement_needs[lv] > dic_exp[crt_achieve_Item.Data.achievement_value])
            {
                Alert_Dec.Show("未达到领取条件");
                return;
            }
        }
        dic_lv[crt_achieve_Item.Data.achievement_value]++;
        string[] temp = crt_achieve_Item.Data.achievement_rewards[lv].Split(' ');
        Battle_Tool.Obtain_Resources(temp[1], int .Parse( temp[2]));
        Alert_Dec.Show("领取成功");
        show_offect.gameObject.SetActive(false);
        crt_achieve_Item.Init(); 
    }






    /// <summary>
    /// 初始化成就列表
    /// </summary>
    private void Insace_Base_Info()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Achieve_Type)).Length; i++)//更具枚举创建成就类型
        {
            btn_item temp = Instantiate(Achieve_Type_Prefab, crt);//实例化
            temp.Show(i,(Achieve_Type)(i + 1));//显示标题信息
            for (int j = 0; j < SumSave.db_Achievement_dic.Count; j++)//成就数据库
            {
                if (SumSave.db_Achievement_dic[j].achievement_type == i + 1)//判断是否在当前类型
                {
                    ach_item item = Instantiate(Achieve_Item_Prefab, crt);//实例化具体成就
                    item.Data = SumSave.db_Achievement_dic[j];//获取成就信息
                    //读取数据
                    if (!dic.ContainsKey(temp)) dic.Add(temp, new List<ach_item>());//判断字典里是否已经存在
                    dic[temp].Add(item);
                    item.GetComponent<Button>().onClick.AddListener(() => { Select_Achieve(item); });//具体成就点击事件
                }
            }
            temp.GetComponent<Button>().onClick.AddListener(() => { Select_Type(temp); });//成就类型点击事件
        }
        foreach (btn_item item in dic.Keys)
        {
            foreach (ach_item crt_item in dic[item])//关闭所以具体成就
            {
                crt_item.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 读取成就
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
    /// 显示成就描述
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
                dec += "\n" + Show_Color.Green(InSetInfo(i) + "(已领取)");
            }
        }
        if (dic_lv[crt_achieve_Item.Data.achievement_value] < crt_achieve_Item.Data.achievement_needs.Count)
        {
            int number = dic_lv[crt_achieve_Item.Data.achievement_value];
            dec += "\nLv" + (number + 1) + ".成就阶段 " + dic_exp[crt_achieve_Item.Data.achievement_value] + "/" + crt_achieve_Item.Data.achievement_needs[number];
            dec += "\n奖励 " + Show_Color.Yellow(InSetInfo(number));
        }
        show_info.text = dec;
    }
    /// <summary>
    /// 选择成就类型
    /// </summary>
    /// <param name="temp"></param>
    private void Select_Type(btn_item temp)
    {
        if (crt_type != null)
        {
            if (crt_type == temp)//判断是否为当前选择类型
            {
                if (!temp.Active())//判断是否激活
                {
                    crt_type.Selected = true;
                    foreach (ach_item item in dic[crt_type])
                    {
                        item.gameObject.SetActive(crt_type.Active());//判断是否需要激活
                        item.Init();//初始化
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
            foreach (ach_item item in dic[crt_type])//不是当前选择类型关闭
            {
                item.gameObject.SetActive(crt_type.Active());
            }
        }
        crt_type = temp;//更新当前选择类型
        crt_type.Selected = true;//选中状态
        foreach (ach_item item in dic[crt_type])//打开并且初始化item
        {
            item.gameObject.SetActive(crt_type.Active());
            item.Init();
        }
    }
    /// <summary>
    /// 获取奖励
    /// </summary>
    /// <param name="i"></param>
    private string InSetInfo(int i)
    {
        string dec = "";
        //string[] temp = crt_achieve_Item.Data.achieve_rewards[i].Split(' ');
        //if (temp.Length < 1) return "";
        //dec += (Achieve_Rewards_Type)int.Parse(temp[0]) + " ";
        //switch ((Achieve_Rewards_Type)int.Parse(temp[0]))
        //{
        //    case Achieve_Rewards_Type.Boss点: dec += "Boss点 +" + temp[1]; break;
        //    case Achieve_Rewards_Type.装备: dec += "装备 +" + temp[1]; break;
        //    case Achieve_Rewards_Type.金币: dec += "金币 +" + temp[1] + "w"; break;
        //    case Achieve_Rewards_Type.材料: dec += temp[1] + " * " + temp[2]; break;
        //    case Achieve_Rewards_Type.伤害减免: dec += (temp[1] == "1" ? "减少物理伤害" : "减少魔法伤害") + " + " + temp[2] + "%"; break;
        //    case Achieve_Rewards_Type.体力: dec += (temp[1] == "1" ? "生命" : "魔法") + " + " + temp[2]; break;
        //    case Achieve_Rewards_Type.元素加成:
        //        {
        //            if (temp[1] == "1") dec += "攻击伤害 + " + temp[2] + "%";
        //            else if (temp[1] == "2") dec += "暴击率 + " + temp[2] + "%";
        //            else if (temp[1] == "3") dec += "暴击伤害 + " + temp[2] + "%";
        //            else if (temp[1] == "4") dec += "攻击速度 + " + temp[2] + "";
        //            else if (temp[1] == "5") dec += "命中 + " + temp[2] + "";
        //        }
        //        break;
        //    case Achieve_Rewards_Type.战力:
        //        if (temp[1] == "1") dec += "物理防御 + " + temp[2] + "";
        //        else if (temp[1] == "2") dec += "魔法防御 + " + temp[2] + "";
        //        else if (temp[1] == "3") dec += "物攻 + " + temp[2] + "";
        //        else if (temp[1] == "4") dec += "魔攻 + " + temp[2] + "";
        //        else if (temp[1] == "5") dec += "道术 + " + temp[2] + "";

        //        break;
        //    case Achieve_Rewards_Type.异常抗性:
        //        if (temp[1] == "1") dec += "暴击抵抗 + " + temp[2] + "%";
        //        else if (temp[1] == "2") dec += "爆伤抵抗 + " + temp[2] + "%";
        //        break;

        //    case Achieve_Rewards_Type.祝福:
        //        if (temp[1] == "1") dec += "幸运 + " + temp[2] + "";
        //        break;
        //    case Achieve_Rewards_Type.回复能力:
        //        if (temp[1] == "1") dec += "生命回复 + " + temp[2] + "";
        //        else if (temp[1] == "2") dec += "魔法回复 + " + temp[2] + "";

        //        break;
        //    case Achieve_Rewards_Type.技能加成:
        //        dec += (Skill_List)(int.Parse(temp[1])) + " 技能效果 + " + temp[2] + "%";
        //        break;

        //    case Achieve_Rewards_Type.领域加成:
        //        dec += (Skill_List)(int.Parse(temp[1])) + " 领域伤害 + " + temp[2] + "";
        //        break;

        //}

        return dec;
    }

}
