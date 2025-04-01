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
    /// ս��ģʽ
    /// </summary>
    /// 
    public class BattleAttack : Base_Mono
    {
        /// <summary>
        /// ��ȡѪֵ
        /// </summary>
        [HideInInspector]
        public BattleHealth target;
        /// <summary>
        /// Ŀ��
        /// </summary>
        protected BattleHealth Terget;

        protected Image frame, icon;

        protected Image targetIcon;
        /// <summary>
        /// �����б�
        /// </summary>
        protected List<skill_offect_item> battle_skills;
        /// <summary>
        /// ���� �ƺ�
        /// </summary>
        protected Text Name, sliderInfo, damageInfo;
        /// <summary>
        /// ������
        /// </summary>
        protected Slider show_hp;

        /// <summary>
        /// ��ɫ״̬��
        /// </summary>
        public AttackStateMachine AttackStateMachine;
        public RolesManage StateMachine;
        /// <summary>
        /// ˢ������
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
            }
            get
            {
                return data;
            }
        }
        /// <summary>
        /// ָ��Ŀ��
        /// </summary>
        /// <param name="health"></param>
        public void SpecifyTarget(BattleHealth health)
        {
            Terget = health;
        }

        public void injured()
        {
            //������Ч
        }

        protected int SkillInfo = 0;
        /// <summary>
        /// �����ٶ�
        /// </summary>
        protected float baseSpeed = 2f;
        /// <summary>
        /// �Զ�ս��
        /// </summary>
        public virtual void OnAuto()
        {

        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public virtual void Instace()
        {

        }

        public void Lose_Terget()
        {
          
            Find_Terget();
         
        }

        /// <summary>
        /// Ѱ�ҹ�������
        /// </summary>
        protected virtual void Find_Terget()
        {
           if(GetComponent<Player>() != null)

            {
                if (SumSave.battleMonsterHealths.Count > 0  )//����ҹ���
                {
                    //Ѱ�Ҿ������������Ŀ��    
                    Terget = ArrayHelper.GetMin(SumSave.battleMonsterHealths, e => Vector2.Distance(transform.position, e.transform.position));

                    AttackStateMachine.Init(this, Terget);
                    StateMachine.Init(this, Terget);
                    //play_move.Instantiate(this);
                    //if (Terget != null) play_move.anto(Terget);
                    //else
                    //{
                    //    StartCoroutine(HideFrame());
                    //}
                }
                else Game_Next_Map();
            }
            else if (GetComponent<Monster>() != null)
            {
                if (SumSave.battleHeroHealths.Count > 0 )//���������
                {
                    //Ѱ�Ҿ������������Ŀ��    
                    Terget = ArrayHelper.GetMin(SumSave.battleHeroHealths, e => Vector2.Distance(transform.position, e.transform.position));
                    StateMachine.Init(this, Terget);
                   

                }
                else game_over();
            }
        }
        /// <summary>
        /// ������һ�غ�
        /// </summary>
        private void Game_Next_Map()
        {
            transform.parent.parent.parent.SendMessage("Game_Next_Map");

        }
        /// <summary>
        /// ��Ϸ����
        /// </summary>
        private void game_over()
        {
            transform.parent.parent.parent.SendMessage("Game_Over");
        }

        /// <summary>
        /// ��ʾ��Ϣ
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
        /// �رչ⻷
        /// </summary>
        /// <returns></returns>
        IEnumerator HideFrame()
        {
            yield return new WaitForSeconds(1f);
            frame.gameObject.SetActive(false);
        }
    }
}
