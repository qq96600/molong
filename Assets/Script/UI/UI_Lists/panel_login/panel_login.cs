
using Common;
using Components;
using System;
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

        private panel_fight fightPanel;
        /// <summary>
        /// Tap登录按钮
        /// </summary>
        private  Button TaploginBt;
        /// <summary>
        /// 服务器obg
        /// </summary>
        private Transform TheServerObg;
        /// <summary>
        /// 服务器列表
        /// </summary>
        private Transform TheServerList;
        /// <summary>
        /// 当前选择的服务器
        /// </summary>
        private Text TheServerText; 
        /// <summary>
        /// 确定选择的服务器
        /// </summary>
        private Button TheServerUP;
        /// <summary>
        /// 按钮预制体
        /// </summary>
        private btn_item btn_Item;
        /// <summary>
        /// 当前选择的服务器
        /// </summary>
        private btn_item select_par;

        private void Start()
        {
            SendNotification(NotiList.Read_Instace);
        }
        public override void Initialize()
        {
            base.Initialize();
            TheServerObg= Find<Transform>("TheServer");
            TheServerObg.gameObject.SetActive(false);
            TheServerList=Find<Transform>("TheServer/TheServerList/Viewport/Content");
            TheServerText=Find<Text>("TheServer/CurrentTheServer/TheServerText");
            TheServerUP=Find<Button>("TheServer/CurrentTheServer/TheServerUP");
            btn_Item= Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
            TheServerUP.onClick.AddListener(OnLoginClick);

            loginBt = Find<Button>("login");
            loginBt.onClick.AddListener(UpTheServer);
            loginBt.gameObject.SetActive(false);
            fightPanel = UI_Manager.I.GetPanel<panel_fight>();
            TaploginBt=Find<Button>("Taplogin");
            TaploginBt.onClick.AddListener(TapLogin);



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

        /// <summary>
        /// 打开服务器选择界面
        /// </summary>
        private void UpTheServer()
        {
            TheServerObg.gameObject.SetActive(true);
            ClearObject(TheServerList);
            for(int i=0;i<SumSave.db_pars.Count; i++)
            {
                btn_item item=Instantiate(btn_Item, TheServerList);
                item.Show(SumSave.db_pars[i].index, (SumSave.db_pars[i].index).ToString()+ "区");
                item.GetComponent<Button>().onClick.AddListener(()=> { SelectPar(item); });
               
            }
            
        }

        /// <summary>
        /// 点击选择服务器
        /// </summary>
        /// <param name="i"></param>
        private void SelectPar(btn_item item)
        {
            Alert_Dec.Show("选择了"+ (item.index).ToString() + "区");
            select_par =item;
            TheServerText.text ="选中"+(item.index).ToString()+"区";
        }

        //tap登录完成之后打开开关
        internal void ShowStartBtn(bool v)
        {
            TaploginBt.gameObject.SetActive(!v);
            loginBt.gameObject.SetActive(v);
        }

        /// <summary>
        /// Tap登录
        /// </summary>
        private void TapLogin()
        {
            _ = GameLogin.Instance.Login();
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



        /// <summary>
        /// 点击登录
        /// </summary>
        private void OnLoginClick()//登录点击
        {
            if(select_par==null)
            {
                Alert_Dec.Show("请先选择服务器");
                return;
            }

            if (!Toggle.isOn)
            {
                Alert_Dec.Show("请先阅读并勾选同意协议");
                Debug.Log("请先阅读并勾选同意协议");
                PlayerPrefs.SetInt("同意阅读协议", 0);
                return;
            }
            SendNotification(NotiList.User_Login);
            //Debug.Log("已阅读并勾选同意协议");
            PlayerPrefs.SetInt("同意阅读协议", 1);
            Hide();
            UI_Manager.I.GetPanel<panel_Mian>().Show();
            //fightPanel.Show();
            ////计算离线收益
            //fightPanel.offline();
        }

        public override void Show()
        {
            base.Show();
        }
    }


}
