
using UI;
using UnityEngine.UI;

namespace MVC
{
    /// <summary>
    /// ���ƿ�����ʾ���
    /// </summary>
    public class show_btn_mian : Panel_Base
    {

        private new void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Show_Panel);
        }
        /// <summary>
        ///  ����б�
        /// </summary>
        public Main_List select_panel;
        /// <summary>
        ///  ��ʾ���
        /// </summary>
        private void Show_Panel()
        {
            transform.parent.parent.parent.parent.SendMessage("OnClickMap", select_panel.ToString());
            Game_Omphalos.i.Show_Screensaver();
        }
    }

}
