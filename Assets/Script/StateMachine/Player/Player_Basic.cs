using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class Player_Basic : PlayerState
    {
        public Player_Basic(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
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
            if (player.isAttackDistance())//½øÈë¹ÖÎï¹¥»÷¾àÀë
            {
                stateMachine.ChangeState(player.attackState);
            }
            else
            {
                stateMachine.ChangeState(player.moveState);
            }
        }

      
    }
}