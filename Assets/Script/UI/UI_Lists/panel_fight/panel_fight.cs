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

public class panel_fight : Panel_Base
{
    /// <summary>
    /// 玩家预制体
    /// </summary>
    private GameObject player_battle_attack_prefabs;

    private GameObject monster_battle_attack_prefabs;

    /// <summary>
    /// 怪物生成位置
    /// </summary>
    private Transform pos_player, pos_monster;
    /// <summary>
    /// 当前怪物列表
    /// </summary>
    private List<crtMaxHeroVO> crt_map_monsters = new List<crtMaxHeroVO>();
    /// <summary>
    /// 当前地图
    /// </summary>
    private user_map_vo select_map;
    /// <summary>
    /// 刷新状态
    /// </summary>
    private bool Open_Monster_State= true;
    /// <summary>
    /// 刷新技能
    /// </summary>
    private pight_show_skill pight_show_skill;
    /// <summary>
    /// 战斗技能
    /// </summary>
    private List<skill_offect_item> battle_skills;
    /// <summary>
    /// 显示基础信息
    /// </summary>
    private panel_role_health role_health;
    /// <summary>
    /// 显示地图名称和回合时间
    /// </summary>
    private Text map_name, map_time;
    /// <summary>
    /// 关闭列表按钮
    /// </summary>
    private Button close_btn;
    /// <summary>
    /// 是否关闭列表
    /// </summary>
    private bool close_panel_state = true;
    /// <summary>
    /// 列表位置
    /// </summary>
    private Transform pos_btn,pos_show_info;
    /// <summary>
    /// 信息框
    /// </summary>
    private show_info_item show_info_prefabs;
    /// <summary>
    /// 存储信息
    /// </summary>
    private List<show_info_item> show_info_list = new List<show_info_item>();
    /// <summary>
    /// 战斗信息汇总
    /// </summary>
    private Text battle_info_list;
    /// <summary>
    /// 战斗统计清空
    /// </summary>
    private Button btn_Combat_statistics;
    protected override void Awake()
    {
        base.Awake();
        Combat_statistics.Init();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_monster = Find<Transform>("battle_pos/monster_pos");
        pos_player = Find<Transform>("battle_pos/player_pos");
        player_battle_attack_prefabs= Resources.Load<GameObject>("Prefabs/panel_fight/player_battle_attck_item");
        monster_battle_attack_prefabs = Resources.Load<GameObject>("Prefabs/panel_fight/monster_battle_attck_item");
        pight_show_skill = Find<pight_show_skill>("skill_list");
        role_health = Find<panel_role_health>("panel_role_health");
        map_name = Find<Text>("battle_pos/map_name/info");
        map_time = Find<Text>("battle_pos/map_name/show_time");
        close_btn = Find<Button>("btn_list/close_btn");
        close_btn.onClick.AddListener(() => { Close(); });
        pos_btn = Find<Transform>("btn_list");
        pos_show_info = Find<Transform>("info_list/Viewport/Content");
        show_info_prefabs = Battle_Tool.Find_Prefabs<show_info_item>("show_info_item");
        btn_Combat_statistics = Find<Button>("Combat_statistics");
        btn_Combat_statistics.onClick.AddListener(() => { clear_Combat_statistics(); });
        battle_info_list= Find<Text>("Combat_statistics/info");
    }
    /// <summary>
    /// 初始状态
    /// </summary>
    private void clear_Combat_statistics()
    {
        Alert.Show("清空战斗信息", "是否清空战斗信息", Clear_Combat_statistics);
       
    }
    /// <summary>
    /// 清空战斗信息
    /// </summary>
    /// <param name="arg0"></param>
    private void Clear_Combat_statistics(object arg0)
    {
        Combat_statistics.Init();
    }

    /// <summary>
    /// 显示战斗信息
    /// </summary>
    public void Show_Combat_statistics()
    {
        battle_info_list.text = Combat_statistics.Show_Info();
    }

    /// <summary>
    /// 关闭列表
    /// </summary>
    private void Close()
    {
        transform.SetAsFirstSibling();
        return;
        close_panel_state=!close_panel_state;
        for (int i = 1; i < pos_btn.childCount; i++)
        { 
            pos_btn.GetChild(i).gameObject.SetActive(close_panel_state);
        }
        close_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>(close_panel_state ? "UI/btn_list/隐藏" : "UI/btn_list/展开");
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    { 
        base.Hide();
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    protected void Game_Over()
    {
        if (Open_Monster_State)
        {
            Open_Monster_State = false;
            //进入复活周期 5s
            StopAllCoroutines();
            StartCoroutine(Game_WaitTime(5));
        }
    }

    public void Open_Map(user_map_vo map)
    {
        Combat_statistics.isTime = true;
        select_map = map;
        init();
        Crate_Init();
        map_name.text=map.map_name;
        Show_Battle_State("战斗中...");
    }
    /// <summary>
    /// 显示战斗状态
    /// </summary>
    /// <param name="dec"></param>
    private void Show_Battle_State(string dec)
    {
        map_time.text= dec;
    }
    /// <summary>
    /// 获取离线收益
    /// </summary>
    public void offline()
    {
        //计算离线收益
        //进入战斗
        Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_name == SumSave.crt_resources.user_map_index));
    }
    /// <summary>
    /// 生成角色初始化
    /// </summary>
    private void Crate_Init()
    {
        crate_Skill();
        crate_hero();
        crate_monster();
    }

