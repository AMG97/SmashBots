using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Online_Player : MonoBehaviour
{
    PhotonView view;
    FixedJoystick joystick;
    Rigidbody rb;

    [SerializeField]
    float speed;

    [SerializeField]
    float max_angle, rotation_wheel;

    [SerializeField]
    Transform FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel;

    public Camera_Follow cam;

    int add = 0;


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        joystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
        rb = GetComponent<Rigidbody>();

        cam = GameObject.Find("Camera").GetComponent<Camera_Follow>();

        if (transform.eulerAngles.y == 180)
        {
            add = 180;
        }

        if (view.IsMine)
        {
            cam.robot = gameObject;
            cam.StartFollow();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            float x = joystick.Vertical;
            float z = joystick.Horizontal;


            if (x != 0 || z != 0)
            {
                float hipo = Mathf.Sqrt(x * x + z * z);
                float dif = 1.0f / hipo;

                x = x * dif;
                z = z * dif;

                float rot_y = Mathf.Atan2(z, x) * 180 / Mathf.PI;
                var qr = Quaternion.Euler(0, rot_y+add, 0);


                rb.transform.rotation = Quaternion.Lerp(transform.rotation, qr, max_angle * Time.deltaTime);


                rb.AddForce(-rb.transform.forward * speed*Time.deltaTime);

                FrontLeftWheel.Rotate(new Vector3(-rotation_wheel * Time.deltaTime, 0, 0));
                FrontRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
                BackRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
                BackLeftWheel.Rotate(new Vector3(-rotation_wheel * Time.deltaTime, 0, 0));
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }
}
