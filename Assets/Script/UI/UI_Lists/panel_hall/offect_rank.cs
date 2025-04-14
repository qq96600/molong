using UnityEngine;
using System.Collections;
using System;
using Common;

namespace MVC
{
    /// <summary>
    /// ≈≈––∞Ò
    /// </summary>
    public class offect_rank : Base_Mono
    {
        private Transform crt;

        /// <summary>
        /// ‘§…ËÃÂ
        /// </summary>
        private rank_item rank_itemPrefab;

        private void Awake()
        {
            crt = Find<Transform>("Scroll View/Viewport/Content");
            rank_itemPrefab= Resources.Load<rank_item>("Prefabs/panel_hall/rank_item");
        }

        public void Show()
        {
            SendNotification(NotiList.Read_User_Ranks);
            GetList();
        }

        private void GetList()
        {
            for (int i = crt.childCount - 1; i >= 0; i--)
            {
                Destroy(crt.GetChild(i).gameObject);
            }
            for (int i = 0; i < SumSave.user_ranks.lists.Count; i++)
            {
                rank_item item = Instantiate(rank_itemPrefab, crt);
                item.Data = SumSave.user_ranks.lists[i];
                item.Show_index(i + 1);
            }
        }
    }

}
