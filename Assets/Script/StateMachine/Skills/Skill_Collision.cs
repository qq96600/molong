using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Skill_Collision : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float MoveSpeed = 1f;//�ƶ��ٶ�
        private Vector2 TatgetPosition;//Ŀ��λ��
        private void Awake()
        {
            rb= GetComponent<Rigidbody2D>();
        }
        public void SetSkillTarget(BattleHealth _tatgetObg)//�ҵ�Ŀ��
        {
            TatgetPosition = _tatgetObg.transform.position;
        }
        public void Init()//��ʼ��
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
        private void Update()
        {
            Init();
            TargetMove(TatgetPosition, MoveSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Moster")
            {
                collision.gameObject.GetComponent<BattleHealth>().TakeDamage(1);
                //gameObject.SetActive(false);
                ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
            }
        }
 

        public void TargetMove(Vector2 targetPosition,float MoveSpeed)//ƽ��
        {
            Vector2 direction = Direction(targetPosition);

            // ʩ��׷����������������
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);

        }

        public Vector2 Direction( Vector2 targetPosition)//�жϷ���
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }
    }
}

