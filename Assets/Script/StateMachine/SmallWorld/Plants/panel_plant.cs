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
    /// ѡ��ֲ�������б�
    /// </summary>
    private Dropdown plantDropdown;
    /// <summary>
    /// ��ֲ��ť,��ˮ��ť,�ջ�ť
    /// </summary>
    private Button seedingButton, wateringButton, harvestButton, expansionButton;
    private Transform Buttons;
    /// <summary>
    /// ��ֲֲ�︸����
    /// </summary>
    private Transform plantFields; 
    /// <summary>
    /// ֲ��Ԥ����
    /// </summary>
    private panltItem plantPrefab;   
    /// <summary>
    /// ��ǰѡ���ֲ��
    /// </summary>
    private string currentPlant;

    /// <summary>
    /// ����ֲ���������
    /// </summary>
    private int maxPlantCount;

    /// <summary>
    /// ���������ֵ�
    /// </summary>
    private Dictionary<PanltEnum,user_plant_vo> plantBag =new Dictionary<PanltEnum, user_plant_vo>();
    /// <summary>
    /// ��ֲ��ֲ���б�
    /// </summary>
    private List<panltItem> panltList=new List<panltItem>();
    /// <summary>
    /// ֲ����Ϣ���
    /// </summary>
    private Transform plantInfo;
    private Text plantText;
    /// <summary>
    /// ��ˮ����ʱ��
    /// </summary>
    public int reduceTime=10; 
    /// <summary>
    /// �Ƿ�������ݣ����ݵ�����
    /// </summary>
    public int isExpansion=0; 



    protected override void Awake()
    {
        #region ��ť�����б��ʼ��
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

        #region ֲ��Ԥ�����ʼ��
        plantPrefab= Resources.Load<panltItem>("Prefabs/panel_smallWorld/plantItem");
        plantFields= transform.Find("plantFields").transform;
        plantInfo=transform.Find("plant_info").transform;
        plantText=plantInfo.Find("plant_Text").GetComponent<Text>();
        plantInfo.gameObject.SetActive(false);
        
        //for (int i = 0; i < plantCount; i++)//��ʼ����ֲ������ӵ��б�
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
    /// ��ʾ��Ʒ
    /// </summary>
    private void baseShow()
    {
        List<(string, DateTime)> Set = SumSave.crt_plant.Set();
        StopAllCoroutines();
        for (int i = plantFields.childCount - 1; i >= 0; i--)//�����ֲ����
        {
            Destroy(plantFields.GetChild(i).gameObject);
        }
        panltList.Clear();//����б�
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
    /// �ж������б�ѡ�е�ֲ��
    /// </summary>
    /// <param name="index"></param>
    private void CurrentPlant(int index)
    {
        currentPlant = plantDropdown.options[index].text;
    }

    /// <summary>
    /// ��ʾֲ����Ϣ
    /// </summary>
    /// <returns></returns>
    private void PlantInfo(panltItem _panltItem)
    {
        if(_panltItem.plantName != "0")
        {
            plantInfo.gameObject.SetActive(true);
            plantInfo.transform.position = _panltItem.transform.position;
            plantText.text = _panltItem.plantName+ "\n����ʱ�䣺"+_panltItem.growTime+"��"
                + "\n�Ƿ�ˮ��"+(_panltItem.isWatered==1?"�ɽ�ˮ":"���ɽ�ˮ")
                +"\n�Ƿ�����ջ�:"+(_panltItem.isMature==1?"���ջ�":"�����ջ�");
        }
        
        
    }
    /// <summary>
    /// ����
    /// </summary>
    private void Expansion()
    {
        if(isExpansion>0)
        {
            for(int i = isExpansion; i > 0; i--)
            {
                Debug.Log("���ݳɹ�" + isExpansion);
                List<(string, DateTime)> Set = SumSave.crt_plant.Set();
                Set.Add(("0", DateTime.Now));
                Wirte(Set);
                baseShow();
                isExpansion--;
                
            }
        }else
        {
            Alert_Dec.Show("����������Ϊ0");
        }
        
    }


    /// <summary>
    /// ��ˮ
    /// </summary>
    private void Watering()
    {
        List<(string, DateTime)> Set = SumSave.crt_plant.Set();
        for (int i = 0; i < panltList.Count; i++)
        {
            if (panltList[i].isWatered == 1)//�ж��Ƿ���Խ�ˮ��ֲ�ﲻΪ��&& Set[i].Item1 != "0"
            {
                panltList[i].isWatered = 0;
                panltList[i].ReduceGrowthTime(reduceTime);//��������ʱ��
                Set[i] = (Set[i].Item1, panltList[i].GetCurrentGrowTimeDate());//���³���ʱ��
                SumSave.crt_plant.Up_user_plants(Set);//���½�ɫ����
                Wirte(Set);//д�����ݿ�
                Alert_Dec.Show("�ѽ�ˮ��" + panltList[i].plantName + "����ʱ�����" + reduceTime + "��");
            }
        }
        
    }




    /// <summary>
    /// д��
    /// </summary>
    private void Wirte(List<(string,DateTime)> Set)
    {
        SumSave.crt_plant.Set_data(Set);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_plant,
            SumSave.crt_plant.Set_Uptade_String(), SumSave.crt_plant.Get_Update_Character());

    }

    /// <summary>
    /// �ջ�
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

                Wirte(Set);//д�����ݿ�
                Alert_Dec.Show("���ջ�");

                
            }
        }
    }


    /// <summary>
    /// ����
    /// </summary>
    private void Seeding()
    {
        if (currentPlant == null) { Alert_Dec.Show("��ѡ������Ʒ"); return; }
        List<(string, DateTime )> Set = SumSave.crt_plant.Set();//��ȡ���������Լ���Ҫ�����ʱ��
        List<int> numbers= new List<int>();//�����ص�����
        for (int i = 0; i < Set.Count; i++)
        {
            if(Set[i].Item1=="0") numbers.Add(i);
        }
        if (numbers.Count == 0)
        { 
            Alert_Dec.Show("��������");
            return;
        }
        int number = numbers.Count;//�����ص�����
       
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
                Alert_Dec.Show("������û�и�����");
                return;
            }
        }
        //�ж��Ƿ������ֲ
        for (int i = 0; i < number; i++)
        {
            Set[numbers[i]]= (currentPlant.ToString(), SumSave.nowtime);
        }
        Wirte(Set);
        baseShow();

    }

    /// <summary>
    /// ��ʼ�������б�
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
