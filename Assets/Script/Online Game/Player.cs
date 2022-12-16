using Microsoft.Unity.VisualStudio.Editor;
using Mirror;
using PlayFab.AuthenticationModels;
using PlayFab.MultiplayerModels;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Player : NetworkBehaviour
{
    [Header("Main")]
    [SyncVar]
    GameChoices selectedChoice;
    [SyncVar] GameChoices serveChoice;
    [SyncVar] GameChoices clientChoice;
    [SerializeField] Image serverChoice_Img , clientChoice_Img;
    [SerializeField] Sprite rock;
    [SerializeField] Sprite paper;
    [SerializeField] Sprite scissor;
    [SerializeField]
    private GameObject winLoseInfo;
    public Text winLoseText;
    private string winLoseString;
    private bool clientSelected = false , serverselected = false;
    bool state = false;
    [SerializeField]
    private Text player1_ScoreText, player2_scoreText;
    private int player1_Score = 0, player2_Score = 0;
    [ClientCallback]
    private void Awake()
    {
       
    }
    private void Update()
    {
     
        if(serverselected && clientSelected)
        {
            PlayerMadeChoice();
            DetermineWhowin();
            clientSelected = false;
            serverselected = false;
        }
    }
    public void Choice()
    {
       state = true;    
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;


        switch (choiceName)
        {
            case "Rock":
                selectedChoice = GameChoices.ROCK;
                //gameChoice = selectedChoice;
                Debug.Log("Player1 " + choiceName);

                break;
            case "Paper":
                selectedChoice = GameChoices.PAPER;
                //gameChoice= selectedChoice;
                Debug.Log("Player1 " + choiceName);

                break;
            case "Scissors":
                selectedChoice = GameChoices.SCISSORS;
                //gameChoice = selectedChoice;
                Debug.Log("Player1 " + choiceName);

                break;
        }
        
        if (isClient)
        {
            Clientsend(selectedChoice);
           
        }
        if(isServer)
        {
            Serversend(selectedChoice);
         
        }


    }
 
    [Command]
    public void Clientsend( GameChoices Sync)
    {
        selectedChoice = Sync;
        clientChoice = Sync;
        ClientChoiceImageOption();
        print("Cmd choice" + Sync);
        Client(Sync);
        clientSelected = true;
      
    }

    [TargetRpc]
    private void Client( GameChoices Sync)
    {
        selectedChoice = Sync;
        clientChoice = Sync;
        ClientChoiceImageOption();
        print("client TargetRpc");
        clientSelected = true;
    }


    [ClientRpc]
    public void Serversend(GameChoices Sync)
    {
        selectedChoice = Sync;
        serveChoice = Sync;
        ServerChoiceImageOption();
        print("rpc choice" + Sync);
        serverselected = true;
    }


    // Server Image Option
    private void ClientChoiceImageOption()
    {
        if (clientChoice == GameChoices.ROCK)
        {
            clientChoice_Img.sprite = rock;
        }else if (clientChoice == GameChoices.PAPER)
        {
            clientChoice_Img.sprite = paper;
        }else if (clientChoice == GameChoices.SCISSORS)
        {
            clientChoice_Img.sprite = scissor;
        }
    }


    //Client Image Option
    private void ServerChoiceImageOption()
    {
        if(serveChoice == GameChoices.ROCK)
        {
            serverChoice_Img.sprite = rock;
        }
        else if (serveChoice == GameChoices.PAPER)
        {
            serverChoice_Img.sprite = paper;
        }else if (serveChoice == GameChoices.SCISSORS)
        {
            serverChoice_Img.sprite = scissor;
        }
    }

    //DetermindWhowin
    private void DetermineWhowin()
    {
        if (serveChoice == clientChoice)
        {
            print("It's Draw !");
            winLoseString = "It'Draw !";
        }
        if (serveChoice == GameChoices.ROCK && clientChoice == GameChoices.PAPER)
        {
            print("client win");
            winLoseString = "client win";
            Invoke("Player2Score", 1f);
        }
        if(clientChoice == GameChoices.ROCK && serveChoice == GameChoices.PAPER)
        {
            print("server win");
            winLoseString = "server win";
            Invoke("Player1Score", 1f);
        }
        if(serveChoice == GameChoices.ROCK && clientChoice == GameChoices.SCISSORS)
        {
            print("server win");
            winLoseString = "server win";
            Invoke("Player1Score", 1f);
        }
        if (clientChoice == GameChoices.ROCK && serveChoice == GameChoices.SCISSORS)
        {
            print("client win");
            winLoseString = "client win";
            Invoke("Player2Score", 1f);
        }
        if (clientChoice == GameChoices.PAPER && serveChoice == GameChoices.SCISSORS)
        {
            print("server win");
            winLoseString = "server win";
            Invoke("Player1Score", 1f);
        }
        if (clientChoice == GameChoices.SCISSORS && serveChoice == GameChoices.PAPER)
        {
            print("client win");
            winLoseString = "client win";
            Invoke("Player2Score", 1f);
        }
        winLoseText.text = winLoseString;
        StartCoroutine(DisplayWinnerAndRestart());
    }

    // Animation Controller 
    [SerializeField]
    private Animator playerHandeAnimation, playerchoiceAnimation;
    public void ResetAnimation()
    {
        playerHandeAnimation.Play("ShowHand");
        playerchoiceAnimation.Play("RemoveChoice");
    }

    public void PlayerMadeChoice()
    {
        playerHandeAnimation.Play("RemoveHand");
        playerchoiceAnimation.Play("ShowChoice");
    }

    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(0.5f);
        //selected1.SetActive(false);
        //selected2.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        //info_txt.gameObject.SetActive(true);
        winLoseInfo.SetActive(true);

        yield return new WaitForSeconds(1f);
        //info_txt.gameObject.SetActive(false);
        winLoseInfo.SetActive(false);
        ResetAnimation();
      
    }

    private void Player1Score()
    {
        player1_Score++;
        player1_ScoreText.text = player1_Score.ToString();
    }
    private void Player2Score()
    {
        player2_Score++;
        player2_scoreText.text = player2_Score.ToString(); 
    }



}//  end of class
