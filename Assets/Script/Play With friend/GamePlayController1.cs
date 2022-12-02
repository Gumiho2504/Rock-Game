using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*public enum GameChoices1
{
    NONE,
    ROCK,
    PAPER,
    SCISSORS
}*/
public class GamePlayController1 : MonoBehaviour
{
    [SerializeField]
    private Sprite rock_Sprite, paper_Sprite, scissors_Sprite;
    [SerializeField]
    private Image player1Choice_Img, player2Choice_Img;
    public GameObject /*info_txt*/ pauseScreen /*winLoseScreen*/;
    [SerializeField]
    /*private Text inforText, player1_ScoreText, player2_scoreText, winLose_text;*/
    /* private int player1_Score = 0, player2_Score = 0;*/
    private GameChoices player1_Choice = GameChoices.NONE, player2_Choice = GameChoices.NONE;
   /* private AnimationController1 animationController1;*/
    private ScenController scenController;
    void Start()
    {

    }
    private void Update()
    {
      
    }


    private void Awake()
    {
        /*animationController1 = GetComponent<AnimationController1>();*/
    }

    // player Choice ANd opponent choice and determind 
    public void SetChoices_Player1(GameChoices gameChoices)
    {
        switch (gameChoices)
        {
            case GameChoices.ROCK:
                player1Choice_Img.sprite = rock_Sprite;
                player1_Choice = GameChoices.ROCK;
                break;
            case GameChoices.PAPER:
                player1Choice_Img.sprite = paper_Sprite;
                player1_Choice = GameChoices.PAPER;
                break;

            case GameChoices.SCISSORS:
                player1Choice_Img.sprite = scissors_Sprite;
                player1_Choice = GameChoices.SCISSORS;
                break;
        }// switch
        /* SetOpponentChoice();*/
       
       
    }
  /*  public void SetChoices(GameChoices gameChoices,Image image,GameChoices player)
    {
        player = GameChoices.NONE;
        switch (gameChoices)
        {
            case GameChoices.ROCK:
               image.sprite = rock_Sprite;
               player = GameChoices.ROCK;
                break;
            case GameChoices.PAPER:
               image.sprite = paper_Sprite;
                player = GameChoices.PAPER;
                break;

            case GameChoices.SCISSORS:
                image.sprite = scissors_Sprite;
                player = GameChoices.SCISSORS;
                break;
        }// switch
        DetermineWinner();
    }*/

    public void SetChoices_Player2(GameChoices gameChoices)
    {
        switch (gameChoices)
        {
            case GameChoices.ROCK:
                player2Choice_Img.sprite = rock_Sprite;
                player2_Choice = GameChoices.ROCK;
                break;
            case GameChoices.PAPER:
                player2Choice_Img.sprite = paper_Sprite;
                player2_Choice = GameChoices.PAPER;
                break;

            case GameChoices.SCISSORS:
                player2Choice_Img.sprite = scissors_Sprite;
                player2_Choice = GameChoices.SCISSORS;
                break;
        }// switch
        /* SetOpponentChoice();*/
      
    }

    // Opponent Choice Random
    /* void SetOpponentChoice()
     {
         int random = Random.Range(0, 3);
         switch (random)
         {
             case 0:
                 player2Choice_Img.sprite = rock_Sprite;
                 player2_Choice = GameChoices.ROCK;
                 break;
             case 1:
                 player2Choice_Img.sprite = paper_Sprite;
                 player2_Choice = GameChoices.PAPER;
                 break;
             case 2:
                 player2Choice_Img.sprite = scissors_Sprite;
                 player2_Choice = GameChoices.SCISSORS;
                 break;
         }
     }*/

    // Compute Score 
   /* private void PlayerWinOrLose()
    {
        if (player1_Score == 3)
        {
            Time.timeScale = 0;
            winLose_text.text = "VICTORY";
            winLoseScreen.gameObject.SetActive(true);

        }
        if (player2_Score == 3)
        {
            Time.timeScale = 0;
            winLose_text.text = "DEFEAT";
            winLoseScreen.gameObject.SetActive(true);

        }
    }*/
    // Compute Who win 
   /* void DetermineWinner()
    {
        if (player1_Choice == player2_Choice)
        {
            // draw
            inforText.text = "It's a Draw !";
            StartCoroutine(DisplayWinnerAndRestart());
            return;
        }

        // papper vs rock
        if (player1_Choice == GameChoices.PAPER && player2_Choice == GameChoices.ROCK)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player1Score", 1f);

            return;
        }

        if (player2_Choice == GameChoices.PAPER && player1_Choice == GameChoices.ROCK)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;
        }

        // rock vs scissor
        if (player1_Choice == GameChoices.ROCK && player2_Choice == GameChoices.SCISSORS)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());

            Invoke("Player1Score", 1f);
            return;
        }

        if (player2_Choice == GameChoices.ROCK && player1_Choice == GameChoices.SCISSORS)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;
        }

        // scissor vs paper
        if (player1_Choice == GameChoices.SCISSORS && player2_Choice == GameChoices.PAPER)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player1Score", 1f);

            return;
        }

        if (player2_Choice == GameChoices.SCISSORS && player1_Choice == GameChoices.PAPER)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("Player2Score", 1f);

            return;

        }


    }
    // Game start again
    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(1f);
        info_txt.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        info_txt.gameObject.SetActive(false);
        animationController1.ResetAnimation();
    }
    private void Player1Score()
    {
        player1_Score++;
        player1_ScoreText.text = player1_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);

    }
    private void Player2Score()
    {
        player2_Score++;
        player2_scoreText.text = player2_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);
    }*/

    // Pause Game
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    // Continue Game
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    // Replay Game
    public void ReplayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
// end of class
