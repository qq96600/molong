using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine
{
    
    public class Player_Attack : PlayerState
    {
        
        public Player_Attack(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
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
            return;
            startTime -= Time.deltaTime;
            player.animStateInfo = player.anim.GetCurrentAnimatorStateInfo(0);//��Ҫ��ÿһ֡���¶���״̬��Ϣ        
            if (startTime <= 0)
            {
                Debug.Log("���������ص�");
                player.attack.OnAnimEnd();
                startTime = player.animStateInfo.length;
            }
        }

        
    }

}
