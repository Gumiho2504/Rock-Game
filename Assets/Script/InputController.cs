using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    private AnimationController animationController;
    private GamePlayController gameplayController;
    private string playerChoice;
    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        gameplayController = GetComponent<GamePlayController>();
    }

    public void GetChoice()
    {
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        print("Player selected : " + choiceName);
        GameChoices selectedChoice = GameChoices.NONE;
        switch (choiceName)
        {
            case "Rock":
                selectedChoice = GameChoices.ROCK;
                break;
            case "Paper":
                selectedChoice = GameChoices.PAPER;
                break;
            case "Scissors":
                selectedChoice = GameChoices.SCISSORS;
                break;
        }
        gameplayController.SetChoices(selectedChoice);
        animationController.PlayerMadeChoice();
    }
}