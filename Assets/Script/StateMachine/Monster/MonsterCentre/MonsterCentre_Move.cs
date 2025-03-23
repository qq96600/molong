using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class MonsterCentre_Move : MonsterCentreState
    {
        public MonsterCentre_Move(MonsterCentre _monster, MonsterCentreStateMachine _sateMachine, string _animBoolName) : base(_monster, _sateMachine, _animBoolName)
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

            if (monsterCentre.IsCentre) //如果怪物是中心怪，并且玩家在攻击范围内
            {
                monsterCentre.TargetMove(monsterCentre.TargetPosition);

                if (monsterCentre.isAttackDistance())//进入怪物攻击距离
                {

                    stateManage.ChangeState(monsterCentre.attackState);
                }
            }
            else
            {
                monsterCentre.TargetMove(monsterCentre.BackstabPosition);
                //stateManage.ChangeState(monsterCentre.attackState);
            }

        }
       
    }


}
