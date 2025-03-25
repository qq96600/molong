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

        [HideInInspector] public BattleAttack BattleAttack;//ս��������
        [HideInInspector] public BattleHealth TatgetObg;//Ŀ������
        [HideInInspector] public Vector2 TargetPosition;//Ŀ��λ��
        [HideInInspector] public Vector2 BackstabPosition;//����λ��
        [HideInInspector] public float animSpeed ;//�����ٶ�
       

        [Header("����˺�")]
        public float attackDamage = 10f;
        [Header("��������")]
        public float AttackDistance = 500f;
        [Header("���̵ľ���")]
        public float BehindDistance = 100f;
        [Header("�����ٶ�")]
        public float AttackSpeed = 200f;
        [Header("�ƶ��ٶ�")]
        public float MoveSpeed = 0.1f;
        [Header("�����ͷŸ���")]
        public float skillRelease = 50f;
        [Header("�Ƿ�������")]
        protected bool facingLeft=true;
        
        protected virtual void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            if (anim == null)
                Debug.LogError("����û��Animator���");
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("����û��Rigidbody2D���");
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


        public void TargetMove( Vector2 targetMove)//ƽ��
        {
            Vector2 direction = Direction(targetMove);

            // ʩ��׷����������������
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);
            FlipControl(Direction(TargetPosition));

        }

        public Vector2 Direction(Vector2 targetPosition)//�жϷ���
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }

        public virtual void Init(float attack_speed, float attack_distance, float move_speed, BattleHealth _tatgetObg)//��ʼ������
        {
            AttackSpeed = attack_speed;
            if(attack_distance>=AttackDistance)
            AttackDistance = attack_distance;
            
            //MoveSpeed = move_speed;

            TatgetObg=_tatgetObg;
        }


        public void RbZero()//ֹͣ�ƶ�
        {
            rb.velocity = Vector2.zero;
        }


        public bool isAttackDistance()//��������
        {
           
            float distance = Vector2.Distance(transform.position, TargetPosition); 

            if (AttackDistance >= distance)
                return true;
            else
                return false;
        }
        public void Flip()//��ת
        {
            
            facingLeft=!facingLeft;
            transform.Rotate(0, 180, 0);

        }

        
       public void FlipControl(Vector2 direction)//��ת����
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

        public void AnimSpeed(float _speed)//���������ٶ�
        {
            anim.speed = _speed;
        }

        public void newAnimSpeed()
        {
            anim.speed = animSpeed;
        }
        public float AttackSpeedCalculation(float _attackSpeed)//�����ٶȼ���
        {
            return _attackSpeed / 60f;
        }
    }

}
