using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_smallWorld : Panel_Base
{
    /// <summary>
    /// 种植界面
    /// </summary>
    private panel_plant _plant;
    /// <summary>
    /// 孵化界面
    /// </summary>
    private Pet_Hatching _Hatching;
    /// <summary>
    /// 宠物探索位置
    /// </summary>
    private Pet_explore _explore;

    /// <summary>
    /// 按钮位置
    /// </summary>
    private Transform pos_btn;
    /// <summary>
    /// 按钮预制体
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// 背景
    /// </summary>
    private Image small_World_bg;
    /// <summary>
    /// 显示值
    /// </summary>
    private Text base_info;
    private string[] btn_list = new string[] { "升级", "农庄", "灵宠", "探险" };
    /// <summary>
    /// 宠物显示位置
    /// </summary>
    private Transform pet_pos;
    /// <summary>
    /// 宠物信息标题
    /// </summary>
    private Text Pet_name;
    /// <summary>
    /// 宠物属性信息文本
    /// <summary>  
    private Text Pet_attribute;

    /// <summary>
    /// 守护植物园的宠物
    /// </summary>
    private db_pet_vo pet_guard;
   /// <summary>
   /// 守护兽显示
   /// </summary>
   private Image display_image;
    /// <summary>
    /// 守护兽信息
    /// </summary>
   private Text  display_info;

    /// <summary>
    /// 宠物信息窗口
    /// </summary>
    private hatching_progress hatching_progress;
    /// <summary>
    /// 关闭宠物信息窗口
    /// </summary>
    private Button close_hatching;
    public override void Initialize()
    {
        base.Initialize();
        small_World_bg = Find<Image>("small_World");
        _plant=Find<panel_plant>("small_World/Panel_plant");
        _Hatching = Find<Pet_Hatching>("small_World/Pet_Hatching");
        _explore = Find<Pet_explore>("small_World/Pet_explore");
        pos_btn=Find<Transform>("bg_main/btn_list");
        base_info = Find<Text>("bg_main/base_info/info");
        btn_item = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        pet_pos= Find<Transform>("small_World/Pet_Hatching/Pet_list/Viewport/items");
        Pet_name= Find<Text>("small_World/Pet_Hatching/Pet_info/Pet_name/Text");
        Pet_attribute = Find<Text>("small_World/Pet_Hatching/Pet_info/Pet_attribute/Viewport/Content/Text");
        
        hatching_progress = Find<hatching_progress>("small_World/hatching_progress");
        
        display_image= Find<Image>("small_World/Panel_plant/pet_guard/display");
        display_info = Find<Text>("small_World/Panel_plant/pet_guard/info/Text");

        close_hatching = Find<Button>("small_World/hatching_progress/but");
        close_hatching.onClick.AddListener(() => { CloseHatching(); });


        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item btn_items = Instantiate(btn_item, pos_btn);//实例化背包装备
            btn_items.Show(i, btn_list[i]);
            btn_items.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_items); });
        }
    }

    /// <summary>
    /// 关闭宠物信息窗口
    /// </summary>
    private void CloseHatching()
    {
        small_World_bg.gameObject.SetActive(false);
        hatching_progress.gameObject.SetActive(false);
        close_hatching.gameObject.SetActive(false);
    }

   

    /// <summary>
    /// 获取守护植物园的宠物
    /// </summary>
    public void Get_pet_guard(db_pet_vo _pet_guard)
    {
        Alert_Dec.Show("设置 " + _pet_guard.petName + " 为植物园守护兽");
        pet_guard = _pet_guard;
        display_image.sprite = UI_Manager.I.GetEquipSprite("icon/", pet_guard.petName); 
        display_info.text = pet_guard.petName+ " Lv." + pet_guard.level ;

    }


    public override void Hide()
    {
        if (small_World_bg.gameObject.activeInHierarchy)//从最上层关闭
        {
            if (_plant.gameObject.activeInHierarchy) _plant.Hide();
            //if (_Hatching.gameObject.activeInHierarchy) _Hatching.Hide();
            
            if (_explore.gameObject.activeInHierarchy) _explore.Hide();

            small_World_bg.gameObject.SetActive(false);
        }
        else
            base.Hide();
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        small_World_bg.gameObject.SetActive(true);
        switch (btn_list[btn_item.index])
        { 
            case "升级":
                small_World_bg.gameObject.SetActive(false);
                uplv();
                break;
            case "农庄":
                _plant.Show();
                break;
            case "灵宠":
                //_Hatching.Show();
                //HatchingInit();
                hatching_progress.gameObject.SetActive(true);
                close_hatching.gameObject.SetActive(true);
                hatching_progress.Show();
                break;
            case "探险":
                _explore.Show();
                break;
        }
    }
    /// <summary>
    /// 灵宠列表初始化
    /// </summary>
    private void HatchingInit()
    {
        ClearObject(pet_pos);
        if(SumSave.crt_pet_list.Count==0)
        {
            btn_item btn_items = Instantiate(btn_item, pet_pos);
            btn_items.Show(1, "宠物列表为空");
        }
        for (int i= 0; i < SumSave.crt_pet_list.Count; i++)
        {
            db_pet_vo pet = SumSave.crt_pet_list[i];
            btn_item btn_items = Instantiate(btn_item, pet_pos);
            btn_items.Show(i, SumSave.crt_pet_list[i].petName+" Lv." + SumSave.crt_pet_list[i].level);
            btn_items.GetComponent<Button>().onClick.AddListener(delegate { Select_Pet(pet); });
        }
    }
    /// <summary>
    /// 显示灵宠信息
    /// </summary>
    /// <param name="pet"></param>
    private void Select_Pet(db_pet_vo pet)
    {
        Pet_name.text = pet.petName + " Lv." + pet.level;
        Pet_attribute.text="";

        Pet_attribute.text = DisplayPetAttribute(pet);
    }
    /// <summary>
    /// 显示宠物属性
    /// </summary>
    private string  DisplayPetAttribute(db_pet_vo pet)
    {
        string dec = "";
        List<string> value = SumSave.db_pet_dic[pet.petName].crate_values;//获得宠物基础属性
       
        for(int i = 0; i < value.Count; i++)
        { 
            enum_attribute_list attribute= (enum_attribute_list)i;
            dec += attribute.ToString() + "：" +(int.Parse( value[i])*pet.level ).ToString()+ "\n";
        }

        return dec;
    }





    /// <summary>
    /// 升级
    /// </summary>
    private void uplv()
    {
        if (SumSave.crt_world.World_Lv >= SumSave.db_lvs.world_lv_list.Count)
        { 
            Alert_Dec.Show("已达最高等级");
            return;
        }
        (string,int) dec = SumSave.db_lvs.world_lv_list[SumSave.crt_world.World_Lv];
        NeedConsumables(dec.Item1, dec.Item2);
        if (RefreshConsumables())
        {
            List<string> list = SumSave.crt_world.Get();
            int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
            SumSave.crt_world.Set(Obtain_Init(1, time, int.Parse(list[1])));
            SumSave.crt_world.World_Lv++;
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
        }
        else Alert_Dec.Show("材料不足 需要" + dec.Item1 + " * " + dec.Item2);
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_world == null)
        {
            Alert_Dec.Show("小世界未激活");
            Hide();
            return;
        }
        Base_Show();
    }

    private void Base_Show()
    {
        List<string> list = SumSave.crt_world.Get();
        int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
        string dec = "界灵：Lv." + SumSave.crt_world.World_Lv + "\n";
        dec += "灵气 ：" + Obtain_Init(1,time,int.Parse(list[1])) + "(Max" + Obtain_Init(2) + ")\n";
        dec += "每分钟可获得 ：" + SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv]+  "灵气\n";
        dec += "历练获得 :" + (SumSave.crt_world.World_Lv * 10) + "%\n";
        if (SumSave.crt_world.World_Lv >= SumSave.db_lvs.world_lv_list.Count)
        {
            dec += "已达最高等级";
        }
        else
        {
            (string, int) item = SumSave.db_lvs.world_lv_list[SumSave.crt_world.World_Lv];
            dec += "升级需求 " + item.Item1 + " * " + item.Item2;
        }
        base_info.text = dec;
    }
    /// <summary>
    /// 获取灵气值
    /// </summary>
    /// <param name="time"></param>
    private int Obtain_Init(int type,int time=0,int crt_value=0)
    {
        int value = 0;
        switch (type)
        {
            case 1:
                ///判断越界
                ArrayHelper.SafeGet(SumSave.db_lvs.world_offect_list, SumSave.crt_world.World_Lv, out int se);
                value = time * SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv];
                value+= crt_value;
                value = Mathf.Min(value, SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv]);
                break;
            case 2:
                value = SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv];
                break;
            default:
                break;
        }

        return value;
    }
    
    protected override void Awake()
    {
        base.Awake();
    }


    

}
