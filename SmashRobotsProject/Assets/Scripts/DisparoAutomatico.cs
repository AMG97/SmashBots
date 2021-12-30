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

    public static bool start = false;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            t += Time.deltaTime;
            if (t >= time_max)
            {
                t = 0;
                s.Shoot_Proyectile();
            }
        }
    }
}
