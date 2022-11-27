using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField PlayerNameInputField;
    
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #endregion

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // If input field has value set that value as name for user in the room.
    public void ConnectToPhotonServer()
    {
        if (PlayerNameInputField != null)
        {
            PhotonNetwork.NickName = PlayerNameInputField.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server with player name" + PhotonNetwork.NickName);
        //Load a scene
        PhotonNetwork.LoadLevel("Lobby");

    }


    #endregion

}
