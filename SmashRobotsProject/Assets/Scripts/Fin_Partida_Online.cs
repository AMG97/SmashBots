using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Fin_Partida_Online : MonoBehaviour
{
    [SerializeField]
    GameObject menu_fin;
    bool fin = false;

    [SerializeField]
    TMPro.TextMeshProUGUI titulo;
    [SerializeField]
    Menu_Sounds m_s;

    public void Terminar (int i)
    {
        Time.timeScale = 0;
        if(!fin)
        {
            menu_fin.SetActive(true);
            Time.timeScale = 0f;
            if (i > 0)
            {
                titulo.text = "GANASTe";
                m_s.PlaySound_Advance();
            }
            else
            {
                titulo.text = "PeRDISTe";
                m_s.PlaySound_Back();
            }
            fin = true;
        }
    }

    static public void Salir()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
