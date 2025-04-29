using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_pos_item : MonoBehaviour
{
    private void Start()
    {
        transform.parent.parent.parent.parent.parent.parent.SendMessage("Instance_Pos", this);
    }

    public int index;
    // Update is called once per frame
    void Update()
    {
        
    }
}
