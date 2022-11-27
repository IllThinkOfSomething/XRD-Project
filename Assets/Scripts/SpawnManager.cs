using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject GenericVRPlayerPrefab;

    public Vector3 spawnPosition;
    public Vector3 spawnPosition2;
    public int rotationX, rotationY, rotationZ;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
                // Instantiate the player
                PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
            else
                PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition2, Quaternion.Euler(rotationX, rotationY, rotationZ));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
