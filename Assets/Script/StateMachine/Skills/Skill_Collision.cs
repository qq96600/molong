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
        private BattleHealth TatgetPosition;//Ŀ��λ��
        private Transform Axle;//ת����
        private base_skill_vo skill;//�����˺�
        private void Awake()
        {
            rb= GetComponent<Rigidbody2D>();
            Axle = GetComponentInChildren<Transform>();
        }
       
        private void Update()
        {
            Init();
            TargetMove(TatgetPosition.transform.position, MoveSpeed);
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
                transform.parent.parent.SendMessage("skill_damage", skill);
                gameObject.SetActive(false);
                ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
                //ObjectPoolManager.instance.PushObjectToPool(skill.skillname, this.gameObject);
                //������ײ����
                TatgetPosition.GetComponent<BattleAttack>().injured();
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
        public void SetSkillTarget(BattleHealth _tatgetObg, base_skill_vo base_skill)//�ҵ�Ŀ��
        {
            TatgetPosition = _tatgetObg;
            skill = base_skill;
        }
        public void Init()//��ʼ��
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
    }
}

