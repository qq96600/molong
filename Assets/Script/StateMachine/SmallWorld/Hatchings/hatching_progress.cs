using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
/// <summary>
/// 孵化进度
/// </summary>
public class hatching_progress : Base_Mono
{

    private Transform pos_list, pos_btn, pos_info, pos_pet_btn;

    /// <summary>
    /// 信息预制体
    /// </summary>
    private info_item info_item_prefabs;

    private Dictionary<enum_attribute_list, info_item> info_item_dic = new Dictionary<enum_attribute_list, info_item>();

    private material_item material_item_Prefabs;
    private store_item store_item_Prefabs;
    private btn_item btn_item_Prefabs;
    private pet_item pet_item_Prefabs;
    /// <summary>
    /// 当前宠物
    /// </summary>
    private pet_item crt_pet;
    private string[] btn_list = new string[] { "宠物", "宠物蛋" };
    private string[] pet_list_btn = new string[] { "孵化", "守护", "丢弃", "喂养", "探险" };
    /// <summary>
    /// 选择分配 0宠物 1蛋
    /// </summary>
    private int index = 0;
    /// <summary>
    /// 宠物蛋
    /// </summary>
    private (string, int) crt_egg;
    /// <summary>
    /// 宠物孵化进度条
    /// </summary>
    private Slider hatching_Slider;
    /// <summary>
    /// 孵化计时器
    /// </summary>
    private int hatchingTimeCounter;
    /// <summary>
    /// 孵化倒计时文本
    /// </summary>
    private Text countdown_text;
    /// <summary>
    /// 宠物孵化开始时间 宠物名字+时间
    /// </summary>
    private string incubate_Time;
    /// <summary>
    /// 领取宠物按钮
    /// </summary>
    private Button pet_receive;


