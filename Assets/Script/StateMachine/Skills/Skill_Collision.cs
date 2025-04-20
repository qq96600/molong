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
        private BattleHealth TatgetPosition;//目标位置
        private Transform Axle;//转向轴
        private base_skill_vo skill;//技能伤害
        private bool is_collider = false;//是否碰撞

        private Animator anim;//火球动画
        private skill_pos_type SkillPosType;//技能释放类型
        /// <summary>
        /// 自身造成伤害
        /// </summary>
        private BattleAttack attack;

        private bool isExplosion= false;//是否爆炸
        private void Awake()
        {
            rb= GetComponent<Rigidbody2D>();
            Axle = GetComponentInChildren<Transform>();
            anim= GetComponentInChildren<Animator>().transform.GetComponentInChildren<Animator>();



        }
        private void Start()
        {
           
        }
        private void Update()
        {
            if(SkillPosType == skill_pos_type.move)
            TargetMove(TatgetPosition.transform.position, MoveSpeed);


            //if(SkillPosType== skill_pos_type.situ&&!is_collider)
            //{
            //    anim.SetBool("Explosion", true);
            //    ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
            //    TatgetPosition.GetComponent<BattleAttack>().injured();
            //}

            if (isExplosion)
            {
                rb.velocity = Vector2.zero;
               
                //animStateInfo = anim.GetCurrentAnimatorStateInfo(0);//需要在每一帧更新动画状态信息
                
                //if(animStateInfo.normalizedTime >= 1f)
                //{
                //    ObjectPoolManager.instance.PushObjectToPool("Skll_HuoQiu", this.gameObject);
                //}

            }
             
        
                  
            
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (is_collider)
            {
                if (collision.gameObject.tag == "Moster"&& SkillPosType== skill_pos_type.move)
                {
                    if (DamageTextManager.Instance == null)
                    {
                        Debug.LogError("DamageTextManager instance is null!");
                        return;
                    }
                    is_collider = false;
                    transform.parent.SendMessage("skill_damage", skill);
                    ObjectPoolManager.instance.PushObjectToPool(skill.skillname, this.gameObject);
                    //this.GetComponent<BattleAttack>().injured();
                    //StartCoroutine(WaitForExplosionEnd());
                }else
                if (collision.gameObject.tag == "Moster" && SkillPosType == skill_pos_type.situ)
                {
                    if (DamageTextManager.Instance == null)
                    {
                        Debug.LogError("DamageTextManager instance is null!");
                        return;
                    }

                    is_collider = false;

                    StartCoroutine(WaitForExplosionEnd());
                }
            }
        }


        private IEnumerator WaitForExplosionEnd()
        {
            rb.velocity = Vector2.zero;
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            
            // 等待动画播放完成
            yield return new WaitForSeconds(animStateInfo.length);
            // 将对象返回对象池
            ObjectPoolManager.instance.PushObjectToPool(skill.skillname, this.gameObject);
            attack.skill_damage(skill); 


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
        public void SetSkillTarget(BattleHealth _tatgetObg, base_skill_vo base_skill)//找到目标
        {
            TatgetPosition = _tatgetObg;
            skill = base_skill;
        }
        public void Init(base_skill_vo _skill,BattleAttack _attack,BattleHealth _target, skill_pos_type _skill_pos_type)//初始化
        {
            is_collider = true;
            skill = _skill;
            attack = _attack;
            TatgetPosition =_target;
            SkillPosType= _skill_pos_type;

        }
    }
}

