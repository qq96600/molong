using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    List<string> ClipList = new List<string>() { "��������", "�����˹���","���˹���", "���˱�����","�ظ�Ѫ��","�س�","ʹ����Ʒ",
            "������Ʒ","�н�ɫ����","Ů��ɫ����","�ͷ��׵���","���Х��","��óƺ�"};//������Ƶ�ļ�����
    static Dictionary<string, AudioClip> BGMdic = new Dictionary<string, AudioClip>();
    static Dictionary<string, AudioClip> Clipdic = new Dictionary<string, AudioClip>();
    public int index;//��Ƶ�ļ�����
    List<string> bgmName = new List<string>() { "����", "������", "������", "��Ĺ", "���ض�", "������", "����", "����", "��", "��ħ��ɲ",
    "ɳĮ��","�߶�","ͨ����","�����","���Ӵ�","���ݳ�"}; //bgm����; //bgm ��ͼ���֡�����Ƶ�ļ�������ͬ���޸���ͬ���޸�
    private void Awake()
    {
        Instance = this.GetComponent<AudioManager>();
        if (Clipdic.Count == 0) StartCoroutine("GetClip");

    }
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public IEnumerator GetClip()
    {
        foreach (var item in ClipList)
        {
            
            ResourceRequest re = Resources.LoadAsync<AudioClip>($"AudioClip/{item.ToString()}");
            Clipdic.Add(item.ToString(), re.asset as AudioClip);
        }
        return null;
    }
    public IEnumerator GetBGM()
    {
        if (File.Exists(Application.dataPath + "/Resources/AudioClip/BGM/" + BGMtempName + ".wav"))
        {
            ResourceRequest re = Resources.LoadAsync<AudioClip>($"AudioClip/BGM/{BGMtempName}");
            BGMdic.Add(BGMtempName, re.asset as AudioClip);
        }
        return null;
    }
    /// <summary>
    /// ������Ч��һ�Σ�
    /// </summary>
    /// <param name="s"></param>
    public void playAudio(ClipEnum clip)
    {
        if(SumSave.crt_setting.user_setting[3]==1) return;
        if (Clipdic.ContainsKey(clip.ToString())) { audioSource.PlayOneShot(Clipdic[clip.ToString()]); return; }
        else StartCoroutine("GetClip");

    }
    string BGMtempName;//���ñ������ŷ����ṩ�Ĳ���
    /// <summary>
    /// �ı䱳������
    /// </summary>
    /// <param name="clip"></param>
    public void ChangeBGM(BGMenum bg)
    {
        if (SumSave.crt_setting.user_setting[3] == 1) return;
        if (BGMdic.ContainsKey(bg.ToString())) { audioSource.clip = BGMdic[bg.ToString()]; Debug.Log("û����Ч��"); return; }
        else
        {
            BGMtempName = bg.ToString();
            StartCoroutine("GetBGM");
        }

    }/// <summary>
     /// ���������Ƿ���
     /// </summary>
     /// <param name="isPlay"></param>
    public void isMuteBGM(bool isPlay)
    {
        if (audioSource == null) return;
        audioSource.mute = isPlay;
    }
    /// <summary>
    /// ���η�תbgm����״̬
    /// </summary>
    /// <param name="isPlay"></param>
    public void isMuteBGMOne()
    {
        if (audioSource == null) return;
        audioSource.mute = !audioSource.mute;
    }
    /// <summary>
    /// �Ƿ����ͣ�������֣������²��ţ�
    /// </summary>
    /// <param name="isPlay"></param>
    public void isPlayBGM(bool isPlay)
    {
        if (audioSource == null) return;
        if (isPlay) audioSource.UnPause();
        else audioSource.Pause();
    }/// <summary>
     /// �Ƿ񲥷ű�������(�������ͷ��ʼ)
     /// </summary>
     /// <param name="isPlay"></param>
    public void isDieBGM(bool isPlay)
    {
        if (audioSource == null) return;
        if (isPlay) audioSource.Play();
        else audioSource.Stop();
    }
}
public enum BGMenum
{
    ����, ������, ������, ��Ĺ, ���ض�, ������,
    ����, ����, ��, ��ħ��ɲ,
    ɳĮ��, �߶�, ͨ����, �����, ���Ӵ�, ���ݳ�
}
public enum ClipEnum
{
    ��������, �����˹���, ���˹���, �ظ�Ѫ��, �س�, ʹ����Ʒ, ������Ʒ, �н�ɫ����, Ů��ɫ����, �ͷ��׵���, ���Х��, ��óƺ�
}
