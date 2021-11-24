using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energia : MonoBehaviour
{
    public Image barra_energia;

    float energia, energia_max = 100;
    [SerializeField]
    float velocidad, incremento;
    // Start is called before the first frame update
    void Start()
    {
        energia = 0;
        barra_energia.fillAmount = 0;
        barra_energia.color = Color.grey;
        velocidad *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(energia < energia_max)
        {
            energia += incremento * Time.deltaTime;
            if (energia > energia_max)
                energia = energia_max;
        }
       

        barra_energia.fillAmount = Mathf.Lerp(barra_energia.fillAmount, (energia / energia_max), velocidad);
        barra_energia.color = Color.Lerp(Color.gray, Color.blue, (energia / energia_max));


    }

    public bool Disparar(float consumo_energia)
    {
        if(consumo_energia <= energia)
        {
            energia -= consumo_energia;
            return true;
        }

        return false;
    }
}
