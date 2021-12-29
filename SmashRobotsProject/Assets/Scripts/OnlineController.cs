using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    public Vector3 pos1;
    public Vector3 pos2;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, pos1, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, pos2, Quaternion.Euler(0,180,0));
        }
    }
}
