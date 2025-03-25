using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class PlayerSkill_HuoQiu : PlayerSkillManager
    {
        Rigidbody2D HuoQiuRb;
        public PlayerSkill_HuoQiu(Player _player, PlayerstateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {

        }
   

        public override void Enter()
        {
            base.Enter();
            Init();
            stateMachine.ChangeState(player.attackState);
        }

        private void Init()
        {
            player.RbZero();
            //实例化火球
            player.skills_HuoQiu.SetActive(true);
            ObjectPoolManager.instance.GetObjectFormPool("火球", player.skills_HuoQiu,
                new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z)
                , Quaternion.identity, player.skillStoragePos);
            
        }

        public override void Exit()
        {
            base.Exit();
            //状态结束时，重置动画播放速度
            player.newAnimSpeed();
        }

        public override void Update()
        {
            base.Update();

        }
    }
}
 
