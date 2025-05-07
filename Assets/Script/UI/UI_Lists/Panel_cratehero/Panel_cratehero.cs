using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Panel_cratehero : Panel_Base
{
    private const string BaseUserID = "账户";
    private const string BaseUserPwd = "密码";
    private const string BaseUserRemember = "记住密码";
    /// <summary>
    /// 账户
    /// </summary>
    private InputField account;
    /// <summary>
    /// 密码
    /// </summary>
    private InputField password;
    /// <summary>
    /// 登录
    /// </summary>
    private Button logon;
    /// <summary>
    /// 记住密码
    /// </summary>
    private Toggle isRemember;


    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        account=Find<InputField>("hero/account");
        account.onValueChanged.AddListener(OnAccount);
        password=Find<InputField>("hero/password");
        password.onValueChanged.AddListener(OnPasswordt);
        logon =Find<Button>("hero/logon");
        logon.onClick.AddListener(()=> { OnLogon(); });
        isRemember=Find<Toggle>("hero/isRemember");
        isRemember.onValueChanged.AddListener(OnRemember);
        if (!PlayerPrefs.HasKey(BaseUserRemember))
        {
            PlayerPrefs.SetInt(BaseUserRemember, 1);
        }
        isRemember.isOn = PlayerPrefs.GetInt(BaseUserRemember) == 1;
        if (PlayerPrefs.GetInt(BaseUserRemember) == 1)
        {
            if (PlayerPrefs.HasKey(BaseUserID))
                account.text = PlayerPrefs.GetString(BaseUserID);
            if( PlayerPrefs.HasKey(BaseUserPwd))
                password.text = PlayerPrefs.GetString(BaseUserPwd);
        }

    }

    private void OnPasswordt(string arg0)
    {
        PlayerPrefs.SetString(BaseUserPwd, arg0);
    }

    private void OnAccount(string arg0)
    {
        PlayerPrefs.SetString(BaseUserID, arg0);
    }


    private void OnRemember(bool arg0)
    {
        PlayerPrefs.SetInt(BaseUserRemember, arg0 ? 1 : 0);
        Alert_Dec.Show(arg0 ? "记住密码" : "取消密码记忆");
    }

    private void OnLogon()
    {
        if (account.text == "" || password.text == "")
        {
            Alert_Dec.Show("请输入账户或密码");
            return;
        }
        string[] id = new string[] { account.text, password.text };
        Game_Omphalos.i.Crate_Accout(id);
        UI_Manager.Instance.GetPanel<panel_login>().UpTheServer();
        Hide();
    }

    public override void Show()
    {
        base.Show();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
