using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_Artifact : Panel_Base
{
    /// <summary>
    /// ����λ��
    /// </summary>
    private Transform pos_artifact, crt_btn;

    private btn_item btn_itm_prefabs;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    private artifact_item crt_artifact;
    /// <summary>
    /// �����б�
    /// </summary>
    private artifact_item artifact_itm_prefabs;
    /// <summary>
    /// ����
    /// </summary>
    private List<string> btn_list = new List<string>() { "����", "����", "����", "����" };
    /// <summary>
    /// ��ǰѡ��ҳ��
    /// </summary>
    private int index = 1;

    private artifact_offect offect;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/btn_list");
        pos_artifact= Find<Transform>("bg_main/Scroll View/Viewport/Content");
        btn_itm_prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        artifact_itm_prefabs = Resources.Load<artifact_item>("Prefabs/panel_Artifact/artifact_item");
        offect = Find<artifact_offect>("bg_main/artifact_offect");
        for (int i = 0; i < btn_list.Count; i++)
        {
            btn_item btn = Instantiate(btn_itm_prefabs, crt_btn);
            btn.Show(i + 1, btn_list[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { OnBtnClick(btn); });
        }
    }
    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="btn"></param>
    private void OnBtnClick(btn_item btn)
    {
        AudioManager.Instance.playAudio(ClipEnum.ʹ����Ʒ);
        index= btn.index;
        Show_List();
    }
    public override void Show()
    {
        base.Show();
        Show_List();

    }
    /// <summary>
    /// ��ʾ�����б�
    /// </summary>
    private void Show_List()
    {
        for (int i = pos_artifact.childCount - 1; i >= 0; i--)
        {
            Destroy(pos_artifact.GetChild(i).gameObject);
        }
        for (int i = 0; i < SumSave.db_Artifacts.Count; i++)
        {
            if (SumSave.db_Artifacts[i].arrifact_type == index)
            {
                artifact_item item = Instantiate(artifact_itm_prefabs, pos_artifact);
                item.Data = SumSave.db_Artifacts[i];
                List<(string, int)> list = SumSave.crt_artifact.Set();
                (string, int) index = ArrayHelper.Find(list, e => e.Item1 == SumSave.db_Artifacts[i].arrifact_name);
                if (index.Item2 > 0) item.Set(index.Item2);
                item.GetComponent<Button>().onClick.AddListener(delegate { Select_artifact(item); });
            }
           
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    private void Select_artifact(artifact_item item)
    {
        crt_artifact = item;
        offect.gameObject.SetActive(true);
        offect.set_artifact(item);
    }
}
