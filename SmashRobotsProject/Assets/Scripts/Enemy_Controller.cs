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

            if (vida <= 0)
            {
                Debug.Log("Muerto");
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
    }

    public void StartEnemy()
    {
        start = true;
    }


}
