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
    /// 目标
    /// </summary>
    private BattleHealth Target;
    /// <summary>
    /// 自身
    /// </summary>
    private BattleAttack battle;
    /// <summary>
    /// 状态控制器
    /// </summary>
    private Arrow_Type arrowType = Arrow_Type.idle;
    /// <summary>
    /// 攻击速度
    /// </summary>
    private float AttackSpeed = 1f;
    /// <summary>
    /// 攻击速度计数器
    /// </summary>
    private float AttackSpeedCounter;
    /// <summary>
    /// 是否在动画中
    /// </summary>
    private bool is_anim = true;

    /// <summary>
    /// 状态控制器
    /// </summary>
    /// <returns></returns>
     private RolesManage StateMachine;
    /// <summary>
    /// 技能预制体
    /// </summary>
    private GameObject skill_prefabs;
    /// <summary>
    /// 刚体组件
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// 移动速度
    /// </summary>
    private float MoveSpeed=1f;

    /// <summary>
    /// 是否面向左
    /// </summary>
    protected bool facingLeft = true;
    /// <summary>
    /// 动画播放时间
    /// </summary>
    private float  animTime=0f;

    /// <summary>
    /// 是否在攻击动作中
    /// </summary>
    private bool isAttacking = false;
    /// <summary>
    /// 冲撞距离
    /// </summary>
    private float dashDistance =3f; // 最大冲撞距离
    /// <summary>
    /// 开始位置
    /// </summary>
    private Vector2 startPosition;
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


        if (arrowType == Arrow_Type.idle&& SumSave.battleMonsterHealths.Count>0)
        {
            AttackSpeedCounter -= Time.deltaTime;
  
            if (AttackSpeedCounter <= 0&&!isAttacking)
            {
                //Debug.Log("触发攻击"+Time.time);
                Transform pos= battle.transform;
                startPosition = transform.position;

                StateMachine.Animator_State(Arrow_Type.attack);

                StartCoroutine(AttackDash());

               // battle.OnAuto();
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

    /// <summary>
    /// 攻击动作
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackDash()
    {
        isAttacking = true;
        float dashDuration = 1f / (AttackSpeed*10);
        float startTime = Time.time;

        // 冲刺阶段
        while (Time.time - startTime < dashDuration)
        {
            transform.position = Vector2.Lerp(
                transform.position,
                new Vector2(transform.position.x + dashDistance, transform.position.y),
                (Time.time - startTime) / dashDuration
            );
           if (GetComponent<player_battle_attck>() != null)
            {
                transform.Translate(Vector2.right * AttackSpeed * Time.deltaTime);
            }else if(GetComponent<monster_battle_attck>() != null)
            {
                transform.Translate(Vector2.left * AttackSpeed * Time.deltaTime*0.8f);
            }

            
            yield return null;
        }

        battle.OnAuto();
        // 返回阶段
        float returnDuration = 0.5f;
        startTime = Time.time;
        while (Time.time - startTime < returnDuration)
        {
            transform.position = Vector2.Lerp(
                transform.position,
                startPosition,
                (Time.time - startTime) / returnDuration
            );
            yield return null;
        }

        transform.position = startPosition;
        isAttacking = false;
    }





    public void Init(BattleAttack _battle,BattleHealth target)
    {
        battle= _battle;
        Target = target;
        
       
     }
    /// <summary>
    /// 技能释放控制
    /// </summary>
    /// <param name="skill"></param>
    public void Skill(base_skill_vo skill)
    {
        Transform pos=battle.transform;
        skill.skill_damage_pos_type = 1;
        switch ((skill_pos_type)skill.skill_damage_pos_type)
        {
            case skill_pos_type.move:
                skill_prefabs = Resources.Load<GameObject>("Prefabs/panel_skill/Skill_Effects/驭火术");
                pos = battle.transform;
                break;
            case skill_pos_type.situ:
                skill_prefabs = Resources.Load<GameObject>("Prefabs/panel_skill/Skill_Effects/强力一击");
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
    /// 动画状态控制
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
    /// 动画结束
    /// </summary>
    public void OnAnimEnd()
    {
        is_anim= true;
    }
    /// <summary>
    /// 判断是否在攻击距离内
    /// </summary>
    private void IsState()
    {
        arrowType = Arrow_Type.idle;

        if (StateMachine.isAttackDistance()) 
        {
            arrowType = Arrow_Type.idle;
            is_anim = true;
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
    move,//移动类
    situ//在目标释放

}

