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

public class panel_store : Base_Mono
{
    /// <summary>
    /// 商店类型父物体
    /// </summary>
    private Transform store_type; 
    /// <summary>
    /// 商店类型名字
    /// </summary>
    private string[] type_name = { "道具", "限购", "离线" };
    /// <summary>
    /// 商店类型按钮
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// 商店内物品父物体
    /// </summary>
    private Transform store_item;
    /// <summary>
    /// 商店内物品预制体
    /// </summary>
    private material_item material_item;
    /// <summary>
    /// 当前商店物品字典
    /// </summary>
    private Dictionary<string, db_store_vo> items_dic = new Dictionary<string, db_store_vo>();
    /// <summary>
    /// 当前商店物品列表
    /// </summary>
    private List<db_store_vo> items_list = new List<db_store_vo>();
    /// <summary>
    /// 商店物品购买界面
    /// </summary>
    private Transform store_item_info;
    /// <summary>
    /// 商品输入的数量
    /// </summary>
    private TMP_InputField inputField;
    /// <summary>
    /// 购买界面标题
    /// </summary>
    private Text buy_item_Title;
    /// <summary>
    /// 购买按钮
    /// </summary>
    private Button buy_btn;
    /// <summary>
    /// 一次性购买商品的最大长度
    /// </summary>
    private int maxLength=3;
    /// <summary>
    /// 购买商品的最大数量
    /// </summary>
    private int max_num = 99;
    /// <summary>
    /// 玩家需要购买的数量
    /// </summary>
    private int buy_num = 1;
    /// <summary>
    /// 当前选择的商店物品
    /// </summary>
    private db_store_vo buy_item;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    private Button btn;
    /// <summary>
    /// 显示最大购买数量
    /// </summary>
    private Text buy_text;
  
