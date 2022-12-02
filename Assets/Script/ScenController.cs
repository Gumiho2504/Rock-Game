using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenController : MonoBehaviour
{
    public void MenuScreen()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayWithOtherScreen()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayWithComputerScreen()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayWithFriendScreen()
    {
        SceneManager.LoadScene(3);
    }
}
