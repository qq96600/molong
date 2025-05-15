using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fate_item : Base_Mono
{

    private Image item_image;
    private Text quantity,number;

    private void Awake()
    {
        item_image=Find<Image>("item_image");
        quantity = Find<Text>("quantity");
        number = Find<Text>("number");
    }

    public void Init((string, int, int, int, int) data, int num)//名字，奖励数量，抽奖次数，抽奖次数上限，权重
    {
        item_image.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", data.Item1);
        quantity.text = data.Item3.ToString();
        
        number.text = num + "/" + data.Item4.ToString();
    }


}
