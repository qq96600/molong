using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 战斗飘血
/// </summary>
public class DamageTextManager : MonoBehaviour// MonoSingleton <DamageTextManager>
{
    public static DamageTextManager Instance;

    private Color normalColor = Color.red;
    private Color volleyColor = Color.yellow;
    private Color readlyColor = Color.green;

    /// <summary>
    /// �˺��ı� ��ʹ���е��б�
    /// </summary>
    private Queue<GameObject> DamageTipsList;

    private int maxDamageTextNum = 10;
    private string ch = "-";


    /// <summary>
    /// 显示伤害文本,伤害字
    /// </summary>
    private GameObject damage_text,Image_text;
    
   

    void Awake()
    {
        DamageTipsList = new Queue<GameObject>();
        damage_text=Resources.Load<GameObject>("Prefabs/panel_text/damage_text");
        
        Image_text=Resources.Load<GameObject>("Prefabs/panel_text/Image_text");
        Init();
    }

    void Update()
    {

    }

    public void Init()
    {
        GameObject parent = GameObject.FindWithTag("DamageTextPool");
        for (int i = 0; i < maxDamageTextNum; i++)
        {
            //ResManger.LoadPrefabInstance("Show/DamageTips", (damageText) =>
            //{
            //    DamageTipsList.Enqueue(damageText);
            //    damageText.SetActive(false);
            //}, parent.transform);
            //GetObjectFormPool
        }
    }

    /// <summary>
    /// 显示艺术字
    /// </summary>
    public void DisplayArtisticCharacters( string text,Transform father)
    {
       
        ObjectPoolManager.instance.GetObjectFormPool("damage_text", damage_text,father.position, Quaternion.identity,father );
       
        List<Sprite> Text= new List<Sprite>();
        for(int i=0;i<text.Length;i++)
        {
           
            Text.Add(Resources.Load<Sprite>("Assets/Resources/panel_fight/digit_Text/" + text[i]));
           
        }

        for(int j=0;j<Text.Count;j++)
        {
            Image_text.GetComponent<Image>().sprite = Text[j];
            ObjectPoolManager.instance.GetObjectFormPool("Image_text", Image_text, father.position, Quaternion.identity, damage_text.transform);
        }

    }



    /// <summary>
    /// 显示伤害文本
    /// </summary>
    /// <param name="damageEnum"></param>
    /// <param name="damage"></param>
    /// <param name="parent"></param>
    public void ShowDamageText(DamageEnum damageEnum, string damage, Transform parent)
    {
        //if (SumSave.Data_User_Setting.battle_DamageText != 0)
            //return;
        GameObject damageText = GetDamageTextFromPool();
        damageText.transform.position = parent.position;
        damageText.transform.SetParent(parent);
        Text textComponent = damageText.GetComponent<Text>();
        textComponent.text = damage.ToString();
        switch (damageEnum)
        {
            case DamageEnum.普通伤害:
                textComponent.color = normalColor;
                break;
            case DamageEnum.治疗伤害:
                textComponent.color = volleyColor;
                break;
            case DamageEnum.真实伤害:
                textComponent.color = readlyColor;
                break;
        }
        //damageText.transform.GetOrAddComponent<DamageAnimiton>().Init();
        //��Ӷ���

    }
    /// <summary>
    /// 从对象池中获取伤害文本
    /// </summary>
    private GameObject GetDamageTextFromPool()
    {
        if (DamageTipsList.Count <= 1)
        {
            GameObject parent = GameObject.FindWithTag("DamageTextPool");
            for (int i = 0; i < maxDamageTextNum; i++)
            {
                //ResManger.LoadPrefabInstance("Show/DamageTips", (damageText) =>
                //{
                //    DamageTipsList.Enqueue(damageText);
                //    damageText.SetActive(false);
                //}, parent.transform);
            }
        }

        GameObject damageText = DamageTipsList.Dequeue();

        damageText.SetActive(true);

        return damageText;
    }
    /// <summary>
    /// 将伤害文本放回对象池
    /// </summary>
    /// <param name="damageText"></param>
    public void ReturnDamageTextToPool(GameObject damageText)
    {
        damageText.SetActive(false);
        DamageTipsList.Enqueue(damageText);
        //damageText.GetComponent<DamageAnimiton>().
    }
}
