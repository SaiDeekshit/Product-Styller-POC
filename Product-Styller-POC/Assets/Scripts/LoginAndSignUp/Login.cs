using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    public void LoginButton()
   {
       if(CheckInputs()){
           if( EmailValidate.IsEmail(LoginSignUpUI.Instance.loginEmail.text))
           {
              if(CheckPasswordLength())
              {
                  if(SaveGetUserDetails.Instance.CheckingLogEmail()){
                      LoginSignUpUI.Instance.ClearAllInputFeilds();
                      LoginSignUpUI.Instance.panelLoginAndSignUp.SetActive(false);
                      LoginSignUpUI.Instance.enableTabNavigator = false;
                  }else{
                      LoginSignUpUI.Instance.loginerrorText.text = "Please signup as new user"; 
                  }
              }else{
                 LoginSignUpUI.Instance.loginerrorText.text = "Please enter minimum 6 characters password"; 
              }
           }
           else{
               LoginSignUpUI.Instance.loginerrorText.text = "Please enter valid email Id";
           }
       }
       else{
           LoginSignUpUI.Instance.loginerrorText.text = "Please enter all fields";
       }
   }
   bool CheckInputs()
   {
       if(LoginSignUpUI.Instance.loginEmail.text != "" && LoginSignUpUI.Instance.loginPassword.text != "" )
       {
        return true;
       }
       return false;
   }
   bool CheckPasswordLength()
   {
     int lenghtPassword = LoginSignUpUI.Instance.loginPassword.text.Length;
     if(lenghtPassword < 6)
     {
         return false;
     }
     return true;
   }
}
