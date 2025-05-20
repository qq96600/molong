using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_pos_item : MonoBehaviour
{
    public int index;
    private void OnEnable()
    {
        transform.parent.parent.parent.parent.parent.parent.SendMessage("Instance_Pos", this);
    }

}
