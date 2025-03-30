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
        public Player_Basic basicState { get; private set; }//基本状态(子类_move,_idle)
        #endregion

        #region 技能
        [SerializeField] public Transform skillStoragePos;//技能储存位置
        public GameObject skills_HuoQiu;//火球技能
        public float skill_damage = 123f;//火球伤害
        public float skill_release_pro = 30f;//技能释放概率

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
            basicState = new Player_Basic(this, stateMachine, "Idle");
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

        public void GetSkill(Skill_Collision _skill)//回调函数
        {
            _skill.SetSkillTarget(TatgetObg, baseskill);
        }


        public override void Init(float attack_speed, float attack_distance, float move_speed, BattleHealth _tatgetObg,BattleAttack battle)
        {
            base.Init(attack_speed, attack_distance, move_speed, _tatgetObg,battle);
           
        }


        public override void stateAutoInit(base_skill_vo skill_name = null)
        {
            base.stateAutoInit(skill_name);
            if (skill_name == null)//平a
            {
                stateMachine.ChangeState(attackState);
            }
            else //if(skill_name== "火球术" 判断预制体
            {
                stateMachine.ChangeState(skillHuoQiuState);
            }
        }
        /// <summary>
        /// 启动携程
        /// </summary>
        public void StartAnimation()
        {
            StartCoroutine(PlayAndWaitForAnimation());
        }
        /// <summary>
        /// 播放动画并等待动画结束
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayAndWaitForAnimation()
        {
            anim.speed = 1.0f;
            
            // 记录动画开始时间
            float startTime = Time.time;
            float animationLength =anim.GetCurrentAnimatorStateInfo(0).length;

            // 等待动画播放完成
            while (Time.time - startTime < animationLength)
            {
                yield return null;
            }

            // 动画播放完毕后的逻辑
           BattleAttack.OnAuto();
        }


    }
}

