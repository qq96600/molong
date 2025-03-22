using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class MonsterCentre_Attack : MonsterCentreState
    {
        public MonsterCentre_Attack(MonsterCentre _monster, MonsterCentreStateMachine _sateManage, string _animBoolName) : base(_monster, _sateManage, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            monsterCentre.RbZero();
            //monsterCentre.BattleAttack.OnAuto();
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

