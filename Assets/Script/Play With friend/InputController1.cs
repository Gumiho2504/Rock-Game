using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using Unity.VisualScripting;
using System.Net;


public class InputController1 : MonoBehaviour
{
    public float timeStart = 20;
    public Text timeText ,timeheader;
    [SerializeField]
    private Image r,l,t,b;
    private bool timeActive = true, autoSelect1 = true ,autoSelect2 = true;
    private AnimationController1 animationController1;
    private GamePlayController1 gameplayController1;
    
    private GameChoices selectedChoice_player2 = GameChoices.NONE;
    private GameChoices selectedChoice_player1 = GameChoices.NONE;
    private bool player1_select = false, player2_select = false ;
    [SerializeField]
    private GameObject selected1, selected2;
    [SerializeField]
    private Text inforText, player1_ScoreText, player2_scoreText, winLose_text,player1_WinScoreText,player2_WinScoreText;
    private int player1_Score = 0, player2_Score = 0;
    public GameObject info_txt,winLoseScreen;
    private string choiceName1,choiceName2;

    private void Awake()
    {
        animationController1 = GetComponent<AnimationController1>();
        gameplayController1 = GetComponent<GamePlayController1>();
    }
    private void Start()
    {
        
        timeText.text = timeStart.ToString();
        timeText.color = Color.green;
        timeheader.color = Color.green;
        r.color = Color.green;
        l.color = Color.green;
        t.color = Color.green;
        b.color = Color.green;
      

    }

    private void Update()
    {
        if (timeActive)
        {
            timeStart -= Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString();
        }

        if (timeStart <= 20)
        {
            timeText.color = Color.green;
            timeheader.color = Color.green;
            r.color = Color.green;
            l.color = Color.green;
            t.color = Color.green;
            b.color = Color.green;
        }
        if (timeStart <= 10)
        {
            timeText.color = Color.yellow;
            timeheader.color = Color.yellow;
            r.color = Color.yellow;
            l.color = Color.yellow;
            t.color = Color.yellow;
            b.color = Color.yellow;
        }
        if (timeStart <= 5)
        {
            timeText.color = Color.red;
            timeheader.color = Color.red;
            r.color = Color.red;
            l.color = Color.red;
            t.color = Color.red;
            b.color = Color.red;
        }
        player2AutoSelect();
        player1AutoSelect();


    }
    private void FixedUpdate()
    {

       
   
    }
    public void player2AutoSelect()
    {
        if (timeStart < 0 && autoSelect2)
        {
            timeActive = false;
            int random2 = Random.Range(0, 3);

            switch (random2)
            {
                case 0:
                    selectedChoice_player2 = GameChoices.ROCK;
                    break;
                case 1:
                    selectedChoice_player2 = GameChoices.PAPER;
                    break;
                case 2:
                    selectedChoice_player2 = GameChoices.SCISSORS;
                    break;
            }
            print("Player 2 selected : " + random2);
            gameplayController1.SetChoices_Player2(selectedChoice_player2);
            animationController1.PlayerMadeChoice();

           
            autoSelect2 = false;
            if (autoSelect1 && autoSelect2)
            {
                DetermineWinner();
            }else if (player1_select)
            {
                player1_select = false;
                DetermineWinner();
            }
            else 
            {
                return;
            }
          
        } 
    }
    public void player1AutoSelect()
    {
        if (timeStart < 0 && autoSelect1)
        {
            timeActive = false;
            int random1 = Random.Range(0, 3);

            switch (random1)
            {
                case 0:
                    selectedChoice_player1 = GameChoices.ROCK;
                    break;
                case 1:
                    selectedChoice_player1 = GameChoices.PAPER;
                    break;
                case 2:
                    selectedChoice_player1 = GameChoices.SCISSORS;
                    break;
            }
            print("Player 1 selected : " + random1);
            gameplayController1.SetChoices_Player1(selectedChoice_player1);
            animationController1.PlayerMadeChoice();


            
            autoSelect1 = false;
            if (autoSelect1 && autoSelect2)
            {
                return;
            }else if (player2_select)
            {
                player2_select = false;
                DetermineWinner();
            }
            else {
                DetermineWinner();
            }
           
        }
       
    }


