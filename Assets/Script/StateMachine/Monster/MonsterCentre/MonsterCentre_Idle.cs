using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class MonsterCentre_Idle : MonsterCentreState
    {
        public MonsterCentre_Idle(MonsterCentre _monster, MonsterCentreStateMachine _sateMachine, string _animBoolName) : base(_monster, _sateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            monsterCentre.rb.velocity = Vector2.zero;

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            

        }


    }
}

