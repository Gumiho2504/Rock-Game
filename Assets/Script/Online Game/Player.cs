using Mirror;
using PlayFab.AuthenticationModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
   void HandleMovement()
    {
        if (isLocalPlayer)
        {
            Vector3 movement;  
        }
    }
    private void Update()
    {
        HandleMovement();
    }
}
