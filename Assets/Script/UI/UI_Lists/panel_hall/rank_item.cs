using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MVC
{
    public class rank_item : Base_Mono
    {
        private Text rank_name, rank_value, rank_index;

        private Text rank_type;

        private void Awake()
        {
            rank_name = Find<Text>("rank_name");

            rank_value = Find<Text>("rank_value");

            rank_type = Find<Text>("rank_type");

            rank_index = Find<Text>("rank_index");
        }

        private base_rank_vo data;
        /// <summary>
        /// Data
        /// </summary>
        public base_rank_vo Data
        {
            set
            {
                data = value;

                if (data == null) return;

                rank_name.text = data.rank_name+ "Lv." + data.lv;
                rank_value.text = "" + data.Ranking_value;
                rank_type.text = data.rank_type + "";

            }
            get
            {
                return data;
            }
        }
        public void Show_index(int idnex)
        {
            rank_index.text = "" + idnex;
        }

    }

}
