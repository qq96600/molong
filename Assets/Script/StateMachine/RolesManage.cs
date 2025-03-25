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
        [HideInInspector] public BattleHealth TatgetObg;//目标物体
        [HideInInspector] public Vector2 TargetPosition;//目标位置
        [HideInInspector] public Vector2 BackstabPosition;//背刺位置
        [HideInInspector] public float animSpeed ;//动画速度
       

        [Header("造成伤害")]
        public float attackDamage = 10f;
        [Header("攻击距离")]
        public float AttackDistance = 500f;
        [Header("背刺的距离")]
        public float BehindDistance = 100f;
        [Header("攻击速度")]
        public float AttackSpeed = 200f;
        [Header("移动速度")]
        public float MoveSpeed = 0.1f;
        [Header("技能释放概率")]
        public float skillRelease = 50f;
        [Header("是否面向左")]
        protected bool facingLeft=true;
        
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
            animSpeed = anim.speed;
        }

        protected virtual void Start()
        {
           

        }

        protected virtual void Update()
        {
            TargetPosition = new Vector2(TatgetObg.transform.position.x, transform.position.y);
            BackstabPosition = new Vector2(TatgetObg.transform.position.x - BehindDistance, transform.position.y);

        }


        public void TargetMove( Vector2 targetMove)//平移
        {
            Vector2 direction = Direction(targetMove);

            // 施加追踪力（物理驱动）
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);
            FlipControl(Direction(TargetPosition));

        }

        public Vector2 Direction(Vector2 targetPosition)//判断方向
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }

        public virtual void Init(float attack_speed, float attack_distance, float move_speed, BattleHealth _tatgetObg)//初始化参数
        {
            AttackSpeed = attack_speed;
            if(attack_distance>=AttackDistance)
            AttackDistance = attack_distance;
            
            //MoveSpeed = move_speed;

            TatgetObg=_tatgetObg;
        }


        public void RbZero()//停止移动
        {
            rb.velocity = Vector2.zero;
        }


        public bool isAttackDistance()//攻击距离
        {
           
            float distance = Vector2.Distance(transform.position, TargetPosition); 

            if (AttackDistance >= distance)
                return true;
            else
                return false;
        }
        public void Flip()//翻转
        {
            
            facingLeft=!facingLeft;
            transform.Rotate(0, 180, 0);

        }

        
       public void FlipControl(Vector2 direction)//翻转控制
        {
            if (direction.x > 0 && facingLeft)
            {
                Flip();
            }
            else if (direction.x < 0 && !facingLeft)
            {
                Flip();
            }
        }

        public void AnimSpeed(float _speed)//动画播放速度
        {
            anim.speed = _speed;
        }

        public void newAnimSpeed()
        {
            anim.speed = animSpeed;
        }
        public float AttackSpeedCalculation(float _attackSpeed)//攻击速度计算
        {
            return _attackSpeed / 60f;
        }
    }

}