    public void Refresh_Max_Hero_Attribute()
    {
        if (gameObject.activeInHierarchy)
        {
            crate_Skill();
            foreach (var item in SumSave.battleHeroHealths)
            {
                item.GetComponent<BattleAttack>().Refresh(SumSave.crt_MaxHero);
                item.GetComponent<BattleAttack>().Refresh_Skill(battle_skills);
            }
        }
    }
    private void crate_Skill()
    {
        battle_skills = pight_show_skill.Init();
    }

    private void crate_hero()
    {
        
        crtMaxHeroVO crt = SumSave.crt_MaxHero;
        GameObject item = ObjectPoolManager.instance.GetObjectFormPool(crt.show_name, player_battle_attack_prefabs,
            new Vector3(pos_player.position.x, pos_player.position.y, pos_player.position.z), Quaternion.identity, pos_player);
        // 设置Data
        item.GetComponent<player_battle_attck>().Data = crt;
        item.GetComponent<player_battle_attck>().Refresh_Skill(battle_skills);
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.购买物品); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleHeroHealths.Add(item.GetComponent<BattleHealth>());
        role_health.SetHealth(item.GetComponent<BattleHealth>());
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void init()
    {
        if (SumSave.battleMonsterHealths != null)
        {
            for (int i = pos_monster.childCount - 1; i >= 0; i--)
            {
                pos_monster.GetChild(i).gameObject.GetComponent<BattleHealth>().Clear();
            }
            SumSave.battleMonsterHealths.Clear();
        }
        if (SumSave.battleHeroHealths != null)
        {
            for (int i = pos_player.childCount - 1; i >= 0; i--)
            {
                pos_player.GetChild(i).gameObject.GetComponent<BattleHealth>().Clear();
            }
            SumSave.battleHeroHealths.Clear();
        }
        crt_map_monsters.Clear();
        for (int i = 0; i < SumSave.db_monsters.Count; i++)
        {
            if (SumSave.db_monsters[i].index == select_map.map_index)
            {
                crt_map_monsters.Add(SumSave.db_monsters[i]);
            }
        }
    }

    /// <summary>
    /// 死亡等待
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Game_WaitTime(float time)
    {
        Combat_statistics.AddDead();
        Combat_statistics.isTime = false;
        while (time > 0)
        {
            Show_Battle_State("复活剩余时间 " + time.ToString("F1") + "s");
            time -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Open_Map(select_map);
    }
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void Game_Start()
    {
        StartCoroutine(ProduceMonster(SumSave.WaitTime));
    }
    // 下一波怪
    protected void Game_Next_Map()
    {
        if (Open_Monster_State)
        {
            Open_Monster_State = false;
            StartCoroutine(ProduceMonster(SumSave.WaitTime));
        }
    }
    private IEnumerator ProduceMonster(float time)
    {
        float basetime = 0.1f;
        Combat_statistics.isTime = false;
        while (time > 0)
        {
            Show_Battle_State("刷新时间 " + time.ToString("F1") +"s") ;
            time -= basetime;
            yield return new WaitForSeconds(basetime);
        }
        Show_Battle_State("战斗中...");
        crate_monster();
    }

    private void crate_monster()
    {
        Combat_statistics.isTime = true;
        crtMaxHeroVO crt = crt_map_monsters[Random.Range(0, crt_map_monsters.Count)];
        crt = Battle_Tool.crate_monster(crt);
        GameObject item = ObjectPoolManager.instance.GetObjectFormPool(crt.show_name, monster_battle_attack_prefabs,
            new Vector3(pos_monster.position.x, pos_monster.position.y,pos_monster.position.z), Quaternion.identity, pos_monster);
        // 设置Data
        item.GetComponent<monster_battle_attck>().Data = crt;
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.购买物品); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleMonsterHealths.Add(item.GetComponent<BattleHealth>());
        Open_Monster_State=true;
    }
    /// <summary>
    /// 显示战斗信息
    /// </summary>
    /// <param name="str"></param>
    protected void show_battle_info(string str)
    {
        if (show_info_list.Count <= 80)
        {
            show_info_item item = Instantiate(show_info_prefabs, pos_show_info);
            item.GetComponent<Text>().text = str;
            show_info_list.Add(item);
        }
        else
        {
            show_info_item item = show_info_list[0];
            item.GetComponent<Text>().text = str;
            item.transform.SetAsLastSibling();
            show_info_list.Remove(item);
            show_info_list.Add(item);
        }    
    }
}
