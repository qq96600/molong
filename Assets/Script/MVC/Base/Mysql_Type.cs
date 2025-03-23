using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 执行命令
/// </summary>
public enum Mysql_Type
{
    MySqlDataReader,//获取时间
    InsertInto,//初始化
    UpdateInto,//更新
    Delete//删除
}

/// <summary>
/// 表名
/// </summary>
public enum Mysql_Table_Name
{
    db_monster,
    db_stditems,
    db_magic,
    db_map,
    db_hero,//标准参数
    mo_user_base,//用户基础信息
    mo_user_value,//用户数值信息
    mo_user_hero

}
