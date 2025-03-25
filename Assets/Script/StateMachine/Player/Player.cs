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

        #endregion


        protected override void Awake()
        {
            base.Awake();
            #region ״̬��ʼ��
            stateMachine = new PlayerstateMachine();
            idleState = new Player_Idle(this, stateMachine, "Idle");
            moveState = new Player_Move(this, stateMachine, "Move");
            attackState = new Player_Attack(this, stateMachine, "Attack");
            skillManagerState = new PlayerSkillManager(this, stateMachine, "Storage");
            skillHuoQiuState = new PlayerSkill_HuoQiu(this, stateMachine, "Storage");
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

        public void GetSkill(Skill_Collision skill)//�ص�����
        {
            skill.SetSkillTarget(TatgetObg,skill_damage);
        }


        public override void Init(float attack_speed, float attack_distance, float move_speed, BattleHealth _tatgetObg)
        {
            base.Init(attack_speed, attack_distance, move_speed, _tatgetObg);
            stateMachine.ChangeState(skillHuoQiuState);
        }
    }
}

