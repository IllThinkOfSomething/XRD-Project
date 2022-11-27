using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectNetworkGrabbing : MonoBehaviour, IPunOwnershipCallbacks
{
    private PhotonView ownership_PhotonView;
    private Rigidbody _rigidbody;
    private bool isBeingUsed = false;
    
    private void Awake()
    {
        ownership_PhotonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Control physics of the item
        if (isBeingUsed)
        {
            _rigidbody.isKinematic = true;
            gameObject.layer = 9;
        }
        else
        {
            _rigidbody.isKinematic = false; 
            gameObject.layer = 8;
        }
    }

    //Enable / Disable Gravity for network items
    [PunRPC]
    private void NetworkItemGrabStart()
    {
        isBeingUsed = true;
    }
    
    [PunRPC]
    private void NetworkItemGrabEnd()
    {
        isBeingUsed = false;

    }

    private void TransferItemOwnership()
    {
        ownership_PhotonView.RequestOwnership();
    }

    public void OnSelectGrab()
    {
        Debug.Log("Grabbed!");
        ownership_PhotonView.RPC("NetworkItemGrabStart", RpcTarget.AllBuffered);
        if (ownership_PhotonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Ownership is not needed");
        }
        else
        {
            TransferItemOwnership();
        }
    }

    public void OnSelectDrop()
    {
        Debug.Log("Dropped!");
        ownership_PhotonView.RPC("NetworkItemGrabEnd", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        //To prevent multiple ownerships of items while using only 1.
        if (targetView != ownership_PhotonView)
        {
            return;
        }
        
        Debug.Log($"Ownership Requested for: {targetView.name} from {requestingPlayer.NickName}");
        ownership_PhotonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log($"Ownership Trasfered for: {targetView.name} from {previousOwner.NickName}");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        //
    }
}
