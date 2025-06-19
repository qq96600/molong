

using Common;
using Components;
using MVC;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_AstrologyPlatform : Panel_Base
{
    /// <summary>
    /// 天气图标
    /// </summary>
    private Image weatherImage;
    /// <summary>
    /// 天气介绍
    /// </summary>
    private Text information;
    /// <summary>
    /// 切换天气按钮
    /// </summary>
    private Button switchButton;
    /// <summary>
    /// 切换天气消耗灵珠数量
    /// </summary>
    private int need = 1;
    
    public override void Initialize()
    {
        base.Initialize();
        weatherImage = Find<Image>("weatherImage");
        information = Find<Text>("information/Viewport/Content/Text");
        switchButton = Find<Button>("switchButton");
        switchButton.onClick.AddListener(SwitchWeather);

    }
    /// <summary>
    /// 界面初始化
    /// </summary>
    private void Init()
    {
        if (SumSave.crt_player_buff.player_Buffs.Count > 0)
        {
            foreach (var _item in SumSave.crt_player_buff.player_Buffs)
            {
                if (_item.Value.Item4 == 4)
                {
                    //weatherImage.sprite = Resources.Load<Sprite>("" + _item.Key);
                    for (int i = 0; i < SumSave.db_weather_list.Count; i++)
                    {
                        if(SumSave.db_weather_list[i].weather_type == _item.Key)
                        {
                            information.text = ShowBonus(SumSave.db_weather_list[i].life_value_list);
                        }
                    }
                }
            }
        }

    }
    /// <summary>
    /// 显示属性
    /// </summary>
    /// <param name="id"></param>
    private string ShowBonus(List<(int, int)> id)
    {
        string str = "";
        for (int j = 0; j <= id.Count; j++)
        {
            switch (id[j].Item1)
            {
                case 1:
                    //int baseValue = SumSave.crt_MaxHero.life[1];
                    //int percentageValue = baseValue * (id.Item2 / 100);
                    //int price = baseValue > percentageValue ? baseValue : percentageValue;
                    str += enum_skill_attribute_list.土.ToString() + ":" + SumSave.crt_MaxHero.life[0] + "%";
                    break;
                case 2:
                    str += enum_skill_attribute_list.火.ToString() + ":" + SumSave.crt_MaxHero.life[1] + "%";
                    break;
                case 3:
                    str += enum_skill_attribute_list.水.ToString() + ":" + SumSave.crt_MaxHero.life[2] + "%";
                    break;
                case 4:
                    str += enum_skill_attribute_list.木.ToString() + ":" + SumSave.crt_MaxHero.life[3] + "%";
                    break;
                case 5:
                    str += enum_skill_attribute_list.金.ToString() + ":" + SumSave.crt_MaxHero.life[4] + "%";
                    break;

            }
        }
       return str;
    }

    /// <summary>
    /// 切换天气
    /// </summary>
    private void SwitchWeather()
    {
        NeedConsumables(currency_unit.灵珠, need);
        if (RefreshConsumables())
        {
            if (SumSave.crt_player_buff.player_Buffs.Count > 0)
            {
                bool isAdd = true;
                foreach (var _item in SumSave.crt_player_buff.player_Buffs)
                {
                    if (_item.Value.Item4 == 4)
                    {
                        isAdd = false;
                        SumSave.crt_player_buff.player_Buffs.Remove(_item.Key);
                        AddWeather();
                        break;
                    }
                }
                if (isAdd)
                {
                    AddWeather();
                }
            }
        }
        else
        {
            Alert_Dec.Show("灵珠不足");
        }
    }
   
    public override void Hide()
    {
        base.Hide();
    }

   

    public override void Show()
    {
        base.Show();
    }
}
