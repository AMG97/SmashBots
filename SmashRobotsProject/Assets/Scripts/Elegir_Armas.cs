using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elegir_Armas : MonoBehaviour
{
    GameObject[] armas = new GameObject[2];

    [SerializeField]
    Transform arma_izquierda_pos, arma_derecha_pos, arma_detras_pos;

    [SerializeField]
    float scale;

    [SerializeField]
    Color activado, desactivado;

    [SerializeField]
    Button b_mina, b_taser, b_laser, b_pistola, b_lanzallamas;

    [SerializeField]
    GameObject o_mina, o_taser, o_laser, o_pistola, o_lanzallamas;

    private void Start()
    {
        b_mina.onClick.AddListener(delegate { Arma(o_mina,b_mina); });
        b_taser.onClick.AddListener(delegate { Arma(o_taser, b_taser); });
        b_laser.onClick.AddListener(delegate { Arma(o_laser, b_laser); });
        b_pistola.onClick.AddListener(delegate { Arma(o_pistola, b_pistola); });
        b_lanzallamas.onClick.AddListener(delegate { Arma(o_lanzallamas, b_lanzallamas); });

    }

    public void Arma(GameObject a,Button b)
    {
        Image i = b.GetComponent<Image>();

        ColorBlock cb = b.colors;
     
        if (armas[0]!=null && armas[0].name.Contains(a.name))
        {
            Destroy(armas[0]);
            armas[0] = null;
            cb.normalColor = desactivado;
            cb.selectedColor = desactivado;
        }
        else if(armas[1] != null && armas[1].name.Contains( a.name))
        {
            Destroy(armas[1]);
            armas[1] = null;
            cb.normalColor = desactivado;
            cb.selectedColor = desactivado;
        }
        else if (armas[0] == null)
        {

            if (a.name == "LanzaMinas")
            {
                armas[0] = Instantiate(a, arma_detras_pos);
            }
            else
            {
                armas[0] = Instantiate(a, arma_derecha_pos);
            }
            armas[0].layer = 5;
            armas[0].transform.localScale = Vector3.Scale(armas[0].transform.localScale, new Vector3(scale, scale, scale));
            cb.normalColor = activado;
            cb.selectedColor = activado;
        }
        else if (armas[1] == null)
        {

            if (a.name == "LanzaMinas")
            {
                armas[1] = Instantiate(a, arma_detras_pos);
            }
            else
            {
                armas[1] = Instantiate(a, arma_izquierda_pos);
            }
            armas[1].layer = 5;
            armas[1].transform.localScale = Vector3.Scale(armas[1].transform.localScale, new Vector3(scale, scale, scale));
            cb.normalColor = activado;
            cb.selectedColor = activado;
        }
        b.colors = cb;
    }

    public GameObject GetArma(int i)
    {
        return armas[i];
    }
}
