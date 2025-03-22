
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 文字弹出
/// </summary>
public class base_info_item : MonoBehaviour
{

    private float fontSize = 30f;

    private float time = 0f;

    private bool state = false;

    private void Start()
    {
        //Destroy(this.gameObject, 1.2f);
    }

    public void show_info(string info)//显示信息
    {
        GetComponent<Text>().fontSize = (int)fontSize;

        time = 0;state = true; 

        GetComponent<Text>().text = info;

        //StartCoroutine(HideFrame(1.2f));
    }

    private void Update()
    {
        if (state)
        {
            if (time < 3)
            {
                time += Time.deltaTime;
                if (time>2)
                GetComponent<Text>().fontSize--;
                //字体渐变透明
                GetComponent<Text>().color = new Color(1, 0, 0, 1 - Time.deltaTime);
            }
            else close();
        } 
    }

    private void close()
    {

        state = false;time = 0;

        ObjectPoolManager.instance.PushObjectToPool("info_base", this.gameObject);

    }
}