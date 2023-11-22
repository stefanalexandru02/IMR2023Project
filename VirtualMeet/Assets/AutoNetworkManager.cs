using Unity.Netcode;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

public class AutoNetworkManager : NetworkManager
{
    public void Start()
    {
        if (SystemInfo.graphicsDeviceName == null || SystemInfo.graphicsDeviceName == "Null Device")
        {
            Debug.Log("Starting as server");
            Singleton.StartServer();
            Debug.Log("Started as server");
        }
        else
        {
            Debug.Log(SystemInfo.graphicsDeviceName);
            Debug.Log("Starting client");
            Singleton.StartClient();
        }
    }
}