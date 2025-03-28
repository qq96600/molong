using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class MonsterState
    {
        public Monster monster;
        public MonsterStateMachine stateMachine;
        public string animBoolName;
        protected float startTime = 0;


        public MonsterState(Monster _monster, MonsterStateMachine _sateManage, string _animBoolName)
        {
            this.monster = _monster;
            this.stateMachine = _sateManage;
            this.animBoolName = _animBoolName;
        }


        public virtual void Enter()
        {
            monster.anim.SetBool(animBoolName, true);
            monster.FlipControl(monster.Direction(monster.TargetPosition));
        }

        
        public virtual void Update()
        {
          
            if (monster.IsCentre) //如果怪物是中心怪，并且玩家在攻击范围内
            {
                if (monster.isAttackDistance())//进入怪物攻击距离
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

        public virtual void Exit()
        {
            monster.anim.SetBool(animBoolName, false);
        }

    }
}
