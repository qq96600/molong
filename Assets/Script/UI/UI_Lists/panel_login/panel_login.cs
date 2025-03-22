
using Components;
using UI;
using UnityEngine;
using UnityEngine.UI;


namespace MVC
{
    public class panel_login : Panel_Base
    {

        private Button loginBt, userBt, selfBt;//��ʼ��ť, �û�����˽

        private GameObject AgreementButter, AgreementWindow, userWd, selfWd;//Э���������Э�����

        private Text titleText;//����

        private  Toggle Toggle;//Э��ȷ������

        private void Start()
        {
            SendNotification(NotiList.Read_Instace);
        }
        public override void Initialize()
        {
            base.Initialize();
            loginBt = Find<Button>("login");
            loginBt.onClick.AddListener(OnLoginClick);

            #region �û�Э��
            AgreementButter = GameObject.Find("AgreementButter");
            if (AgreementButter == null)
            {
                Debug.Log("Э�����Ϊ��");
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
            Toggle.isOn = PlayerPrefs.GetInt("ͬ���Ķ�Э��", 0) == 1;
            OpenUser();
            #endregion

        }
        private void OpenUser()//���û�Э��
        {
            if (IsAgreementWdNull())
            {
                titleText.text = "�û�Э��";
                selfWd.SetActive(false);
                userWd.SetActive(true);
            }
        }

        private void OpenSelf()//����˽Э��
        {
            if (IsAgreementWdNull())
            {

                titleText.text = "��˽Э��";
                userWd.SetActive(false);
                selfWd.SetActive(true);
            }
        }

        private bool IsAgreementWdNull()//�ж�Э���Ƿ�Ϊ��
        {
            if (selfWd != null && userWd != null && titleText != null && AgreementWindow != null)
            {
                AgreementWindow.SetActive(true);
                return true;
            }
            else
            {
                Debug.LogError("Э�鴰��Ϊ��");
                return false;
            }
        }




        private void OnLoginClick()//��¼���
        {
            if (!Toggle.isOn)
            {
                Alert_Dec.Show("�����Ķ�����ѡͬ��Э��");
                Debug.Log("�����Ķ�����ѡͬ��Э��");
                PlayerPrefs.SetInt("ͬ���Ķ�Э��", 0);
                return;
            }
            SendNotification(NotiList.User_Login);
            Debug.Log("���Ķ�����ѡͬ��Э��");
            PlayerPrefs.SetInt("ͬ���Ķ�Э��", 1);
        }

        public override void Show()
        {
            base.Show();
        }
    }


}
