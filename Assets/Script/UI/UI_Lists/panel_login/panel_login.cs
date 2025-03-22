
using Components;
using UI;
using UnityEngine;
using UnityEngine.UI;


namespace MVC
{
    public class panel_login : Panel_Base
    {

        private Button loginBt, userBt, selfBt;//开始按钮, 用户，隐私

        private GameObject AgreementButter, AgreementWindow, userWd, selfWd;//协议点击组件，协议面板

        private Text titleText;//标题

        private  Toggle Toggle;//协议确定开关

        private void Start()
        {
            SendNotification(NotiList.Read_Instace);
        }
        public override void Initialize()
        {
            base.Initialize();
            loginBt = Find<Button>("login");
            loginBt.onClick.AddListener(OnLoginClick);

            #region 用户协议
            AgreementButter = GameObject.Find("AgreementButter");
            if (AgreementButter == null)
            {
                Debug.Log("协议组件为空");
                return;
            }
            userBt = AgreementButter.transform.Find("user").GetComponent<Button>();
            selfBt = AgreementButter.transform.Find("self").GetComponent<Button>();
            userBt = Find<Button>("AgreementButter/user");
            userBt.onClick.AddListener(OpenUser);
            selfBt = Find<Button>("AgreementButter/self");
            selfBt.onClick.AddListener(OpenSelf);

            AgreementWindow = GameObject.Find("AgreementWindow");
            userWd = AgreementWindow.transform.Find("userWd").gameObject;
            selfWd = AgreementWindow.transform.Find("selfWd").gameObject;

            titleText = AgreementWindow.transform.Find("Title").GetComponentInChildren<Text>();

            Toggle = AgreementButter.transform.Find("Toggle").GetComponent<Toggle>();
            Toggle.isOn = PlayerPrefs.GetInt("同意阅读协议", 0) == 1;
            OpenUser();
            #endregion

        }
        private void OpenUser()//打开用户协议
        {
            if (IsAgreementWdNull())
            {
                titleText.text = "用户协议";
                selfWd.SetActive(false);
                userWd.SetActive(true);
            }
        }

        private void OpenSelf()//打开隐私协议
        {
            if (IsAgreementWdNull())
            {

                titleText.text = "隐私协议";
                userWd.SetActive(false);
                selfWd.SetActive(true);
            }
        }

        private bool IsAgreementWdNull()//判断协议是否为空
        {
            if (selfWd != null && userWd != null && titleText != null && AgreementWindow != null)
            {
                AgreementWindow.SetActive(true);
                return true;
            }
            else
            {
                Debug.LogError("协议窗口为空");
                return false;
            }
        }




        private void OnLoginClick()//登录点击
        {
            if (!Toggle.isOn)
            {
                Alert_Dec.Show("请先阅读并勾选同意协议");
                Debug.Log("请先阅读并勾选同意协议");
                PlayerPrefs.SetInt("同意阅读协议", 0);
                return;
            }
            SendNotification(NotiList.User_Login);
            Debug.Log("已阅读并勾选同意协议");
            PlayerPrefs.SetInt("同意阅读协议", 1);
        }

        public override void Show()
        {
            base.Show();
        }
    }


}
