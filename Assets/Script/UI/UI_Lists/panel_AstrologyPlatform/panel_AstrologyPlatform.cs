

using Common;
using Components;
using MVC;
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
    private void AddWeather()
    {
        string name = "";
        int weight = 0;
        bool isAdd = true;
        for (int i = 0; i < SumSave.db_weather_list.Count; i++)
        {
            weight += SumSave.db_weather_list[i].probability;
        }

        while (isAdd)
        {
            for (int i = 0; i < SumSave.db_weather_list.Count; i++)
            {
                if (SumSave.db_weather_list[i].probability >= Random.Range(0, weight))
                {
                    name = SumSave.db_weather_list[i].weather_type;
                    isAdd = false;
                    break;
                }
            }
        }
        SumSave.crt_player_buff.player_Buffs.Add(name, (SumSave.nowtime, 60 * 6, 1, 4));
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.user_player_buff, SumSave.crt_player_buff.Set_Uptade_String(), SumSave.crt_player_buff.Get_Update_Character());
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
