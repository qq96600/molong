using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tool_Categoryt : MonoBehaviour
{

    public static tool_Categoryt Tool;
    public T Find<T>(string name)
    {
        if (transform.Find(name) == null)
        {
            Debug.LogError(this + " �Ӷ���: " + name + " û���ҵ�!");
            return default(T);
        }
        return transform.Find(name).GetComponent<T>();
    }
}
