using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class GameController2 : NetworkBehaviour
{
    [SerializeField]
    private Sprite rock_Sprite, paper_Sprite, scissors_Sprite;
    [SerializeField]
    private Image player1Choice_Img, player2Choice_Img;
    // public GameObject pauseScreen;
    private GameChoices player1_Choice = GameChoices.NONE, player2_Choice = GameChoices.NONE;

   
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
    }
 
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

    }

}   
