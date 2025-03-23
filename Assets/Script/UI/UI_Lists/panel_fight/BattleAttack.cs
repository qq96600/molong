using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MVC
{
    /// <summary>
    /// 战斗模式
    /// </summary>
    /// 
    public class BattleAttack : Base_Mono
    {
        /// <summary>
        /// 获取血值
        /// </summary>
        [HideInInspector]
        public BattleHealth target;
        /// <summary>
        /// 目标
        /// </summary>
        protected BattleHealth Terget;

        protected Image frame, icon;

        protected Image targetIcon;


        /// <summary>
        /// 抗性
        /// </summary>
        public int BaseParalysis = 0;
        /// <summary>
        /// 生成魂环
        /// </summary>
        protected bool Zodiac = true;

        //protected List<MonsterBattleAttack> monsters = new List<MonsterBattleAttack>();

        //protected List<playerBattleAttack> players = new List<playerBattleAttack>();
        /// <summary>
        /// 技能列表
        /// </summary>
        [HideInInspector]
        //public SkillBattleItem[] skills = new SkillBattleItem[] { };

        /// <summary>
        /// 名称 称号
        /// </summary>
        protected Text Name, sliderInfo, damageInfo;
        /// <summary>
        /// 计数器
        /// </summary>
        protected Slider show_hp;

        /// <summary>
        /// 角色状态机
        /// </summary>
        public RolesManage StateMachine;
        
        
        public virtual void Awake()
        {
            target = GetComponent<BattleHealth>();


            StateMachine = GetComponent<RolesManage>();
            frame = transform.GetComponent<Image>();
            //icon = Find<Image>("icon");
            //Name = Find<Text>("nameinfo");
            show_hp = Find<Slider>("Slider");
            //damageInfo = Find<Text>("damageInfo");
        }

        protected crtMaxHeroVO data;
        /// <summary>
        /// Data
        /// </summary>
        public crtMaxHeroVO Data
        {
            set
            {
                data = value;
                if (data == null) return;
                target.maxHP = data.MaxHP;
                target.HP = data.MaxHP;
                show_hp.maxValue = target.maxHP;
                show_hp.value = target.HP;
                BaseParalysis = 0;

            }
            get
            {
                return data;
            }
        }
        /// <summary>
        /// 指定目标
        /// </summary>
        /// <param name="health"></param>
        public void SpecifyTarget(BattleHealth health)
        {
            Terget = health;
        }

        protected int SkillInfo = 0;
        /// <summary>
        /// 基础速度
        /// </summary>
        protected float baseSpeed = 2f;
        /// <summary>
        /// 自动战斗
        /// </summary>
        public virtual void OnAuto()
        {
            frame.color = Color.red;
            //play_move.anto_State();
            StartCoroutine(HideFrame());
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public virtual void Instace()
        {

        }

        public void Lose_Terget()
        {
          
            Find_Terget();
         
        }

        /// <summary>
        /// 寻找攻击对象
        /// </summary>
        protected virtual void Find_Terget()
        {
<<<<<<< HEAD
            data.index = -1;
            if (data.index == -1)
=======
           // if (data.index == -1)
           if(GetComponent<Player>() != null)
>>>>>>> d23897bd1b0c2aa1dee2b0e1c68244f79093bb9b
            {
                if (SumSave.battleMonsterHealths.Count > 0  )//玩家找怪物
                {
                    //寻找距离自身最近的目标    
                    Terget = ArrayHelper.GetMin(SumSave.battleMonsterHealths, e => Vector2.Distance(transform.position, e.transform.position));
                    
                    StateMachine.Init(data.attack_speed, data.attack_distance, data.move_speed, Terget);
                    //play_move.Instantiate(this);
                    //if (Terget != null) play_move.anto(Terget);
                    //else
                    //{
                    //    StartCoroutine(HideFrame());
                    //}
                }
                else Game_Next_Map();
            }
            else if (GetComponent<MonsterCentre>() != null)
            {
                if (SumSave.battleHeroHealths.Count > 0 )//怪物找玩家
                {
                    //寻找距离自身最近的目标    
                    Terget = ArrayHelper.GetMin(SumSave.battleHeroHealths, e => Vector2.Distance(transform.position, e.transform.position));

                    StateMachine.Init(data.attack_speed, data.attack_distance, data.move_speed, Terget);
                    //if (Terget != null) play_move.anto(Terget);
                    //else
                    //{
                    //    StartCoroutine(HideFrame());
                    //}
                }
                else game_over();
            }
        }
        /// <summary>
        /// 进行下一回合
        /// </summary>
        private void Game_Next_Map()
        {
            transform.parent.parent.parent.SendMessage("Game_Next_Map");

        }
        /// <summary>
        /// 游戏结束
        /// </summary>
        private void game_over()
        {
            transform.parent.parent.parent.SendMessage("Game_Over");
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="dec"></param>
        protected virtual void Show_Info(string dec)
        {
            if (transform.parent.parent.parent.parent) transform.parent.parent.parent.parent.SendMessage("show_info", dec);
        }

        public virtual void Update()
        {
            if (data != null)
            {
                if (Terget != null)
                {
                    if (Terget.HP <= 0 || !Terget.gameObject.activeInHierarchy) Find_Terget();
                }
                else Find_Terget();
                show_hp.value = target.HP;
            }
        }

        /// <summary>
        /// 关闭光环
        /// </summary>
        /// <returns></returns>
        IEnumerator HideFrame()
        {
            yield return new WaitForSeconds(0.5f);
            frame.color = Color.white;
        }
    }
}
