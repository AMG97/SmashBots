using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fin_Partida : MonoBehaviour
{

    public GameObject menu_fin;
    bool fin = false;

    [SerializeField]
    TMPro.TextMeshProUGUI titulo;


    [SerializeField]
    Menu_Sounds m_s;

    public void Terminar (int i)
    {
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


    public void Repetir()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void Salir()
    {
        //menu_pausa.SetActive(false);
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Seguir()
    {
        SceneManager.LoadScene("Game2", LoadSceneMode.Single);
    }
}
