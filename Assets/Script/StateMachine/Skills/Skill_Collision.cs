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
        private float skill_damage;//技能伤害
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
        public void SetSkillTarget(BattleHealth _tatgetObg,float _attack_distance)//找到目标
        {
            TatgetPosition = _tatgetObg.transform.position;
            skill_damage = _attack_distance;
        }
        public void Init()//初始化
        {
            transform.parent.parent.SendMessage("GetSkill", this);
        }
    }
}

