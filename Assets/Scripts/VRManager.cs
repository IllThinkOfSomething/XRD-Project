using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class VRManager : MonoBehaviourPunCallbacks
{

    #region Photon Callbacks Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Notify when player joins the room.
        Debug.Log($"{newPlayer.NickName} joined to: {PhotonNetwork.CurrentRoom.Name}, Player count: {PhotonNetwork.CurrentRoom.PlayerCount}");
    }
    
    
    

    #endregion
}
