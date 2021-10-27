using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Movement : MonoBehaviour
{
    FixedJoystick joystick;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speed;

    [SerializeField]
    float max_angle;

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
            Debug.Log("Antes X " + x);
            Debug.Log("Z" + z);

            x = x * dif;
            z = z * dif;

            Debug.Log("Despues X " + x);
            Debug.Log("Z" + z);

            if (Mathf.Abs(x-previous_x)>max_angle)
            {
                if (x > previous_x)
                    x = previous_x + max_angle;
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
            vel = -rb.transform.forward * speed;
            vel -= new Vector3(0, rb.velocity.y, 0);
            rb.velocity = vel;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        previous_x = x;
        previous_z = z;
    }
}
