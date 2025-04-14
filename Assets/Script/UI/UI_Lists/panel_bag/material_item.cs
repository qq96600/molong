using MVC;
using UnityEngine;
using UnityEngine.UI;

public class material_item : Base_Mono
{
    private Image item_icon, item_frame;
    /// <summary>
    /// 基础信息
    /// </summary>
    private Text base_info;
    /// <summary>
    /// 资源
    /// </summary>
    private (string, int) data;
    private void Awake()
    {
        item_icon = Find<Image>("icon");
        item_frame = GetComponent<Image>();
        base_info= Find<Text>("base_info");
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bag_Resources"></param>
    public void Init((string, int) bag_Resources)
    { 
        data = bag_Resources;
        //item_icon.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", data.Name);
        base_info.text = data.Item2.ToString() +"";
    }

}
