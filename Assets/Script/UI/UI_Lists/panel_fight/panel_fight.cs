using Common;
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
    /// ���Ԥ����
    /// </summary>
    private GameObject player_battle_attack_prefabs;

    private GameObject monster_battle_attack_prefabs;

    /// <summary>
    /// ��������λ��
    /// </summary>
    private Transform pos_player, pos_monster;
    /// <summary>
    /// ��ǰ�����б�
    /// </summary>
    private List<crtMaxHeroVO> crt_map_monsters = new List<crtMaxHeroVO>();
    /// <summary>
    /// ��ǰ��ͼ
    /// </summary>
    private user_map_vo select_map;
    /// <summary>
    /// ˢ��״̬
    /// </summary>
    private bool Open_Monster_State= true;
    /// <summary>
    /// ˢ�¼���
    /// </summary>
    private pight_show_skill pight_show_skill;
    /// <summary>
    /// ս������
    /// </summary>
    private List<skill_offect_item> battle_skills;
    /// <summary>
    /// ��ʾ������Ϣ
    /// </summary>
    private panel_role_health role_health;
    /// <summary>
    /// ��ʾ��ͼ���ƺͻغ�ʱ��
    /// </summary>
    private Text map_name, map_time;
    /// <summary>
    /// �ر��б�ť
    /// </summary>
    private Button close_btn;
    /// <summary>
    /// �Ƿ�ر��б�
    /// </summary>
    private bool close_panel_state = false;
    /// <summary>
    /// �б�λ��
    /// </summary>
    private Transform pos_btn;
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
        pight_show_skill = Find<pight_show_skill>("skill_list");
        role_health = Find<panel_role_health>("panel_role_health");
        map_name = Find<Text>("battle_pos/map_name/info");
        map_time = Find<Text>("battle_pos/map_name/show_time");
        close_btn = Find<Button>("btn_list/close_btn");
        close_btn.onClick.AddListener(() => { Close(); });
        pos_btn = Find<Transform>("btn_list");
    }
    /// <summary>
    /// �ر��б�
    /// </summary>
    private void Close()
    {
        close_panel_state=!close_panel_state;
        for (int i = 1; i < pos_btn.childCount; i++)
        { 
            pos_btn.GetChild(i).gameObject.SetActive(close_panel_state);
        }
        //close_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>(close_panel_state ? "UI/panel_fight/btn_list_open" : "UI/panel_fight/btn_list_close");
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
    /// ��Ϸ����
    /// </summary>
    protected void Game_Over()
    {
        if (Open_Monster_State)
        {
            Open_Monster_State = false;
            //���븴������ 5s
            StopAllCoroutines();
            StartCoroutine(Game_WaitTime(5));
        }
    }

    public void Open_Map(user_map_vo map)
    { 
        select_map = map;
        init();
        Crate_Init();
        map_name.text=map.map_name;
        Show_Battle_State("ս����...");
    }
    /// <summary>
    /// ��ʾս��״̬
    /// </summary>
    /// <param name="dec"></param>
    private void Show_Battle_State(string dec)
    {
        map_time.text= dec;
    }
    /// <summary>
    /// ��ȡ��������
    /// </summary>
    public void offline()
    {
        //������������
        //����ս��
        Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_name == SumSave.crt_resources.user_map_index));
    }
    /// <summary>
    /// ���ɽ�ɫ��ʼ��
    /// </summary>
    private void Crate_Init()
    {
        crate_Skill();
        crate_hero();
        crate_monster();
    }

    public void Refresh_Max_Hero_Attribute()
    {
        crate_Skill();
        foreach (var item in SumSave.battleHeroHealths)
        {
            item.GetComponent<BattleAttack>().Refresh(SumSave.crt_MaxHero);
            item.GetComponent<BattleAttack>().Refresh_Skill(battle_skills);
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
        // ����Data
        item.GetComponent<player_battle_attck>().Data = crt;
        item.GetComponent<player_battle_attck>().Refresh_Skill(battle_skills);
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.������Ʒ); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleHeroHealths.Add(item.GetComponent<BattleHealth>());
        role_health.SetHealth(item.GetComponent<BattleHealth>());
    }

    /// <summary>
    /// ��ʼ��
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
    /// �����ȴ�
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Game_WaitTime(float time)
    {
        
        while (time > 0)
        {
            Show_Battle_State("����ʣ��ʱ�� " + time.ToString()[..Math.Min(2, time.ToString().Length)]);
            time -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Show_Battle_State("ս����...");
        Game_Start();
    }
    /// <summary>
    /// ��ʼ��Ϸ
    /// </summary>
    public void Game_Start()
    {
        StartCoroutine(ProduceMonster(SumSave.WaitTime));
    }
    // ��һ����
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
            Show_Battle_State("ˢ��ʱ�� " + time.ToString("F1") +"s") ;//[..Math.Min(2, time.ToString().Length)]); ;
            time -= basetime;
            yield return new WaitForSeconds(basetime);
        }
        Show_Battle_State("ս����...");
        crate_monster();
        //StartCoroutine(ProduceMonster(SumSave.WaitTime));
    }

    private void crate_monster()
    {
        crtMaxHeroVO crt = crt_map_monsters[Random.Range(0, crt_map_monsters.Count)];
        crt = Battle_Tool.crate_monster(crt);
        GameObject item = ObjectPoolManager.instance.GetObjectFormPool(crt.show_name, monster_battle_attack_prefabs,
            new Vector3(pos_monster.position.x, pos_monster.position.y,pos_monster.position.z), Quaternion.identity, pos_monster);
        // ����Data
        item.GetComponent<monster_battle_attck>().Data = crt;
        //if (item.GetComponent<Button>().enabled)
        //    item.GetComponent<Button>().onClick.AddListener(delegate { AudioManager.Instance.playAudio(ClipEnum.������Ʒ); SelectMonster(item.GetComponent<MonsterBattleAttack>()); });
        //item.GetComponent<Button>().enabled = true;
        SumSave.battleMonsterHealths.Add(item.GetComponent<BattleHealth>());
        Open_Monster_State=true;
    }
}
