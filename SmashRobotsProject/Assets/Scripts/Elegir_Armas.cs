using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elegir_Armas : MonoBehaviour
{
    GameObject[] armas = new GameObject[2];

    [SerializeField]
    Transform arma_izquierda_pos, arma_derecha_pos, arma_detras_pos;

    [SerializeField]
    float scale;

    public void Arma(GameObject a)
    {
        if (armas[0] == null)
        {

            if (a.name == "LanzaMinas")
            {
                armas[0] = Instantiate(a, arma_detras_pos);
            }
            else
            {
                armas[0] = Instantiate(a, arma_izquierda_pos);
            }
            armas[0].layer = 5;
            armas[0].transform.localScale = Vector3.Scale(armas[0].transform.localScale, new Vector3(scale, scale, scale));
        }
        else if (armas[1] == null)
        {

            if (a.name == "LanzaMinas")
            {
                armas[1] = Instantiate(a, arma_detras_pos);
            }
            else
            {
                armas[1] = Instantiate(a, arma_derecha_pos);
            }
            armas[1].layer = 5;
            armas[1].transform.localScale = Vector3.Scale(armas[1].transform.localScale, new Vector3(scale, scale, scale));
        }
    }

    public GameObject GetArma(int i)
    {
        return armas[i];
    }
}
