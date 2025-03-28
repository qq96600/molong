using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class MonsterStateMachine
    {
        public MonsterState currentState;//��ǰ״̬

        public void Initialized(MonsterState _monsterState)//��ʼ��״̬
        {
            currentState = _monsterState;
            currentState.Enter();
        }

        public void ChangeState(MonsterState _newState)//�ı�״̬
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();

        }


    }

}
