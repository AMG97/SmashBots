using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagenFondo : MonoBehaviour
{
    const float ImageWidth = -2730.0f;
    const float ImageHeight = -610;

    public Vector2 Speed;  //Speed for moving the image on the screen

    Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        image.transform.Translate(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime, 0.0f);
        
        if (transform.position.x<ImageWidth)
        {
            transform.position = new Vector3(ImageWidth, transform.position.y, transform.position.z);
            Speed.x = -Speed.x;
        }
        else if(transform.position.x>0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            Speed.x = -Speed.x;
        }

        if (transform.position.y < ImageHeight)
        {
            transform.position = new Vector3(transform.position.x, ImageHeight, transform.position.z);
            Speed.y = -Speed.y;
        }
        else if(transform.position.y>0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Speed.y = -Speed.y;
        }
    }
}
