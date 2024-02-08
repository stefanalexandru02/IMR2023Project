using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
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
