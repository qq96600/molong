using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill_offect_item : Base_Mono
{
    private Text info;
    private void Awake()
    {
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
            info.text = data.skillname;
        }
        get
        {
            return data;
        }
    }
}
