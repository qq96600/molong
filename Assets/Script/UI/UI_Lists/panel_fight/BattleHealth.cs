using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Components;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MVC
{
    /// <summary>
    ///  战斗显示血量
    /// </summary>
    public class BattleHealth : Base_Mono
    {
        /// <summary>
        /// 基础数值
        /// </summary>
        [HideInInspector]
        public float maxHP, HP, maxMP, MP, add_hp = 0;

        public float internalforceMP, EnergyMp, internalforcemaxMP, EnergymaxMp;

        /// <summary>
        /// 战斗信息
        /// </summary>
        public  panel_fight panel_fight;
        /// <summary>
        /// 战斗位置
        /// </summary>
        [HideInInspector]
        public int Pos = 0;

     
        private void Awake()
        {
            HP = maxHP;
            MP = maxMP;
            internalforceMP = internalforcemaxMP;
            EnergyMp = EnergymaxMp;
            panel_fight = transform.parent.parent.parent.GetComponent<panel_fight>();
        }
        public void Clear()
        {
            ObjectPoolManager.instance.PushObjectToPool(GetComponent<BattleAttack>().Data.show_name, this.gameObject);
        }
        /// <summary>
        /// 回复生命魔法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="dec"></param>
        public void HealConsumables(int hp, int mp)
        {
            HP = Mathf.Clamp(HP + hp, 0, maxHP);
            MP = Mathf.Clamp(MP + mp, 0, maxMP);
            if (internalforcemaxMP > 0) internalforceMP = Mathf.Clamp(internalforceMP + 2, 0, internalforcemaxMP);
            if (EnergymaxMp > 0) EnergyMp = Mathf.Clamp(EnergyMp + 1, 0, EnergymaxMp);
        }
        /// <summary>
        /// 消耗蓝
        /// </summary>
        /// <param name="value"></param>
        public void UseMp(float value)
        {
            if (value < 0) value = -value;

            MP -= value;

            MP = Mathf.Max(0, MP);

        }
        public void TakeDamage(float damage, DamageEnum damageEnum )
        {
            if (HP <= 0) return;
            //damage = 10000;
            HP -= damage;
            AudioManager.Instance.playAudio(ClipEnum.被敌人攻击);

            Hurt(damage, damageEnum);
            //测试掉落
            //ConfigBattle.LoadSetting(monster, 2);
            //if (monster.GetComponent<monster_battle_attck>() != null) WaitAndDestory(monster);
            if (HP <= 0)
            {
                //死亡 掉落
                if (GetComponent<monster_battle_attck>() != null)
                {
                    WaitAndDestory();
                    AudioManager.Instance.playAudio(ClipEnum.男角色死亡);
                }
                else if (GetComponent<player_battle_attck>() != null)
                {

                    SumSave.crt_achievement.increase_date_Exp((Achieve_collect.死亡).ToString(), 1);
                    SumSave.battleHeroHealths.Remove(this);
                }
                StartCoroutine(WaitAndDestory(GetComponent<BattleAttack>().Data.show_name));
            }
        }

      
        /// <summary>
        /// 文字偏移量
        /// </summary>
        int offset = 1;

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="type">1伤害2治疗</param>
        private void Hurt(float dec, DamageEnum type)
        {
            offset *= -1;

            string _dec=dec.ToString("F0");
            DamageTextManager.Instance.ShowDamageText(type, _dec, this.transform, offset);
            transform.parent.parent.parent.SendMessage("show_battle_info",
                    GetComponent<BattleAttack>().Data.show_name+" 受到 "+type+" 效果"+"造成"+dec+"伤害");
        }
        /// <summary>
        /// 延时销毁，掉落物品
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private void WaitAndDestory()
        {
            BattleAttack monster = GetComponent<BattleAttack>();
            KillMonsterMission(monster);
            SumSave.battleMonsterHealths.Remove(this);
            SumSave.crt_achievement.increase_date_Exp((Achieve_collect.击杀怪物).ToString(), 1);
            Battle_Tool.Obtain_Exp(monster.Data.Exp);
            SumSave.crt_user_unit.verify_data(currency_unit.灵珠, monster.Data.unit);
            int number = 1;
            Combat_statistics.AddMaxNumber();
            //判断是否增加历练值
            if (monster.Data.Monster_Lv != 1)
            {
                number = Random.Range(2, 5);

                if (monster.Data.Monster_Lv == 3)
                {
                    SumSave.crt_achievement.increase_date_Exp((Achieve_collect.击杀Boss).ToString(), 1);
                    ClearanceMapTask(monster);
                    number = Random.Range(5, 11);
                    Combat_statistics.AddBossNumber();
                    SumSave.crt_pass.progress(3);//通行证任务
                }
                else if (monster.Data.Monster_Lv == 2)
                {
                    Combat_statistics.AddEliteNumber();
                    SumSave.crt_pass.progress(2);
                }
            
               if (panel_fight.isMapType4())
                {
                    SumSave.crt_pass.progress(4);
                }
                SumSave.crt_user_unit.verify_data(currency_unit.历练, monster.Data.Point);//monster.Data.Point
                transform.parent.parent.parent.SendMessage("show_battle_info",
                "击杀 " + monster.Data.show_name + " 获得 " + monster.Data.Point + "历练");//monster.Data.Point 
            }
            Game_Omphalos.i.GetQueue(
                        Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user, SumSave.crt_user_unit.Set_Uptade_String(), SumSave.crt_user_unit.Get_Update_Character());
            List<string> lists = ConfigBattle.LoadSetting(monster, number);
            //增加经验
            if (SumSave.crt_setting.user_setting[2] == 0)
            {
                transform.parent.parent.parent.SendMessage("show_battle_info",
            "击杀 " + monster.Data.show_name + " 获得 " + monster.Data.Exp + "经验");
                transform.parent.parent.parent.SendMessage("show_battle_info",
            "击杀 " + monster.Data.show_name + " 获得 " + monster.Data.unit + "灵珠");
                if (lists.Count > 0)
                {
                    for (int i = 0; i < lists.Count; i++)
                    {
                        transform.parent.parent.parent.SendMessage("show_battle_info", lists[i]);
                    }
                }
            }
            //获取金币
            /*
            StartCoroutine(WaitAndDestory(monster.Data.Name));
            long tempEXP = monster.Data.Exp;
            //计算加成
            tempEXP = SumSave.BattleState.exp * tempEXP / 100;
            SumSave.crtHero.Exp += tempEXP;
            if (SumSave.user_OffLine.OpenShow == 0) AlertDec.Show("击杀" + monster.Data.Name + " 获得经验 " + tempEXP);
            if (monster.Data.monsterTypes == MonsterTypes.Boss)
            {
                if (SumSave.crtTask != null)
                {
                    if (SumSave.crtTask.target == monster.Data.Name || SumSave.crtTask.target == ((MapList)SumSave.LoadScene).ToString())
                    {
                        SumSave.crtTask.progressState++;
                        transform.parent.parent.SendMessage("ShowprogressState");
                    }
                }
            }
            if (monster.isDrop)
            {
                if (SumSave.isShen || SumSave.isXShen) ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(6, 16) : 1);
                //物品掉落
                else ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(5, 15) : 1);

                if (Random.Range(0, 100) < SumSave.HouseHold.Item3)
                {
                    称号中心.Instance.Pet_show("勤俭持家发动 获得二次奖励");
                    if (SumSave.isShen || SumSave.isXShen) ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(6, 16) : 1, true);
                    //物品掉落
                    else ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(5, 15) : 1, true);
                }
            */

        }
        /// <summary>
        /// 击杀怪物任务
        /// </summary>
        private static void KillMonsterMission(BattleAttack monster)
        {
            tool_Categoryt.Base_Task(1006);
            tool_Categoryt.Base_Task(1008);
            tool_Categoryt.Base_Task(1010);
            tool_Categoryt.Base_Task(1011);
            tool_Categoryt.Base_Task(1015);


            if (monster.Data.show_name == "昏眼牛")
            {
                tool_Categoryt.Base_Task(1017);
            }
            if (monster.Data.show_name == "扎纸鬼")
            {
                tool_Categoryt.Base_Task(1018);
            }
            if (monster.Data.show_name == "黑爪猫")
            {
                tool_Categoryt.Base_Task(1019);
            }
            if (monster.Data.show_name == "黑鳞君")
            {
                tool_Categoryt.Base_Task(1031);
            }
            if (monster.Data.show_name == "刺皮将")
            {
                tool_Categoryt.Base_Task(1032);
            }
            if (monster.Data.show_name == "血刀鬼")
            {
                tool_Categoryt.Base_Task(1038);
            }
            if (monster.Data.show_name == "独角魔")
            {
                tool_Categoryt.Base_Task(1045);
            }
            if (monster.Data.show_name == "肉瘤怪")
            {
                tool_Categoryt.Base_Task(1046);
            }
            if (monster.Data.show_name == "断角尊")
            {
                tool_Categoryt.Base_Task(1047);
            }
            if (monster.Data.show_name == "七爪狼")
            {
                tool_Categoryt.Base_Task(1053);
            }
            if (monster.Data.show_name == "玄铁帅")
            {
                tool_Categoryt.Base_Task(1061);
            }
            if (monster.Data.show_name == "血喉尸")
            {
                tool_Categoryt.Base_Task(1079);
            }
            if (monster.Data.show_name == "地龙虫")
            {
                tool_Categoryt.Base_Task(1081);
            }
        }




        /// <summary>
        /// 通关地图任务
        /// </summary>
        private static void ClearanceMapTask(BattleAttack monster)
        {
            if(monster.Data.show_name == "鹿妖"|| monster.Data.show_name == "猪妖" || monster.Data.show_name == "幽冥鸡")
            {
                tool_Categoryt.Base_Task(1016);
            }
            if(monster.Data.show_name == "昏眼牛" || monster.Data.show_name == "扎纸鬼" || monster.Data.show_name == "黑爪猫")
            {
                tool_Categoryt.Base_Task(1020);
            }
            if (monster.Data.show_name == "黑鳞君" || monster.Data.show_name == "刺皮将")
            {
                tool_Categoryt.Base_Task(1035);
            }
            if (monster.Data.show_name == "疤脸鬼" || monster.Data.show_name == "铁骨兵" || monster.Data.show_name == "血刀鬼")
            {
                tool_Categoryt.Base_Task(1039);
            }

            if(monster.Data.show_name == "啸月鬼")
            {
                tool_Categoryt.Base_Task(1054);
            }
            if (monster.Data.show_name == "墟界法王")
            {
                tool_Categoryt.Base_Task(1066);
            }
        }



        /// <summary>
        /// 删除销毁
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator WaitAndDestory(string healthname)
        {
            if (gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.8f);
                ObjectPoolManager.instance.PushObjectToPool(healthname, this.gameObject);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Destroy()
        {

        }

        private void OnDestroy()
        {
          
        }
        /// <summary>
        ///  是否已经挂掉
        /// </summary>
        public bool Dead
        {
            get { return HP <= 0; }
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

    }
}
