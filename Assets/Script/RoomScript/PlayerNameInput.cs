using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.ComponentModel;

namespace Assets.Script
{
    public class PlayerNameInput : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private InputField Name;
        [SerializeField] private Button continueButton;

        public static string DisplayName { get; private set; }
        private const string PlayerPrefsNameKey = "PlayerName";



        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }
            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            Name.text = defaultName;
            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        { 
            DisplayName = Name.text;
            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }

    }
}