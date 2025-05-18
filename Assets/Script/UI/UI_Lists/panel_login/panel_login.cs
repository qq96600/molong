
using Common;
using Components;
using System;
using System.Collections;
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
        /// <summary>
        /// 记住上一次登录的服务器
        /// </summary>
        private string lastServer;
        private void Start()
        {
            SendNotification(NotiList.Read_Instace);

            StartCoroutine("Read_Instace");
        }

        private IEnumerator Read_Instace()
        {
            while (SumSave.db_monsters==null)
            {
                yield return new WaitForSeconds(1f);
                SendNotification(NotiList.Read_Instace);
                Alert_Dec.Show("网络链接中断，请重试");
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            TheServerObg= Find<Transform>("TheServer");
            TheServerObg.gameObject.SetActive(false);
            TheServerList=Find<Transform>("TheServer/TheServerList/Viewport/Content");
            TheServerText=Find<Text>("TheServer/CurrentTheServer/TheServerText/Text");
            TheServerUP=Find<Button>("TheServer/CurrentTheServer/TheServerUP");
            btn_Item= Battle_Tool.Find_Prefabs<btn_item>("btn_item");// Resources.Load<btn_item>("Prefabs/base_tool/btn_item"); 
            TheServerUP.onClick.AddListener(OnLoginClick);

            loginBt = Find<Button>("login");
            loginBt.onClick.AddListener(Open_function);
            loginBt.gameObject.SetActive(false);
            fightPanel = UI_Manager.I.GetPanel<panel_fight>();
            TaploginBt=Find<Button>("Taplogin");
            TaploginBt.onClick.AddListener(TapLogin);

#if UNITY_EDITOR
            TaploginBt.gameObject.SetActive(false);
            loginBt.gameObject.SetActive(true);
#elif UNITY_ANDROID

            TaploginBt.gameObject.SetActive(true);//true
            loginBt.gameObject.SetActive(false);
#elif UNITY_IPHONE
            TaploginBt.gameObject.SetActive(false);
            loginBt.gameObject.SetActive(true);
#endif
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

        private void Open_function()
        {
#if UNITY_EDITOR
            UpTheServer();
            //UI_Manager.Instance.GetPanel<Panel_cratehero>().Show();
#elif UNITY_ANDROID
            UpTheServer();
#elif UNITY_IPHONE
            UI_Manager.Instance.GetPanel<Panel_cratehero>().Show();
#endif
        }

        /// <summary>
        /// 打开服务器选择界面
        /// </summary>
        public void UpTheServer()
        {
            if (SumSave.db_pars==null)
            {
                Alert_Dec.Show("网络链接中断，请重试");
                return;
            }
            TheServerObg.gameObject.SetActive(true);
            ClearObject(TheServerList);
            int device = 1;
#if UNITY_EDITOR
            device = 1;
#elif UNITY_ANDROID
            device = 1;
#elif UNITY_IPHONE
            device = 2;
#endif
            for (int i=0;i<SumSave.db_pars.Count; i++)
            {
                if (SumSave.db_pars[i].device == device)
                {
                    btn_item item = Instantiate(btn_Item, TheServerList);
                    item.Show(SumSave.db_pars[i].index, (SumSave.db_pars[i].index).ToString() + "区");
                    item.GetComponent<Button>().onClick.AddListener(() => { SelectPar(item); });

                }
            }

            if (PlayerPrefs.HasKey(lastServer))
            {
                string index= PlayerPrefs.GetString(lastServer);
                TheServerText.text = "选中" + index + "区";
                SumSave.par = int.Parse(index);
            }
            else
            {
                string index = SumSave.db_pars[0].index.ToString();
                TheServerText.text = "选中" + index + "区";
                SumSave.par = int.Parse(index);
            }

        }

        /// <summary>
        /// 点击选择服务器
        /// </summary>
        /// <param name="i"></param>
        private void SelectPar(btn_item item)
        {
            Alert_Dec.Show("选择了"+ (item.index).ToString() + "区");
            select_par = item;
            TheServerText.text ="选中"+(item.index).ToString()+"区";
            SumSave.par = item.index;
        }

        //tap登录完成之后打开开关
        public void ShowStartBtn(bool v)
        {
            TaploginBt.gameObject.SetActive(!v);
            loginBt.gameObject.SetActive(v);
        }

        /// <summary>
        /// Tap登录
        /// </summary>
        private void TapLogin()
        {
            #if UNITY_EDITOR
            TaploginBt.gameObject.SetActive(false);
            loginBt.gameObject.SetActive(true);
            #elif UNITY_ANDROID
            _ = GameLogin.Instance.Login();
           
            #elif UNITY_IPHONE
            UI_Manager.Instance.GetPanel<Panel_cratehero>().Show();

#endif
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
            PlayerPrefs.SetInt("同意阅读协议", 1);
#if UNITY_EDITOR
            SumSave.uid = "DSFSDFSDFSDF";//"05c8cc2e26234ec0acc690343a598eba";
            //Game_Omphalos.i.Wirte_Iphone();
#elif UNITY_ANDROID
            Game_Omphalos.i.Wirte_Tap();
#elif UNITY_IPHONE
            Game_Omphalos.i.Wirte_Iphone();
#endif
            TheServerObg.gameObject.SetActive(false);
            if (SumSave.uid != null)
            {
                PlayerPrefs.SetString(lastServer, select_par.index.ToString());

                SendNotification(NotiList.User_Login);
                UI_Manager.I.GetPanel<panel_Mian>().Show();
                Hide();
            }
            else
            {
                #if UNITY_EDITOR

#elif UNITY_ANDROID
                _ = GameLogin.Instance.Login();

#elif UNITY_IPHONE
                UI_Manager.Instance.GetPanel<Panel_cratehero>().Show();
#endif
            }

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
