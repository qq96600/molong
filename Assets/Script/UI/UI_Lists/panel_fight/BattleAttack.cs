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
        /// ����
        /// </summary>
        public int BaseParalysis = 0;
        /// <summary>
        /// ���ɻ껷
        /// </summary>
        protected bool Zodiac = true;

        //protected List<MonsterBattleAttack> monsters = new List<MonsterBattleAttack>();

        //protected List<playerBattleAttack> players = new List<playerBattleAttack>();
        /// <summary>
        /// �����б�
        /// </summary>
        [HideInInspector]
        //public SkillBattleItem[] skills = new SkillBattleItem[] { };

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
        /// ָ��Ŀ��
        /// </summary>
        /// <param name="health"></param>
        public void SpecifyTarget(BattleHealth health)
        {
            Terget = health;
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
            frame.color = Color.red;
            //play_move.anto_State();
            StartCoroutine(HideFrame());
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
<<<<<<< HEAD
            data.index = -1;
            if (data.index == -1)
=======
           // if (data.index == -1)
           if(GetComponent<Player>() != null)
>>>>>>> d23897bd1b0c2aa1dee2b0e1c68244f79093bb9b
            {
                if (SumSave.battleMonsterHealths.Count > 0  )//����ҹ���
                {
                    //Ѱ�Ҿ������������Ŀ��    
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
                if (SumSave.battleHeroHealths.Count > 0 )//���������
                {
                    //Ѱ�Ҿ������������Ŀ��    
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
            yield return new WaitForSeconds(0.5f);
            frame.color = Color.white;
        }
    }
}
