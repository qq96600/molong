using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Monster :RolesManage
    {
        [Header("是否为中间怪")]
        public bool IsCentre = true;

        #region 状态
        public MonsterStateMachine stateMachine { get; private set; }//状态机
        public Monster_Move moveState { get; private set; }//移动状态
        public Monster_Idle idleState { get; private set; }//空闲状态

        public Monster_Attack attackState { get; private set; }//攻击状态
        #endregion
        protected override void Awake()
        {
            base.Awake();
            #region 状态初始化
            stateMachine = new MonsterStateMachine();
            moveState=new Monster_Move(this,stateMachine,"Move");
            idleState = new Monster_Idle(this, stateMachine, "Idle");
            attackState = new Monster_Attack(this, stateMachine, "Attack");
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
        /// 数据传输
        /// </summary>
        /// <param name="attack_speed"></param>
        /// <param name="attack_distance"></param>
        /// <param name="move_speed"></param>
        /// <param name="_tatgetObg"></param>
        /// <param name="battle"></param>
        public override void Init(float attack_speed, float attack_distance, float move_speed, BattleHealth _tatgetObg,BattleAttack battle)
        {
            base.Init(attack_speed, attack_distance, move_speed, _tatgetObg, battle);

           
        }

        /// <summary>
        /// 判定使用什么攻击方式
        /// </summary>
        /// <param name="skill_name"></param>
        public override void stateAutoInit(base_skill_vo skill_name = null)
        {
            base.stateAutoInit(skill_name);
            stateMachine.ChangeState(attackState);
        
        }

      

    }

}



