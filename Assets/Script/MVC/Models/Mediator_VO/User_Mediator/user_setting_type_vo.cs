using Common;
using MVC;
public class user_setting_type_vo : Base_VO
{
    /// <summary>
    /// ����id
    /// </summary>
    public int id_setting;

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns></returns>
    public string type_setting;
    /// <summary>
    /// ����ѡ��
    /// </summary>
    /// <returns></returns>
    public string option_setting;
 

    public override string[] Set_Instace_String()
    {
        return
            new string[] {
                GetStr(0),
                GetStr(type_setting),
                GetStr(option_setting),

                };
    }
    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <returns></returns>
    public override string[] Get_Update_Character()
    {
        return new string[] {
            "type_setting",
            "option_setting",
        };
    }
    /// <summary>
    /// д�����
    /// </summary>
    /// <returns></returns>
    public override string[] Set_Uptade_String()
    {
        return new string[] {
           GetStr(type_setting),
           GetStr(option_setting)
        };
    }
}
