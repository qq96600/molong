using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill_item : Base_Mono
{
    private Image item_icon, item_frame;

    private Text info;
    private void Awake()
    {
        item_icon = Find<Image>("frame/icon");
        item_frame = Find<Image>("frame/state");
        info = Find<Text>("info");
    }

    private base_skill_vo data;

    /// <summary>
    /// Data
    /// </summary>
    public base_skill_vo Data
    {
        set
        {
            data = value;
            if (data == null) return;
            //item_icon.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", "10050" + Random.Range(16, 55));
            info.text= data.skillname+"Lv."+ data.skilllv;
            item_frame.gameObject.SetActive(data.skillpos != 0);
        }
        get
        {
            return data;
        }
    }
}
