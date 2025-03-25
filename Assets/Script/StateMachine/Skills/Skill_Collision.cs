using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Skill_Collision : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float MoveSpeed = 1f;//移动速度
        private Vector2 TatgetPosition;//目标位置
        private Transform Axle;//转向轴
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
                DamageTextManager.Instance.ShowDamageText(DamageEnum.普通伤害, dec, collision.transform);
                //collision.gameObject.GetComponent<BattleHealth>().TakeDamage(1);
                //gameObject.SetActive(false);
                //ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
            }
        }

        IEnumerator tool_tesk(string dec, Collider2D collision)
        {
            Debug.Log("开枪");
            yield return new WaitForSeconds(1f);
            DamageTextManager.Instance.ShowDamageText(DamageEnum.普通伤害, dec, collision.transform);
            StartCoroutine(tool_tesk(dec, collision));

        }

        public void TargetMove(Vector2 targetPosition,float MoveSpeed)//平移
        {
            Vector2 direction = Direction(targetPosition);

            // 施加追踪力（物理驱动）
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);

            // 计算所需旋转角度
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//目标角度
            Axle.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
           

        }

        public Vector2 Direction( Vector2 targetPosition)//判断方向
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }
        public void SetSkillTarget(BattleHealth _tatgetObg)//找到目标
        {
            TatgetPosition = _tatgetObg.transform.position;
        }
        public void Init()//初始化
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
    }
}

