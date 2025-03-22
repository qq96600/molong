using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_equip_item : Base_Mono
{
    public EquipTypeList type;
    /// <summary>
    /// 装备弹出框
    /// </summary>
    //private EquipPopups equipPopup;
    /// <summary>
    /// 预制件
    /// </summary>
    public bag_item BagItemPrefabs;

    private void Awake()
    {
        BagItemPrefabs = Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
    }
    private void Start()
    {
        transform.parent.parent.parent.parent.SendMessage("Instance_Pos", this);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        data = null;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 显示装备信息
    /// </summary>
    private void ShowEquip()
    {
        //equipPopup.gameObject.SetActive(true);
        //equipPopup.Data = data;
       
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

            bag_item item = Instantiate(BagItemPrefabs, transform);
            item.Data = data;
            //item.GetComponent<Button>().onClick.AddListener(() => { AudioManager.Instance.playAudio(ClipEnum.购买物品); ShowEquip(); });
            

        }
        get
        {
            return data;
        }
    }
}
