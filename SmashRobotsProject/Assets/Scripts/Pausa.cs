using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pausa : MonoBehaviour
{

    public GameObject menu_pausa;

    public void Pausar()
    {
        menu_pausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        menu_pausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        //menu_pausa.SetActive(false);
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
