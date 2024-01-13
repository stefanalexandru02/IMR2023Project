using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

public class VoiceNetworkingManager : MonoBehaviour
{
    [SerializeField] private Text connectionStatusText;
    [SerializeField] private Text roomStatusText;
    [SerializeField] private Text roomDebugStatusText;
    
    protected UnityVoiceClient voiceConnection;
    private ConnectAndJoin connectAndJoin;

    [SerializeField] private GameObject Room1Checkpoint;
    [SerializeField] private GameObject Room2Checkpoint;
    [SerializeField] private GameObject Room3Checkpoint;
    [SerializeField] private GameObject Room4Checkpoint;
    [SerializeField] private GameObject Room5Checkpoint;
    [SerializeField] private GameObject Room6Checkpoint;

    private void Start()
    {
        this.connectAndJoin = this.GetComponent<ConnectAndJoin>();
        this.voiceConnection = this.GetComponent<UnityVoiceClient>();
    }

    protected virtual void Update()
    {
        this.connectionStatusText.text = this.voiceConnection.Client.State.ToString();
        this.roomStatusText.text = this.connectAndJoin.RandomRoom ? string.Empty : this.connectAndJoin.RoomName;

        AllocateRoom();
    }

    private int AllocateRoom()
    {
        double distance1 = GetDistanceToRoom(Room1Checkpoint);
        double distance2 = GetDistanceToRoom(Room2Checkpoint);
        double distance3 = GetDistanceToRoom(Room3Checkpoint);
        double distance4 = GetDistanceToRoom(Room4Checkpoint);
        double distance5 = GetDistanceToRoom(Room5Checkpoint);
        double distance6 = GetDistanceToRoom(Room6Checkpoint);

        List<(int, double)> rooms = new List<(int, double)>();
        rooms.Add((1, distance1));
        rooms.Add((2, distance2));
        rooms.Add((3, distance3));
        rooms.Add((4, distance4));
        rooms.Add((5, distance5));
        rooms.Add((6, distance6));

        rooms = rooms.OrderBy(x => x.Item2).ToList();

        // roomStatusText.text = $"Distance till Room1: {GetDistanceToRoom(Room1Checkpoint).ToString()}\n" +
        //                       $"Distance till Room2: {GetDistanceToRoom(Room2Checkpoint).ToString()}\n" +
        //                       $"Distance till Room3: {GetDistanceToRoom(Room3Checkpoint).ToString()}\n" +
        //                       $"Distance till Room4: {GetDistanceToRoom(Room4Checkpoint).ToString()}\n" +
        //                       $"Distance till Room5: {GetDistanceToRoom(Room5Checkpoint).ToString()}\n" +
        //                       $"Distance till Room6: {GetDistanceToRoom(Room6Checkpoint).ToString()}\n";

        roomDebugStatusText.text = $"Room{rooms.First().Item1} - Distance: {rooms.First().Item2}";

        int assignedRoom = rooms.First().Item1;
        double assignedRoomDistance = rooms.First().Item2;

        if (assignedRoom == 2 && assignedRoomDistance > 3)
        {
            return 0;
        }

        if (assignedRoom == 4 && assignedRoomDistance > 4)
        {
            return 0;
        }
        
        JoinOrCreateRoom($"ROOM_{assignedRoom}");
        
        return rooms.First().Item1;
    }

    private double GetDistanceToRoom(GameObject room)
    {
        Vector3 difference = new Vector3(
            room.transform.position.x - Camera.main.transform.position.x,
            room.transform.position.y - Camera.main.transform.position.y,
            room.transform.position.z - Camera.main.transform.position.z);
        
        double distance = Math.Sqrt(
            Math.Pow(difference.x, 2f) +
            // Math.Pow(difference.y, 2f)
            Math.Pow(difference.z, 2f)
        );

        return distance;
    }
    
    private void JoinOrCreateRoom(string roomName)
    {
        if(this.connectAndJoin.RoomName == roomName) return;
        
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
