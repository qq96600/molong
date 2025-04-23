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
        item_frame = Find<Image>("frame");
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
                int lv = int.Parse(info_str[2]);
                if (lv <= 5)
                {
                    item_frame.sprite = UI.UI_Manager.I.GetEquipSprite("frame/", lv.ToString());
                    item_frame.color= Color.white;
                }
                else
                {
                    lv++;
                    item_frame.sprite = UI.UI_Manager.I.GetEquipSprite("frame/", "5");
                    item_frame.color = Color.white;
                    Instantiate(Resources.Load<GameObject>("Prefabs/frame/" + lv), item_frame.transform);
                }
            }
            
        }
        get
        {
            return data;
        }
    }
}
