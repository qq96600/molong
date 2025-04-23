using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 种子数据
/// </summary>
public class db_seed_vo : Base_VO
{
    public string type;
    /// <summary>
    /// 编号
    /// </summary>
    public int sequence;
    /// <summary>
    /// 材料名称
    /// </summary>
    public string seed_name;
    /// <summary>
    /// 配方
    /// </summary>
    public string seed_formula;
    /// <summary>
    /// 成名名称
    /// </summary>
    public string pill;
    /// <summary>
    /// 合成公式
    /// </summary>
    public string formula;
    /// <summary>
    /// 药品加成效果最小值最大值
    /// </summary>
    public string pill_effect;
    /// <summary>
    /// 注灵需要数量
    /// </summary>
    public int seed_number=10;
    /// <summary>
    /// 权重
    /// </summary>
    public int Weight;
    /// <summary>
    /// 定义类型
    /// </summary>
    public int rule;
    /// <summary>
    /// 累积可以吞的次数
    /// </summary>
    public int limit;
    /// <summary>
    /// 对应字典编号
    /// </summary>
    public int dicdictionary_index;

}
