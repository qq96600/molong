using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_suit_vo : Base_VO
{
    public string suit_name;
    public int suit_type;
    public int suit_number;
    public List<(int,int,int)> suit_list;
    public void Init(string value)
    {
        suit_list = new List<(int, int, int)>();
        string[] values= value.Split('&');
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i].Length > 1)
            { 
                string[] temp = values[i].Split(' ');
                if (temp.Length == 3)
                {
                    (int, int, int) temp1 = (int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]));
                    suit_list.Add(temp1);
                }
                
            }
        }
    }
}
