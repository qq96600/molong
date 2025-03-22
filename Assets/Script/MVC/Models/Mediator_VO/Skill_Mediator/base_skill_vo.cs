using MVC;
using System.Collections.Generic;

public class base_skill_vo : Base_VO
{
    private string skill_name;
    /// <summary>
    /// ��������
    /// </summary>
    public string skillname
    {
        get { return skill_name; }

        set { skill_name = value; }
    }

    private int skill_lv;
    /// <summary>
    /// J���ܵȼ�
    /// </summary>
    public int skilllv
    { 
        get { return skill_lv; }
        set { skill_lv = value; }
    }

    private int skill_pos;

    /// <summary>
    /// ����λ��
    /// </summary>
    public int skillpos
    { 
        get { return skill_pos; }
        set { skill_pos = value; }
    }

    private int skill_internalforceMP;

    /// <summary>
    /// ��������
    /// </summary>
    public int skillinternalforceMP
    { 
        get { return skill_internalforceMP; }
        set { skill_internalforceMP = value; }
    }

    /// <summary>
    /// �������� 1ս��2����3����
    /// </summary>
    public int skill_type;
    /// <summary>
    /// �����˺����� 1����2ħ��3����4��������6��Ѫ7����
    /// </summary>
    public int skill_damage_type;
    /// <summary>
    /// �������ȼ�
    /// </summary>
    public int skill_max_lv;
    /// <summary>
    /// ���ܳ�ʼ����������
    /// </summary>
    public int skill_need_exp;
    /// <summary>
    /// ��������ϵ�� [0]*mathf.pow([1],[2])
    /// </summary>
    public List<int> skill_need_coefficient;
    /// <summary>
    /// ���ܼ���Ч�� �ȼ�+�������� *�ָ�
    /// </summary>
    public List<(int,string)> skill_need_state;
    /// <summary>
    /// �����Ч������
    /// 1����ֵ
    /// 2ħ��ֵ
    /// 3����ֵ
    /// 4������
    /// 5ħ������
    /// 6�������
    /// 7ħ������
    /// 8�����ٶ�
    /// 9������
    /// 10���
    /// </summary>
    public List<int> skill_open_type;
    /// <summary>
    /// ��Ӧ����ֵ
    /// </summary>
    public List<int> skill_open_value;
    /// <summary>
    /// ����Ч�� ͬ����Ч��
    /// </summary>
    public List<int> skill_pos_type;
    /// <summary>
    /// ����Ч��ֵ
    /// </summary>
    public List<int> skill_pos_value;
    /// <summary>
    /// �����˺�
    /// </summary>
    public int skill_damage;
    /// <summary>
    /// �������������˺�
    /// </summary>
    public int skill_power;
    /// <summary>
    /// ���ķ����ٷֱ�
    /// </summary>
    public int skill_spell=7;
    /// <summary>
    /// ����cd
    /// </summary>
    public int skill_cd;
    /// <summary>
    /// ������װ����
    /// </summary>
    public int skill_suit_type;
    /// <summary>
    /// ������װЧ��
    /// </summary>
    public int skill_suit_value;

}
