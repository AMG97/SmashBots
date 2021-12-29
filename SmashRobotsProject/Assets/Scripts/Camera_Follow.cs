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
        else if (Mathf.Abs(robot.transform.rotation.eulerAngles.y) > 170)
        {
            posicionRelativa = new Vector3(posicionRelativa.x, posicionRelativa.y, posicionRelativa.z * -1);
            Vector3 rot = transform.rotation.eulerAngles;
            transform.eulerAngles=(new Vector3(rot.x, rot.y +180, rot.z));
        }
    }
}
