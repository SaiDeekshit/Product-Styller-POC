using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveGetUserDetails : MonoBehaviour
{
    private static SaveGetUserDetails _instance;
    public static SaveGetUserDetails Instance
    {
        get{
            if(_instance == null)
            {
                Debug.Log("SaveGetUserDetails is empty");
            }
            return _instance;
        }
    }
   
    void Awake()
    {
        _instance = this;
    }
    
    public List<UserDetails> userDetails;
    public string filePathForUserDetails;
    void Start() {
        
        if(File.Exists(filePathForUserDetails)){
            string loadJson = File.ReadAllText(filePathForUserDetails); 
            UserDetails[] loadUserData = JsonHelper.FromJson<UserDetails>(loadJson);
            userDetails = new List<UserDetails>(loadUserData);
        }

    }
    public void SignUp(string _firstName,string _lastName,string _emailID,string _password)
    {
        UserDetails user =  new UserDetails(_firstName,_lastName,_emailID,_password);
        userDetails.Add(user);

        string jsonToSave = JsonHelper.ToJson(userDetails.ToArray());

        Debug.Log(jsonToSave);
       
        File.WriteAllText(filePathForUserDetails , jsonToSave);

    }

    //Check email email ID exist while login
    public bool CheckingLogEmail(){
        if(File.Exists(filePathForUserDetails)){
        foreach(UserDetails user in userDetails){
            if(user.emailID == LoginSignUpUI.Instance.loginEmail.text)
            {
                if(user.password == LoginSignUpUI.Instance.loginPassword.text){
                    return true;
                }
            }
         }
        }

        return false;
    }
    //Check email email ID exist while login
    public bool CheckingSignUpEmail(){
        if(File.Exists(filePathForUserDetails)){
        foreach(UserDetails user in userDetails){
            if(user.emailID == LoginSignUpUI.Instance.signUpEmail.text)
            {
                    return false;
            }
         }
        }
        return true;
    }
}
