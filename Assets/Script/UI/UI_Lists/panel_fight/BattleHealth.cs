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
        /// <summary>
        /// 战斗位置
        /// </summary>
        [HideInInspector]
        public int Pos = 0;
      
        private void Awake()
        {
            HP = maxHP;
            MP = maxMP;
        }

        public void Clear()
        {
         
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
        public void TakeDamage(float damage)
        {
            if (HP <= 0) return;
            HP -= damage;
            // Hurt("伤害 " + "-" + (int)damage);
            Hurt(damage.ToString());
            if (HP <= 0)
            {
                //死亡 掉落

                //monster.newValueClear();
                WaitAndDestory("死亡消失");
            }
        }
        /*
        /// <summary>
        /// 对怪物产生伤害
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="monster"></param>
        /// <param name="dec"></param>
        public void TakeDamage(float damage, MonsterBattleAttack monster, DamageList aMAGE = DamageList.伤害, string skill = "")
        {
            if (HP <= 0) return;
            HP -= damage;
            if (monster.Data.monsterTypes != MonsterTypes.Nothing)
            {
                transform.parent.parent.parent.parent.SendMessage("ShowBossSlider", monster);
            }
            Hurt(skill + aMAGE + " " + "-" + (int)damage);
            if (HP <= 0)
            {
                monster.newValueClear();
                WaitAndDestory(monster);
            }
        }
        */
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="type">1伤害2治疗</param>
        private void Hurt(string dec, int type = 1)
        {
            DamageTextManager.Instance.ShowDamageText(DamageEnum.普通伤害, dec, this.transform);
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="dec"></param>
        private void Show_info(string dec)
        {
            transform.parent.parent.parent.SendMessage("show_info", dec);
        }

        private void Show_info(List<string> dec)
        {
            foreach (string item in dec)
            {
                Show_info(item);
            }
        }

        /// <summary>
        /// 延时销毁，掉落物品
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private void WaitAndDestory()//MonsterBattleAttack monster)
        {
            OnDestroy();
            //monster.OnDestroy();
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
