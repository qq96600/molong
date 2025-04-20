using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using StateMachine;
using UI;
using Common;

public class AttackStateMachine : MonoBehaviour
{

    private float AttackDistance = 500f;
    /// <summary>
    /// Ŀ��
    /// </summary>
    private BattleHealth Target;
    /// <summary>
    /// ����
    /// </summary>
    private BattleAttack battle;
    /// <summary>
    /// ״̬������
    /// </summary>
    private Arrow_Type arrowType = Arrow_Type.idle;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    private float AttackSpeed = 1f;
    /// <summary>
    /// �����ٶȼ�����
    /// </summary>
    private float AttackSpeedCounter;
    /// <summary>
    /// �Ƿ��ڶ�����
    /// </summary>
    private bool is_anim = true;

    /// <summary>
    /// ״̬������
    /// </summary>
    /// <returns></returns>
     private RolesManage StateMachine;
    /// <summary>
    /// ����Ԥ����
    /// </summary>
    private GameObject skill_prefabs;
    /// <summary>
    /// �������
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    private float MoveSpeed=1f;

    /// <summary>
    /// �Ƿ�������
    /// </summary>
    protected bool facingLeft = true;
    /// <summary>
    /// ��������ʱ��
    /// </summary>
    private float  animTime=0f;


     private void Awake()
    {
        StateMachine = GetComponent<RolesManage>();
        battle= GetComponent<BattleAttack>();
        Target= GetComponent<BattleHealth>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        IsState();
        Animator_State();


        if (arrowType == Arrow_Type.idle&& SumSave.battleMonsterHealths.Count>0&& SumSave.battleHeroHealths.Count >0)
        {
            AttackSpeedCounter -= Time.deltaTime;
  
            if (AttackSpeedCounter <= 0)
            {
                //Debug.Log("��������"+Time.time);
                StateMachine.Animator_State(Arrow_Type.attack);
                battle.OnAuto();
                AttackSpeedCounter = AttackSpeed;//battle.Data.attack_speed; 
            }
            else
            {
                StateMachine.Animator_State(Arrow_Type.idle);
            }
        }
        else
        {
            IsState();
        }
      
    }




    public void Init(BattleAttack _battle,BattleHealth target)
    {
        battle= _battle;
        Target = target;
        
       
     }
    /// <summary>
    /// �����ͷſ���
    /// </summary>
    /// <param name="skill"></param>
    public void Skill(base_skill_vo skill)
    {
        Transform pos=battle.transform;
        skill.skill_damage_pos_type = 1;
        switch ((skill_pos_type)skill.skill_damage_pos_type)
        {
            case skill_pos_type.move:
                skill_prefabs = Resources.Load<GameObject>("Prefabs/panel_skill/Skill_Effects/HuoQiu");
                pos = battle.transform;
                break;
            case skill_pos_type.situ:
                skill_prefabs = Resources.Load<GameObject>("Prefabs/panel_skill/Skill_Effects/BaoZha");
                pos = Target.transform;
                break;
            default:
                break;
        }
        GameObject go = ObjectPoolManager.instance.GetObjectFormPool(skill.skillname, skill_prefabs,
              new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z)
              , Quaternion.identity, pos.transform);
        go.GetComponent<Skill_Collision>().Init(skill, battle, Target, (skill_pos_type)skill.skill_damage_pos_type);
    }



    /// <summary>
    /// ����״̬����
    /// </summary>
    private void Animator_State()
    {
        if (is_anim)
        {
            switch (arrowType)
            {
                case Arrow_Type.idle:
                    StateMachine.Animator_State(Arrow_Type.idle);
                    break;
                case Arrow_Type.move:
                    StateMachine.Animator_State(Arrow_Type.move);
                    break;
                case Arrow_Type.attack:
                    StateMachine.Animator_State(Arrow_Type.attack);
                    break;
                default:
                    break;
            }
            is_anim = false;
        }
    }

    

    /// <summary>
    /// ��������
    /// </summary>
    public void OnAnimEnd()
    {
        is_anim= true;
    }
    /// <summary>
    /// �ж��Ƿ��ڹ���������
    /// </summary>
    private void IsState()
    {
        arrowType = Arrow_Type.idle;

        if (StateMachine.isAttackDistance()) 
        {
            if (AttackSpeed > 0)
            {
                arrowType = Arrow_Type.idle;
            }
        }
        else
        {
            arrowType= Arrow_Type.move;
            is_anim = true;
        }
    }

 
}
public enum Arrow_Type
{
    idle,
    move,
    attack,
}

public enum skill_pos_type
{ 
    move,//�ƶ���
    situ//��Ŀ���ͷ�

}

