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
        [SerializeField] private float rotationSpeed = 5f;
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
                string dec = "1234";
                StartCoroutine(tool_tesk(dec, collision));
                DamageTextManager.Instance.ShowDamageText(DamageEnum.��ͨ�˺�, dec, collision.transform);
                //collision.gameObject.GetComponent<BattleHealth>().TakeDamage(1);
                //gameObject.SetActive(false);
                //ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
            }
        }

        IEnumerator tool_tesk(string dec, Collider2D collision)
        {
            Debug.Log("��ǹ");
            yield return new WaitForSeconds(1f);
            DamageTextManager.Instance.ShowDamageText(DamageEnum.��ͨ�˺�, dec, collision.transform);
            StartCoroutine(tool_tesk(dec, collision));

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
        public void SetSkillTarget(BattleHealth _tatgetObg)//�ҵ�Ŀ��
        {
            TatgetPosition = _tatgetObg.transform.position;
        }
        public void Init()//��ʼ��
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
    }
}

