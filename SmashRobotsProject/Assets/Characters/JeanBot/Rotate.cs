using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    Vector3 rotation;

    void Update()
    {
        gameObject.transform.Rotate(rotation * Time.deltaTime, Space.World);
    }
}
