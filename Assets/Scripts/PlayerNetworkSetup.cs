using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Allows to access Photon View attached to gameObject
// When player is initiated this script gets executed. 
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXROriginGameObject;

    // Render only on local so it won't get in the way for the player
    public GameObject AvatarHeadObject;
    public GameObject AvatarBodyObject;

    // Start is called before the first frame update
    void Start()
    {
        // To avoid having multiple active XR 
        // This allows for everyone to have their own XR origin
        if (photonView.IsMine)
        {
            // The player is local
            // Create XR Origin
            LocalXROriginGameObject.SetActive(true);
            
            // Head layer is 6th
            SetLayerRecursively(AvatarHeadObject, 6);
            // Body layer is 7th
            SetLayerRecursively(AvatarBodyObject,  7 );
        }
        else
        {
            //Remote player
            // Do not create XR Origin
            LocalXROriginGameObject.SetActive(false);
            
            SetLayerRecursively(AvatarHeadObject, 0);
            SetLayerRecursively(AvatarBodyObject,  0 );
        }

        // Only for rooms with hand tracking.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null)
            return;

        foreach (Transform transform in go.GetComponentsInChildren<Transform>(true))
        {
            transform.gameObject.layer = layerNumber;
        }
    }
}
