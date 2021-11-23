using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elegir_Armas : MonoBehaviour
{
    bool [] armas = new bool[2];
    string[] armas_nombre = new string[2];

    [SerializeField]
    Transform arma_izquierda_pos, arma_derecha_pos, arma_detras_pos;

    [SerializeField]
    float scale;
  
    public void Arma(GameObject a)
    {
        GameObject new_arma;

        if(!armas[0])
        {
            armas[0] = true;
            armas_nombre[0] = a.name;
            if(armas_nombre[0] == "LanzaMinas")
            {
                new_arma = Instantiate(a, arma_detras_pos);
            }
            else
            {
                new_arma = Instantiate(a, arma_izquierda_pos);
            }
            new_arma.layer = 5;
            new_arma.transform.localScale=Vector3.Scale(new_arma.transform.localScale, new Vector3(scale,scale,scale));
        }
        else if(!armas[1])
        {
            armas[1] = true;
            armas_nombre[1] = a.name;
            if(armas_nombre[1]!= "LanzaMinas")
            {
                new_arma = Instantiate(a, arma_derecha_pos);
            }
            else
            {
                new_arma = Instantiate(a, arma_detras_pos);
            }
            new_arma.layer = 5;
            new_arma.transform.localScale=Vector3.Scale(new_arma.transform.localScale, new Vector3(scale, scale, scale));
        }
    }
}