    private void Awake()
    {
        store_type = Find<Transform>("store_type");
        btn_item = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        store_item = Find<Transform>("store_item/Viewport/Content");
        material_item = Resources.Load<material_item>("Prefabs/panel_bag/material_item"); 
         btn = Find<Button>("but");

        btn.onClick.AddListener(() =>{ CloseBuyInterface(); });

        #region 商店物品购买界面
        store_item_info = Find<Transform>("store_item_buy");
        inputField = Find<TMP_InputField>("store_item_buy/inputField");
        buy_item_Title = Find<Text>("store_item_buy/buy_item_Title/Title");
        buy_btn = Find<Button>("store_item_buy/buy_btn");
        buy_text = Find<Text>("store_item_buy/buy_text");

        inputField.onEndEdit.AddListener(OnInputChanged);//监听输入框
        buy_btn.onClick.AddListener(BuyItem);//监听购买按钮
        store_item_info.gameObject.SetActive(false);
        #endregion

        ClearObject(store_type);
        for (int i = 0; i < type_name.Length; i++)
        {
            btn_item btn = Instantiate(btn_item, store_type);
            btn.Show(i, type_name[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { ShowItem(btn.index); });
        }
        ShowItem(0);
       
    }
    /// <summary>
    /// 点击关闭购买界面
    /// </summary>
    private void CloseBuyInterface()
    {
        store_item_info.gameObject.SetActive(false);
        btn.gameObject.SetActive(false);
    }

  

    /// <summary>
    /// 点击购买
    /// </summary>
    private void BuyItem()
    {
        NeedConsumables(buy_item.unit,(buy_num * buy_item.ItemPrice));//需要购买的商品以及价格
        if (RefreshConsumables())//判断是否购买成功
        {
            if (buy_item.ItemMaxQuantity > 0)//限购物品
            {
                //减少限购物品可购买的数量
                for(int i= 0; i < SumSave.crt_needlist.store_value_list.Count; i++)//查找限购物品
                {
                    int nums = buy_item.ItemMaxQuantity - int.Parse(SumSave.crt_needlist.store_value_list[i][1]);//判断限购商品是否购买完
                    if (SumSave.crt_needlist.store_value_list[i][0] == buy_item.ItemName&& nums > 0)//查找限购物品
                    {
                        if(buy_num> nums)//不更改数量多次购买时判断是否超出限购数量
                        {
                            buy_num= nums;
                        }
                        int num =int.Parse(SumSave.crt_needlist.store_value_list[i][1])+ buy_num;
                        SumSave.crt_needlist.store_value_list[i][1]= num.ToString();
                        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
                        Battle_Tool.Obtain_Resources(buy_item.ItemName, buy_num);//获取奖励
                        Alert_Dec.Show(buy_item.ItemName + "X" + buy_num + " 购买成功(限购物品) ");
                        return;
                    }
                }
                Alert_Dec.Show("限购商品 " + buy_item.ItemName + " 无购买次数 ");
                return;
            }
            Battle_Tool.Obtain_Resources(buy_item.ItemName, buy_num);//获取奖励
            Alert_Dec.Show(buy_item.ItemName + "X" + buy_num + " 购买成功 ");
        }
        else
        {
            Alert_Dec.Show(buy_item.unit + " 数量不够");
            
        }

    }


    

    /// <summary>
    /// 显示商店内容
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void ShowItem(int index)
    {
        items_dic.Clear();
        items_list.Clear();
        ClearObject(store_item);
        for (int i = 0; i < SumSave.db_stores_list.Count; i++)//判断是否为当前商店类型
        {
            if (SumSave.db_stores_list[i].store_Type == (index + 1))
            {
                if (!items_dic.ContainsKey(SumSave.db_stores_list[i].ItemName))
                {
                    items_dic.Add(SumSave.db_stores_list[i].ItemName, SumSave.db_stores_list[i]);
                    items_list.Add(SumSave.db_stores_list[i]);
                }
                else
                {
                    Debug.LogError("商店内" + SumSave.db_stores_list[i].ItemName + "物品重复");
                }
            }
        }
        
        for (int i = 0; i < items_list.Count; i++)//显示商店内物品
        {
            material_item item = Instantiate(material_item, store_item);

            item.Init((items_list[i].ItemName, items_list[i].ItemPrice));
            db_store_vo items = items_list[i];
            item.GetComponent<Button>().onClick.AddListener(() => { ShowItemInfo(items); });
        }
       
    }
    /// <summary>
    /// 点击物品显示物品信息
    /// </summary>
    /// <param name="db_store_vo"></param>
    private void ShowItemInfo(db_store_vo item)
    {
        btn.gameObject.SetActive(true);
        store_item_info.gameObject.SetActive(true);
        buy_item_Title.text = item.ItemName;
        buy_item= item;
        if(buy_item.ItemMaxQuantity > 0)
        {
           buy_text.text = "最大购买数量：" + buy_item.ItemMaxQuantity;
        }else
        {
            buy_text.text = " ";
        }

    }

    /// <summary>
    /// 购买物品输入数量
    /// </summary>
    /// <param name="arg0"></param>
    private void OnInputChanged(string newText)
    {

        char[] chars = newText.ToCharArray();
        string filteredText= "";
        // 仅保留数字
        foreach (char c in chars)
        {
            if (char.IsDigit(c)) 
            {
                filteredText+= c;
            }
        }
        // 截断超长部分
        if (filteredText.Length > maxLength)
        {
            filteredText = filteredText.Substring(0, maxLength);
        }

        if (buy_item.ItemMaxQuantity > 0)//当有最大购买数量时
        {
            


            if (buy_item.ItemMaxQuantity > 0)//限购物品
            {
                //减少限购物品可购买的数量
                for (int i = 0; i < SumSave.crt_needlist.store_value_list.Count; i++)//查找限购物品
                {
                    if (SumSave.crt_needlist.store_value_list[i][0] == buy_item.ItemName)
                    {
                        int num = int.Parse(SumSave.crt_needlist.store_value_list[i][1]);//玩家以购买的数量
                        if (int.Parse(filteredText) > (buy_item.ItemMaxQuantity - num))//判断输入的值是否大于最大购买数量
                        {
                            filteredText = (buy_item.ItemMaxQuantity - num).ToString();
                            Alert_Dec.Show("超过最大购买数量");
                        }

                    }
                }
            }
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
        }else
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
}
