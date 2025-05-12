using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_base_par : Base_VO
{
    /// <summary>
    /// 选中区域
    /// </summary>
    public int index;
    /// <summary>
    /// 开启时间
    /// </summary>
    public DateTime opentime;

    /// <summary>
    /// 开启状态 1开启2关闭
    /// </summary>
    public int openstate;
    /// <summary>
    /// 设备类型
    /// </summary>
    public int device;


}
