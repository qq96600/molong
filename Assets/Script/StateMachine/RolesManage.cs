using Common;
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

        [HideInInspector] public BattleAttack BattleAttack = new BattleAttack();//战斗管理器
        [HideInInspector] public BattleHealth TatgetObg = new BattleHealth();//目标物体
        [HideInInspector] public Vector2 TargetPosition;//目标位置
        [HideInInspector] public Vector2 BackstabPosition;//背刺位置
        [HideInInspector] public float animSpeed ;//动画速度
        [HideInInspector] public AnimatorStateInfo animStateInfo;//动画状态
        [HideInInspector] public AttackStateMachine attack;//战斗逻辑

        /// <summary>
        /// 获取技能
        /// </summary>
        protected base_skill_vo baseskill;
       

        [Header("造成伤害")]
        public float attackDamage = 10f;
        [Header("攻击距离")]
        public float AttackDistance = 500f;
        [Header("背刺的距离")]
        public float BehindDistance = 100f;
        [Header("攻击速度")]
        public float AttackSpeed = 5f;
        [Header("移动速度")]
        public float MoveSpeed = 0.1f;
        [Header("技能释放概率")]
        public float skillRelease = 50f;
        [Header("是否面向左")]
        protected bool facingLeft=true;


        #region 角色外观
        /// <summary>
        /// 角色类型
        /// </summary>
        private enum_skin_state skin_state;
        /// <summary>
        /// 皮肤预制体
        /// </summary>
        private GameObject skin_prefabs;
        /// <summary>
        /// 角色外观位置
        /// </summary>
        private Transform panel_role_health;
        /// <summary>
        /// 怪物类型
        /// </summary>
        private enum_monster_type monster_type;
      

        #endregion


        protected virtual void Awake()
        {
            #region 角色外观初始化
            if(GetComponent<Player>()!= null)
            {
                int hero_index = int.Parse(SumSave.crt_hero.hero_index);
                skin_state = (enum_skin_state)hero_index;
                skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/外观_" + SumSave.crt_hero.hero_pos);
                panel_role_health = transform.Find("Appearance");
                Instantiate(skin_prefabs, panel_role_health);
            }else if(GetComponent<Monster>() != null)
            {
                monster_type = enum_monster_type.黑鳞君;
                skin_prefabs = Resources.Load<GameObject>("Prefabs/monsters/mon_" + monster_type.ToString());
                panel_role_health = transform.Find("Appearance");
                Instantiate(skin_prefabs, panel_role_health);
            }


           
            #endregion

            anim = GetComponentInChildren<Animator>();
            if (anim == null)
                Debug.LogError("怪物没有Animator组件");
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("怪物没有Rigidbody2D组件");

            attack= GetComponent<AttackStateMachine>();
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


        public virtual void Init(BattleAttack battle, BattleHealth _tatgetObg)//初始化参数
        {
            //anim.speed = attack_speed;
            //AttackDistance = attack_distance;
            //MoveSpeed = move_speed;
            TatgetObg = _tatgetObg;
            BattleAttack = battle;
        }

        public virtual void Animator_State(Arrow_Type arrowType)
        {

        }

     



        
     


        /// <summary>
        /// 延迟关闭动画
        /// </summary>
        /// <param name="animName"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public IEnumerator CloseAnimAfterDelay(string animName, float delay)
        {
            yield return new WaitForSeconds(delay);

            CloseAnim(animName); // 传递参数
        }
        /// <summary>
        /// 关闭动画 
        /// </summary>
        /// <param name="animName"></param>
        public void CloseAnim(string animName)
        {
            anim.SetBool(animName, false);
        }

        public bool isAttackDistance()//攻击距离
        {

            float distance = Vector2.Distance(transform.position, TatgetObg.transform.position);

            if (AttackDistance >= distance)
            {
                RbZero();
                return true;
            }
            else
                return false;
        }

        public void TargetMove(Vector2 targetMove)//平移 
        {
            Vector2 direction = Direction(targetMove);
            //Debug.Log(direction + "方向" + TatgetObg.transform.position + "目标位置" + transform.position + "自己位置");
            // 施加追踪力（物理驱动）
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);
            FlipControl(Direction(TatgetObg.transform.position));

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

        public void Flip()//翻转
        {
            facingLeft = !facingLeft;
            //transform.Rotate(0, 180, 0);
        }



        public Vector2 Direction(Vector2 targetPosition)//判断方向
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }



        public void RbZero()//停止移动
        {
            rb.velocity = Vector2.zero;
        }


    }

}
