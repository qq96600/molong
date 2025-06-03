using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chemical_furnace : Base_Mono
{
    /// <summary>
    /// 合成物品位置，材料位置
    /// </summary>
    private Transform synthesis_Items, material_Items;
    /// <summary>
    /// 合成按钮
    /// </summary>
    private Button synthesisButtom;
    /// <summary>
    /// 商品输入的数量
    /// </summary>
    private TMP_InputField inputField;
    /// <summary>
    /// 合成物品的最大长度
    /// </summary>
    private int maxLength = 2;
    /// <summary>
    /// 玩家需要合成的数量
    /// </summary>
    private int buy_num = 1;
    /// <summary>
    /// 合成物品预制体
    /// </summary>
    private btn_item btn_item_Prefabs;

    /// <summary>
    /// 当前选择的合成物品
    /// </summary>
    private (string,int,int) synthesis_item;

 

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item");
        inputField = Find<TMP_InputField>("InputField");
        inputField.onEndEdit.AddListener(OnInputChanged);//监听输入框
        synthesis_Items= Find<Transform>("synthesis_Items/Viewport/Content");
        material_Items = Find<Transform>("material_Items/Viewport/Content");
        synthesisButtom = Find<Button>("synthesisButtom");
        synthesisButtom.onClick.AddListener(Synthesis);
        Init();
        ShowMaterial(SumSave.db_formula_list[0].formula_result_list);
    }
    
    private void Init()
    {
        ClearObject(synthesis_Items);
        for(int i = 0; i < SumSave.db_formula_list.Count; i++)
        {
           (string, int,int) item1 = SumSave.db_formula_list[i].formula_result_list;
           btn_item item = Instantiate(btn_item_Prefabs, synthesis_Items);
           string text = item1.Item1+"*"+ item1.Item2;
           item.Show(i, text);
           item.GetComponent<Button>().onClick.AddListener(() => { ShowMaterial(item1); });
        }

    }

    /// <summary>
    /// 显示需要的材料
    /// </summary>

    private void ShowMaterial((string , int,int) item)
    {
        ClearObject(material_Items);
        synthesis_item= item;
        for (int i = 0; i < SumSave.db_formula_list.Count; i++)
        {
            if(item.Item1==SumSave.db_formula_list[i].formula_result_list.Item1)
            {
                if(SumSave.db_formula_list[i].formula_type == "1")
                {
                    inputField.gameObject.SetActive(false);
                }else
                {
                    inputField.gameObject.SetActive(true);
                    inputField.text=1.ToString();
                }
                ClearObject(material_Items);
                for (int j=0;j< SumSave.db_formula_list[i].formula_need_list.Count;j++)
                {
                    btn_item _item = Instantiate(btn_item_Prefabs, material_Items);
                    (string, int) item1 = SumSave.db_formula_list[i].formula_need_list[j];
                    string text = item1.Item1 + "*" + item1.Item2;
                    _item.Show(i, text);
                }

            }
        }
    }




    /// <summary>
    /// 合成物品
    /// </summary>
    private void Synthesis()
    {
        for (int i = 0; i < SumSave.db_formula_list.Count; i++)
        {
            if (synthesis_item.Item1 == SumSave.db_formula_list[i].formula_result_list.Item1)
            {
                if (SumSave.db_formula_list[i].formula_type == "1")
                {
                    buy_num = 1;   
                }

                for (int j = 0; j < SumSave.db_formula_list[i].formula_need_list.Count; j++)
                {
                    NeedConsumables(SumSave.db_formula_list[i].formula_need_list[j].Item1, SumSave.db_formula_list[i].formula_need_list[j].Item2* buy_num);
                   
                }

                if (RefreshConsumables())
                {
                    Battle_Tool.Obtain_result(synthesis_item.Item1, buy_num);
                    Alert.Show("合成成功", "获得物品" + synthesis_item.Item1 + "*" + synthesis_item.Item2 * buy_num);
                }
            }
        }



    }




    /// <param name="arg0"></param>
    private void OnInputChanged(string newText)
    {
        char[] chars = newText.ToCharArray();
        string filteredText = "";
        // 仅保留数字
        foreach (char c in chars)
        {
            if (char.IsDigit(c))
            {
                filteredText += c;
            }
        }
        // 截断超长部分
        if (filteredText.Length > maxLength)
        {
            filteredText = filteredText.Substring(0, maxLength);
        }
        // 同步输入框内容
        if (filteredText != inputField.text)
        {
            inputField.text = filteredText;
            SetCursorToEnd();
        }
        //获取购买数量
        if (int.TryParse(inputField.text, out int value))
        {
            buy_num = value;
        }
        else
        {
            buy_num = 1;
        }
    }
    // 保持光标在末尾（避免跳动）
    private void SetCursorToEnd()
    {
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;
    }


    public override void Show()
    {
        base.Show();
    }

}
