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
    db_setting,//标准设置信息
    db_artifact,//标准神器
    mo_user_base,//用户基础信息
    mo_user_value,//用户资源信息
    mo_user_hero,//用户英雄信息
    mo_user_setting,//用户设置信息
    mo_user_artifact,//用户神器信息
    mo_user,//用户货币信息
    loglist,//日志
    user_login,//用户登录
    db_pass,//通行证
    mo_user_pass,//用户通行证
    mo_user_plant,//用户种植信息
    db_plant,//种植信息
    db_pet,//宠物信息
    mo_user_pet_hatching,//用户宠物孵化信息
    db_pet_explore,//宠物探险信息
    mo_user_world,
    db_lv,//等级信息
    user_rank,//排行榜
    db_hall,//大厅信息
    db_seed,//炼丹种子信息
    mo_user_seed,//用户炼丹种子信息
}
