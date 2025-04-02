using Common;
using Components;
using MVC;
using UnityEngine.UI;

public class artifact_offect : Base_Mono
{
    private Button close;
    /// <summary>
    /// ȷ������
    /// </summary>
    private Button confirm;
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private Text artifact_name, artifact_info;
    /// <summary>
    /// ȷ������
    /// </summary>
    private Text confirm_info;
    /// <summary>
    /// ��ǰ��ʾ����Ʒ
    /// </summary>
    private artifact_item crt_artifact;
    private void Awake()
    {
        close=Find<Button>("close_button");
        close.onClick.AddListener(()=> { gameObject.SetActive(false); });
        confirm = Find<Button>("confirm");
        confirm.onClick.AddListener(()=> { confirm_artifact(); });
        artifact_name = Find<Text>("show_name/info");
        artifact_info = Find<Text>("info");
        confirm_info = Find<Text>("confirm/info");
    }

    /// <summary>
    /// ѡ�й���
    /// </summary>
    private void confirm_artifact()
    {
        AudioManager.Instance.playAudio(ClipEnum.ʹ����Ʒ);
        if (crt_artifact != null)
        {
            (string, int) result = ArrayHelper.Find(SumSave.crt_artifact.Set(), e => e.Item1 == crt_artifact.Data.arrifact_name);
            if (result.Item2 == 0)//δ����
            {
                NeedConsumables(crt_artifact.Data.arrifact_needs[0], int.Parse(crt_artifact.Data.arrifact_needs[1]));
                if (RefreshConsumables())
                {
                    result.Item1 = crt_artifact.Data.arrifact_name;
                    result.Item2 = 1;
                }
                SumSave.crt_artifact.Get(result);
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto,Mysql_Table_Name.mo_user_artifact, SumSave.crt_artifact.Set_Uptade_String(),SumSave.crt_artifact.Get_Update_Character());
                SendNotification(NotiList.Refresh_Max_Hero_Attribute);
                crt_artifact.Set(1);
                set_artifact(crt_artifact);
                Alert_Dec.Show("����ɹ�");
            }
            else
            {
                if (result.Item2 < int.Parse(crt_artifact.Data.arrifact_needs[2]))
                {
                    NeedConsumables(crt_artifact.Data.arrifact_needs[0], int.Parse(crt_artifact.Data.arrifact_needs[1]));
                    if (RefreshConsumables())
                    {
                        result.Item2 += 1;
                    }
                    SumSave.crt_artifact.Get(result);
                    crt_artifact.Set(result.Item2);
                    set_artifact(crt_artifact);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_artifact, SumSave.crt_artifact.Set_Uptade_String(), SumSave.crt_artifact.Get_Update_Character());
                    SendNotification(NotiList.Refresh_Max_Hero_Attribute);
                    Alert_Dec.Show("�����ɹ�");
                }
                else Alert_Dec.Show("������");
            }
        }
    }
    /// <summary>
    /// ���õ�ǰ��ʾ����Ʒ
    /// </summary>
    /// <param name="item"></param>
    public void set_artifact(artifact_item item)
    {
        crt_artifact=item;
        artifact_name.text = crt_artifact.Data.arrifact_name + (item.base_lv == 0
            ? "(δ����)" : Show_Color.Red("(Lv." + item.base_lv + ")"));
        string dec = Show_Color.Green("�ȼ�: ")+Show_Color.Red(item.base_lv);
        string[] splits = crt_artifact.Data.arrifact_effects;

        dec += Show_Color.Green("\n����: ");
        if (splits.Length > 1)
        {
            foreach (var base_info in splits)
            {
                string[] infos= base_info.Split(' ');
                //1���� 2ÿһ���ӳ� 3�����ȼ�
                if (infos.Length >= 3)
                {
                    dec += (infos[2] == "0" ? Show_Color.Green("�����ӳ�:") : infos[2] + "�� ����: ") +
                        (item.base_lv >= int.Parse(infos[2]) ? Show_Color.Red((enum_skill_attribute_list)int.Parse(infos[0]) + " + " + (int.Parse(infos[1]) * item.base_lv)) :
                        Show_Color.Grey((enum_skill_attribute_list)int.Parse(infos[0]) + " + " + (int.Parse(infos[1]) * item.base_lv) + "(δ����)")) +
                        tool_Categoryt.Obtain_unit(int.Parse(infos[0])) + "\n";
                    
                }
            }
        }
        dec+="\n "+ crt_artifact.Data.Artifact_dec;
        artifact_info.text = dec;
    }
}
