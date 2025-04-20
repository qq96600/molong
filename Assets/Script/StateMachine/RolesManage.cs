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

        [HideInInspector] public BattleAttack BattleAttack = new BattleAttack();//ս��������
        [HideInInspector] public BattleHealth TatgetObg = new BattleHealth();//Ŀ������
        [HideInInspector] public Vector2 TargetPosition;//Ŀ��λ��
        [HideInInspector] public Vector2 BackstabPosition;//����λ��
        [HideInInspector] public float animSpeed ;//�����ٶ�
        [HideInInspector] public AnimatorStateInfo animStateInfo;//����״̬
        [HideInInspector] public AttackStateMachine attack;//ս���߼�

        /// <summary>
        /// ��ȡ����
        /// </summary>
        protected base_skill_vo baseskill;
       

        [Header("����˺�")]
        public float attackDamage = 10f;
        [Header("��������")]
        public float AttackDistance = 500f;
        [Header("���̵ľ���")]
        public float BehindDistance = 100f;
        [Header("�����ٶ�")]
        public float AttackSpeed = 5f;
        [Header("�ƶ��ٶ�")]
        public float MoveSpeed = 0.1f;
        [Header("�����ͷŸ���")]
        public float skillRelease = 50f;
        [Header("�Ƿ�������")]
        protected bool facingLeft=true;


        #region ��ɫ���
        /// <summary>
        /// ��ɫ����
        /// </summary>
        private enum_skin_state skin_state;
        /// <summary>
        /// ��ɫƤ��Ԥ����
        /// </summary>
        private GameObject skin_prefabs;
        /// <summary>
        /// ��ɫ�ڹ�λ��
        /// </summary>
        private Transform panel_role_health;
        #endregion


        protected virtual void Awake()
        {
            #region ��ɫ��۳�ʼ��
            int hero_index = int.Parse(SumSave.crt_hero.hero_index);
            skin_state = (enum_skin_state)hero_index;
            skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/���_" + skin_state.ToString());
            panel_role_health = transform.Find("Appearance");
            Instantiate(skin_prefabs, panel_role_health);
            #endregion

            anim = GetComponentInChildren<Animator>();
            if (anim == null)
                Debug.LogError("����û��Animator���");
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("����û��Rigidbody2D���");

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


        public virtual void Init(BattleAttack battle, BattleHealth _tatgetObg)//��ʼ������
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
        /// ״̬�л�
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="skill_name"></param>
        public virtual void stateAutoInit(base_skill_vo skill_name=null)
        {
            baseskill = skill_name;
        }

        
     


        /// <summary>
        /// �ӳٹرն���
        /// </summary>
        /// <param name="animName"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public IEnumerator CloseAnimAfterDelay(string animName, float delay)
        {
            yield return new WaitForSeconds(delay);

            CloseAnim(animName); // ���ݲ���
        }
        /// <summary>
        /// �رն��� 
        /// </summary>
        /// <param name="animName"></param>
        public void CloseAnim(string animName)
        {
            anim.SetBool(animName, false);
        }

        public bool isAttackDistance()//��������
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

        public void TargetMove(Vector2 targetMove)//ƽ�� 
        {
            Vector2 direction = Direction(targetMove);
            //Debug.Log(direction + "����" + TatgetObg.transform.position + "Ŀ��λ��" + transform.position + "�Լ�λ��");
            // ʩ��׷����������������
            rb.AddForce(direction * MoveSpeed, ForceMode2D.Impulse);
            FlipControl(Direction(TatgetObg.transform.position));

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

        public void Flip()//��ת
        {
            facingLeft = !facingLeft;
            //transform.Rotate(0, 180, 0);
        }



        public Vector2 Direction(Vector2 targetPosition)//�жϷ���
        {
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize();
            return direction;
        }



        public void RbZero()//ֹͣ�ƶ�
        {
            rb.velocity = Vector2.zero;
        }


    }

}
