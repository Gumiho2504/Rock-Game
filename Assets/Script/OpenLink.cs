using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenLink : MonoBehaviour
{
    [SerializeField]
    private Text language_Text;
    private string[] language = new string[] { "ENGLISH", "CHINESE", "KHMER" };
    private int n = 0;
    
   public void Openfacebook()
    {
        Application.OpenURL("https://web.facebook.com/heam.chanraksmey/");
    }
    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/i/flow/login");
    }
    public void OpenTiktok()
    {
        Application.OpenURL("https://www.tiktok.com/@satavat");
    }
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/t.r_e.y/?igshid=ZDdkNTZiNTM%3D&fbclid=IwAR3-TTC7xqkvwDGs23-bD2JRNt-XIWivs2DUE1ZwZXa1flFRFlRahEA5vZ8");
    }

    public void Go() 
    {
        n++;
        if( n > 2)
        {
            n=0;
        }
        language_Text.text = language[n];

    }
    public void Back()
    { 
        n--;
        if (n < 0)
        {
            n = 2;
        }
      
        language_Text.text = language[n];
    }

}
