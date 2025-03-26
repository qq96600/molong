using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class panel_fight : Panel_Base
{
    /// <summary>
    /// 玩家预制体
    /// </summary>
    private GameObject player_battle_attack_prefabs;

    private GameObject monster_battle_attack_prefabs;

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
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_monster = Find<Transform>("battle_pos/monster_pos");
        pos_player = Find<Transform>("battle_pos/player_pos");
        player_battle_attack_prefabs= Resources.Load<GameObject>("Prefabs/panel_fight/player_battle_attck_item");
        monster_battle_attack_prefabs = Resources.Load<GameObject>("Prefabs/panel_fight/monster_battle_attck_item");
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
        select_map = map;
        init();
        Crate_Init();
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
        crate_hero();
        crate_monster();
    }

    private void crate_hero()
    {
        crtMaxHeroVO crt = SumSave.crt_MaxHero;
        GameObject item = ObjectPoolManager.instance.GetObjectFormPool(crt.show_name, player_battle_attack_prefabs,
            new Vector3(pos_player.position.x, pos_player.position.y, pos_player.position.z), Quaternion.identity, pos_player);
        // 设置Data
        item.GetComponent<player_battle_attck>().Data = crt;
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.购买物品); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleHeroHealths.Add(item.GetComponent<BattleHealth>());
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
            else crt_map_monsters.Add(SumSave.db_monsters[i]);

        }
    }

    /// <summary>
    /// 死亡等待
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Game_WaitTime(float time)
    {
        while (time > 0)
        {
            time -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        Game_Start();
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

        while (time > 0)
        {
            time-= basetime;
            yield return new WaitForSeconds(basetime);
        }
        crate_monster();
        //StartCoroutine(ProduceMonster(SumSave.WaitTime));
    }

    private void crate_monster()
    {
        crtMaxHeroVO crt = crt_map_monsters[Random.Range(0, crt_map_monsters.Count)];
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
}
