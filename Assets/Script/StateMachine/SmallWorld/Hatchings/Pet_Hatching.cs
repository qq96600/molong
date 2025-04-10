
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
    /// ��ť����
    /// </summary>
    private Transform hatching_Buttons;
    /// <summary>
    /// �鿴���Ȱ�ť��������ť,���������ȡ��ť
    /// </summary>
    private Button displayPet, hatching_Button, receive_Button;
    /// <summary>
    /// ���ﵰ�����б�
    /// </summary>
    private Dropdown hatching_Dropdown;
    /// <summary>
    /// �����б�ѡ�еĳ��ﵰ
    /// </summary>
    private string currentPet;
    /// <summary>
    /// �����ɹ��ĳ�������
    /// </summary>
    private string Pet;

    /// <summary>
    /// ��������
    /// </summary>
    private Transform hatching_progress;
    /// <summary>
    /// ���ڷ���Image
    /// </summary>
    private Image petEggs_Image;
    /// <summary>
    /// ����������
    /// </summary>
    private Slider hatching_Slider;
    /// <summary>
    /// �����������ı�
    /// </summary>
    private Text hatching_Text;
    /// <summary>
    /// ��Ҫ������ʱ��
    /// </summary>
    public int hatchingTime;
    /// <summary>
    /// ������ʱ��
    /// </summary>
    private int hatchingTimeCounter;
    /// <summary>
    /// ��ʼ������ʱ��
    /// </summary>
    private DateTime startHatchingTime;
    /// <summary>
    /// �����Ƿ���� 0���գ�1�������У�2���������
    /// </summary>
    private int isHatching=0;
    /// <summary>
    /// ������Image
    /// </summary  >
    private Sprite sprite;

    protected override void Awake()
    {
        //base.Awake();
        #region ��ť��ʼ��
        hatching_Buttons = transform.Find("hatching_Buttons");
        displayPet = hatching_Buttons.Find("displayPet").GetComponent<Button>();
        hatching_Button = hatching_Buttons.Find("hatching_Button").GetComponent<Button>();
        hatching_Dropdown = hatching_Buttons.Find("hatching_Dropdown").GetComponent<Dropdown>();
        displayPet.onClick.AddListener(OnDisplayPet);
        hatching_Button.onClick.AddListener(OnHatching);
        hatching_Dropdown.onValueChanged.AddListener(OnHatching_Dropdown);
        currentPet = "ѡ����Ҫ�����ĳ���";
        InitDropdown();
        #endregion

        #region ������ʾ��ʼ��
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
    /// �����ɹ���ȡ����
    /// </summary>
    private void OnReceive()
    {
        if(isHatching==2)
        {
            
            Debug.Log("��ȡ����"+ isHatching);
            //��ȡ����
            Battle_Tool.Obtain_Resources(Pet, 1);
            isHatching = 0;
        }
       
    }

    /// <summary>
    /// ����
    /// </summary>
    private void OnHatching()
    {

        (string, DateTime) Set = SumSave.crt_hatching.Set();
        if (isHatching == 0)
        {
            //NeedConsumables(currentPet, 1);
            //if(RefreshConsumables())
            //{
                if (SumSave.db_pet_dic.TryGetValue(currentPet, out user_pet_vo quantity))//����ѡ��ĳ��ﵰ�����÷���ʱ�䣬�ͷ����ɹ��ĳ���
                {
                    hatchingTime = quantity.hatchingTime;
                    Pet = quantity.petName;
                    //petEggs_Image.sprite = Resources.Load<Sprite>("panel_fight/_icon_shop/hatching_" + quantity.petEggsName); //hatching_÷��¹��

                    petEggs_Image.sprite = Resources.Load<Sprite>("panel_fight/panlt_С��");
                }
                else
                {
                    Debug.Log("û��������ﵰ");
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
        //        Alert_Dec.Show("����û��"+ currentPet+"���ﵰ");
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
            Debug.Log("����ʱ" + hatchingTimeCounter+"��������"+ hatching_Slider.value);
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
    /// �򿪳����������
    /// </summary>
    private void OnDisplayPet()
    {
        if(isHatching != 0)
        {
            hatching_progress.gameObject.SetActive(true);
        }else
        {
            Alert_Dec.Show("û�з����ĳ���");
        }
        
    }


    /// <summary>
    /// ��ʼ��
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
        hatchingTimeCounter = (int)(SumSave.nowtime - startHatchingTime).TotalSeconds;//��ǰʱ��-ֲ����ֲʱ�� ���ֲ����ֲ�����ڵ�ʱ��
        if (hatchingTimeCounter <= hatchingTime)//ֲ���Ѿ�������ʱ��С��ֲ����Ҫ������ʱ��
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
    /// д��
    /// </summary>
    private void Wirte((string, DateTime) Set)
    {
        SumSave.crt_hatching.Set_data(Set);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_pet_hatching,
            SumSave.crt_hatching.Set_Uptade_String(), SumSave.crt_hatching.Get_Update_Character());
    }


    /// <summary>
    /// ���������б�ѡ�еĳ��ﵰ
    /// </summary>
    /// <param name="index"></param>
    private void OnHatching_Dropdown(int index)
    {
        currentPet = hatching_Dropdown.options[index].text;

    }    
    /// <summary>
    /// ��ʼ�������б�
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
