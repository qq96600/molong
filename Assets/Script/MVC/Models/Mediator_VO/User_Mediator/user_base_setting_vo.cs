using Common;
using MVC;

public class user_base_setting_vo : Base_VO
{
    /// <summary>
    /// 读取用户设置数据
    /// </summary>
    public string user_value;
    /// <summary>
    /// 转译设置
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
