using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat_statistics 
{
    public static long bossnumber, elitenumber, maxnumber, exp, moeny, Point, bag, time,detead;
    /// <summary>
    /// 初始化
    /// </summary>
    public static void Init()
    {
        elitenumber = 0;
        bossnumber = 0;
        maxnumber = 0;
        exp = 0;
        moeny = 0;
        bag = 0;
        time = 0;
        Point = 0;
        detead= 0;
    }
    /// <summary>
    /// 是否增加时间
    /// </summary>
    public static bool isTime = false;

    public static string Show_Info()
    {
        string info = "战斗时长：" + ConvertSecondsToHHMMSS((int)time) + "秒\n" +
            "击杀总数：" + maxnumber + "个\n" +
            "击杀精英：" + elitenumber + "个\n" +
            "击杀Boss：" + bossnumber + "个\n" +
            "经验收益：" + exp + "\n" +
            "灵珠收益：" + moeny + "\n" +
            "历练收益：" + Point + "\n" +
            "装备收益：" + bag + "个\n" +
            "死亡次数：" + detead + "次";
        return info;
    }

    public static void Time()
    {
        if (isTime) time++;
    }
    public static void AddBossNumber()
    {
        bossnumber++;
    }

    public static void AddEliteNumber()
    { 
        elitenumber++;
    }
    public static void AddMaxNumber()
    { 
        maxnumber++;
    }
    public static void AddExp(long exp)
    { 
        Combat_statistics.exp += exp;
    }
    public static void AddMoeny(long moeny)
    { 
        Combat_statistics.moeny += moeny;
    }
    public static void AddPoint(long Point)
    { 
        Combat_statistics.Point += Point;
    }
    public static void AddBag(int bag)
    { 
        Combat_statistics.bag += bag;
    }
    public static void AddDead()
    { 
        detead++;
    }

    public static string ConvertSecondsToHHMMSS(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

}
