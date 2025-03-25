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
    ///  ս����ʾѪ��
    /// </summary>
    public class BattleHealth : Base_Mono
    {
        /// <summary>
        /// ������ֵ
        /// </summary>
        [HideInInspector]
        public float maxHP, HP, maxMP, MP, add_hp = 0;
        /// <summary>
        /// ս��λ��
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
        /// �ظ�����ħ��
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
        /// ������
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
            // Hurt("�˺� " + "-" + (int)damage);
            Hurt(damage.ToString());
            if (HP <= 0)
            {
                //���� ����

                //monster.newValueClear();
                WaitAndDestory("������ʧ");
            }
        }
        /*
        /// <summary>
        /// �Թ�������˺�
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="monster"></param>
        /// <param name="dec"></param>
        public void TakeDamage(float damage, MonsterBattleAttack monster, DamageList aMAGE = DamageList.�˺�, string skill = "")
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
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="type">1�˺�2����</param>
        private void Hurt(string dec, int type = 1)
        {
            DamageTextManager.Instance.ShowDamageText(DamageEnum.��ͨ�˺�, dec, this.transform);
        }
        /// <summary>
        /// ��ʾ��Ϣ
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
        /// ��ʱ���٣�������Ʒ
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
            //����ӳ�
            tempEXP = SumSave.BattleState.exp * tempEXP / 100;
            SumSave.crtHero.Exp += tempEXP;
            if (SumSave.user_OffLine.OpenShow == 0) AlertDec.Show("��ɱ" + monster.Data.Name + " ��þ��� " + tempEXP);
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
                //��Ʒ����
                else ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(5, 15) : 1);

                if (Random.Range(0, 100) < SumSave.HouseHold.Item3)
                {
                    �ƺ�����.Instance.Pet_show("�ڼ�ּҷ��� ��ö��ν���");
                    if (SumSave.isShen || SumSave.isXShen) ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(6, 16) : 1, true);
                    //��Ʒ����
                    else ConfigBattle.LoadSetting(monster, monster.Data.monsterTypes == MonsterTypes.Boss ? Random.Range(5, 15) : 1, true);
                }
            */
            
        }
       
        /// <summary>
        /// ɾ������
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
        /// ɾ��
        /// </summary>
        public void Destroy()
        {

        }

        private void OnDestroy()
        {
          
        }
        /// <summary>
        ///  �Ƿ��Ѿ��ҵ�
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
