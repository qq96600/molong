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

public class panel_wodleBoss : Panel_Base
{
    /// <summary>
    /// 世界boss图标
    /// </summary>
    private Image icon;
    /// <summary>
    /// 进入调整按钮
    /// </summary>
    private Button up_map;
    /// <summary>
    /// 战斗地图
    /// </summary>
    private panel_fight fight_panel;
    /// <summary>
    /// 属性显示位置
    /// </summary>
    private Transform crt;
    /// <summary>
    /// boss挑战进度条
    /// </summary>
    private Slider progress;
    /// <summary>
    /// 血量显示，次数显示
    /// </summary>
    private Text Hp_Text, numberText, nameText;
    /// <summary>
    /// 最大生命值
    /// </summary>
    private long maxHp;
    /// <summary>
    /// 当前世界Boss挑战次数，最大挑战次数
    /// </summary>
    private int boss_number = 0, boss_number_max = 3;
    /// <summary>
    /// 排行榜预制体
    /// </summary>
    private rank_item rank_itemPrefab;
    /// <summary>
    /// 排行榜位置
    /// </summary>
    private Transform information;

    public override void Initialize()
    {
        base.Initialize();
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        icon = Find<Image>("boss_icon");
        up_map = Find<Button>("up_map");
        up_map.onClick.AddListener(Challenge);
        crt = Find<Transform>("information/Viewport/Content");
        progress = Find<Slider>("progress");
        Hp_Text = Find<Text>("progress/Hp_Text");
        numberText = Find<Text>("up_map/number");
        nameText =  Find<Text>("boss_icon/nameText");
        rank_itemPrefab = Battle_Tool.Find_Prefabs<rank_item>("rank_item");
        information= Find<Transform>("information/Viewport/Content");

        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == SumSave.db_world_boos.name)
            {
                boss_number = list[i].Item2;
            }
        }

        if (SumSave.db_world_boos.Get() <= 0)///如果boss死亡超过一天，重置挑战
        {
            Debug.Log("世界boss重置时间："+( SumSave.db_world_boos.UpTime.Day - SumSave.nowtime.Day));
            if (SumSave.nowtime.Day-SumSave.db_world_boos.UpTime.Day >= 1)
            {
                SumSave.db_world_boos.number++;
                SumSave.db_world_boos.InitFinalDamage(SumSave.db_world_boos.BossHpBasic * SumSave.db_world_boos.number);
                SumSave.db_world_boos.UpTime = SumSave.nowtime;
                SendNotification(NotiList.Read_Crate_world_boss_update);
            }
        }


        Init();
    }

   /// <summary>
   /// 初始化世界boss排行榜
   /// </summary>
    private void GetList()
    {
        ClearObject(crt);
        for (int i = 0; i < SumSave.crt_world_boss_rank.lists.Count; i++)
        {
            rank_item item = Instantiate(rank_itemPrefab, crt);
            item.Data2 = SumSave.crt_world_boss_rank.lists[i];
            item.Show_index2(i + 1);
        }
    }


    /// <summary>
    /// 点击挑战
    /// </summary>
    private void Challenge()
    {
        if(SumSave.db_world_boos.Get()<=0)
        {
            Alert_Dec.Show("世界Boss已被击败");
            return;
        }
        if (boss_number >= boss_number_max)
        {
            Alert_Dec.Show("挑战次数不足");
            return;
        }
        IncreaseFrequency();
        long hurt=Random.Range(SumSave.crt_MaxHero.totalPower*50/100, SumSave.crt_MaxHero.totalPower * 60 / 100);//每次挑战根据战力50%-60%随机伤害

        world_Boss_set(hurt);
        crate_rank(hurt);
        GainRewards(hurt);
        Init();
    }
    /// <summary>
    /// 获得奖励
    /// </summary>
    private void GainRewards(long hurt)
    {
        List < (int, long) > list = SumSave.db_world_boos.DamageLevel_List;
        for (int i = list.Count-1; i >=0; i--)
        {
            if(list[i].Item2<= hurt)
            {
                long experience = 40 * list[i].Item1;
                long MagicPill = 20 * list[i].Item1;
                int honor = 2;
                SumSave.crt_user_unit.verify_data(currency_unit.历练, experience);
                SumSave.crt_user_unit.verify_data(currency_unit.魔丸, MagicPill);
                SumSave.crt_accumulatedrewards.Set(2, honor);
                Alert.Show("世界Boss挑战奖励", "造成伤害：" + hurt + "\n 获得奖励：历练x" + experience + " 魔丸x" + MagicPill + " 荣誉x" + honor);
            }
        }
        

    }



    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        SendNotification(NotiList.Read_Crate_world_boss_Login);
        icon.sprite = Resources.Load<Sprite>("Prefabs/monsters/" + SumSave.db_world_boos.name);
        maxHp = SumSave.db_world_boos.BossHpBasic * SumSave.db_world_boos.number;
        progress.maxValue = maxHp;
        progress.value = SumSave.db_world_boos.Get();
        numberText.text = "挑战次数:" + boss_number.ToString() + "/3";
        nameText.text = SumSave.db_world_boos.name;
        GetList();
        if (SumSave.db_world_boos.Get() <= 0)
        {
            Hp_Text.text="世界Boss已被击败,等待下轮Boss";
            if(SumSave.db_world_boss_hurt.Count>0)
            {
                SendNotification(NotiList.Read_Crate_RecordAndClearWorldBoss);
                SumSave.db_world_boss_hurt.Clear();
            }
           

        }
        else
        {
            Hp_Text.text = SumSave.db_world_boos.Get() + "/" + maxHp;
        }
       
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
     
    }
    /// <summary>
    /// 写入对世界boss的伤害
    /// </summary>
    /// <param name="finalDamage"></param>
    private void world_Boss_set(long finalDamage)
    {
        SumSave.db_world_boos.Set(finalDamage);
        SendNotification(NotiList.Read_Crate_world_boss_update);
        SumSave.crt_world_boss_hurt.CauseDamage(finalDamage);
    }

    /// <summary>
    /// 挑战次数++
    /// </summary>
    private void IncreaseFrequency()
    {
        boss_number++;
        bool exist = true;
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == SumSave.db_world_boos.name)
            {
                exist = false;
                list[i] = (list[i].Item1, list[i].Item2 + 1);
                SumSave.crt_needlist.SetMap(list[i]);
                break;
            }
        }
        if (exist)
        {
            SumSave.crt_needlist.SetMap((SumSave.db_world_boos.name, 1));

        }

        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist,
           SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
    }




    /// <summary>
    /// 刷新Boss排行榜
    /// </summary>
    private  void Refresh_Rank()
    {
        SumSave.crt_world_boss_rank.lists = ArrayHelper.OrderDescding(SumSave.crt_world_boss_rank.lists, x => x.Item3);
        if(SumSave.crt_world_boss_rank.lists.Count>50)//最多显示50条
        {
            for (int i = 49; i <= SumSave.crt_world_boss_rank.lists.Count-1; i++)
            {
                SumSave.crt_world_boss_rank.lists.RemoveAt(i);
            }
        }
        SumSave.crt_world_boss_rank.SetData();
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.user_world_boss_rank,
          SumSave.crt_world_boss_rank.Set_Uptade_String(), SumSave.crt_world_boss_rank.Get_Update_Character());
    }
    /// <summary>
    /// 创建Boss排行榜
    /// </summary>
    private void crate_rank(long rank)
    {
        List<(string, string, long)> Boss_list = SumSave.crt_world_boss_rank.lists;
        //SumSave.user_ranks.lists.Add(rank);
        if(Boss_list.Count==0)
        {
            Boss_list.Add((SumSave.crt_user.uid, SumSave.crt_hero.hero_name, rank));
        }
        for (int i = 0; i < Boss_list.Count; i++)
        {
            if (Boss_list[i].Item1 == SumSave.crt_user.uid)
            {
                long old_rank = Boss_list[i].Item3+rank;
                Boss_list[i]= (Boss_list[i].Item1, Boss_list[i].Item2, old_rank);
                SumSave.crt_world_boss_rank.lists = Boss_list;
                //排序
                Refresh_Rank();
                return;
            }
        }

        Boss_list.Add((SumSave.crt_user.uid, SumSave.crt_hero.hero_name, rank));
        Refresh_Rank();
        return;

    }

}
