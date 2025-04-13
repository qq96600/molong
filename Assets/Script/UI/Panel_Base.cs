
using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    ///  显示和隐藏功能的面板,closeButton
    /// </summary>
    public class Panel_Base : Base_Mono
    {
        protected Button closeButton;
        protected string panelName;
        protected virtual void Awake()
        {
            closeButton = Find<Button>("close_button");
            if (closeButton != null) closeButton.onClick.AddListener(Hide);
            //if (TipsBtn != null) TipsBtn.onClick.AddListener(() =>{ });
            Initialize();
        }
        /// <summary>
        ///  显示
        /// </summary>
        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        ///  隐藏
        /// </summary>
        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
