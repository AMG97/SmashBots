using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot_Movement : MonoBehaviour
{
    FixedJoystick joystick;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speed;

    [SerializeField]
    float max_angle, rotation_wheel;

    [SerializeField]
    Transform FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel;


    [SerializeField]
    Vida vida;


    float previous_x = 0;
    float previous_z = 0;
    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = joystick.Vertical;
        float z = joystick.Horizontal;


        if (x != 0 || z != 0)
        {
            float hipo = Mathf.Sqrt(x * x + z * z);
            float dif = 1.0f / hipo;

            x = x * dif;
            z = z * dif;

            if (Mathf.Abs(x-previous_x)>max_angle)
            {
                if (x > previous_x)
                    x = previous_x + max_angle; //aqui poner algo de girar las ruedassssssssssssss
                else
                    x = previous_x - max_angle;
            }

            if (Mathf.Abs(z - previous_z) > max_angle)
            {
                if (z > previous_z)
                    z = previous_z + max_angle;
                else
                    z = previous_z - max_angle;
            }
            rb.transform.localEulerAngles = new Vector3(0, Mathf.Atan2(z, x) * 180 / Mathf.PI, 0);

            Vector3 vel = rb.velocity;
            /*vel = -rb.transform.forward * speed;
            vel.y = rb.velocity.y;
            rb.velocity = vel;*/

            rb.AddForce(-rb.transform.forward * speed);

            FrontLeftWheel.Rotate(new Vector3(- rotation_wheel * Time.deltaTime, 0, 0));
            FrontRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
            BackRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
            BackLeftWheel.Rotate(new Vector3(- rotation_wheel * Time.deltaTime, 0, 0));
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        previous_x = x;
        previous_z = z;
    }

    public void Daño ( float daño)
    {
        vida.Daño(daño);
    }
}
