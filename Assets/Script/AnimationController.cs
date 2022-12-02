using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerChoiceHanderAnimation, choiceAnimation;
    public void ResetAnimation()
    {
        playerChoiceHanderAnimation.Play("ShowHander");
        choiceAnimation.Play("RemoveChoice");
    }

    public void PlayerMadeChoice()
    {
        playerChoiceHanderAnimation.Play("RemoveHander");
        choiceAnimation.Play("ShowChoice");
    }
}
// end of class