    public void Player1_GetChoice()
    {
        selected1.SetActive(true);
        player1_select = true;
        autoSelect1 = false;
        choiceName1 = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        print("Player 1 selected : " + choiceName1);

        switch (choiceName1)
        {
            case "Rock":
                selectedChoice_player1 = GameChoices.ROCK;
                break;
            case "Paper":
                selectedChoice_player1 = GameChoices.PAPER;
                break;
            case "Scissors":
                selectedChoice_player1 = GameChoices.SCISSORS;
                break;
        }
        gameplayController1.SetChoices_Player1(selectedChoice_player1);
        if (player2_select)
        {
            player2_select = false;
            player1_select = false;
            animationController1.PlayerMadeChoice();
            DetermineWinner();
        }
        
    }
        public void Player2_GetChoice()
    {
        autoSelect2 = false;
        selected2.SetActive(true);
        player2_select = true;
        choiceName2 = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        print("Player 2 selected : " + choiceName2);
        
        switch (choiceName2)
        {
            case "Rock":
                selectedChoice_player2 = GameChoices.ROCK;
                break;
            case "Paper":
                selectedChoice_player2 = GameChoices.PAPER;
                break;
            case "Scissors":
                selectedChoice_player2 = GameChoices.SCISSORS;
                break;
        }
        gameplayController1.SetChoices_Player2(selectedChoice_player2);
        if (player1_select)
        {         
            player1_select=false;
            player2_select = false;
             animationController1.PlayerMadeChoice();
             DetermineWinner();
        }
           
    }

    void DetermineWinner()
    {
        if (selectedChoice_player1 == selectedChoice_player2)
        {
            // draw
            inforText.text = "It's a Draw !";
            StartCoroutine(DisplayWinnerAndRestart());
            return;
        }

        // papper vs rock
        if (selectedChoice_player1 == GameChoices.PAPER && selectedChoice_player2 == GameChoices.ROCK)
        {
            // player won
            inforText.text = "Player1 Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player1Score", 1f);

            return;
        }

        if (selectedChoice_player2 == GameChoices.PAPER && selectedChoice_player1 == GameChoices.ROCK)
        {
            // opponent won
            inforText.text = "Player2 win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;
        }

        // rock vs scissor
        if (selectedChoice_player1 == GameChoices.ROCK && selectedChoice_player2 == GameChoices.SCISSORS)
        {
            // player won
            inforText.text = "Player1 Win !";
            StartCoroutine(DisplayWinnerAndRestart());

            Invoke("Player1Score", 1f);
            return;
        }

        if (selectedChoice_player2 == GameChoices.ROCK && selectedChoice_player1 == GameChoices.SCISSORS)
        {
            // opponent won
            inforText.text = "Player2 Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;
        }

        // scissor vs paper
        if (selectedChoice_player1 == GameChoices.SCISSORS && selectedChoice_player2 == GameChoices.PAPER)
        {
            // player won
            inforText.text = "Player1 Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player1Score", 1f);

            return;
        }

        if (selectedChoice_player2 == GameChoices.SCISSORS && selectedChoice_player1 == GameChoices.PAPER)
        {
            // opponent won
            inforText.text = "Player2 Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;

        }


    }
    // Game start again
    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(0.5f);
        selected1.SetActive(false);
        selected2.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        info_txt.gameObject.SetActive(true);
       

        yield return new WaitForSeconds(1f);
        info_txt.gameObject.SetActive(false);
        animationController1.ResetAnimation();
        timeStart = 20;
        timeActive = true;
        autoSelect1 = true;
        autoSelect2 = true;
    }
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
        print("score 2 : " + player2_Score);
    }
    private void PlayerWinOrLose()
    {
        if (player1_Score == 5)
        {
            Time.timeScale = 0;
            winLose_text.text = "PLAYER IS WINNER";
            winLoseScreen.gameObject.SetActive(true);

        }
        if (player2_Score == 5)
        {
            Time.timeScale = 0;
            winLose_text.text = "PLAYER2 IS WINNER";
            winLoseScreen.gameObject.SetActive(true);

        }
    }
    

}

