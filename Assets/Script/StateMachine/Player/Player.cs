using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace StateMachine
{
    public class Player : RolesManage
    {

        #region 状态
        public PlayerstateMachine stateMachine { get; private set; }//状态机
        public Player_Idle idleState { get; private set; }//空闲状态
        public Player_Move moveState { get; private set; }//移动状态
        public Player_Attack attackState { get; private set; }//攻击状态
        public PlayerSkillManager skillManagerState { get; private set; }//技能管理器
        public PlayerSkill_HuoQiu skillHuoQiuState { get; private set; }//火球技能
        #endregion

        #region 技能
        [SerializeField] public Transform skillStoragePos;//技能储存位置
        public GameObject skills_HuoQiu;//火球技能
        public float skill_damage = 123f;//火球伤害

        #endregion


        protected override void Awake()
        {
            base.Awake();
            #region 状态初始化
            stateMachine = new PlayerstateMachine();
            idleState = new Player_Idle(this, stateMachine, "Idle");
            moveState = new Player_Move(this, stateMachine, "Move");
            attackState = new Player_Attack(this, stateMachine, "Attack");
            skillManagerState = new PlayerSkillManager(this, stateMachine, "Storage");
            skillHuoQiuState = new PlayerSkill_HuoQiu(this, stateMachine, "Storage");
            #endregion

            #region 技能初始化
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

        public void GetSkill(Skill_Collision skill)//回调函数
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

