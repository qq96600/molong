
using Common;
using MVC;
using System;
using System.Data.Common;
using UnityEngine;
/// <summary>
/// 读取基准值
/// </summary>
public static class Mysql_Read
{
    /// <summary>
    /// 读取路径
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="col"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string Select(Mysql_Table_Name tableName, string col, string values)
    {
        string query = "SELECT * FROM " + tableName + " WHERE " + col + " = " + values;

        return query;
    }
    public static void Read(DbDataReader mysqlReader)
    {
        for (int i = 0; i < mysqlReader.FieldCount; i++)
        {
            SumSave.nowtime = Convert.ToDateTime(mysqlReader[i].ToString());
            //Debug.Log("基准值读取成功" + SumSave.nowtime);
        }
    }
    /// <summary>
    /// 读取试练塔
    /// </summary>
    /// <param name="mysqlReader"></param>
    public static void Read_towers(DbDataReader mysqlReader)
    {
        SumSave.crt_Trial_Tower_rank = new mo_world_boss_rank();
        SumSave.crt_Trial_Tower_rank.Ranking_value = mysqlReader.GetString(mysqlReader.GetOrdinal("value"));
        SumSave.crt_Trial_Tower_rank.InitLists();
    }
    /// <summary>
    /// 读取用户排名
    /// </summary>
    /// <param name="reader"></param>
    public static void Read_user_rank(DbDataReader mysqlReader)
    {
        SumSave.user_ranks = new rank_vo();
        SumSave.user_ranks.Ranking_value = mysqlReader.GetString(mysqlReader.GetOrdinal("value"));
        string[] splits = SumSave.user_ranks.Ranking_value.Split(';');
        //读取排行榜
        foreach (string base_value in splits)
        {
            if (base_value != "")
            {
                base_rank_vo bag_Base_VO = new base_rank_vo();
                bag_Base_VO.SetPropertyValue(base_value);
                SumSave.user_ranks.lists.Add(bag_Base_VO);
            }
        }
    }
    /// <summary>
    /// 读取无尽塔排行榜
    /// </summary>
    /// <param name="reader"></param>
    public static void Read_user_endless_battle(DbDataReader mysqlReader)
    {
        SumSave.crt_endless_battle = new user_endless_battle();
        SumSave.crt_endless_battle.endless_value = mysqlReader.GetString(mysqlReader.GetOrdinal("value"));
        SumSave.crt_endless_battle.Split_endless();
    }

    /// <summary>
    /// 获取写入格式
    /// </summary>
    /// <param name="type"></param>
    /// <param name="tableName"></param>
    /// <param name="sql"></param>
    /// <param name="sql_names"></param>
    /// <returns></returns>
    public static Base_Wirte_VO GetQueue(Mysql_Type type, Mysql_Table_Name tableName, string[] sql, string[] sql_names = null)
    {
        //获取新列表
        Base_Wirte_VO vo = new Base_Wirte_VO();
        vo.type = type;
        vo.tableName = tableName;
        vo.columnNames = sql_names;
        vo.columnValues = sql;
        vo.exist = true;
        return vo;
    }
}