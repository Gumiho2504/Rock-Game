using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private GameObject signInDisplay = default;
    [SerializeField] private InputField userName = default;
    [SerializeField] private InputField password = default;
    [SerializeField] private InputField email = default;
    public static string SessionTicket;
    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = userName.text,
            Email = email.text,
            Password = password.text,
        }, result =>
        {
            SessionTicket = result.SessionTicket;
            signInDisplay.SetActive(false);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        }
        );
        
    }
    
    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = userName.text,
            Password = password.text,
        }, result =>
        {

            SessionTicket = result.SessionTicket;
            signInDisplay.SetActive(false);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        }
       );
    }
}
