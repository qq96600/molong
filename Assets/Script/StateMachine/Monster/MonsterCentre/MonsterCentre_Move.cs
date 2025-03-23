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

            if (monsterCentre.IsCentre) //������������Ĺ֣���������ڹ�����Χ��
            {
                monsterCentre.TargetMove(monsterCentre.TargetPosition);

                if (monsterCentre.isAttackDistance())//������﹥������
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
