using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public Elegir_Armas armas;

    public void LoadGame()
    {
        Game_Controller.arma_1 = armas.GetArma(0).name;
        Game_Controller.arma_2 = armas.GetArma(1).name;
        SceneManager.LoadScene("Game2", LoadSceneMode.Single);
    }
}
