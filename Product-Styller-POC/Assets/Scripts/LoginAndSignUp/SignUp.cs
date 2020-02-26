using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUp : MonoBehaviour
{
   public void SignUpButton()
   {
       if(CheckInputs()){
           if( EmailValidate.IsEmail(LoginSignUpUI.Instance.signUpEmail.text))
           {
              if(CheckPasswordLength())
              {
                  if(SaveGetUserDetails.Instance.CheckingSignUpEmail()){
                  SaveGetUserDetails.Instance.SignUp(LoginSignUpUI.Instance.signUpFirstName.text,LoginSignUpUI.Instance.signUpLastName.text,LoginSignUpUI.Instance.signUpEmail.text,LoginSignUpUI.Instance.signUpPassword.text);
                  LoginSignUpUI.Instance.ClearAllInputFeilds();
                  LoginSignUpUI.Instance.SignUpToLogin();
                  }else{
                      LoginSignUpUI.Instance.signUperrorText.text = "Please use another email Id";
                  }
              }else{
                 LoginSignUpUI.Instance.signUperrorText.text = "Please enter minimum 6 characters password"; 
              }
           }
           else{
               LoginSignUpUI.Instance.signUperrorText.text = "Please enter valid email Id";
           }
       }
       else{
           LoginSignUpUI.Instance.signUperrorText.text = "Please enter all fields";
       }
   }
   bool CheckInputs()
   {
       if(LoginSignUpUI.Instance.signUpFirstName.text != "" && LoginSignUpUI.Instance.signUpLastName.text != "" && LoginSignUpUI.Instance.signUpEmail.text != "" && LoginSignUpUI.Instance.signUpPassword.text != "")
       {
        return true;
       }
       return false;
   }
   bool CheckPasswordLength()
   {
     int lenghtPassword = LoginSignUpUI.Instance.signUpPassword.text.Length;
     if(lenghtPassword < 6)
     {
         return false;
     }
     return true;
   }
}
