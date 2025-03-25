using Common;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class User_Instace_Proxy : Base_Proxy
    {
        /// <summary>
        ///  NAME
        /// </summary>
        public new const string NAME = "User_Instace_Proxy";
        /// <summary>
        ///  构造函数
        /// </summary>
        public User_Instace_Proxy()
        {
            this.ProxyName = NAME;
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void User_Login()
        {
            OpenMySqlDB();
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_base, "uid", GetStr(SumSave.uid));
            SumSave.crt_user = new user_base_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                { 
                    SumSave.crt_user = ReadDb.Read(mysqlReader, SumSave.crt_user);
                }
                SumSave.crt_user.Nowdate = DateTime.Now;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_base, SumSave.crt_user.Set_Uptade_String(), SumSave.crt_user.Get_Update_Character());
            }
            else
            {
                SumSave.crt_user.uid = SumSave.uid; //Guid.NewGuid().ToString("N");
                SumSave.crt_user.Nowdate = DateTime.Now;
                SumSave.crt_user.RegisterDate = DateTime.Now;
                SumSave.crt_user.par = SumSave.par;
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_base, SumSave.crt_user.Set_Instace_String());
            }

            Read_Instace();
            CloseMySqlDB();
        }
        //初始化文件
        private void Init()
        {
            SumSave.crt_bag = new List<Bag_Base_VO>();
            SumSave.crt_euqip = new List<Bag_Base_VO>();
            if (SumSave.crt_resources.bag_value.Length > 0)
            { 
                string[] Splits= SumSave.crt_resources.bag_value.Split(';');
                for (int i = 0; i < Splits.Length; i++)
                {
                    if (Splits[i].Length > 0)
                    {
                        Bag_Base_VO bag = new Bag_Base_VO();
                        bag.user_value= Splits[i];
                        SumSave.crt_bag.Add(tool_Categoryt.Read_Bag(bag));
                    }
                }
                
            }
        }
        /// <summary>
        /// 读取自身数据
        /// </summary>
        private void Read_Instace()
        {
            Read_User_Hero();
            Read_User_Resources();
            refresh_Max_Hero_Attribute();

        }
        /// <summary>
        /// 刷新属性
        /// </summary>
        private void refresh_Max_Hero_Attribute()
        {
            SumSave.crt_MaxHero = new crtMaxHeroVO();
            crtMaxHeroVO crt = new crtMaxHeroVO();
            for (int i = 0; i < SumSave.db_heros.Count; i++)
            {
                if (SumSave.db_heros[i].hero_name == SumSave.crt_hero.hero_pos)
                {
                    
                    for (int j = 0; j < SumSave.db_heros[i].crate_value.Length; j++)
                    {
                        int value = SumSave.db_heros[i].crate_value[j] + (SumSave.db_heros[i].up_value[j] * (SumSave.crt_hero.hero_Lv / SumSave.db_heros[i].up_base_value[j]));
                        switch ((enum_attribute_list)j)
                        {
                            case enum_attribute_list.生命值:
                                crt.MaxHP += value;
                                break;
                            case enum_attribute_list.法力值:
                                crt.MaxMp += value;
                                break;
                            case enum_attribute_list.内力值:
                                crt.internalforceMP += value;
                                break;
                            case enum_attribute_list.蓄力值:
                                crt.EnergyMp += value;
                                break;
                            case enum_attribute_list.物理防御:
                                crt.DefMax += value;
                                break;
                            case enum_attribute_list.魔法防御:
                                crt.MagicDefMax += value;
                                break;
                            case enum_attribute_list.物理攻击:
                                crt.damageMax += value;
                                break;
                            case enum_attribute_list.魔法攻击:
                                crt.MagicdamageMax+= value;
                                break;
                            case enum_attribute_list.命中:
                                crt.hit += value;
                                break;
                            case enum_attribute_list.躲避:
                                crt.dodge+= value;
                                break;
                            case enum_attribute_list.穿透:
                                crt.penetrate += value;
                                break;
                            case enum_attribute_list.格挡:
                                crt.block+= value;
                                break;
                            case enum_attribute_list.暴击:
                                crt.crit_rate+= value;
                                break;
                            case enum_attribute_list.幸运:
                                crt.Lucky += value;
                                break;
                            case enum_attribute_list.暴击伤害:
                                crt.crit_damage += value;
                                break;
                            case enum_attribute_list.伤害加成:
                                crt.double_damage+= value;
                                break;
                            case enum_attribute_list.真实伤害:
                                crt.Real_harm += value;
                                break;
                            case enum_attribute_list.伤害减免:
                                crt.Damage_Reduction+= value;
                                break;
                            case enum_attribute_list.伤害吸收:
                                crt.Damage_absorption+= value;
                                break;
                            case enum_attribute_list.异常抗性:
                                crt.resistance+= value;
                                break;
                            case enum_attribute_list.攻击速度:
                                crt.attack_speed+= value;
                                break;
                            case enum_attribute_list.移动速度:
                                crt.move_speed+= value;
                                break;
                            case enum_attribute_list.生命加成:
                                crt.bonus_Hp+= value;
                                break;
                            case enum_attribute_list.法力加成:
                                crt.bonus_Mp+= value;
                                break;
                            case enum_attribute_list.生命回复:
                                crt.Heal_Hp+= value;
                                break;
                            case enum_attribute_list.法力回复:
                                crt.Heal_Mp+= value;
                                break;
                            case enum_attribute_list.物攻加成:
                                crt.bonus_Damage+= value;
                                break;
                            case enum_attribute_list.魔攻加成:
                                crt.bonus_MagicDamage+= value;
                                break;
                            case enum_attribute_list.物防加成:
                                crt.bonus_Def+= value;
                                break;
                            case enum_attribute_list.魔防加成:
                                crt.bonus_MagicDef+= value;
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            //添加装备效果
            //添加技能效果
            //添加神器效果
            //称号属性
            //成就属性
            //皮肤
            SumSave.crt_MaxHero = crt;



        }

        ///
        /// <summary>
        /// 
        /// </summary>
        public void Refresh_Max_Hero_Attribute()
        {
            refresh_Max_Hero_Attribute();
        }

        /// <summary>
        /// 读取资源数据
        /// </summary>
        private void Read_User_Resources()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_value, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_resources = new user_base_Resources_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_resources = ReadDb.Read(mysqlReader, new user_base_Resources_vo());
                }
                Init();
                SumSave.crt_resources.now_time= DateTime.Now;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Uptade_String(), SumSave.crt_resources.Get_Update_Character());

            }
            else
            {
                SumSave.crt_resources.now_time = DateTime.Now;
                SumSave.crt_resources.skill_value = "";
                SumSave.crt_resources.house_value = "";
                SumSave.crt_resources.bag_value = "";
                SumSave.crt_resources.material_value = "";
                SumSave.crt_resources.equip_value = "";
                SumSave.crt_resources.pages = new int[] { 120, 60, 0, 0, 0};
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Instace_String());
            }
        }

        /// <summary>
        /// 读取英雄数据
        /// </summary>
        private void Read_User_Hero()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_hero, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_hero = new Hero_VO();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_hero = ReadDb.Read(mysqlReader, new Hero_VO());
                }
            }
            else
            {
                SumSave.crt_hero.hero_name = "007";
                SumSave.crt_hero.hero_type = enum_hero_type_list.物理攻击.ToString();
                SumSave.crt_hero.hero_index = "1";
                SumSave.crt_hero.hero_list = "江湖人";
                SumSave.crt_hero.hero_lv = "1";
                SumSave.crt_hero.hero_exp = "0";
                SumSave.crt_hero.hero_Lv = 1;
                SumSave.crt_hero.hero_Exp = 0;
                SumSave.crt_hero.hero_pos = "江湖人";
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_hero, SumSave.crt_hero.Set_Instace_String());
            }
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        public void Execute_Write(List<Base_Wirte_VO> list)
        {
            OpenMySqlDB();
            //写入数据
            ExecuteWrite(list);
            CloseMySqlDB();
        }
        public void Read_Locality()
        {

        }
    }
}