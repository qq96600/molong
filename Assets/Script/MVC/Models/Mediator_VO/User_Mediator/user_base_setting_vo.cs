using Common;
using MVC;

public class user_base_setting_vo : Base_VO
{
    /// <summary>
    /// ��ȡ�û���������
    /// </summary>
    public string user_value;
    /// <summary>
    /// ת������
    /// </summary>
    public int[] user_setting;

    public override string[] Set_Instace_String()
    {
        return 
            new string[] { 
                GetStr(0),
                GetStr(SumSave.crt_user.uid),
                GetStr(user_value),
                };
    }

    public override string[] Get_Update_Character()
    {
        return new string[] {
            "setting_value",
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] {
            GetStr(user_value)
        };
    }
}
