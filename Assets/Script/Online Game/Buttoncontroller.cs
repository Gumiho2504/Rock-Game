using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class Buttoncontroller : MonoBehaviour
{
   [SerializeField] private RockNetworkManager rockNetworkManager = null;
    public GameObject panel, sBtn, jBtn;
    bool Sever = false;

   
    public void StartBtn()
    {
            rockNetworkManager.StartHost();
            Sever = true;
       
       
        
        panel.SetActive(false);
        sBtn.SetActive(false);
        jBtn.SetActive(false);
        
       
    }

    
    public void JBtn()
    {         
        rockNetworkManager.StartClient();
        panel.SetActive(false);
        jBtn.SetActive(false);
        sBtn.SetActive(false);
    }

    [ServerCallback]
    public void HomeBtn()
    {
       
        rockNetworkManager.StopClient();
        print("btn click");
        
    }
}
