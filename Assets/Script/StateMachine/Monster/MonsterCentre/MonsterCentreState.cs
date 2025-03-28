using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class MonsterCentreState
    {
        public MonsterCentre monsterCentre;
        public MonsterCentreStateMachine stateManage;
        public string animBoolName;
        protected float startTime = 0;


        public MonsterCentreState(MonsterCentre _monster, MonsterCentreStateMachine _sateManage, string _animBoolName)
        {
            this.monsterCentre = _monster;
            this.stateManage = _sateManage;
            this.animBoolName = _animBoolName;
        }


        public virtual void Enter()
        {
            monsterCentre.anim.SetBool(animBoolName, true);
            monsterCentre.FlipControl(monsterCentre.Direction(monsterCentre.TargetPosition));
        }

        
        public virtual void Update()
        {
          
            if (monsterCentre.IsCentre) //如果怪物是中心怪，并且玩家在攻击范围内
            {
                if (monsterCentre.isAttackDistance())//进入怪物攻击距离
                {
                    stateManage.ChangeState(monsterCentre.attackState);
                }
                else
                {
                    stateManage.ChangeState(monsterCentre.moveState);
                }
            }
            else
            {
                stateManage.ChangeState(monsterCentre.moveState);
            }


        }

        public virtual void Exit()
        {
            monsterCentre.anim.SetBool(animBoolName, false);
        }

    }
}
