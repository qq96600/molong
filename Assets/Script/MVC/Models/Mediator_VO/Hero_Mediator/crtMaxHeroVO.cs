
using System.Collections.Generic;
/// <summary>
/// ���Ի��ܱ�
/// </summary>
public class crtMaxHeroVO
{
    /// <summary>
    /// �жϹ���״̬ 0 �������Ĳ�ͬ�ӳ�
    /// </summary>
    public List<int> monster_attrList= new List<int>();
    /// <summary>
    /// չʾ����
    /// </summary>
    public string show_name;
    /// <summary>
    /// ְҵ 1ս 2�� 3�� 11ս�˺� 12���˺��ٻ�
    /// </summary>
    public int Type;//��ɫְҵ
    /// <summary>
    /// ��ͼ�������
    /// </summary>
    public int map_index;
    /// <summary>
    /// ״̬��� ���-1
    /// </summary>
    public int index;
    /// <summary>
    /// ������
    /// </summary>
    public int PetPos;
    /// <summary>
    /// �ȼ�
    /// </summary>
    public int Lv;//��ɫ�ȼ�
    /// <summary>
    /// ����
    /// </summary>
    public long Exp;//��ǰ����
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int Point;//����ֵ
    /// <summary>
    /// ͼ��
    /// </summary>
    public string icon;
    /// <summary>
    /// ����
    /// </summary>
    public long MaxHP;//�������
    /// <summary>
    /// ħ��ֵ
    /// </summary>
    public int MaxMp;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int internalforceMP;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int EnergyMp;
    /// <summary>
    /// �������
    /// </summary>
    public int DefMin;
    /// <summary>
    /// �������
    /// </summary>
    public int DefMax;
    /// <summary>
    /// ħ������
    /// </summary>
    public int MagicDefMin;
    /// <summary>
    /// ħ������
    /// </summary>
    public int MagicDefMax;
    /// <summary>
    /// �����˺�
    /// </summary>
    public int damageMin;
    /// <summary>
    /// �����˺�
    /// </summary>
    public int damageMax;
    /// <summary>
    /// ħ���˺�
    /// </summary>
    public int MagicdamageMin;
    /// <summary>
    /// ħ���˺�
    /// </summary>
    public int MagicdamageMax;
    /// <summary>
    /// ����
    /// </summary>
    public int hit;
    /// <summary>
    /// ����
    /// </summary>
    public int dodge;

    /// <summary>
    /// ��͸
    /// </summary>
    public int penetrate;
    /// <summary>
    /// ��
    /// </summary>
    public int block;
    /// <summary>
    /// ����
    /// </summary>
    public int crit_rate;
    /// <summary>
    /// �����˺�
    /// </summary>
    public int crit_damage;
    /// <summary>
    /// �˺��ӳ�
    /// </summary>
    public int double_damage;
    /// <summary>
    /// ����
    /// </summary>
    public int Lucky;
    /// <summary>
    /// ��ʵ�˺�
    /// </summary>
    public int Real_harm;
    /// <summary>
    /// �˺�����
    /// </summary>
    public int Damage_Reduction;
    /// <summary>
    /// �˺�����
    /// </summary>
    public int Damage_absorption;
    /// <summary>
    /// �쳣����
    /// </summary>
    public int resistance;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public int move_speed;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    public int attack_speed;
    /// <summary>
    /// ��������
    /// </summary>
    public int attack_distance;
    /// <summary>
    /// �����ӳ�
    /// </summary>
    public int bonus_Hp;
    /// <summary>
    /// �����ӳ�
    /// </summary>
    public int bonus_Mp;
    /// <summary>
    /// s�˺��ӳ�
    /// </summary>
    public int bonus_Damage;
    /// <summary>
    /// ħ���˺�
    /// </summary>
    public int bonus_MagicDamage;
    /// <summary>
    /// �����ӳ�
    /// </summary>
    public int bonus_Def;
    /// <summary>
    /// ħ�������ӳ�
    /// </summary>
    public int bonus_MagicDef;
    /// <summary>
    /// �����ظ�
    /// </summary>
    public int Heal_Hp;
    /// <summary>
    /// ħ���ظ�
    /// </summary>
    public int Heal_Mp;
    /// <summary>
    /// ��������
    /// </summary>
    public int[] life = new int[] { 0, 0, 0, 0, 0 };
    /// <summary>
    /// ���������
    /// </summary>
    public int unit = 0;

    public int totalPower;
    /// <summary>
    /// ��ʾս����
    /// </summary>
    public void Init()
    {
        totalPower = (int)(MaxHP / 10 + MaxMp / 5 + internalforceMP + EnergyMp +
            DefMin + DefMax + MagicDefMin + MagicDefMax + damageMin + damageMax + MagicdamageMin + MagicdamageMax +
            hit + dodge * 5 + penetrate * 5 + block * 5 + crit_rate * 10 + crit_damage + double_damage * 10 + Lucky * 100 +
            Damage_Reduction * 10 + Damage_absorption * 10 + resistance * 10 + move_speed + (200 - attack_speed) * 10 + attack_distance +
            bonus_Hp * 20 + bonus_Mp * 20 + bonus_Damage * 20 + bonus_MagicDamage * 20 + bonus_Def * 20 + bonus_MagicDef * 20 +
            Heal_Hp * 20 + Heal_Mp * 20 + life[0] * 20 + life[1] * 20 + life[2] * 20 + life[3] * 20 + life[4]) * 20;
    }
}
