using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoAutomatico : MonoBehaviour
{
    [SerializeField]
    Shoot s;

    [SerializeField]
    float time_max;

    float t=-3;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t>=time_max)
        {
            t = 0;
            s.Shoot_Proyectile();
        }
    }
}
