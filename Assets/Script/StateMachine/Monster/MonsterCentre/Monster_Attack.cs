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
            monster.RbZero();
            monster.anim.SetBool(animBoolName, true);
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            startTime -= Time.deltaTime;
            monster.animStateInfo = monster.anim.GetCurrentAnimatorStateInfo(0);//��Ҫ��ÿһ֡���¶���״̬��Ϣ
            if (startTime <=0)
            {
                monster.BattleAttack.OnAuto();
                startTime = monster.animStateInfo.length;
            }
        }
    }
}

