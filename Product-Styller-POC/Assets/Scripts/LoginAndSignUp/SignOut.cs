using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOut : MonoBehaviour
{
   public void SIgnOutFromCapture()
   {
       ProductHandlerManager.Instance.BackButton();
       ItemsCategoryUI.Instance.panelItemsCategory.SetActive(true);
       SignOutFromItemsCategory();

   }
    public void SignOutFromItemsCategory()
   {
       LoginSignUpUI.Instance.panelLoginAndSignUp.SetActive(true);
       LoginSignUpUI.Instance.enableTabNavigator = true;
       if( ShotButtonManager.CurrentRefrenceShotDetail != null){
       ShotButtonManager.CurrentRefrenceShotDetail.DeSelectShot();
       }
   }
}
