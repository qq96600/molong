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
        public void HealConsumables(float value, int type, string dec)
        {
            if (type == 1)
            {
                HP += value; HP = Mathf.Min(maxHP, HP);
            }
            if (type == 2)
            {
                MP += value; MP = Mathf.Min(maxMP, MP);
            }
            if (type == 3)
            {
                HP += value; HP = Mathf.Min(maxHP , HP);

                MP += value; MP = Mathf.Min(maxMP, MP);
            }

            //Hurt(dec, 2);
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
            OnDestroy();
            BattleAttack monster = GetComponent<BattleAttack>();
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

                    number = Random.Range(5, 11);
                    Combat_statistics.AddBossNumber();
                }
                else if (monster.Data.Monster_Lv == 2)
                {
                    Combat_statistics.AddEliteNumber();
                }
                SumSave.crt_user_unit.verify_data(currency_unit.历练, monster.Data.Point);
                transform.parent.parent.parent.SendMessage("show_battle_info",
                "击杀 " + monster.Data.show_name + " 获得 " + monster.Data.Point + "历练");
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
        /// 删除销毁
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator WaitAndDestory(string healthname)
        {
            if (gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.3f);
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
