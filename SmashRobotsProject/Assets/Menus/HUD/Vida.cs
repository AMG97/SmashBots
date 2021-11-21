using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Image barra_vida;

    float vida, vida_max = 100;
    [SerializeField]
    float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        vida = vida_max;
        velocidad *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        barra_vida.fillAmount = Mathf.Lerp(barra_vida.fillAmount, (vida / vida_max), velocidad);
        barra_vida.color = Color.Lerp(Color.red, Color.green, (vida / vida_max));


    }

    public void Daño(float daño)
    {
        if (vida > 0)
        {
            vida -= daño;
            if (vida <= 0)
                Debug.Log("Muerto"); //Aqui hacer algo luego de saltar la pantalla de derrota
        }
    }
}
