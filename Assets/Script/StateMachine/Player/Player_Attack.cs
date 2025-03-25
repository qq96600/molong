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
            
            //startTime = 0;
            player.RbZero();  
        }

        public override void Exit()
        {

            
        }

        public override void Update()
        {
            base.Update();
            startTime += 3;
            if (startTime> player.AttackSpeed)//¹¥»÷¼ä¸ô
            {
                player.anim.SetBool("Attack", true);
                player.TatgetObg.TakeDamage(player.attackDamage);
                startTime =0;
                player.anim.SetBool("Attack", false);
            }
           

        }

        
    }

}
