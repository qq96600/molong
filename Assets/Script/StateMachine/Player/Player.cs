using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Player :RolesManage
    {

        #region ״̬
        public PlayerstateMachine stateMachine { get; private set; }//״̬��
        public Player_Idle idleState { get; private set; }//����״̬
        public Player_Move moveState { get; private set; }//�ƶ�״̬
        public Player_Attack attackState { get; private set; }//����״̬
        #endregion
        protected override void Awake()
        {
            base.Awake();
            #region ״̬��ʼ��
            stateMachine = new PlayerstateMachine();
            idleState = new Player_Idle(this, stateMachine, "Idle");
            moveState = new Player_Move(this, stateMachine, "Move");
            attackState = new Player_Attack(this, stateMachine, "Attack");
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

        public override void Init(float attack_speed, float attack_distance, float move_speed, Transform monsterPosition)
        {
            base.Init(attack_speed, attack_distance, move_speed, monsterPosition);
            stateMachine.ChangeState(moveState);
        }

    }
}

