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
        private Transform Axle;//ת����
        private float skill_damage;//�����˺�
        private void Awake()
        {
            rb= GetComponent<Rigidbody2D>();
            Axle = GetComponentInChildren<Transform>();
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
                if (DamageTextManager.Instance == null)
                {
                    Debug.LogError("DamageTextManager instance is null!");
                    return;
                }
                
                //collision.gameObject.GetComponent<BattleHealth>().TakeDamage(skill_damage);
                gameObject.SetActive(false);
                ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
            }
        }

    

        public void TargetMove(Vector2 targetPosition,float MoveSpeed)//ƽ��
        {
            Vector2 direction = Direction(targetPosition);

            // ʩ��׷����������������
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);

            // ����������ת�Ƕ�
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Ŀ��Ƕ�
            Axle.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
           

        }

        public Vector2 Direction( Vector2 targetPosition)//�жϷ���
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }
        public void SetSkillTarget(BattleHealth _tatgetObg,float _attack_distance)//�ҵ�Ŀ��
        {
            TatgetPosition = _tatgetObg.transform.position;
            skill_damage = _attack_distance;
        }
        public void Init()//��ʼ��
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
    }
}

