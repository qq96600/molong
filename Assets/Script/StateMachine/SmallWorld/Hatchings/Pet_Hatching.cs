
using Common;
using Components;
using MVC;
using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Pet_Hatching : Panel_Base
{
    /// <summary>
    /// 按钮父类
    /// </summary>
    private Transform hatching_Buttons;
    /// <summary>
    /// 查看进度按钮，孵化按钮,孵化完成领取按钮
    /// </summary>
    private Button displayPet, hatching_Button, receive_Button;
    /// <summary>
    /// 宠物蛋下拉列表
    /// </summary>
    private Dropdown hatching_Dropdown;
    /// <summary>
    /// 下拉列表选中的宠物蛋
    /// </summary>
    private string currentPet;
    /// <summary>
    /// 孵化成功的宠物名字
    /// </summary>
    private string Pet;

    /// <summary>
    /// 孵化界面
    /// </summary>
    private Transform hatching_progress;
    /// <summary>
    /// 正在孵化Image
    /// </summary>
    private Image petEggs_Image;
    /// <summary>
    /// 孵化进度条
    /// </summary>
    private Slider hatching_Slider;
    /// <summary>
    /// 孵化进度条文本
    /// </summary>
    private Text hatching_Text;
    /// <summary>
    /// 需要孵化的时间
    /// </summary>
    public int hatchingTime;
    /// <summary>
    /// 孵化计时器
    /// </summary>
    private int hatchingTimeCounter;
    /// <summary>
    /// 开始孵化的时间
    /// </summary>
    private DateTime startHatchingTime;
    /// <summary>
    /// 孵化是否完成 0：空，1：孵化中，2：孵化完成
    /// </summary>
    private int isHatching=0;
    /// <summary>
    /// 孵化器Image
    /// </summary  >
    private Sprite sprite;

    protected override void Awake()
    {
        //base.Awake();
        #region 按钮初始化
        hatching_Buttons = transform.Find("hatching_Buttons");
        displayPet = hatching_Buttons.Find("displayPet").GetComponent<Button>();
        hatching_Button = hatching_Buttons.Find("hatching_Button").GetComponent<Button>();
        hatching_Dropdown = hatching_Buttons.Find("hatching_Dropdown").GetComponent<Dropdown>();
        displayPet.onClick.AddListener(OnDisplayPet);
        hatching_Button.onClick.AddListener(OnHatching);
        hatching_Dropdown.onValueChanged.AddListener(OnHatching_Dropdown);
        currentPet = "选择需要孵化的宠物";
        InitDropdown();
        #endregion

        #region 孵化显示初始化
        hatching_progress= transform.Find("hatching_progress");
        petEggs_Image = hatching_progress.Find("petEggs_Image").GetComponent<Image>();
        sprite=petEggs_Image.sprite;
        hatching_Slider = hatching_progress.Find("hatching_Slider").GetComponent<Slider>();
        hatching_Text=hatching_progress.Find("hatching_Text").GetComponent<Text>();
        receive_Button= hatching_progress.Find("receive_Button").GetComponent<Button>();
        receive_Button.onClick.AddListener(OnReceive);
        hatching_progress.gameObject.SetActive(false);
        #endregion
    }

    public override void Show()
    {
        base.Show();
    }

 


    /// <summary>
    /// 孵化成功领取宠物
    /// </summary>
    private void OnReceive()
    {
        if(isHatching==2)
        {
            
            Debug.Log("领取宠物"+ isHatching);
            //领取宠物
            Battle_Tool.Obtain_Resources(Pet, 1);
            isHatching = 0;
        }
       
    }

    /// <summary>
    /// 孵化
    /// </summary>
    private void OnHatching()
    {

        (string, DateTime) Set = SumSave.crt_hatching.Set();
        if (isHatching == 0)
        {
            //NeedConsumables(currentPet, 1);
            //if(RefreshConsumables())
            //{
                if (SumSave.db_pet_dic.TryGetValue(currentPet, out user_pet_vo quantity))//查找选择的宠物蛋，设置孵化时间，和孵化成功的宠物
                {
                    hatchingTime = quantity.hatchingTime;
                    Pet = quantity.petName;
                    //petEggs_Image.sprite = Resources.Load<Sprite>("panel_fight/_icon_shop/hatching_" + quantity.petEggsName); //hatching_梅花鹿蛋

                    petEggs_Image.sprite = Resources.Load<Sprite>("panel_fight/panlt_小麦");
                }
                else
                {
                    Debug.Log("没有这个宠物蛋");
                    return;
                }

                hatching_Slider.maxValue = hatchingTime;
                startHatchingTime = DateTime.Now;
                StartCoroutine(ShowPlant());

                Countdown();

                Set = (currentPet, startHatchingTime);
                isHatching = 1;
                Wirte(Set);
            //}
        //else
        //    {
        //        Alert_Dec.Show("背包没有"+ currentPet+"宠物蛋");
        //    }
            
        }
    }


    private IEnumerator ShowPlant()
    {
     
        Fixed_Update(1);
        yield return new WaitForSeconds(1f);
        if(isHatching != 2)
        {
            StartCoroutine(ShowPlant());
        }else
        {
            StopCoroutine(ShowPlant());
        }

    }

    private void Fixed_Update(int time)
    {
        if (hatchingTimeCounter > 0)
        {
            hatchingTimeCounter -= time;
            hatching_Text.text = ConvertSecondsToHHMMSS(hatchingTimeCounter);
            hatching_Slider.value = hatchingTime - hatchingTimeCounter;
            Debug.Log("倒计时" + hatchingTimeCounter+"进度条："+ hatching_Slider.value);
        }
        else
        {
            hatching_Text.text = "";
            hatchingTimeCounter = -1;
            hatching_Slider.value = 0;
            isHatching = 2;
            
        }
    }


    /// <summary>
    /// 打开宠物孵化界面
    /// </summary>
    private void OnDisplayPet()
    {
        if(isHatching != 0)
        {
            hatching_progress.gameObject.SetActive(true);
        }else
        {
            Alert_Dec.Show("没有孵化的宠物");
        }
        
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        hatching_Text.text = "";
        hatchingTimeCounter = -1;
        hatching_Slider.value = 0;
        isHatching = 0;
        hatchingTimeCounter = 0;
        petEggs_Image.sprite = sprite;
        Countdown();
    }

    private void Countdown()
    {
        hatchingTimeCounter = (int)(SumSave.nowtime - startHatchingTime).TotalSeconds;//当前时间-植物种植时间 获得植物种植到现在的时间
        if (hatchingTimeCounter <= hatchingTime)//植物已经生长的时间小于植物需要生长的时间
        {
            hatchingTimeCounter = hatchingTime - hatchingTimeCounter;
        }
        else
        {
            hatchingTimeCounter = -1;
            isHatching = 2;
        }
    }

    /// <summary>
    /// 写入
    /// </summary>
    private void Wirte((string, DateTime) Set)
    {
        SumSave.crt_hatching.Set_data(Set);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet_hatching,
            SumSave.crt_hatching.Set_Uptade_String(), SumSave.crt_hatching.Get_Update_Character());
    }


    /// <summary>
    /// 根据下拉列表选中的宠物蛋
    /// </summary>
    /// <param name="index"></param>
    private void OnHatching_Dropdown(int index)
    {
        currentPet = hatching_Dropdown.options[index].text;

    }    
    /// <summary>
    /// 初始化下拉列表
    /// </summary>
    private void InitDropdown()
    {
        hatching_Dropdown.ClearOptions();
        foreach (user_pet_vo type in SumSave.db_pet)
        {
            hatching_Dropdown.options.Add(new Dropdown.OptionData(type.petEggsName.ToString()));
        }
    }


    
    
}
