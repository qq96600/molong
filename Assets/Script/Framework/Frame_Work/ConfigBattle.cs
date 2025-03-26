using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MVC
{
    /// <summary>
    /// 读取文件
    /// </summary>
    public static class ConfigBattle
    {
        /// <summary>
        /// 掉落金币
        /// </summary>
        public static int Money = 0;
        /// <summary>
        /// 掉落物品
        /// </summary>  
        public static Dictionary<string, string[]> CalculationBattle = new Dictionary<string, string[]>();

        /// <summary>
        /// 保底值
        /// </summary>
        private static Dictionary<string, (int, string[])> keyValuePairs = new Dictionary<string, (int, string[])>();
        /// <summary>
        /// 掉落集合
        /// </summary>
        public static string[] Calculations;
        public static void Instance()
        {
            CalculationBattle.Clear();
        }

        /// <summary>
        ///  读取设置
        /// </summary>
        public static void LoadSetting(BattleAttack monster, int number)
        {
            string base_name = SumSave.crt_resources.user_map_index;
            if (!CalculationBattle.ContainsKey(SumSave.crt_resources.user_map_index))
            {
                CalculationBattle.Add(base_name, ArrayHelper.Find(SumSave.db_maps, e => e.map_name == base_name).ProfitList.Split(' '));
            }
               
            for (int i = 0; i < number; i++)
            {
                string countEquip = CalculationBattle[base_name][Random.Range(0, CalculationBattle[base_name].Length)];
                CalculationBag(countEquip,monster.Data.show_name,false);

            }
        }
    
        /// <summary>
        /// 掉落配置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="Name"></param>
        /// <param name="boss"></param>
        /// <param name="age"></param>
        static void CalculationBag(string line, string Name, bool boss,int age=1)
        {

            Bag_Base_VO bag = new Bag_Base_VO();
            bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == line);
            bool exist = true;
            switch ((EquipConfigTypeList)Enum.Parse(typeof(EquipConfigTypeList), bag.StdMode))
            {
                case EquipConfigTypeList.武器:
                case EquipConfigTypeList.衣服:
                case EquipConfigTypeList.头盔:
                case EquipConfigTypeList.项链:
                case EquipConfigTypeList.护臂:
                case EquipConfigTypeList.戒指:
                case EquipConfigTypeList.手镯:
                case EquipConfigTypeList.扳指:
                case EquipConfigTypeList.腰带:
                case EquipConfigTypeList.靴子:
                case EquipConfigTypeList.护符:
                case EquipConfigTypeList.灵宝:
                case EquipConfigTypeList.勋章:
                case EquipConfigTypeList.饰品:
                case EquipConfigTypeList.玉佩:
                case EquipConfigTypeList.披风:
                    bag = tool_Categoryt.crate_equip(bag.Name);
                    break;
                default:
                    exist = false;
                    break;
            }
            if (exist)
            {
                if (SumSave.crt_resources.pages[0] > SumSave.crt_bag.Count)
                {
                    SumSave.crt_bag.Add(bag);
                    Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
                    Obtian_Bag(bag);
                    //判断回收
                    if (SumSave.crt_setting.user_setting[0] > bag.need_lv)
                    {
                    }
                }
                
            }

        }

        private static void Obtian_Bag(Bag_Base_VO bag)
        { 
            string[] keyValue = bag.user_value.Split(' ');
            Show_Info("获得 " + (enum_equip_quality_list)int.Parse(keyValue[2]) + " " + bag.Name);

        }

        private static void Show_Info(string info)
        { 
            Alert_Dec.Show(info);
         
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="line"></param>
        private static void Read(string line)
        {
            if (line.Length <= 0) return;

            //vs.Add(line);

            string[] keyValue = line.Split(' ');

            if (keyValue.Length <= 1) return;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="content">配置文件内容</param>
        /// <param name="handle">每行处理逻辑</param>
        public static void ReadConfig(string content, Action<string> lineHandle)
        {
            //字符串读取器
            using (StringReader sr = new StringReader(content))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    lineHandle(line);
                }
            }
        }

    }
}