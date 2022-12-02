using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class AnimationController1 : MonoBehaviour
{
    [SerializeField]
    private Animator playerChoiceHanderAnimation, choiceAnimation;
    public void ResetAnimation()
    {
        playerChoiceHanderAnimation.Play("ShowHand");
        choiceAnimation.Play("RemoveChoice");    
    }

    public void PlayerMadeChoice()
    {
        playerChoiceHanderAnimation.Play("RemoveHand");
        choiceAnimation.Play("ShowChoice");
    }
}
// end of class

