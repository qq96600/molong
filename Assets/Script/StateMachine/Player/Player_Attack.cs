using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine
{
    public class Player_Attack : Player_Basic
    {
        
        public Player_Attack(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.SetBool("Attack", false);
            player.RbZero();
            //player.FlipControl(player.Direction(player.TargetPosition));
        }


        public override void Exit()
        {
            base.Exit();
            
        }

        public override void Update()
        {
            base.Update();
            startTime += Time.deltaTime;
            if (startTime > player.AttackSpeed)//¹¥»÷¼ä¸ô
            {
                player.anim.SetBool("Attack", true);
                startTime = 0;
                player.BattleAttack.OnAuto();
                player.CloseAnimAfterDelay("Attack", 3f);
            }
            //stateMachine.ChangeState(player.idleState);
           

        }

        
    }

}
