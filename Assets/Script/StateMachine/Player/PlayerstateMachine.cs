using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class PlayerstateMachine
    {
        public PlayerState currentState;//��ǰ״̬

        public void Initialized(PlayerState _playerState)//��ʼ��״̬
        {
            currentState = _playerState;
            currentState.Enter();
        }

        public void ChangeState(PlayerState _newState)//�ı�״̬
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();

        }

    }

}
