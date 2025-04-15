using MVC;
using UnityEngine;
using UnityEngine.UI;

public class btn_item : Base_Mono
{
    private Text info;
    public int index;
    /// <summary>
    /// �Ƿ�Ϊ����״̬
    /// </summary>
    private bool active = false;

    private void Awake()
    {

        info = Find<Text>("info"); 

    }

    /// <summary>
    ///  ѡ��״̬
    /// </summary>
    public bool Selected
    {
        set
        {
            active = value;
            info.color = value ? Color.red : Color.white;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="dec"></param>
    public void Show(int _index, object dec)
    {
        index = _index;
        info.text = dec.ToString();
    }
    // <summary>
    // �Ƿ񼤻�
    // </summary>
    // <returns></returns>
    public bool Active()
    {
        return active;
    }
}
