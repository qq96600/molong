using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class Monster_Move : MonsterState
    {
        public Monster_Move(Monster _monster, MonsterStateMachine _sateMachine, string _animBoolName) : base(_monster, _sateMachine, _animBoolName)
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


            if (monster.IsCentre)
            {
                monster.TargetMove(monster.TargetPosition);
            }else
            {
                monster.TargetMove(monster.BackstabPosition);

                if ((Vector2)monster.transform.position == monster.BackstabPosition)
                    stateMachine.ChangeState(monster.attackState);
            }
                
            
        }
       
    }


}
