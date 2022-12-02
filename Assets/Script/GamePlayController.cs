using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameChoices
{
    NONE,
    ROCK,
    PAPER,
    SCISSORS
}
public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private Sprite rock_Sprite, paper_Sprite, scissors_Sprite;
    [SerializeField]
    private Image playerChoice_Img, oponnentChoice_Img;
    public GameObject info_txt,pauseScreen,winLoseScreen;
    [SerializeField]
    private Text inforText,player_ScoreText,opponent_scoreText,winLose_text;
    private int player_Score = 0,opponent_Score = 0;
    private GameChoices player_Choice = GameChoices.NONE, opponent_Choice = GameChoices.NONE;
    private AnimationController animationController;
    private ScenController scenController;
    void Start()
    {
      
    }
    private void Update()
    {
       
    }
   
   
    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
    }

    // player Choice ANd opponent choice and determind 
    public void SetChoices(GameChoices gameChoices)
    {
        switch (gameChoices)
        {
            case GameChoices.ROCK:
                playerChoice_Img.sprite = rock_Sprite;
                player_Choice = GameChoices.ROCK;
                break;
            case GameChoices.PAPER:
                playerChoice_Img.sprite = paper_Sprite;
                player_Choice = GameChoices.PAPER;
                break;

            case GameChoices.SCISSORS:
                playerChoice_Img.sprite = scissors_Sprite;
                player_Choice = GameChoices.SCISSORS;
                break;
        }// switch
        SetOpponentChoice();
        DetermineWinner();
    }
    
    // Opponent Choice Random
    void SetOpponentChoice()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                oponnentChoice_Img.sprite = rock_Sprite;
                opponent_Choice = GameChoices.ROCK;
                break;
            case 1:
                oponnentChoice_Img.sprite = paper_Sprite;
                opponent_Choice = GameChoices.PAPER;
                break;
            case 2:
                oponnentChoice_Img.sprite = scissors_Sprite;
                opponent_Choice = GameChoices.SCISSORS;
                break;
        }
    }

    // Compute Score 
    private void PlayerWinOrLose()
    {
        if (player_Score == 3)
        {
            Time.timeScale = 0;
            winLose_text.text = "VICTORY";
            winLoseScreen.gameObject.SetActive(true);
        
        }
        if (opponent_Score == 3)
        {
            Time.timeScale = 0;
            winLose_text.text = "DEFEAT";
            winLoseScreen.gameObject.SetActive(true);
           
        }
    }
    // Compute Who win 
    void DetermineWinner()
    {
        if(player_Choice == opponent_Choice)
        {
            // draw
            inforText.text = "It's a Draw !";
            StartCoroutine(DisplayWinnerAndRestart());
            return;
        }

        // papper vs rock
        if(player_Choice == GameChoices.PAPER && opponent_Choice == GameChoices.ROCK)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("PlayerScore", 1f);
           
            return;
        }

        if (opponent_Choice == GameChoices.PAPER && player_Choice == GameChoices.ROCK)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("OpponentScore", 1f);

            return;
        }

        // rock vs scissor
        if (player_Choice == GameChoices.ROCK && opponent_Choice == GameChoices.SCISSORS)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());
          
            Invoke("PlayerScore", 1f);
            return;
        }

        if (opponent_Choice == GameChoices.ROCK && player_Choice == GameChoices.SCISSORS)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("OpponentScore", 1f);
            
            return;
        }

        // scissor vs paper
        if (player_Choice == GameChoices.SCISSORS && opponent_Choice == GameChoices.PAPER)
        {
            // player won
            inforText.text = "You Win !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("PlayerScore", 1f);
            
            return;
        }

        if (opponent_Choice == GameChoices.SCISSORS && player_Choice == GameChoices.PAPER)
        {
            // opponent won
            inforText.text = "You Lose !";
            StartCoroutine(DisplayWinnerAndRestart());
            Invoke("OpponentScore", 1f);
           
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
        animationController.ResetAnimation();

    }
    private void PlayerScore()
    {
        player_Score++;
        player_ScoreText.text = player_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);

    }
    private void OpponentScore()
    {  
        opponent_Score++; 
        opponent_scoreText.text = opponent_Score.ToString();
        Invoke("PlayerWinOrLose", 2.0f);
    }

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
