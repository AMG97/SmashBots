using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject robot;
    [SerializeField]
    Vector3 posicionRelativa;


    bool started = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if(started)
            transform.position = robot.transform.position + posicionRelativa;
    }

    public void StartFollow ()
    {
        started = true;
        if (posicionRelativa.Equals(new Vector3(0, 0, 0)))
            posicionRelativa = transform.position - robot.transform.position;
    }
}
