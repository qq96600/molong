using MVC;
using UnityEngine;
using UnityEngine.UI;

public class bag_item : Base_Mono
{
    private Image item_icon, item_frame;
    private void Awake()
    {
        item_icon = Find<Image>("icon");
        item_frame = GetComponent<Image>();
    }

    private Bag_Base_VO data;

    /// <summary>
    /// Data
    /// </summary>
    public Bag_Base_VO Data
    {
        set
        {
            data = value;
            if (data == null) return;
            item_icon.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", data.Name);
            //Resources.Load<Sprite>("icon/1102" + Random.Range(100,450));
        }
        get
        {
            return data;
        }
    }
}
