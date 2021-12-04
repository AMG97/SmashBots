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
    Shoot arma1, arma2;


    [SerializeField]
    Vida vida;

    [SerializeField]
    Energia energia;

    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
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
                var qr = Quaternion.Euler(0, rot_y, 0);


                rb.transform.rotation = Quaternion.Lerp(transform.rotation, qr, max_angle * Time.deltaTime);


                rb.AddForce(-rb.transform.forward * speed);

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

    public void Daño ( float daño)
    {
        vida.Daño(daño);
    }

    public void DispararArma1()
    {
        if(start && energia.Disparar(arma1.Get_Energy()) )
        {
            arma1.Shoot_Proyectile();
        }
        
    }

    public void DispararArma2()
    {
        if ( start && energia.Disparar(arma2.Get_Energy()))
        {
            arma2.Shoot_Proyectile();
        }
    }

    public void SetArma1(GameObject a)
    {
        arma1 = a.GetComponent<Shoot>();
    }

    public void SetArma2(GameObject a)
    {
        arma2 = a.GetComponent<Shoot>();
    }

    public void  SetVidaEnergia(Vida v, Energia e)
    {
        vida = v;
        energia = e;
    }

    public void StartPlayer()
    {
        start = true;
    }

    public float GetVida()
    {
        return vida.Get_Vida();
    }
}
