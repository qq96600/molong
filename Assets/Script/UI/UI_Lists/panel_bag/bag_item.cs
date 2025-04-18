using MVC;
using UnityEngine;
using UnityEngine.UI;

public class bag_item : Base_Mono
{
    private Image item_icon, item_frame;
    private Text info;
    private void Awake()
    {
        item_icon = Find<Image>("icon");
        item_frame = GetComponent<Image>();
        info = Find<Text>("info");
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
            if(data.user_value!=null)
            {
                string[] info_str = data.user_value.Split(' ');
                info.text = info_str[1] == "0" ? "" : ("+" + info_str[1]);
            }
            
        }
        get
        {
            return data;
        }
    }
}
