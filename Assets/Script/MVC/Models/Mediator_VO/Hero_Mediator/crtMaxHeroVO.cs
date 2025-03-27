
/// <summary>
/// 属性汇总表
/// </summary>
public class crtMaxHeroVO
{
    /// <summary>
    /// 展示名称
    /// </summary>
    public string show_name;
    /// <summary>
    /// 职业 1战 2法 3道 11战伤害 12法伤害召唤
    /// </summary>
    public int Type;//角色职业
    /// <summary>
    /// 地图产出编号
    /// </summary>
    public int map_index;
    /// <summary>
    /// 状态编号 玩家-1
    /// </summary>
    public int index;
    /// <summary>
    /// 宠物编号
    /// </summary>
    public int PetPos;
    /// <summary>
    /// 等级
    /// </summary>
    public int Lv;//角色等级
    /// <summary>
    /// 经验
    /// </summary>
    public long Exp;//当前经验
    /// <summary>
    /// 声望值
    /// </summary>
    public int Point;//声望值
    /// <summary>
    /// 图标
    /// </summary>
    public string icon;
    /// <summary>
    /// 生命
    /// </summary>
    public long MaxHP;//最大生命
    /// <summary>
    /// 魔法值
    /// </summary>
    public int MaxMp;
    /// <summary>
    /// 内力值
    /// </summary>
    public int internalforceMP;
    /// <summary>
    /// 蓄力值
    /// </summary>
    public int EnergyMp;
    /// <summary>
    /// 物理防御
    /// </summary>
    public int DefMin;
    /// <summary>
    /// 物理防御
    /// </summary>
    public int DefMax;
    /// <summary>
    /// 魔法防御
    /// </summary>
    public int MagicDefMin;
    /// <summary>
    /// 魔法防御
    /// </summary>
    public int MagicDefMax;
    /// <summary>
    /// 物理伤害
    /// </summary>
    public int damageMin;
    /// <summary>
    /// 物理伤害
    /// </summary>
    public int damageMax;
    /// <summary>
    /// 魔法伤害
    /// </summary>
    public int MagicdamageMin;
    /// <summary>
    /// 魔法伤害
    /// </summary>
    public int MagicdamageMax;
    /// <summary>
    /// 命中
    /// </summary>
    public int hit;
    /// <summary>
    /// 闪避
    /// </summary>
    public int dodge;

    /// <summary>
    /// 穿透
    /// </summary>
    public int penetrate;
    /// <summary>
    /// 格挡
    /// </summary>
    public int block;
    /// <summary>
    /// 暴击
    /// </summary>
    public int crit_rate;
    /// <summary>
    /// 暴击伤害
    /// </summary>
    public int crit_damage;
    /// <summary>
    /// 伤害加成
    /// </summary>
    public int double_damage;
    /// <summary>
    /// 幸运
    /// </summary>
    public int Lucky;
    /// <summary>
    /// 真实伤害
    /// </summary>
    public int Real_harm;
    /// <summary>
    /// 伤害减免
    /// </summary>
    public int Damage_Reduction;
    /// <summary>
    /// 伤害吸收
    /// </summary>
    public int Damage_absorption;
    /// <summary>
    /// 异常抗性
    /// </summary>
    public int resistance;
    /// <summary>
    /// 移动速度
    /// </summary>
    public int move_speed;
    /// <summary>
    /// 攻击速度
    /// </summary>
    public int attack_speed;
    /// <summary>
    /// 攻击距离
    /// </summary>
    public int attack_distance;
    /// <summary>
    /// 生命加成
    /// </summary>
    public int bonus_Hp;
    /// <summary>
    /// 法力加成
    /// </summary>
    public int bonus_Mp;
    /// <summary>
    /// s伤害加成
    /// </summary>
    public int bonus_Damage;
    /// <summary>
    /// 魔法伤害
    /// </summary>
    public int bonus_MagicDamage;
    /// <summary>
    /// 防御加成
    /// </summary>
    public int bonus_Def;
    /// <summary>
    /// 魔法防御加成
    /// </summary>
    public int bonus_MagicDef;
    /// <summary>
    /// 生命回复
    /// </summary>
    public int Heal_Hp;
    /// <summary>
    /// 魔法回复
    /// </summary>
    public int Heal_Mp;
    /// <summary>
    /// 五行属性
    /// </summary>
    public int[] life = new int[] { 0, 0, 0, 0, 0 };
}
