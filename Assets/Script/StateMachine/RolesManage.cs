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
        [HideInInspector] public Vector2 TargetPosition;//Ŀ��λ��
        [HideInInspector] public Vector2 BackstabPosition;//����λ��
        [Header("��������")]
        public float AttackDistance = 3f;
        [Header("���̵ľ���")]
        public float BehindDistance = 100f;
        [Header("�����ٶ�")]
        public float AttackSpeed = 1f;
        [Header("�ƶ��ٶ�")]
        public float MoveSpeed = 0.1f;
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

        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }


        public void TargetMove(Vector2 targetMove)//ƽ��
        {
            //transform.position = Vector2.Lerp(transform.position, targetMove, Time.deltaTime/MoveSpeed);

            Vector2 direction = targetMove - rb.position;
            direction.Normalize();

            // ʩ��׷����������������
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);

        }

        public virtual void Init(float attack_speed, float attack_distance, float move_speed, Transform playerPosition)//��ʼ������
        {
            AttackSpeed = attack_speed;
            AttackDistance = attack_distance;
            //MoveSpeed = move_speed;
            TargetPosition = new Vector2(playerPosition.position.x, transform.position.y);
            BackstabPosition = new Vector2(playerPosition.position.x - BehindDistance, transform.position.y);

        }


        public void RbZero()//ֹͣ�ƶ�
        {
            rb.velocity = Vector2.zero;
        }


        public bool isAttackDistance()//��������
        {
            float distance = Vector2.Distance(transform.position, BackstabPosition);
           
            if (AttackDistance > distance)
                return true;
            else
                return false;
        }

       

    }

}
