using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill_offect_item : Base_Mono
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    private Text info;
    /// <summary>
    /// ս������ʱ
    /// </summary>
    private Text WaitTime;
    private void Awake()
    {
        info = Find<Text>("info");
        WaitTime = Find<Text>("WaitTime");
    }
    private base_skill_vo data;

    /// <summary>
    /// Data
    /// </summary>
    public base_skill_vo Data
    {
        set
        {
            data = value;
            if (data == null) return;
            //item_icon.sprite = UI.UI_Manager.I.GetEquipSprite("icon/", "10050" + Random.Range(16, 55));
            info.text = data.skillname;
        }
        get
        {
            return data;
        }
    }

    public void Battle()
    {
        info.text="";
        StartCoroutine(Skill_WaitTime());
    }
    /// <summary>
    /// �Ƿ�����ͷ�
    /// </summary>
    /// <returns></returns>
    public bool IsState()
    {
        return data.battle_CD <= 0;
    }
    /// <summary>
    /// ˢ����ȴ
    /// </summary>
    /// <returns></returns>
    private IEnumerator Skill_WaitTime()
    {
        data.battle_CD = data.skill_cd;
        info.text = "";
        float base_time = 0.1f;
        while (data.battle_CD > 0)
        {
            data.battle_CD -= base_time;
            WaitTime.text = data.battle_CD.ToString("0.0")+"S";
            if(data.battle_CD<=0)data.battle_CD = 0;
            yield return new WaitForSeconds(base_time);
        }
        WaitTime.text = "";
        info.text= data.skillname;
    }


}
