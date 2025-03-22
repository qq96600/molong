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

     

        public MonsterCentreState(MonsterCentre _monster, MonsterCentreStateMachine _sateManage, string _animBoolName)
        {
            this.monsterCentre = _monster;
            this.stateManage = _sateManage;
            this.animBoolName = _animBoolName;
        }


        public virtual void Enter()
        {
            monsterCentre.anim.SetBool(animBoolName, true);

        }

        
        public virtual void Update()
        {

        }

        public virtual void Exit()
        {
            monsterCentre.anim.SetBool(animBoolName, false);
        }

    }
}
