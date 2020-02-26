using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserDetails 
{
    public string firstName;
        public string lastName;
        public string emailID;
        public string password;

        public UserDetails(string _firstName,string _lastName,string _email,string _password)
        {
            this.firstName = _firstName;
            this.lastName = _lastName;
            this.emailID = _email;
            this.password = _password;
        }
}
