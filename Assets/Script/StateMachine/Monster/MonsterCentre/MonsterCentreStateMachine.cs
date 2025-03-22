using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class MonsterCentreStateMachine
    {
        public MonsterCentreState currentState;//��ǰ״̬

        public void Initialized(MonsterCentreState _monsterState)//��ʼ��״̬
        {
            currentState = _monsterState;
            currentState.Enter();
        }

        public void ChangeState(MonsterCentreState _newState)//�ı�״̬
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();

        }


    }

}
