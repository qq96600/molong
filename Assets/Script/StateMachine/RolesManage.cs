using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class RolesManage : MonoBehaviour
    {
        [HideInInspector] public Animator anim;
     
        [HideInInspector] public Rigidbody2D rb;

        [HideInInspector] public BattleAttack BattleAttack;//战斗管理器
        [HideInInspector] public Vector2 TargetPosition;//目标位置
        [HideInInspector] public Vector2 BackstabPosition;//背刺位置
        [Header("攻击距离")]
        public float AttackDistance = 3f;
        [Header("背刺的距离")]
        public float BehindDistance = 100f;
        [Header("攻击速度")]
        public float AttackSpeed = 1f;
        [Header("移动速度")]
        public float MoveSpeed = 0.1f;
        protected virtual void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            if (anim == null)
                Debug.LogError("怪物没有Animator组件");
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("怪物没有Rigidbody2D组件");
            TargetPosition = new Vector2(transform.position.x, transform.position.y);
            BackstabPosition = new Vector2(transform.position.x, transform.position.y);

        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }


        public void TargetMove(Vector2 targetMove)//平移
        {
            //transform.position = Vector2.Lerp(transform.position, targetMove, Time.deltaTime/MoveSpeed);

            Vector2 direction = targetMove - rb.position;
            direction.Normalize();

            // 施加追踪力（物理驱动）
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);

        }

        public virtual void Init(float attack_speed, float attack_distance, float move_speed, Transform playerPosition)//初始化参数
        {
            AttackSpeed = attack_speed;
            AttackDistance = attack_distance;
            //MoveSpeed = move_speed;
            TargetPosition = new Vector2(playerPosition.position.x, transform.position.y);
            BackstabPosition = new Vector2(playerPosition.position.x - BehindDistance, transform.position.y);

        }


        public void RbZero()//停止移动
        {
            rb.velocity = Vector2.zero;
        }


        public bool isAttackDistance()//攻击距离
        {
            float distance = Vector2.Distance(transform.position, BackstabPosition);
           
            if (AttackDistance > distance)
                return true;
            else
                return false;
        }

       

    }

}
