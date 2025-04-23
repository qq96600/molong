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
        /// 技能列表
        /// </summary>
        protected List<skill_offect_item> battle_skills;
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
        public AttackStateMachine AttackStateMachine;
        public RolesManage StateMachine;

        private Text name_text;
        /// <summary>
        /// 刷新属性
        /// </summary>
        /// <param name="hero"></param>
        public virtual void Refresh(crtMaxHeroVO hero)
        { 

        }

        public virtual void Refresh_Skill(List<skill_offect_item> skills)
        { 
        
        }
        public virtual void Awake()
        {
            target = GetComponent<BattleHealth>();
            AttackStateMachine = GetComponent<AttackStateMachine>();
            StateMachine= GetComponent<RolesManage>();
            frame = Find<Image>("frame");
            show_hp = Find<Slider>("Slider");
            name_text = Find<Text>("base_info/info");
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
                frame.gameObject.SetActive(false);
                target.maxHP = data.MaxHP;
                target.HP = data.MaxHP;
                show_hp.maxValue = target.maxHP;
                show_hp.value = target.HP;
                target.maxMP= data.MaxMp;
                target.MP= data.MaxMp;
                if (data.monster_attrList.Count > 0)
                {
                    name_text.text = (enum_monster_state)data.monster_attrList[0] + " " + data.show_name;
                }
                else name_text.text = data.show_name;
            }
            get
            {
                return data;
            }
        }
        /// <summary>
        /// 对目标造成伤害
        /// </summary>
        /// <param name="skill"></param>
        public void skill_damage(base_skill_vo skill) 
        {
            float damage = 0f;
            BattleAttack monster = Terget.GetComponent<BattleAttack>();
            if (monster.target.HP <= 0) return;//结战斗
            if (Data.Type == 1)
            {
                damage = Random.Range(Data.damageMin, Data.damageMax) - Random.Range(monster.Data.DefMin, monster.Data.DefMax);
            }
            else
            if (Data.Type == 2)
            {
                damage = Random.Range(Data.MagicdamageMin, Data.MagicdamageMax) - Random.Range(monster.Data.MagicDefMin, monster.Data.MagicDefMax);
            }
            if (Random.Range(0, 100) > Data.hit - monster.Data.dodge)
            {
                //传递消息，未命中;
                monster.target.TakeDamage(1, DamageEnum.技能未命中, monster);
                return;
            }
            damage = damage * (skill.skill_damage + (skill.skill_power * int.Parse(skill.user_values[1]))) / 100;

            bool isCrit = false;
            if (Random.Range(0, 100) > data.crit_rate - monster.Data.resistance)
            {
                isCrit = true;
                damage = damage * data.crit_damage / 100;
            }
            damage = 100;
            monster.target.TakeDamage(damage, isCrit ? DamageEnum.暴击技能伤害 : DamageEnum.技能伤害, monster);
        }

        /// <summary>
        /// 指定目标
        /// </summary>
        /// <param name="health"></param>
        public void SpecifyTarget(BattleHealth health)
        {
            Terget = health;
        }

        public void injured()
        {
            //播放音效
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
           if(GetComponent<Player>() != null)

            {
                if (SumSave.battleMonsterHealths.Count > 0)//玩家找怪物
                {
                    //寻找距离自身最近的目标    
                    Terget = ArrayHelper.GetMin(SumSave.battleMonsterHealths, e => Vector2.Distance(transform.position, e.transform.position));

                    // StateMachine.Init(data.attack_speed, data.attack_distance, data.move_speed, Terget,this);


                    AttackStateMachine.Init(this, Terget);
                    StateMachine.Init(this, Terget);
                }
                else
                {
                   // StateMachine.Animator_State(Arrow_Type.idle);
                    Game_Next_Map();
                }
            }
            else if (GetComponent<Monster>() != null)
            {
                if (SumSave.battleHeroHealths.Count > 0 )//怪物找玩家
                {
                    //寻找距离自身最近的目标    
                    Terget = ArrayHelper.GetMin(SumSave.battleHeroHealths, e => Vector2.Distance(transform.position, e.transform.position));
                    AttackStateMachine.Init(this, Terget);
                    StateMachine.Init(this, Terget);
                   

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
            if (data != null)//判断是否有怪物
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
            yield return new WaitForSeconds(1f);
            frame.gameObject.SetActive(false);
        }
    }
}
