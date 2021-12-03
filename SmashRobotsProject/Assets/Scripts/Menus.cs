using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public Elegir_Armas armas;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        if (armas.GetArma(0) != null)
            Game_Controller.arma_1 = armas.GetArma(0).name;
        else
            Game_Controller.arma_1 = "BBGun(Clone)";

        if (armas.GetArma(1) != null)
            Game_Controller.arma_2 = armas.GetArma(1).name;
        else
            Game_Controller.arma_2 = "Lanzallamas(Clone)";

        SceneManager.LoadScene("Game2", LoadSceneMode.Single);
    }
}
