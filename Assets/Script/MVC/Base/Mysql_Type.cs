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
    mo_user_pet_explore,//用户宠物探险信息
    mo_user_pet,//用户宠物信息
    db_pet_explore,//宠物探险信息
    mo_user_world,
    db_lv,//等级信息
    user_rank,//排行榜
    db_hall,//大厅信息
    mo_user_achieve,//用户成就信息
    db_achieve,//成就数据信息
    db_store,//商店信息
    db_seed,//炼丹种子信息
    mo_user_seed,//用户炼丹种子信息
    mo_user_needlist,//用户需求信息
    db_collect,//收集信息
    mo_user_collect,//用户收集完成信息
    db_signin,//签到信息
    mo_user_signin,
    mo_user_tap,//tap登录
    mo_user_iphone,//苹果登录
    db_par,//服务器列表
    user_message_window,//消息窗口
    db_basetask,//大世界信息
    mo_user_greenhandguide,//新手引导
    user_player_buff,//玩家buff
    user_emial,//自身邮件
    server_mail,//当前邮件
    history_server_mail,//历史邮件
    db_accumulatedrewards,//累计奖励
    mo_user_rewards_state,//用户累计奖励状态
    db_fate,//命运殿堂
    db_vip,//vip信息
    db_world_boss,//世界boss
    user_world_boss_rank,//世界boss排行榜
    user_world_boss,//世界boss伤害
    history_world_boss,//世界boss历史伤害
    db_formula,//造化炉合成信息
    db_suit,//套装信息
    db_dec,//具体功能消息
    versions,//版本信息
}
