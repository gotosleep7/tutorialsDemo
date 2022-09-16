using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Login : MonoBehaviour
{


    [SerializeField]
    TMP_InputField userIdInputField;

    [SerializeField]
    TMP_InputField passwordInputField;

    [SerializeField]
    ToggleGroup loginModel;

    [SerializeField]
    Toggle AutoLogin;

    [SerializeField]
    Button loginButton;

    [SerializeField]
    GameObject FailMessage;


    Color LoginBtnEnableColor;
    Color LoginBtnDisableColor;

    string userId, passwword;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#233D57", out LoginBtnEnableColor);
        ColorUtility.TryParseHtmlString("#CAD1D7", out LoginBtnDisableColor);
        SetLoginButtonState(false);
    }
    private void OnEnable()
    {
        userIdInputField?.onValueChanged.AddListener(OnUserIdChange);
        passwordInputField?.onValueChanged.AddListener(OnPwdChange);
    }

    private void OnDisable()
    {
        userIdInputField?.onValueChanged.RemoveListener(OnUserIdChange);
        passwordInputField?.onValueChanged.RemoveListener(OnPwdChange);

    }
    private void OnUserIdChange(string arg0)
    {
        userId = arg0;
        CanLogin();
    }
    private void OnPwdChange(string arg0)
    {
        passwword = arg0;
        CanLogin();
    }


    public void CanLogin()
    {
        if (userId != null && userId != "" && passwword != null && passwword != "")
        {
            SetLoginButtonState(true);
        }
        else
        {
            SetLoginButtonState(false);
        }

    }

    public void SetLoginButtonState(bool isEnable)
    {

        loginButton.enabled = isEnable;
        loginButton.image.color = isEnable ? LoginBtnEnableColor : LoginBtnDisableColor;
    }


    // Update is called once per frame
    void Update()
    {

    }



    public void OnLogin()
    {
        Debug.Log("userId:" + userIdInputField.text);
        Debug.Log("password:" + passwordInputField.text);
        Debug.Log("loginModel:" + loginModel.GetFirstActiveToggle().name);
        Debug.Log("AutoLogin:" + AutoLogin.isOn);

    }
}
