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
        /// 生命值显示文本
        /// </summary>
        protected Text hp_text;

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
            hp_text = Find<Text>("Slider/Hp_text");
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

                hp_text.text = target.HP + "/" + target.maxHP;

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
                hp_text.text = target.HP + "/" + target.maxHP;
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

        /// <summary>
        /// 对目标造成伤害
        /// </summary>
        /// <param name="skill"></param>
        public void skill_damage(base_skill_vo skill)
        {
            BattleAttack monster = Terget.GetComponent<BattleAttack>();
            if (monster.target.HP <= 0) return;//结战斗
            float damage = Base_Damage(monster);
            damage = damage * (skill.skill_damage + (skill.skill_power * int.Parse(skill.user_values[1]))) / 100;
            if (iSnHit(monster))
            {
                //传递消息，未命中;
                monster.target.TakeDamage(1, DamageEnum.技能未命中);
                return;
            }
            bool isCrit = isCrate(monster);
            if (isCrit)
            {
                damage = damage * data.crit_damage / 100;
            }
            monster.target.TakeDamage(damage, isCrit ? DamageEnum.暴击技能伤害 : DamageEnum.技能伤害);
        }

        protected virtual void BaseAttack()//判断伤害
        {
            BattleAttack monster = Terget.GetComponent<BattleAttack>();
            if (monster.target.HP <= 0) return;//结战斗
            float damage = Base_Damage(monster);
            if (iSnHit(monster))
            {
                monster.target.TakeDamage(1, DamageEnum.未命中);
                return;
            }
            bool isCrit = iSnHit(monster);
            if (isCrit)
            {
                damage = damage * data.crit_damage / 100;
            }
            monster.target.TakeDamage(damage, isCrit ? DamageEnum.暴击伤害 : DamageEnum.普通伤害);
            if (data.Real_harm > 0)
            {
                monster.target.TakeDamage(data.Real_harm, DamageEnum.真实伤害);
            }
        }

        private int Base_Damage(BattleAttack monster)
        {
            float damage = 0f;
            if (Data.Type == 1)
            {
                damage = Lucky(Data.damageMin, Data.damageMax, data.Lucky) -
                    (Random.Range(monster.Data.DefMin, monster.Data.DefMax) * (100 + monster.Data.bonus_Def) / 100);
                damage = damage * (100 + data.bonus_Damage) / 100;
            }
            else
            if (Data.Type == 2)
            {
                damage = Lucky(Data.MagicdamageMin, Data.MagicdamageMax, data.Lucky) -
                    (Random.Range(monster.Data.MagicDefMin, monster.Data.MagicDefMax) * (100 + monster.Data.bonus_MagicDef) / 100);
                damage = damage * (100 + data.bonus_MagicDamage) / 100;
            }
            if (monster.Data.Damage_absorption > 0)
            {
                damage = damage * (100 - monster.Data.Damage_absorption) / 100;
            }
            damage = damage * (100 + penetrate(monster)) / 100;
            damage = damage * (data.double_damage - monster.Data.double_damage) / 100;
            return (int)damage;
        }
        /// <summary>
        /// 判断命中
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private bool iSnHit(BattleAttack monster)
        {
            bool exist= false;
            if (Data.hit < monster.Data.dodge && Random.Range(0, 100) > 10)//命中不达标也有10%的概率
            {
                //传递消息，未命中;
                exist = true;
            }
            return exist;
        }

        /// <summary>
        /// 判断是否暴击
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private bool isCrate(BattleAttack monster)
        {
            bool isCrit= false;
            if (Random.Range(0, 100) > data.crit_rate - monster.Data.crit_rate)
            {
                isCrit = true;
            }
            return isCrit;
        }
        /// <summary>
        /// 幸运加成
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="lucky"></param>
        private int Lucky(int min, int max, int lucky)
        {
            int value = 0;
            value = Random.Range(min + (max - min) * lucky / 10, max);
            if (lucky > 10)
            {
                value = value * (100 + (lucky * 10)) / 100;
            }
            return value;
        }
        /// <summary>
        /// 计算穿透效果 
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private int penetrate(BattleAttack monster)
        {
            int value = (data.penetrate - monster.Data.block) * 100 / (data.penetrate + 500);
            if (value < 0) value = 0;
            return value;
        }
    }
}