    private void Awake()
    {
        pos_list = Find<Transform>("Pet_list/Viewport/items");
        pos_btn = Find<Transform>("btn_list");
        pos_info = Find<Transform>("Pet_info/base_info/Scroll View/Viewport/Content");
        pos_pet_btn = Find<Transform>("Pet_info/btn_list");
        material_item_Prefabs = Battle_Tool.Find_Prefabs<material_item>("material_item"); //Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item"); //Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        pet_item_Prefabs = Battle_Tool.Find_Prefabs<pet_item>("pet_item"); //Resources.Load<pet_item>("Prefabs/panel_smallWorld/pets/pet_item");
        store_item_Prefabs = Battle_Tool.Find_Prefabs<store_item>("store_item"); //Resources.Load<store_item>("Prefabs/panel_hall/panel_store/store_item");
        info_item_prefabs = Battle_Tool.Find_Prefabs<info_item>("info_item"); //Resources.Load<info_item>("Prefabs/base_tool/info_item");
        hatching_Slider = Find<Slider>("Pet_info/hatching_Slider");
        countdown_text = Find<Text>("Pet_info/hatching_Slider/countdown_text/info");
        hatching_Slider.gameObject.SetActive(false);
        pet_receive = Find<Button>("Pet_info/pet_receive");
        pet_receive.onClick.AddListener(() => { ReceivePet(); });
        pet_receive.gameObject.SetActive(false);
        
        DisplayProperties();
        ClearObject(pos_btn);
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, pos_btn);
            item.Show(i, btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { onClick(item); });
        }
        ClearObject(pos_pet_btn);
        for (int i = 0; i < pet_list_btn.Length; i++)
        {
            btn_item btn = Instantiate(btn_item_Prefabs, pos_pet_btn);
            btn.Show(i, pet_list_btn[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { onPetClick(btn); });
        }

    }

    /// <summary>
    /// 显示属性
    /// </summary>
    private void DisplayProperties()
    {
        ClearObject(pos_info);
        for (int i = 0; i < Enum.GetNames(typeof(enum_attribute_list)).Length; i++)//显示属性
        {
            string info = "";
            info_item item = Instantiate(info_item_prefabs, pos_info);
            item.Show((enum_attribute_list)i, info);
            if (!info_item_dic.ContainsKey((enum_attribute_list)i))
                info_item_dic.Add((enum_attribute_list)i, item);
        }

    }


    /// <summary>
    /// 刷新属性
    /// </summary>
    private void UpProperties(db_pet_vo _crt_pet_vo)
    {
        for (int i = 0; i < info_item_dic.Count; i++)
        {
            string info = (int.Parse(_crt_pet_vo.crate_values[i]) + (int.Parse(_crt_pet_vo.up_values[i]) * _crt_pet_vo.level)).ToString();
            info_item_dic[(enum_attribute_list)i].Show((enum_attribute_list)i, info);
        }
    }

    /// <summary>
    /// 宠物功能
    /// </summary>
    /// <param name="btn"></param>
    private void onPetClick(btn_item btn)
    {
        switch (pet_list_btn[btn.index])
        {
            case "孵化":
                for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
                {
                    string[] data = SumSave.crt_pet.crt_pet_list[i].Split(",");
                    if (data.Length == 2)
                    {
                        Alert_Dec.Show("宠物" + data[0] + " 正在孵化");
                        return;
                    }

                }

                Dictionary<string, int> dic = new Dictionary<string, int>();
                if(crt_egg.Item2 <= 0)
                {
                    Alert_Dec.Show("请选择需要孵化的宠物");
                    return;
                }
                dic.Add(crt_egg.Item1, -crt_egg.Item2);
                
                SumSave.crt_bag_resources.Get(dic);
               
                Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());

               

                db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == crt_egg.Item1);
                if (pet != null)
                {
                    incubate_Time = "";
                    incubate_Time = pet.petName + "," + SumSave.nowtime;
                    SumSave.crt_pet.crt_pet_list.Add(incubate_Time);
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
                    SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());


                    Alert_Dec.Show("宠物" + pet.petName + " 孵化开始");
                    pos_pet_btn.gameObject.SetActive(false);
                    hatchingTimeCounter=PetIncubationTime();

                    hatching_Slider.gameObject.SetActive(true);
                    hatching_Slider.maxValue = pet.hatchingTime;
                    StartCoroutine(ShowPlant(pet));


                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
                    SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());


                }
                break;
            case "守护":
                Get_pet_guard();
                break;
            case "丢弃":
                DeletePet();
                break;
            case "喂养":
                FeedPet();
                break;
            case "探险":
                //transform.parent.parent.SendMessage("PetExpeditionGo", crt_pet.SetPet());
                PetExpeditionGo();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 找到正在孵化宠物的孵化时间
    /// </summary>
    /// <returns></returns>
    private int PetIncubationTime()
    {
        int Time = 0;
        for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
        {
            string[] data = SumSave.crt_pet.crt_pet_list[i].Split(",");
            if (data.Length == 2)
            {
                Time = ((int)(SumSave.nowtime - Convert.ToDateTime(data[1])).TotalMinutes) * 60;
            }
        }
        return Time;
    }

    private void Get_pet_guard()
    {
        for (int i = 0; i < SumSave.crt_pet_list.Count; i++)
        {
            if (SumSave.crt_pet_list[i].pet_state == "1")
            {
                string v1 = IntegrationData(SumSave.crt_pet_list[i]);//升级之前的数据
                SumSave.crt_pet_list[i].pet_state = "1";
                string v2 = IntegrationData(SumSave.crt_pet_list[i]);//升级之后的数据
                for (int h = 0; h < SumSave.crt_pet.crt_pet_list.Count; h++)
                {
                    if (SumSave.crt_pet.crt_pet_list[h] == v1)
                    {
                        SumSave.crt_pet.crt_pet_list[h] = "";
                        SumSave.crt_pet.crt_pet_list[h] = v2;
                    }
                }
            }
        }
        db_pet_vo crt_pet_vo = crt_pet.SetPet();
        string value = IntegrationData(crt_pet_vo);//升级之前的数据
        crt_pet_vo.pet_state = "1";
        string value1 = IntegrationData(crt_pet_vo);//升级之后的数据
        for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
        {
            if (SumSave.crt_pet.crt_pet_list[i] == value)
            {
                SumSave.crt_pet.crt_pet_list[i] = "";
                SumSave.crt_pet.crt_pet_list[i] = value1;
            }
        }
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
        SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());
       
    }

    /// <summary>
    /// 宠物探险
    /// </summary>
    private void PetExpeditionGo()
    {
        int number = 0;
        for (int i = 0; i < SumSave.crt_pet_list.Count; i++)
        {
            if (SumSave.crt_pet_list[i].pet_state == "2")
            {
                number++;
            }
        }
        if (number >= SumSave.crt_world.World_Lv / 30 + 1)
        { 
             Alert_Dec.Show("当前探险队伍已满，请先完成探险");
        }
        else
        {
            db_pet_vo crt_pet_vo = crt_pet.SetPet();
            string value = IntegrationData(crt_pet_vo);//升级之前的数据
            crt_pet_vo.pet_state = "2";
            string value1 = IntegrationData(crt_pet_vo);//升级之后的数据
            for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
            {
                if (SumSave.crt_pet.crt_pet_list[i] == value)
                {
                    SumSave.crt_pet.crt_pet_list[i] = "";
                    SumSave.crt_pet.crt_pet_list[i] = value1;
                }
            }
            SumSave.crt_explore.SetValues(crt_pet_vo.petName + " " + crt_pet_vo.startHatchingTime, SumSave.nowtime + "," + SumSave.nowtime + ",");
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet_explore,
         SumSave.crt_explore.Set_Uptade_String(), SumSave.crt_explore.Get_Update_Character());
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
     SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());
            Alert_Dec.Show("当前宠物已开始探险");
        }
    }

    /// <summary>
    /// 喂养宠物
    /// </summary>
    public void FeedPet()
    {
        db_pet_vo crt_pet_vo = crt_pet.SetPet();
        string value = IntegrationData(crt_pet_vo);//升级之前的数据
        NeedConsumables("宠物口粮", 1);
        if (RefreshConsumables())
        {
            for (int i = 0; i < SumSave.crt_pet_list.Count; i++)
            {
                if (SumSave.crt_pet_list[i] == crt_pet_vo)
                {
                    crt_pet_vo.exp += 10;
                    int maxlevel = crt_pet_vo.level * 10;
                    if (crt_pet_vo.exp >= maxlevel)
                    {
                        crt_pet_vo.exp -= maxlevel;
                        crt_pet_vo.level += 1;
                    }
                    crt_pet.Init(SumSave.crt_pet_list[i]);
                }
            }
            string value1 = IntegrationData(crt_pet_vo);//升级之后的数据
            for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
            {
                if (SumSave.crt_pet.crt_pet_list[i] == value)
                {
                    SumSave.crt_pet.crt_pet_list[i] = "";
                    SumSave.crt_pet.crt_pet_list[i] = value1;
                }
            }
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
       SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());

            Alert_Dec.Show("喂养成功,宠物 " + crt_pet_vo.petName + "等级lv：" + crt_pet_vo.level + "经验：" + crt_pet_vo.exp + "/" + crt_pet_vo.level * 10);
            UpProperties(crt_pet_vo);
        }
    }





    /// <summary>
    /// 整合宠物数据
    /// </summary>
    private string IntegrationData(db_pet_vo crt_pet_vo)
    {
        string value = "";
        value += crt_pet_vo.petName + ",";
        value += crt_pet_vo.startHatchingTime + ",";
        value += crt_pet_vo.quality + ",";
        value += crt_pet_vo.level + ",";
        value += crt_pet_vo.exp + ",";
        value += crt_pet_vo.crate_value + "|" + crt_pet_vo.up_value + "|" + crt_pet_vo.up_base_value + ",";
        value += crt_pet_vo.pet_state;
        return value;
    }


    /// <summary>
    /// 删除宠物
    /// </summary>
    public void DeletePet()
    {
        db_pet_vo crt_pet_vo = crt_pet.SetPet();
        string value = IntegrationData(crt_pet_vo);
        SumSave.crt_pet.crt_pet_list.Remove(value);
        SumSave.crt_pet_list.Remove(crt_pet_vo);
        //SumSave.crt_pet.DataSet();
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
        SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());
        Alert_Dec.Show("宠物" + crt_pet_vo.petName + "已丢弃");
        SumSave.crt_achievement.increase_date_Exp((Achieve_collect.放生宠物).ToString(), 1);
        btn_item _item=new btn_item();
        _item.index=0;
        onClick(_item);
    }
    /// <summary>
    /// 孵化倒计时
    /// </summary>
    /// <param name="pet"></param>
    /// <returns></returns>
    private IEnumerator ShowPlant(db_pet_vo pet)
    {
        Fixed_Update(1, pet);
        int remainingTime = pet.hatchingTime - PetIncubationTime();
        yield return new WaitForSeconds(1f);
        if (remainingTime >= 0)
        {
            StartCoroutine(ShowPlant(pet));
        }
        else
        {
            StopCoroutine(ShowPlant(pet));
        }
    }
    /// <summary>
    /// 倒计时
    /// </summary>
    /// <param name="time"></param>
    private void Fixed_Update(int time, db_pet_vo pet)
    {
        int remainingTime = pet.hatchingTime - PetIncubationTime();
        if (remainingTime >= 0)
        {
            //hatchingTimeCounter -= time;
            //countdown_text.text = ConvertSecondsToHHMMSS(hatchingTimeCounter);
            //hatching_Slider.value = pet.hatchingTime - hatchingTimeCounter;
            //Debug.Log("倒计时" + hatchingTimeCounter + "进度条：" + hatching_Slider.value);

            countdown_text.text ="剩余时间："+ ConvertSecondsToHHMMSS(remainingTime);
            hatching_Slider.value = remainingTime;
        }
        else if (remainingTime < 0)//孵化完成
        {
            hatching_Slider.gameObject.SetActive(false);
            //countdown_text.text = "";
            hatchingTimeCounter = -1;
            hatching_Slider.value = 0;
            //Debug.Log("孵化完成");
            pet_receive.gameObject.SetActive(true);

        }
    }
    /// <summary>
    /// 领取宠物
    /// </summary>
    private void ReceivePet()
    {
        pet_receive.gameObject.SetActive(false);
        string data = incubate_Time;
        SumSave.crt_pet.crt_pet_list.Remove(data);//孵化宠物只有这一个类型可以直接找到删除
        SumSave.crt_pet.crt_pet_list.Remove("");
        //db_pet_vo _pet = ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == crt_egg.Item1);//更具宠物蛋找到对应宠物
        //db_pet_vo pet_init = SumSave.db_pet_dic[_pet.petName];
        db_pet_vo pet_init= ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == crt_egg.Item1);

        string value_data = "";
        value_data += pet_init.petName + ",";
        value_data += SumSave.nowtime + ",";
        value_data += (SumSave.crt_world.World_Lv / 5 + 1) + ",";
        value_data += pet_init.level + ",";
        value_data += pet_init.exp + ",";
        value_data += crate_value(pet_init, (SumSave.crt_world.World_Lv / 5 + 1)) + ",";
        value_data += 0.ToString();
        SumSave.crt_pet.crt_pet_list.Add(value_data);


        db_pet_vo pet = new db_pet_vo();
        string[] splits = SumSave.crt_pet.crt_pet_list[SumSave.crt_pet.crt_pet_list.Count - 1].Split(',');

        if (splits.Length == 7)
        {
            pet.petName = splits[0];
            pet.startHatchingTime = DateTime.Parse(splits[1]);
            pet.quality = splits[2];
            pet.level = int.Parse(splits[3]);
            pet.exp = int.Parse(splits[4]);

            string[] attributes = splits[5].Split('|');
            if (attributes.Length == 3)
            {
                pet.crate_value = attributes[0];
                pet.up_value = attributes[1];
                pet.up_base_value = attributes[2];
                pet.GetNumerical();
            }
            pet.pet_state = splits[6];
        }

        SumSave.crt_pet_list.Add(pet);

        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet,
        SumSave.crt_pet.Set_Uptade_String(), SumSave.crt_pet.Get_Update_Character());


        SumSave.crt_achievement.increase_date_Exp((Achieve_collect.孵化宠物).ToString(), 1);
        Show();
        DisplayPetEggs();

    }

    /// <summary>
    /// 添加宠物属性
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="lv"></param>
    /// <returns></returns>
    private string crate_value(db_pet_vo pet, int lv)
    {
        string data = "";
        for (int i = 0; i < pet.crate_values.Count; i++)
        {
            data += Random.Range(int.Parse(pet.crate_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.crate_values[i]) * (lv * 20 + 100) / 100) + " ";
        }
        data += "|";
        for (int i = 0; i < pet.up_values.Count; i++)
        {
            data += Random.Range(int.Parse(pet.up_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.up_values[i]) * (lv * 20 + 100) / 100) + " ";
        }
        data += "|";
        for (int i = 0; i < pet.up_base_values.Count; i++)
        {
            data += Random.Range(int.Parse(pet.up_base_values[i]) * (lv * 20 + 100) / 200, int.Parse(pet.up_base_values[i]) * (lv * 20 + 100) / 100) + " ";
        }

        return data;
    }
    /// <summary>
    /// 选择分配 0宠物 1蛋
    /// </summary>
    /// <param name="item"></param>
    private void onClick(btn_item item)
    {
        Alert_Dec.Show("切换到" + btn_list[item.index]);
        index = item.index;
        pet_receive.gameObject.SetActive(false);
        hatching_Slider.gameObject.SetActive(false);
        Show();
    }

    public override void Show()
    {
        base.Show();
        Base_Show();
        Show_Btn(false, 0);
    }

    /// <summary>
    /// 显示宠物
    /// </summary>
    private void Base_Show()
    {

        ClearObject(pos_list);
        pos_pet_btn.gameObject.SetActive(true);
        
        if (index == 0)
        {
            hatching_Slider.gameObject.SetActive(false);
            for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)//显示宠物
            {
                string[] data = SumSave.crt_pet.crt_pet_list[i].Split(",");
                if (data.Length!=2&& SumSave.crt_pet.crt_pet_list[i]!="")
                {
                    pet_item item = Instantiate(pet_item_Prefabs, pos_list);
                    item.Init(SumSave.crt_pet_list[i]);
                    item.GetComponent<Button>().onClick.AddListener(() => { Select_Pet(item); });
                    if (crt_pet == null)
                    {
                        Select_Pet(item);
                    }
                    else
                    {
                        if (item == crt_pet) Select_Pet(item);
                    }
                }
               
            }
        }
        if (index == 1)
        {
            DisplayPetEggs();
        }
           
    }
    /// <summary>
    /// 显示宠物蛋列表
    /// </summary>
    private void DisplayPetEggs()
    {
        for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
        {
            string[] data = SumSave.crt_pet.crt_pet_list[i].Split(",");
            if (data.Length == 2)
            {
                db_pet_vo pet_init = ArrayHelper.Find(SumSave.db_pet, e => e.petName == data[0]);//更具宠物名字找到宠物蛋名字
                store_item item = Instantiate(store_item_Prefabs, pos_list);
                (string, int) lists = (pet_init.petEggsName, -1);
                item.PetInit(lists, "");
                item.GetComponent<Button>().onClick.AddListener(() => { Select_Egg(lists); });
            }
        }


        List<(string, int)> list = SumSave.crt_bag_resources.Set();//获得背包物品名字和数量
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item2 <= 0) continue;
            Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == list[i].Item1);
            if (bag != null)
            {
                switch ((EquipConfigTypeList)Enum.Parse(typeof(EquipConfigTypeList), bag.StdMode))
                {
                    case EquipConfigTypeList.宠物蛋:
                        (string, int) lists = list[i];
                        store_item item = Instantiate(store_item_Prefabs, pos_list);
                        item.PetInit(list[i], "");
                        item.GetComponent<Button>().onClick.AddListener(() => { Select_Egg(lists); });
                        if (crt_egg.Item2 == 0) Select_Egg(lists);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Show_Info(crtMaxHeroVO crt_MaxHero)
    {
        foreach (var item in info_item_dic.Keys)
        {
            switch (item)
            {
                case enum_attribute_list.生命值:
                    info_item_dic[item].Show(item, crt_MaxHero.MaxHP+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.法力值:
                    info_item_dic[item].Show(item, crt_MaxHero.MaxMp+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.内力值:
                    info_item_dic[item].Show(item, crt_MaxHero.internalforceMP+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.蓄力值:
                    info_item_dic[item].Show(item, crt_MaxHero.EnergyMp+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.物理防御:
                    info_item_dic[item].Show(item, crt_MaxHero.DefMin + " - " + crt_MaxHero.DefMax+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.魔法防御:
                    info_item_dic[item].Show(item, crt_MaxHero.MagicDefMin + " - " + crt_MaxHero.MagicDefMax+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.物理攻击:
                    info_item_dic[item].Show(item, crt_MaxHero.damageMin + " - " + crt_MaxHero.damageMax+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.魔法攻击:
                    info_item_dic[item].Show(item, crt_MaxHero.MagicdamageMin + " - " + crt_MaxHero.MagicdamageMax+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.命中:
                    info_item_dic[item].Show(item, crt_MaxHero.hit+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.躲避:
                    info_item_dic[item].Show(item, crt_MaxHero.dodge+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.穿透:
                    info_item_dic[item].Show(item, crt_MaxHero.penetrate+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.格挡:
                    info_item_dic[item].Show(item, crt_MaxHero.block+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.暴击:
                    info_item_dic[item].Show(item, crt_MaxHero.crit_rate+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.幸运:
                    info_item_dic[item].Show(item, crt_MaxHero.Lucky+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.暴击伤害:
                    info_item_dic[item].Show(item, crt_MaxHero.crit_damage+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.伤害加成:
                    info_item_dic[item].Show(item, crt_MaxHero.double_damage+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.真实伤害:
                    info_item_dic[item].Show(item, crt_MaxHero.Real_harm+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.伤害减免:
                    info_item_dic[item].Show(item, crt_MaxHero.Damage_Reduction + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.伤害吸收:
                    info_item_dic[item].Show(item, crt_MaxHero.Damage_absorption + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.异常抗性:
                    info_item_dic[item].Show(item, crt_MaxHero.resistance + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.攻击速度:
                    info_item_dic[item].Show(item, crt_MaxHero.attack_speed + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.移动速度:
                    info_item_dic[item].Show(item, crt_MaxHero.move_speed + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.生命加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Hp + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.法力加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Mp + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.生命回复:
                    info_item_dic[item].Show(item, crt_MaxHero.Heal_Hp + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.法力回复:
                    info_item_dic[item].Show(item, crt_MaxHero.Heal_Mp + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.物攻加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Damage + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.魔攻加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_MagicDamage + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.物防加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_Def + tool_Categoryt.Obtain_unit((int)item));
                    break;
                case enum_attribute_list.魔防加成:
                    info_item_dic[item].Show(item, crt_MaxHero.bonus_MagicDef+ tool_Categoryt.Obtain_unit((int)item));
                    break;
                default:
                    break;
            }
        }

    }

    /// <summary>
    /// 显示宠物蛋
    /// </summary>
    /// <param name="bag"></param>
    private void Select_Egg((string, int) bag)
    {
        db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petEggsName == bag.Item1);
        if (bag.Item2 == -1)
        {
            for (int i = 0; i < SumSave.crt_pet.crt_pet_list.Count; i++)
            {
                string[] data = SumSave.crt_pet.crt_pet_list[i].Split(",");
                if(data.Length==2)
                {
                   
                    db_pet_vo _pet=new db_pet_vo();
                    incubate_Time = SumSave.crt_pet.crt_pet_list[i];
                    pos_pet_btn.gameObject.SetActive(false);
                    hatchingTimeCounter = ((int)(SumSave.nowtime - Convert.ToDateTime(data[1])).TotalMinutes)*60;
                    hatching_Slider.gameObject.SetActive(true);
                    hatching_Slider.maxValue = pet.hatchingTime;
                    StartCoroutine(ShowPlant(pet));
                }
            }

        }
        else
        {
            Show_Btn(false, 0);
        }

        crt_egg = bag;
        UpProperties(pet);
        Obtain_Pet(pet, 1);
    }
    /// <summary>
    /// 展示宠物
    /// </summary>
    /// <param name="item"></param>
    private void Select_Pet(pet_item item)
    {
        if (crt_pet != null) crt_pet.Selected = false;
        crt_pet = item;
        crt_pet.Selected = true;
        UpProperties(item.SetPet());
        Show_Btn(true, 0);
        db_pet_vo pet = ArrayHelper.Find(SumSave.db_pet, e => e.petName == item.name);
        Obtain_Pet(crt_pet.SetPet(), 1);
    }

    /// <summary>
    /// 获取宠物属性
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="lv"></param>
    private void Obtain_Pet(db_pet_vo pet, int lv)
    {
        if (pet != null)
        {
            crtMaxHeroVO crt = new crtMaxHeroVO();
            //List<string> v = pet.crate_values;//宠物基础属性
            //List<string> va = pet.up_values;//宠物成长属性
            for (int i = 0; i < pet.crate_values.Count; i++)
            {
                //int value = int.Parse(v[i]) + (int.Parse(va[i]) * lv);
                if(pet.crate_values[i]!=""&&pet.up_values[i] != "")
                {
                    int info = int.Parse(pet.crate_values[i]) + (int.Parse(pet.up_values[i]) * pet.level);
                    Battle_Tool.Enum_Value(crt, i, info);
                }
                
            }
            Show_Info(crt);
        }
    }
    /// <summary>
    /// 功能显示
    /// </summary>
    /// <param name="state"></param>
    private void Show_Btn(bool state, int pos = -1)
    {
        for (int i = 0; i < pos_pet_btn.childCount; i++)
        {
            pos_pet_btn.GetChild(i).gameObject.SetActive(i == pos ? !state : state);
        }
    }
}
