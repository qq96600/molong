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
    /// �̵����͸�����
    /// </summary>
    private Transform store_type; 
    /// <summary>
    /// �̵���������
    /// </summary>
    private string[] type_name = { "����", "�޹�", "����" };
    /// <summary>
    /// �̵����Ͱ�ť
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// �̵�����Ʒ������
    /// </summary>
    private Transform store_item;
    /// <summary>
    /// �̵�����ƷԤ����
    /// </summary>
    private material_item material_item;
    /// <summary>
    /// ��ǰ�̵���Ʒ�ֵ�
    /// </summary>
    private Dictionary<string, db_store_vo> items_dic = new Dictionary<string, db_store_vo>();
    /// <summary>
    /// ��ǰ�̵���Ʒ�б�
    /// </summary>
    private List<db_store_vo> items_list = new List<db_store_vo>();
    /// <summary>
    /// �̵���Ʒ�������
    /// </summary>
    private Transform store_item_info;
    /// <summary>
    /// ��Ʒ���������
    /// </summary>
    private TMP_InputField inputField;
    /// <summary>
    /// ����������
    /// </summary>
    private Text buy_item_Title;
    /// <summary>
    /// ����ť
    /// </summary>
    private Button buy_btn;
    /// <summary>
    /// һ���Թ�����Ʒ����󳤶�
    /// </summary>
    private int maxLength=3;
    /// <summary>
    /// ������Ʒ���������
    /// </summary>
    private int max_num = 99;
    /// <summary>
    /// �����Ҫ���������
    /// </summary>
    private int buy_num = 1;
    /// <summary>
    /// ��ǰѡ����̵���Ʒ
    /// </summary>
    private db_store_vo buy_item;
    /// <summary>
    /// �رհ�ť
    /// </summary>
    private Button btn;
    /// <summary>
    /// ��ʾ���������
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

        #region �̵���Ʒ�������
        store_item_info = Find<Transform>("store_item_buy");
        inputField = Find<TMP_InputField>("store_item_buy/inputField");
        buy_item_Title = Find<Text>("store_item_buy/buy_item_Title/Title");
        buy_btn = Find<Button>("store_item_buy/buy_btn");
        buy_text = Find<Text>("store_item_buy/buy_text");

        inputField.onEndEdit.AddListener(OnInputChanged);//���������
        buy_btn.onClick.AddListener(BuyItem);//��������ť
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
    /// ����رչ������
    /// </summary>
    private void CloseBuyInterface()
    {
        store_item_info.gameObject.SetActive(false);
        btn.gameObject.SetActive(false);
    }

  

    /// <summary>
    /// �������
    /// </summary>
    private void BuyItem()
    {
        NeedConsumables(buy_item.unit,(buy_num * buy_item.ItemPrice));//��Ҫ�������Ʒ�Լ��۸�
        if (RefreshConsumables())//�ж��Ƿ���ɹ�
        {
            if (buy_item.ItemMaxQuantity > 0)//�޹���Ʒ
            {
                //�����޹���Ʒ�ɹ��������
                for(int i= 0; i < SumSave.crt_needlist.store_value_list.Count; i++)//�����޹���Ʒ
                {
                    int nums = buy_item.ItemMaxQuantity - int.Parse(SumSave.crt_needlist.store_value_list[i][1]);//�ж��޹���Ʒ�Ƿ�����
                    if (SumSave.crt_needlist.store_value_list[i][0] == buy_item.ItemName&& nums > 0)//�����޹���Ʒ
                    {
                        if(buy_num> nums)//������������ι���ʱ�ж��Ƿ񳬳��޹�����
                        {
                            buy_num= nums;
                        }
                        int num =int.Parse(SumSave.crt_needlist.store_value_list[i][1])+ buy_num;
                        SumSave.crt_needlist.store_value_list[i][1]= num.ToString();
                        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
                        Battle_Tool.Obtain_Resources(buy_item.ItemName, buy_num);//��ȡ����
                        Alert_Dec.Show(buy_item.ItemName + "X" + buy_num + " ����ɹ�(�޹���Ʒ) ");
                        return;
                    }
                }
                Alert_Dec.Show("�޹���Ʒ " + buy_item.ItemName + " �޹������ ");
                return;
            }
            Battle_Tool.Obtain_Resources(buy_item.ItemName, buy_num);//��ȡ����
            Alert_Dec.Show(buy_item.ItemName + "X" + buy_num + " ����ɹ� ");
        }
        else
        {
            Alert_Dec.Show(buy_item.unit + " ��������");
            
        }

    }


    

    /// <summary>
    /// ��ʾ�̵�����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void ShowItem(int index)
    {
        items_dic.Clear();
        items_list.Clear();
        ClearObject(store_item);
        for (int i = 0; i < SumSave.db_stores_list.Count; i++)//�ж��Ƿ�Ϊ��ǰ�̵�����
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
                    Debug.LogError("�̵���" + SumSave.db_stores_list[i].ItemName + "��Ʒ�ظ�");
                }
            }
        }
        
        for (int i = 0; i < items_list.Count; i++)//��ʾ�̵�����Ʒ
        {
            material_item item = Instantiate(material_item, store_item);

            item.Init((items_list[i].ItemName, items_list[i].ItemPrice));
            db_store_vo items = items_list[i];
            item.GetComponent<Button>().onClick.AddListener(() => { ShowItemInfo(items); });
        }
       
    }
    /// <summary>
    /// �����Ʒ��ʾ��Ʒ��Ϣ
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
           buy_text.text = "�����������" + buy_item.ItemMaxQuantity;
        }else
        {
            buy_text.text = " ";
        }

    }

    /// <summary>
    /// ������Ʒ��������
    /// </summary>
    /// <param name="arg0"></param>
    private void OnInputChanged(string newText)
    {

        char[] chars = newText.ToCharArray();
        string filteredText= "";
        // ����������
        foreach (char c in chars)
        {
            if (char.IsDigit(c)) 
            {
                filteredText+= c;
            }
        }
        // �ضϳ�������
        if (filteredText.Length > maxLength)
        {
            filteredText = filteredText.Substring(0, maxLength);
        }

        if (buy_item.ItemMaxQuantity > 0)//�������������ʱ
        {
            


            if (buy_item.ItemMaxQuantity > 0)//�޹���Ʒ
            {
                //�����޹���Ʒ�ɹ��������
                for (int i = 0; i < SumSave.crt_needlist.store_value_list.Count; i++)//�����޹���Ʒ
                {
                    if (SumSave.crt_needlist.store_value_list[i][0] == buy_item.ItemName)
                    {
                        int num = int.Parse(SumSave.crt_needlist.store_value_list[i][1]);//����Թ��������
                        if (int.Parse(filteredText) > (buy_item.ItemMaxQuantity - num))//�ж������ֵ�Ƿ�������������
                        {
                            filteredText = (buy_item.ItemMaxQuantity - num).ToString();
                            Alert_Dec.Show("�������������");
                        }

                    }
                }
            }
        }

        // ͬ�����������
        if (filteredText != inputField.text)
        {
            inputField.text = filteredText;
            SetCursorToEnd();
        }
        //��ȡ��������
        if (int.TryParse(inputField.text, out int value))
        {
            buy_num = value;   
        }else
        {
            buy_num = 1;
        }
    }
    // ���ֹ����ĩβ������������
    private void SetCursorToEnd()
    {
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;
    }
}
