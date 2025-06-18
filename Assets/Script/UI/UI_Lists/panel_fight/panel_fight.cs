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

    public Button close_battle;
    /// <summary>
    /// 当前出现怪物数量 总怪物数量
    /// </summary>
    private int crt_monster_number = 0, maxnumber = 0;
    /// <summary>
    /// 五行种子
    /// </summary>
    private string[] FiveElementSeeds= { "牡丹皮", "青蒿", "苦参", "葛根", "金银花" };
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
        player_battle_attack_prefabs= Resources.Load<GameObject>("Prefabs/prefab/player_battle_attck_item"); ; //Battle_Tool.Find_Prefabs<GameObject>("player_battle_attck_item")
        monster_battle_attack_prefabs = Resources.Load<GameObject>("Prefabs/prefab/monster_battle_attck_item");// Battle_Tool.Find_Prefabs<GameObject>("monster_battle_attck_item"); 
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
        IsReply(SumSave.battleHeroHealths);
        IsReply(SumSave.battleMonsterHealths);
    }
    /// <summary>
    /// 回复生命魔法
    /// </summary>
    /// <param name="list"></param>
    private void IsReply(List<BattleHealth> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            BattleAttack attack = list[i].GetComponent<BattleAttack>();
            if (attack.Data.Heal_Hp > 0|| attack.Data.Heal_Mp > 0)
            {
                list[i].HealConsumables(attack.Data.Heal_Hp, attack.Data.Heal_Mp);
            }
        }
    }

    /// <summary>
    /// 关闭列表
    /// </summary>
    private void Close()
    {
        close_battle.gameObject.SetActive(true);
        transform.SetAsFirstSibling();
        return;
        //close_panel_state=!close_panel_state;
        //for (int i = 1; i < pos_btn.childCount; i++)
        //{ 
        //    pos_btn.GetChild(i).gameObject.SetActive(close_panel_state);
        //}
        //close_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>(close_panel_state ? "UI/btn_list/隐藏" : "UI/btn_list/展开");
    }
    public override void Show()
    {
        base.Show();
        AudioManager.Instance.ChangeBGM(BGMenum.开启);
    }
    public override void Hide()
    { 
        base.Hide();
    }
    /// <summary>
    /// 副本初始化
    /// </summary>
    /// <param name="damage"></param>
    protected void DailyCopies(BattleHealth target)
    {
        int damge = (int)(target.maxHP - target.HP);
        long max = 0;
        long value = 0;

     

        switch (select_map.map_index)
        {
            case 37: //历练
                max = SumSave.crt_MaxHero.Lv * 1000;
                value = (long)(damge * max / target.maxHP)*100;
                Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得历练 " + value);
                Battle_Tool.Obtain_Unit(currency_unit.历练, value) ;
                break;
            case 36: //魔丸
                max = SumSave.crt_MaxHero.Lv / 5 + 20;
                value = (long)(damge * max / target.maxHP);
                Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得魔丸 " + value );
                Battle_Tool.Obtain_Unit(currency_unit.魔丸, value);
                break;
            case 35: //灵气
                if (SumSave.crt_world != null)
                {
                    max = (SumSave.crt_world.World_Lv + 1) * 100;
                    value = (long)(damge * max / target.maxHP);
                    Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得灵气 " + value );
                    SumSave.crt_world.Set((int)value);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
                }
                break;
            case 34: //经验
                max = SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv];
                value = (long)(damge * max / target.maxHP );
                Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得经验 " + value );
                Battle_Tool.Obtain_Exp(value);
                break;
            case 33://灵珠
                max = SumSave.crt_MaxHero.Lv * 100000;
                value = (long)(damge * max / target.maxHP);
                Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得灵珠 " + value );
                Battle_Tool.Obtain_Unit(currency_unit.灵珠, value);
                break;
            case 32://魔丹
                max = SumSave.crt_MaxHero.Lv + 50;
                value = (int)(damge * max / target.maxHP);
                (string,int) str = SumSave.db_lvs.world_lv_list[0];
                Alert.Show(select_map.map_name, "副本战斗结束,造成伤害 " + damge + "\n获得 " + str.Item1+" * "+value );
                Battle_Tool.Obtain_Resources(str.Item1,(int)value);
                break;
            default:
                break;
        }
     
        Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_name == SumSave.crt_resources.user_map_index));
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
  
    /// <summary>
    /// 进入地图
    /// </summary>
    /// <param name="map"></param>
    /// <param name="isCopies">是否副本</param>
    public void Open_Map(user_map_vo map,bool isCopies = false)
    {
        if (!isCopies)
        {
            if(map.map_type==1)//普通地图记录切换
            {
                SumSave.crt_resources.user_map_index = map.map_name;
                
            }
            
        }else
        {
            //获得五行随机五行种子
            string material = FiveElementSeeds[Random.Range(0, FiveElementSeeds.Length - 1)];
            int num = Random.Range(1, 3);
            Battle_Tool.Obtain_Resources(material, num);
            Alert.Show(map.map_name, "获得 " + material + " * " + num);
        }

        Combat_statistics.isTime = true;
        select_map = map;
        map_name.text = map.map_name;
        
        init();
        Crate_Init();
        StopAllCoroutines();
        
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
        crt_monster_number = 0;
        maxnumber = 100;
        switch (select_map.map_type)
        {
            case 2: maxnumber = 10; break;
            case 3: maxnumber = 1; break;
            case 4: maxnumber = 1; break;
            default:
                break;
        }
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
        //判断是否切换地图
        if (iSOpenMap(true))
            Open_Map(select_map);
        else Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_name == SumSave.crt_resources.user_map_index));
    }
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void Game_Start()
    {
        StartCoroutine(ProduceMonster(WaitTime()));
    }

    private float WaitTime()
    {
        float Waittime = 5f;
        if(SumSave.crt_MaxHero.bufflist.Count> (int)enum_skill_attribute_list.寻怪间隔)
        Waittime -= SumSave.crt_MaxHero.bufflist[(int)enum_skill_attribute_list.寻怪间隔]/10f;
        Waittime = Mathf.Clamp(Waittime, 1f, 5f);
        return Waittime;
    }
    // 下一波怪
    protected void Game_Next_Map()
    {
        if (Open_Monster_State)
        {
            Open_Monster_State = false;
            if(iSOpenMap())
                StartCoroutine(ProduceMonster(WaitTime()));
            else Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_name == SumSave.crt_resources.user_map_index));
        }
    }
    /// <summary>
    /// 判断地图是否可以打开
    /// </summary>
    /// <returns></returns>
    private bool iSOpenMap(bool state=false)
    {
        bool exist = true;
        if (select_map.need_Required != "")
        {
            if (state || crt_monster_number >= maxnumber)
            {
                NeedConsumables(select_map.need_Required, 1);
                if (!RefreshConsumables())
                {
                    exist = false;
                }
            }
        }
        if (select_map.map_type == 4)
        {
            exist = false;
        }
        return exist;
      
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
        if (crt_monster_number >= maxnumber) crt_monster_number = 0;
        crt_monster_number++;
        crtMaxHeroVO crt = crt_map_monsters[Random.Range(0, crt_map_monsters.Count)];
        if (crt_monster_number == maxnumber)
        {
            crt = crt_map_monsters[crt_map_monsters.Count - 1];
        }
        else
        {
            if (maxnumber == 10)
            {
                crt = crt_map_monsters[0];
            }
        }
        //crt = crt_map_monsters[1];
        crt = Battle_Tool.crate_monster(crt, select_map, crt_monster_number == maxnumber);

        GameObject item = ObjectPoolManager.instance.GetObjectFormPool(crt.show_name, monster_battle_attack_prefabs,
            new Vector3(pos_monster.position.x, pos_monster.position.y,pos_monster.position.z), Quaternion.identity, pos_monster);
        // 设置Data
        item.GetComponent<monster_battle_attck>().Data = crt;
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.购买物品); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleMonsterHealths.Add(item.GetComponent<BattleHealth>());
        Open_Monster_State=true;
        ShowInfoMap();
    }

    private void ShowInfoMap()
    {
        map_name.text = select_map.map_name + "(" + crt_monster_number + "/" + maxnumber + ")";
    }
    /// <summary>
    /// 判断当前地图是否为对应地图
    /// </summary>
    public  bool isMapType(int type)
    {
        if(select_map.map_type==type)
        {
            return true;
        }
        return false;
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
            item.GetComponent<Text>().text = " " + str;
            show_info_list.Add(item);
        }
        else
        {
            show_info_item item = show_info_list[0];
            item.GetComponent<Text>().text = " " + str;
            item.transform.SetAsLastSibling();
            show_info_list.Remove(item);
            show_info_list.Add(item);
        }    
    }
}
