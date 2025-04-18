using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace StateMachine
{
    public class Player : RolesManage
    {
        #region ״̬
        public PlayerstateMachine stateMachine { get; private set; }//״̬��
        public Player_Idle idleState { get; private set; }//����״̬
        public Player_Move moveState { get; private set; }//�ƶ�״̬
        public Player_Attack attackState { get; private set; }//����״̬
        public PlayerSkillManager skillManagerState { get; private set; }//���ܹ�����
        public PlayerSkill_HuoQiu skillHuoQiuState { get; private set; }//������

        #endregion

        #region ����
        [SerializeField] public Transform skillStoragePos;//���ܴ���λ��
        public GameObject skills_HuoQiu;//������
        public float skill_damage = 123f;//�����˺�
        public float skill_release_pro = 30f;//�����ͷŸ���

        #endregion


    

        protected override void Awake()
        {
            base.Awake();
            #region ״̬��ʼ��
            stateMachine = new PlayerstateMachine();
            idleState = new Player_Idle(this, stateMachine, "Idle");
            moveState = new Player_Move(this, stateMachine, "Move");
            attackState = new Player_Attack(this, stateMachine, "Attack");
            skillManagerState = new PlayerSkillManager(this, stateMachine, "Skill");
            skillHuoQiuState = new PlayerSkill_HuoQiu(this, stateMachine, "Skill");
            #endregion

            #region ���ܳ�ʼ��
            skills_HuoQiu = Resources.Load<GameObject>("Prefabs/panel_skill/Skill_Effects/HuoQiu");
            skillStoragePos=GameObject.Find("Skills").transform;
            #endregion

        }

       

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialized(idleState);

        }

        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
         
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public override void Animator_State(Arrow_Type arrowType)
        {     
            base.Animator_State(arrowType);
            switch (arrowType)
            {
                case Arrow_Type.idle:
                    stateMachine.ChangeState(idleState);
                    break;
                case Arrow_Type.move:
                    stateMachine.ChangeState(moveState);   
                    break;
                case Arrow_Type.attack:
                    stateMachine.ChangeState(attackState);
                    break;
                default:
                    break;
            }
        }






        //public void GetSkill(Skill_Collision _skill)//�ص�����
        //{
        //    _skill.SetSkillTarget(TatgetObg, baseskill);
        //}


        public override void Init( BattleAttack battle, BattleHealth _tatgetObg)
        {
            base.Init( battle, _tatgetObg);
           
           
        }


    }
}

