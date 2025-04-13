using MVC;
using UnityEngine;
using UnityEngine.UI;

public class material_item : Base_Mono
{
    private Image item_icon, item_frame;
    /// <summary>
    /// ������Ϣ
    /// </summary>
    private Text base_info;
    /// <summary>
    /// ��Դ
    /// </summary>
    private (string, int) data;
    private void Awake()
    {
        item_icon = Find<Image>("icon");
        item_frame = GetComponent<Image>();
        base_info= Find<Text>("base_info");
    }
    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="bag_Resources"></param>
    public void Init((string, int) bag_Resources)
    { 
        data = bag_Resources;
        //item_icon.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", data.Name);
        base_info.text = data.Item2.ToString() +"";
    }

}
