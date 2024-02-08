using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] private TMP_InputField  TextField;
    public static string GameName { get; set; }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LobbyScene");
    }

    public void SetGameName(string name)
    {
        GameName = name;
    }
}
