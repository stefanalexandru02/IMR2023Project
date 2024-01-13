using System.Collections;
using System.Collections.Generic;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

public class VoiceNetworkingManager : MonoBehaviour
{
    [SerializeField]
    private Text connectionStatusText;

    [SerializeField]
    private Text roomStatusText;
    
    protected UnityVoiceClient voiceConnection;
    private ConnectAndJoin connectAndJoin;

    [SerializeField] private GameObject Room1Checkpoint;
    [SerializeField] private GameObject Room2Checkpoint;
    [SerializeField] private GameObject Room3Checkpoint;
    [SerializeField] private GameObject Room4Checkpoint;
    [SerializeField] private GameObject Room5Checkpoint;

    private void Start()
    {
        this.connectAndJoin = this.GetComponent<ConnectAndJoin>();
        this.voiceConnection = this.GetComponent<UnityVoiceClient>();
    }

    protected virtual void Update()
    {
        this.connectionStatusText.text = this.voiceConnection.Client.State.ToString();
        this.roomStatusText.text = this.connectAndJoin.RandomRoom ? string.Empty : this.connectAndJoin.RoomName;
    }
    
    private void JoinOrCreateRoom(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            this.connectAndJoin.RoomName = string.Empty;
            this.connectAndJoin.RandomRoom = true;
        }
        else
        {
            this.connectAndJoin.RoomName = roomName.Trim();
            this.connectAndJoin.RandomRoom = false;
        }
        if (this.voiceConnection.Client.InRoom)
        {
            this.voiceConnection.Client.OpLeaveRoom(false);
        }
        else if (!this.voiceConnection.Client.IsConnected)
        {
            this.voiceConnection.ConnectUsingSettings();
        }
    }
}
