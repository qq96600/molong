using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_base_par : Base_VO
{
    /// <summary>
    /// ѡ������
    /// </summary>
    public int index;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime opentime;

    /// <summary>
    /// ����״̬ 1����2�ر�
    /// </summary>
    public int openstate;
    /// <summary>
    /// �豸����
    /// </summary>
    public int device;
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public string par_name;

    public db_base_par(int index, DateTime opentime, int openstate, int device, string par_name)
    {
        this.index = index;
        this.opentime = opentime;
        this.openstate = openstate;
        this.device = device;
        this.par_name = par_name;
    }
}
