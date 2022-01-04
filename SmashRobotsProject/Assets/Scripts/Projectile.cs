using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float max_time_alive, damage;
    [SerializeField]
    float speed;

    float time_alive;

    float time_colision = 0;

    int viewID = -1;


    public float rotacion=-10;
    public float velocidad=2;
    public float max_rotacion = 90;
    private float rotacion_actual = 0;
    bool rotando = false;

    Quaternion rotacion_inicial;


    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        time_alive = 0;
        if (gameObject.name == "PinchosShoot(Clone)")
        {
            rotacion_inicial = transform.parent.parent.rotation;

            if(SceneManager.GetActiveScene().name != "Multiplayer")
                damage /= 2;
        }

    }

    public void setViewID(int v)
    {
        viewID = v;
    }

    // Update is called once per frame
    void Update()
    {
        time_alive += Time.deltaTime;
        if (gameObject.name != "PinchosShoot(Clone)")
        {
            if (time_alive >= max_time_alive)
                Destroy(gameObject);
            gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * speed;
        }
        else
        {
            transform.parent.parent.Rotate(rotacion * Time.deltaTime* velocidad,0,0);
            rotacion_actual += rotacion * Time.deltaTime * velocidad;

            if(Mathf.Abs(rotacion_actual)>=Mathf.Abs(max_rotacion) && rotando != true)
            {
                rotando = true;
                rotacion *= -1;
            }
            if(Mathf.Abs(rotacion_actual)< 5 && rotando == true)
            {
                transform.parent.parent.rotation = rotacion_inicial;
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((speed != 0 || gameObject.name == "PinchosShoot(Clone)") && time_alive > Time.deltaTime)
        {
            //if (time_alive > Time.deltaTime)
            //{
            Bot_Movement bot = other.GetComponent<Bot_Movement>();
            if (bot != null)
                bot.Daño(damage);
            else
            {
                Online_Player o = other.GetComponent<Online_Player>();
                if (o != null)
                    o.Daño(damage);
                else
                {
                    Enemy_Controller e = other.GetComponentInParent<Enemy_Controller>();
                    if (e != null)
                        e.Daño(damage);
                }
            }
            if(speed!=0)
                Destroy(gameObject);
            //}
        }
    }

    public float Daño()
    {
        return damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        count++;
        if(count < 60)
        {
            Bot_Movement bot = other.GetComponent<Bot_Movement>();
            if (bot != null)
                bot.Daño(damage / 60);
            else
            {
                Online_Player o = other.GetComponent<Online_Player>();
                if (o != null && o.getViewID() != viewID)
                    o.Daño(damage / 50);
                else
                {
                    Enemy_Controller e = other.GetComponentInParent<Enemy_Controller>();
                    if (e != null)
                        e.Daño(damage / 60);
                }
            }
        }
    }
}
