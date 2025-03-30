using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Monster_Basic : MonsterState
    {
        public Monster_Basic(Monster _monster, MonsterStateMachine _sateManage, string _animBoolName) : base(_monster, _sateManage, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (monster.IsCentre) //如果怪物是中心怪，并且玩家在攻击范围内
            {
                if (monster.isAttackDistance())//进入攻击距离
                {
                    stateMachine.ChangeState(monster.attackState);
                }
                else
                {
                    stateMachine.ChangeState(monster.moveState);
                }
            }
            else
            {
                stateMachine.ChangeState(monster.moveState);
            }

        }
    }
}


