using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_artifact_vo : Base_VO
{
    /// <summary>
    /// 神器名称
    /// </summary>
    public string arrifact_name;
    /// <summary>
    /// 神器ID
    /// </summary>
    public int arrifact_type;
    /// <summary>
    /// 神器升级需求
    /// </summary>
    public string[] arrifact_needs;
    /// <summary>
    /// 神器效果
    /// </summary>
    public string[] arrifact_effects;
    /// <summary>
    /// 神器描述
    /// </summary>
    public string Artifact_dec;

}
