using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_plant : Panel_Base
{
    /// <summary>
    /// 选择植物下拉列表
    /// </summary>
    private Dropdown plantDropdown;
    /// <summary>
    /// 种植按钮,浇水按钮,收获按钮
    /// </summary>
    private Button seedingButton, wateringButton, harvestButton, expansionButton;
    private Transform Buttons;
    /// <summary>
    /// 种植植物父物体
    /// </summary>
    private Transform plantFields; 
    /// <summary>
    /// 植物预制体
    /// </summary>
    private panltItem plantPrefab;   
    /// <summary>
    /// 当前选择的植物
    /// </summary>
    private string currentPlant;

    /// <summary>
    /// 可种植的最大数量
    /// </summary>
    private int maxPlantCount;

    /// <summary>
    /// 背包种子字典
    /// </summary>
    private Dictionary<PanltEnum,user_plant_vo> plantBag =new Dictionary<PanltEnum, user_plant_vo>();
    /// <summary>
    /// 种植的植物列表
    /// </summary>
    private List<panltItem> panltList=new List<panltItem>();
    /// <summary>
    /// 植物信息面板
    /// </summary>
    private Transform plantInfo;
    private Text plantText;
    /// <summary>
    /// 浇水减少时间
    /// </summary>
    public int reduceTime=10; 
    /// <summary>
    /// 是否可以扩容，扩容的数量
    /// </summary>
    public int isExpansion=0; 



    protected override void Awake()
    {
        #region 按钮下拉列表初始化
        Buttons = transform.Find("plant_Buttons");
        seedingButton = Buttons.Find("seeding_Button").GetComponent<Button>();
        wateringButton = Buttons.Find("watering_Button").GetComponent<Button>();
        harvestButton = Buttons.Find("harvest_Button").GetComponent<Button>();
        expansionButton = Buttons.Find("expansion_Button").GetComponent<Button>();
        plantDropdown=Buttons.Find("plant_Dropdown").GetComponent<Dropdown>();
        plantDropdown.onValueChanged.AddListener(CurrentPlant);
        seedingButton.onClick.AddListener(Seeding);
        wateringButton.onClick.AddListener(Watering);
        harvestButton.onClick.AddListener(Harvest);
        expansionButton.onClick.AddListener(Expansion);
        InitDropdown();
        #endregion

        #region 植物预制体初始化
        plantPrefab= Resources.Load<panltItem>("Prefabs/panel_smallWorld/plantItem");
        plantFields= transform.Find("plantFields").transform;
        plantInfo=transform.Find("plant_info").transform;
        plantText=plantInfo.Find("plant_Text").GetComponent<Text>();
        plantInfo.gameObject.SetActive(false);
        
        //for (int i = 0; i < plantCount; i++)//初始化种植区域并添加到列表
        //{
        //    GameObject plantItem = Instantiate(plantPrefab, plantFields);
        //    panltItem Item= plantItem.GetComponent<panltItem>();
        //    Item.Init();
        //    panltList.Add(Item);
        //    plantItem.GetComponent<Button>().onClick.AddListener(delegate { PlantInfo(Item); }); 
        //}



        
        #endregion
    }

   

    public override void Show()
    {
        base.Show();
        baseShow();
    }
    /// <summary>
    /// 显示物品
    /// </summary>
    private void baseShow()
    {
        List<(string, DateTime)> Set = SumSave.crt_plant.Set();
        StopAllCoroutines();
        for (int i = plantFields.childCount - 1; i >= 0; i--)//清空种植区域
        {
            Destroy(plantFields.GetChild(i).gameObject);
        }
        panltList.Clear();//清空列表
        for (int i = 0; i < Set.Count; i++)
        { 
            panltItem item= Instantiate(plantPrefab, plantFields).GetComponent<panltItem>();
            item.GetComponent<Button>().onClick.AddListener(delegate { PlantInfo(item); });
            user_plant_vo vo = new user_plant_vo();
            //vo = ArrayHelper.Find(SumSave.db_plants, e => e.plantName== Set[i].Item1);
            item.GetData(vo);

            item.Init(i, Set[i]); 
            panltList.Add(item);
        }
        StartCoroutine(ShowPlant());
    } 

    private IEnumerator ShowPlant()
    {
        for (int i = 0; i < panltList.Count; i++)
        {
            panltList[i].Fixed_Update(1);   
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShowPlant());

    }
    /// <summary>
    /// 判断下拉列表选中的植物
    /// </summary>
    /// <param name="index"></param>
    private void CurrentPlant(int index)
    {
        currentPlant = plantDropdown.options[index].text;
    }

    /// <summary>
    /// 显示植物信息
    /// </summary>
    /// <returns></returns>
    private void PlantInfo(panltItem _panltItem)
    {
        if(_panltItem.plantName != "0")
        {
            plantInfo.gameObject.SetActive(true);
            plantInfo.transform.position = _panltItem.transform.position;
            plantText.text = _panltItem.plantName+ "\n生长时间："+_panltItem.growTime+"秒"
                + "\n是否浇水："+(_panltItem.isWatered==1?"可浇水":"不可浇水")
                +"\n是否可以收获:"+(_panltItem.isMature==1?"可收获":"不可收获");
        }
        
        
    }
    /// <summary>
    /// 扩容
    /// </summary>
    private void Expansion()
    {
        if(isExpansion>0)
        {
            for(int i = isExpansion; i > 0; i--)
            {
                Debug.Log("扩容成功" + isExpansion);
                List<(string, DateTime)> Set = SumSave.crt_plant.Set();
                Set.Add(("0", DateTime.Now));
                Wirte(Set);
                baseShow();
                isExpansion--;
                
            }
        }else
        {
            Alert_Dec.Show("可扩容数量为0");
        }
        
    }


    /// <summary>
    /// 浇水
    /// </summary>
    private void Watering()
    {
        List<(string, DateTime)> Set = SumSave.crt_plant.Set();
        for (int i = 0; i < panltList.Count; i++)
        {
            if (panltList[i].isWatered == 1)//判断是否可以浇水且植物不为空&& Set[i].Item1 != "0"
            {
                panltList[i].isWatered = 0;
                panltList[i].ReduceGrowthTime(reduceTime);//减少生长时间
                Set[i] = (Set[i].Item1, panltList[i].GetCurrentGrowTimeDate());//更新成熟时间
                SumSave.crt_plant.Up_user_plants(Set);//更新角色数据
                Wirte(Set);//写入数据库
                Alert_Dec.Show("已浇水，" + panltList[i].plantName + "生长时间减少" + reduceTime + "秒");
            }
        }
        
    }




    /// <summary>
    /// 写入
    /// </summary>
    private void Wirte(List<(string,DateTime)> Set)
    {
        SumSave.crt_plant.Set_data(Set);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_plant,
            SumSave.crt_plant.Set_Uptade_String(), SumSave.crt_plant.Get_Update_Character());

    }

    /// <summary>
    /// 收获
    /// </summary>
    private void Harvest()
    {
        List<(string, DateTime)> Set = SumSave.crt_plant.Set();
        
        for (int i = 0; i < panltList.Count; i++)
        {
            if (panltList[i].isMature==1&&Set[i].Item1 != "0")
            {
                panltList[i].Init();
                Set[(panltList[i].Obtain_Index())] = ("0", DateTime.Now);

                Wirte(Set);//写入数据库
                Alert_Dec.Show("已收获");

                
            }
        }
    }


    /// <summary>
    /// 播种
    /// </summary>
    private void Seeding()
    {
        if (currentPlant == null) { Alert_Dec.Show("请选择播种物品"); return; }
        List<(string, DateTime )> Set = SumSave.crt_plant.Set();//获取种子名称以及需要成熟的时间
        List<int> numbers= new List<int>();//空土地的索引
        for (int i = 0; i < Set.Count; i++)
        {
            if(Set[i].Item1=="0") numbers.Add(i);
        }
        if (numbers.Count == 0)
        { 
            Alert_Dec.Show("土地已满");
            return;
        }
        int number = numbers.Count;//空土地的数量
       
        NeedConsumables(currentPlant, number);
        
        while (!RefreshConsumables())
        {
            if (number > 0)
            {
                number--;
                NeedConsumables(currentPlant, number);

            }
            else
            {
                Alert_Dec.Show("背包中没有该种子");
                return;
            }
        }
        //判断是否可以种植
        for (int i = 0; i < number; i++)
        {
            Set[numbers[i]]= (currentPlant.ToString(), SumSave.nowtime);
        }
        Wirte(Set);
        baseShow();

    }

    /// <summary>
    /// 初始化下拉列表
    /// </summary>
    private void InitDropdown()
    {
        plantDropdown.ClearOptions();
        foreach (user_plant_vo type in SumSave.db_plants)
        {
            plantDropdown.options.Add(new Dropdown.OptionData(type.plantName.ToString()));
        }
    }
}
