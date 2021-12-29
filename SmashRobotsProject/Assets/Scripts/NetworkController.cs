using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement; // ESTO ES SOLO PARA SI QUEREMOS CAMBIAR DE ESCENAS
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;


    /// ///////////////////////
    /// CONECTARSE A PHOTON ///
    ///////////////////////////
    void Start()
    {
        if(!PhotonNetwork.IsConnected)
            if (PhotonNetwork.ConnectUsingSettings())
                Debug.Log("Conectado");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {

    }

    /// ///////////////////////
    /// CREAR SALAS Y UNIRSE //
    ///////////////////////////

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);

    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Conectado a Sala");
        PhotonNetwork.LoadLevel("Multiplayer"); // ESTE GAME ES EL NOMBRE DE LA ESCENA QUE HAY QUE CARGAR
    }



}
