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

    public Elegir_Armas armas;


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
        if (armas.GetArma(0) != null)
            OnlineController.arma_1 = armas.GetArma(0).name;
        else
            OnlineController.arma_1 = "BBGun(Clone)";

        if (armas.GetArma(1) != null)
            OnlineController.arma_2 = armas.GetArma(1).name;
        else
            OnlineController.arma_2 = "Taser(Clone)";
        PhotonNetwork.CreateRoom(createInput.text);

    }

    public void JoinRoom()
    {
        if (armas.GetArma(0) != null)
            OnlineController.arma_1 = armas.GetArma(0).name;
        else
            OnlineController.arma_1 = "BBGun(Clone)";

        if (armas.GetArma(1) != null)
            OnlineController.arma_2 = armas.GetArma(1).name;
        else
            OnlineController.arma_2 = "Taser(Clone)";
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Conectado a Sala");
        PhotonNetwork.LoadLevel("Multiplayer"); // ESTE GAME ES EL NOMBRE DE LA ESCENA QUE HAY QUE CARGAR
    }



}
