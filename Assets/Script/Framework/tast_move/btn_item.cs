using MVC;
using UnityEngine.UI;

public class btn_item : Base_Mono
{
    private Text info;
    public int index;
    private void Awake()
    {
        info = Find<Text>("info");
    }
   
    /// <summary>
    /// ÒýÓÃ
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="dec"></param>
    public void Show(int _index, object dec)
    { 
        index = _index;
        info.text = dec.ToString();
    }
}
