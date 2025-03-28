using Common;
using MVC;
/// <summary>
/// ��ɫ����
/// </summary>
public class Hero_VO : Base_VO
{
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public string hero_name;
    /// <summary>
    /// ��ɫְҵ
    /// </summary>
    public string hero_type;
    /// <summary>
    /// ��ɫ���
    /// </summary>
    public string hero_index;
    /// <summary>
    /// ����Ӣ���б�
    /// </summary>
    public string hero_list;
    /// <summary>
    /// ��ɫ�ȼ�
    /// </summary>
    public string hero_lv;
    /// <summary>
    /// ��ɫ�ȼ�
    /// </summary>
    public int hero_Lv;
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public string hero_exp;
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public long hero_Exp;
    /// <summary>
    /// ��ɫ����ѡ��
    /// </summary>
    public string hero_pos;
    /// <summary>
    /// ������
    /// </summary>
    public string hero_value;
    /// <summary>
    /// ��ȡӢ����Դ
    /// </summary>
    public string hero_material;
    /// <summary>
    /// Ӣ����Դ 1��� 2שʯ 3����ֵ 4����
    /// </summary>
    public int[] hero_material_list;
    public override string[] Set_Instace_String()
    {
        return new string[]
        {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(hero_name),
            GetStr(hero_type),
            GetStr(hero_index),
            GetStr(hero_list),
            GetStr(hero_lv),
            GetStr(hero_exp),
            GetStr(hero_pos),
            GetStr(hero_value),
            GetStr(ArrayHelper.Data_Encryption(hero_material_list)),
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "hero_type",
            "hero_list",
            "hero_lv",
            "hero_exp",
            "hero_pos",
            "hero_material"
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
       {
            GetStr(hero_type),
            GetStr(hero_list),
            GetStr(hero_lv),
            GetStr(hero_exp),
            GetStr(hero_pos),
            GetStr(ArrayHelper.Data_Encryption(hero_material_list))
       };
    }

}
