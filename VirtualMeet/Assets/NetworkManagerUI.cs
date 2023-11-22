using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartServer();
        }));
        clientButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartClient();
        }));
        hostButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartHost();
        }));
    }
}
