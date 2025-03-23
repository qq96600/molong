using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero_item : Base_Mono
{
    private Text base_info;
    private Image item_icon;
    private void Awake()
    {
        base_info=Find<Text>("base_info");
    }


    private db_hero_vo data;

    /// <summary>
    /// Data
    /// </summary>
    public db_hero_vo Data
    {
        set
        {
            data = value;
            if (data == null) return;
            base_info.text = data.hero_name;
            //∂¡»°œ‘ æÕº±Í
        }
        get
        {
            return data;
        }
    }
}
