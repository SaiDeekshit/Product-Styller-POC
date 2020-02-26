
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSignUpUI : MonoBehaviour
{
    private static LoginSignUpUI _instance;
    public static LoginSignUpUI Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("LoginSignUp is empty");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }
    public GameObject panelLoginAndSignUp;
    public GameObject panelLogin;
    public GameObject panelSignUp;

    public InputField loginEmail,loginPassword;
    public InputField signUpFirstName,signUpLastName,signUpEmail,signUpPassword;
    public Text signUperrorText,loginerrorText;
    public bool enableTabNavigator = false;
    // Start is called before the first frame update
    void Start()
    {
        enableTabNavigator = true;
        panelLoginAndSignUp.SetActive(true);
        SignUpToLogin();
    }
    
    public void LoginToSignUp()
    {
        panelLogin.SetActive(false);
        panelSignUp.SetActive(true);
        ClearAllInputFeilds();
    }
     public void SignUpToLogin()
    {
        panelLogin.SetActive(true);
        panelSignUp.SetActive(false);
        ClearAllInputFeilds();
    }
    public void ClearAllInputFeilds()
    {
        loginEmail.text = "";
        loginPassword.text = "";
        signUpFirstName.text = "";
        signUpLastName.text = "";
        signUpEmail.text = "";
        signUpPassword.text = "";
        signUperrorText.text = "";
        loginerrorText.text = "";
    }
}
