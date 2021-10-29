using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField]
    GameObject robot;
    [SerializeField]
    Vector3 posicionRelativa;
    // Start is called before the first frame update
    void Start()
    {
        if(posicionRelativa.Equals(new Vector3(0,0,0)))
            posicionRelativa = transform.position - robot.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = robot.transform.position + posicionRelativa;
    }
}
