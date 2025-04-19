using MVC;
using UnityEngine.UI;

public class artifact_item : Base_Mono
{
    /// <summary>
    /// ��Ʒͼ��
    /// </summary>
    private Image icon;
    /// <summary>
    /// ��Ʒ��Ϣ
    /// </summary>
    private Text info;

    public int base_lv = 0;
    private void Awake()
    {
        icon = Find<Image>("icon");
        info = Find<Text>("info");
    }

    private db_artifact_vo data;

    /// <summary>
    /// Data
    /// </summary>
    public db_artifact_vo Data
    {
        set
        {
            data = value;
            if (data == null) return;
            icon.sprite = UI.UI_Manager.I.GetEquipSprite("UI/show_Artifact/", data.arrifact_name);
            info.text = data.arrifact_name + "(δ����)";
        }
        get
        {
            return data;
        }
    }
    /// <summary>
    /// ��ȡ�ȼ�
    /// </summary>
    /// <param name="lv"></param>
    public void Set(int lv)
    {
        base_lv = lv;
        info.text = data.arrifact_name+"Lv."+lv;

    }
}
