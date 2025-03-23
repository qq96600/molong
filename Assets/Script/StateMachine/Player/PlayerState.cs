using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class PlayerState
    {
        protected Player player;
        protected PlayerstateMachine stateMachine;
        protected string animBoolName;

        public PlayerState(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _playerStateMachine;
            this.animBoolName = _animBoolName;
        }


        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName, true);
            player.FlipControl(player.Direction(player.TargetPosition));
        }

        public virtual void Update()
        {

        }
        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName, false);
        }
    }

}
