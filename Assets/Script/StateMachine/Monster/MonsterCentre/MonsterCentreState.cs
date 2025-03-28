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
          
            if (monsterCentre.IsCentre) //������������Ĺ֣���������ڹ�����Χ��
            {
                if (monsterCentre.isAttackDistance())//������﹥������
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
