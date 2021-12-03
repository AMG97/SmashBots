using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField]
    float max_vida;
    float vida;

    [SerializeField]
    Slider slider;

    [SerializeField]
    Canvas canvas;

    Camera cam;

    [SerializeField]
    float velocidad;

    Fin_Partida fin;

    bool start = false;
    
    // Start is called before the first frame update
    void Start()
    {
        vida = max_vida;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            slider.value = Mathf.Lerp(slider.value, (vida / max_vida), velocidad);

            if (slider.value <= 0.01 && Time.timeScale > 0)
            {
                fin.Terminar(1);
            }
        }
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(cam.transform);
        canvas.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void Daño(float d)
    {
        vida -= d;
        //if (vida <= 0)
          //  fin.Terminar(1);
    }

    public void StartEnemy()
    {
        start = true;
    }

    public float Get_Vida()
    {
        return vida;
    }

    public void Set_Fin_Partida(Fin_Partida f)
    {
        fin = f;
    }

}
