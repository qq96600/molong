using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class PlayerSkill_HuoQiu : PlayerSkillManager
    {
        public PlayerSkill_HuoQiu(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {

        }
        public override void Enter()
        {
            base.Enter();

            

        }

        private void Init()
        {
           
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            startTime -= Time.deltaTime;
            player.animStateInfo = player.anim.GetCurrentAnimatorStateInfo(0);//需要在每一帧更新动画状态信息        
            if (startTime <=0)
            {
                ObjectPoolManager.instance.GetObjectFormPool("Skll_HuoQiu", player.skills_HuoQiu,
                new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z)
                , Quaternion.identity, player.skillStoragePos);

                player.BattleAttack.OnAuto();
                startTime = player.animStateInfo.length;
            }
        }
    }
}
 
