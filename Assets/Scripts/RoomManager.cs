using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Oculus.Platform.Models;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI NumberOfPlayers_BeerPong;
    public TextMeshProUGUI NumberOfPlayers_Desert;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // To sync scenes to all users. 
        PhotonNetwork.AutomaticallySyncScene = true;

        // Join default lobby
        // Calls overwritten method in Photon Callback 
        PhotonNetwork.JoinLobby();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        //Join random room
        PhotonNetwork.JoinRandomRoom();
    }


    public void OnEnterButtonClicked_Desert()
    {
        mapType = MultiplayerMapConstants.MAP_TYPE_VALUE_DESERT;
        ExitGames.Client.Photon.Hashtable expectedRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerMapConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 0);
    }

    public void OnEnterButtonClicked_Beerpong()
    {
        mapType = MultiplayerMapConstants.MAP_TYPE_VALUE_BEERPONG;
        ExitGames.Client.Photon.Hashtable expectedRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerMapConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 0);
    }
    public void OnEnterButtonClicked_HandTrack()
    {
        mapType = MultiplayerMapConstants.MAP_TYPE_VALUE_HANDTRACK;
        ExitGames.Client.Photon.Hashtable expectedRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerMapConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 1);
    }

    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //If user fails to join random room
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"The Local player: {PhotonNetwork.NickName} joined to {PhotonNetwork.CurrentRoom.Name} " +
                  $"Player count {PhotonNetwork.CurrentRoom.PlayerCount}");

        //Specify to what room user joined.
        //Key -> map
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerMapConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerMapConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log($"Joined to room with map: {(string)mapType}");
                if ((string)mapType == MultiplayerMapConstants.MAP_TYPE_VALUE_BEERPONG)
                {
                    // Loads Beer pong placeholder scene
                    PhotonNetwork.LoadLevel("PongGameMultiplayer");
                    
                }
                else if ((string)mapType == MultiplayerMapConstants.MAP_TYPE_VALUE_DESERT)
                {
                    // Load desert
                    PhotonNetwork.LoadLevel("DesertScene");
                }
                else if ((string)mapType == MultiplayerMapConstants.MAP_TYPE_VALUE_HANDTRACK)
                {
                    // Load desert
                    PhotonNetwork.LoadLevel("PongGameHandTracking");
                }

            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Unity Log alert of player that joined the room.
        Debug.Log($"{newPlayer.NickName} joined to: {PhotonNetwork.CurrentRoom.Name}, Player count: {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Called for any update of the room-listing while in a Lobby (Master server)
        // Tracks when user joinned/left the room, updates UI in the lobby.
        if (roomList.Count == 0)
        {
            // There is no room at all
            NumberOfPlayers_BeerPong.text = 0 + " / " + 10;
            NumberOfPlayers_Desert.text = 0 + " / " + 10;
        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerMapConstants.MAP_TYPE_VALUE_BEERPONG))
            {
                //Update beerpong room player count
                Debug.Log($"Room: Beer Pong. Player count: {room.PlayerCount}");
                NumberOfPlayers_BeerPong.text = room.PlayerCount + " / " + 10;
            }
            else if (room.Name.Contains(MultiplayerMapConstants.MAP_TYPE_VALUE_DESERT))
            {
                //Update beerpong room player count
                Debug.Log($"Room: Desert. Player count: {room.PlayerCount}");
                NumberOfPlayers_Desert.text = room.PlayerCount + " / " + 10;
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby.");
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        var randomRoomName = "Room_" + mapType + Random.Range(0, 10000);
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 10 // Photon free offers max 20 people per application
        };

        string[] roomPropsInLobby = {"map"};
        //We'll have only one map for now:
        // 1. Beer pong scene = "beerpong multiplayer"
        // 2. Hand track
        // 3. Desert Scene = desert

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerMapConstants.MAP_TYPE_KEY, mapType } };
        
        //set room properties
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    #endregion
}
