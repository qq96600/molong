using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class PlayerSkillManager : PlayerState//���ܹ���
    {

        public PlayerSkillManager(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.RbZero();


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
