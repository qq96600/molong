
using Common;
using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panltItem : Panel_Base
{
    

    /// <summary>
    /// ֲ������
    /// </summary>
    public string plantName; // ֲ������
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public int growTime;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    private int growTimeInt;
    /// <summary>
    /// ֲ����ֲʱ��
    /// </summary>
    private DateTime currentGrowTimeDate;
    /// <summary>
    /// �Ƿ��ѽ�ˮ
    /// </summary>
    public int  isWatered = 0; //0�����ɽ�ˮ 1���ɽ�ˮ
    /// <summary>
    /// �Ƿ��ѳ���
    /// </summary>
    public int  isMature = 0;  //0�������ջ� 1�����ջ�
    /// <summary>
    /// ֲ��Image
    /// </summary>
    public Image image; 
    /// <summary>
    /// ����ʱText
    /// </summary>
    public Text CountdownText;
    /// <summary>
    /// ����λ��
    /// </summary>
    private int index = -1;

   protected override void Awake()
    {
        image = GetComponent<Image>();
        CountdownText = GetComponentInChildren<Text>();
        CountdownText.transform.gameObject.SetActive(false);
    }
   



    /// <summary>
    /// ��ʾֲ����������ʱ
    /// </summary>
    //public void Countdown()
    //{
    //    currentGrowTime -= Time.deltaTime;
    //    CountdownText.text = ((int)currentGrowTime).ToString();
    //    if (currentGrowTime <= 0)
    //    {
    //        isMature = true;
    //        CountdownText.transform.gameObject.SetActive(false);
    //    }
    //}



    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Init()
    {
       
        growTime = 0;
        isWatered = 0;
        isMature = 0;
        image.sprite = Resources.Load<Sprite>("panel_fight/panlt_����");
    }


    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <returns></returns>
    public int Obtain_Index()
    {
        return index;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="_index">�������</param>
    /// <param name="data">���ݿ�ֵ</param>
    public void Init(int _index, (string,DateTime) data)
    {
        index = _index;
        plantName = data.Item1;
        currentGrowTimeDate = data.Item2;
        isWatered = 0;
        isMature = 0;
        image.sprite = Resources.Load<Sprite>("panel_fight/panlt_����");

        growTimeInt = (int)(SumSave.nowtime - currentGrowTimeDate).TotalSeconds;//��ǰʱ��-ֲ����ֲʱ�� ���ֲ����ֲ�����ڵ�ʱ��
        if (growTimeInt <= growTime)//ֲ���Ѿ�������ʱ��С��ֲ����Ҫ������ʱ��
        {
            growTimeInt = growTime - growTimeInt;
        }
        else
        {
            growTimeInt = -1;
            isMature = 1;
        } 
    }
    /// <summary>
    /// ˢ��״̬
    /// </summary>
    /// <param name="time"></param>
    public void Fixed_Update(int time)//��ʾֲ�ﵹ��ʱ
    {
        if (growTimeInt > 0)   
        {
            growTimeInt -= time;
            CountdownText.text = growTimeInt.ToString();
        }
        else
        {
            CountdownText.text = "";
            growTimeInt = -1;
            isMature = 1;
        }


    }

    /// <summary>
    /// ����ֲ������
    /// </summary>
    /// <param name="_Data"></param>
    public void GetData(user_plant_vo _Data)
    {
        //panltType = _Data.plantType;

        plantName= _Data.plantName;
       
        CountdownText.transform.gameObject.SetActive(true);
        image.sprite = Resources.Load<Sprite>("panel_fight/panlt_" + plantName);
    }
    /// <summary>
    /// ��������ʱ��
    /// </summary>
    public void ReduceGrowthTime(int time)
    {
        Debug.Log("δ��ˮ��" + plantName + "��ó���" + currentGrowTimeDate);
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        currentGrowTimeDate -= timeSpan;
        growTimeInt -= time;
        
        Debug.Log("�ѽ�ˮ��" + plantName + "��ó���" + currentGrowTimeDate);

    }
    /// <summary>
    /// ��ȡ��ǰֲ�����ʱ��
    /// </summary>
    /// <returns></returns>
    public DateTime GetCurrentGrowTimeDate()
    {
        return currentGrowTimeDate;
    }

    /// <summary>
    /// ת��int����Ϊʱ��
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public DateTime ConvertFromUnixTimestamp(int timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return origin.AddSeconds(timestamp).ToLocalTime();
    }

}

