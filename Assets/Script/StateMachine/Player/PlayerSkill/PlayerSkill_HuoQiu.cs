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
            
            player.RbZero();
         
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
            startTime += Time.deltaTime;
            if (startTime > player.AttackSpeed)
            {
                ObjectPoolManager.instance.GetObjectFormPool("Skll_HuoQiu", player.skills_HuoQiu,
            new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z)
            , Quaternion.identity, player.skillStoragePos);
               
                player.BattleAttack.OnAuto();
                startTime = 0;
                player.CloseAnimAfterDelay("Storage", 0.5f);
            }
               
        }
    }
}
 
