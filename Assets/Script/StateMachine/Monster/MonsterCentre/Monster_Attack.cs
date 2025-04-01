using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class Monster_Attack : MonsterState
    {
        public Monster_Attack(Monster _monster, MonsterStateMachine _sateManage, string _animBoolName) : base(_monster, _sateManage, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter(); 
           
            
        }

        public override void Exit()
        {
            base.Exit();
            monster.anim.speed = 1;
        }

        public override void Update()
        {
            base.Update();
            //monster.anim.speed = monster.animSpeed;
            //startTime -= Time.deltaTime;
            //monster.animStateInfo = monster.anim.GetCurrentAnimatorStateInfo(0);//需要在每一帧更新动画状态信息
            //if (startTime <=0)
            //{
            //    monster.BattleAttack.OnAuto();
            //    startTime = monster.animStateInfo.length;
            //}
        }
    }
}

