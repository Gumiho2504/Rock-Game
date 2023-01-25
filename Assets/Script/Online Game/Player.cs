
using Mirror;


using System.Collections;
using Telepathy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Player : NetworkBehaviour
{
    RockNetworkManager networkManager;
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
    private GameObject winLoseInfo, winLoseScreen;
    public Text winLoseText;
    private string winLoseString;
    private bool clientSelected = false , serverselected = false;
    bool serverClientUnselected = true;
    [SerializeField]
    private Text player1_ScoreText, player2_scoreText, winLose_text, player1_WinScoreText, player2_WinScoreText;
    private int player1_Score = 0, player2_Score = 0;
    private float timeStart = 10;
    public Text timeText, timeheader;
    private bool timeActive = true, autoSelect1 = true, autoSelect2 = true ;
    [ClientCallback]
    private void Start()
    {
        //timeText.text = timeStart.ToString();
        

    }

    [ClientCallback]
    private void Update()
    {
        if (timeActive)
        {
            timeStart -= Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString();
        }

     
        // /client Server made choice
        if (serverselected && clientSelected)
        {
            PlayerMadeChoice();
            DetermineWhowin();
            clientSelected = false;
            serverselected = false;
        }

        // Client and Server Auto Choice
       
    }



    [ClientCallback]
    private void FixedUpdate()
    {

        if (timeStart <= 0 && serverClientUnselected)
        {
            serverClientUnselected = false;
            timeActive = false;
            int k = Random.Range(0, 3);
            switch (k)
            {
                case 0:
                    serveChoice = clientChoice = GameChoices.ROCK;
                    break;
                case 1:
                    serveChoice = clientChoice = GameChoices.PAPER;
                    break;
                case 2:
                    serveChoice = clientChoice = GameChoices.SCISSORS;
                    break;
            }
            ServerChoiceImageOption();
            ClientChoiceImageOption();
            PlayerMadeChoice();
            DetermineWhowin();


        }
        

        if (timeStart <= 0 && !serverselected && clientSelected)
        {
            timeActive = false;
            if(clientChoice == GameChoices.PAPER)
            {
                serveChoice = GameChoices.ROCK;
            }else if(clientChoice == GameChoices.SCISSORS)
            {
                serveChoice = GameChoices.PAPER;
            }else if(clientChoice == GameChoices.ROCK)
            {
                serveChoice = GameChoices.SCISSORS;
                
            }
            ServerChoiceImageOption();
            PlayerMadeChoice();
            DetermineWhowin();
            clientSelected = false;

        }


        if (timeStart <= 0 && !clientSelected && serverselected)
        {
            timeActive = false;
            if (serveChoice == GameChoices.PAPER)
            {
                clientChoice = GameChoices.ROCK;
            }
            else if (serveChoice == GameChoices.SCISSORS)
            {
                clientChoice = GameChoices.PAPER;
            }
            else if (serveChoice == GameChoices.ROCK)
            {
                clientChoice = GameChoices.SCISSORS;
            }
            ClientChoiceImageOption();
            PlayerMadeChoice();
            DetermineWhowin();
            serverselected = false;
        }
       
    }


    // server and client choice
    public void Choice()
    {
       //state = true;    
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
        autoSelect2 = false;
      
    }

    [TargetRpc]
    private void Client( GameChoices Sync)
    {
        selectedChoice = Sync;
        clientChoice = Sync;
        ClientChoiceImageOption();
        print("client TargetRpc");
        clientSelected = true;
        autoSelect2 = false;
    }


    [ClientRpc]
    public void Serversend(GameChoices Sync)
    {
        selectedChoice = Sync;
        serveChoice = Sync;
        ServerChoiceImageOption();
        print("rpc choice" + Sync);
        serverselected = true;
        autoSelect1 = false;
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


    // Caculate and then restart choice 
    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(0.5f);
        //selected1.SetActive(false);
        //selected2.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        winLoseInfo.SetActive(true);

        yield return new WaitForSeconds(1f);
        winLoseInfo.SetActive(false);
        ResetAnimation();
        timeStart = 10;
      
        timeActive = true;
        serverClientUnselected = true;

    }

     // Game Score
    private void Player1Score()
    {
        player1_Score++;
        player1_ScoreText.text = player1_Score.ToString();
        player1_WinScoreText.text = player1_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);
        print("score 1 : " + player1_Score);
    }
    private void Player2Score()
    {
        player2_Score++;
        player2_scoreText.text = player2_Score.ToString();
        player2_WinScoreText.text = player2_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);
        print("score 1 : " + player1_Score);
    }
    private void PlayerWinOrLose()
    {
        if (player1_Score == 2)
        {
            Time.timeScale = 0;
            winLose_text.text = "PLAYER IS WINNER";

            winLoseScreen.gameObject.SetActive(true);
            NetworkManager.singleton.StopHost();
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopServer();
        }
        if (player2_Score == 2)
        {
            Time.timeScale = 0;
            winLose_text.text = "PLAYER2 IS WINNER";
            winLoseScreen.gameObject.SetActive(true);
            NetworkManager.singleton.StopHost();
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopServer();
        }
    }
   
    public override void OnStopLocalPlayer()
    {
        base.OnStopLocalPlayer();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    

   
    public void HomeBtn()
    {
        if (isServer)
        {
            NetworkManager.singleton.StopHost();
            
        }
        else
        {
            NetworkManager.singleton.StopClient();
           
        }

        NetworkManager.singleton.ServerChangeScene("SampleScene");




    }



}//  end of class
