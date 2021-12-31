using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float max_time_alive, damage;
    [SerializeField]
    float speed;

    float time_alive;

    float time_colision = 0;

    int viewID = -1;


    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        time_alive = 0;
    }

    public void setViewID(int v)
    {
        viewID = v;
    }

    // Update is called once per frame
    void Update()
    {
        time_alive += Time.deltaTime;
        if (time_alive >= max_time_alive)
            Destroy(gameObject);
        gameObject.transform.position -= gameObject.transform.forward*Time.deltaTime*speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (speed != 0 && time_alive > Time.deltaTime)
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

    /*private void OnTriggerStay(Collider other)
    {
        time_colision += Time.deltaTime;
        if(time_colision >= max_time_alive/10)
        {
            time_colision = 0;

            Bot_Movement bot = other.GetComponent<Bot_Movement>();
            if (bot != null)
                bot.Daño(damage/10);
            else
            {
                Enemy_Controller e = other.GetComponentInParent<Enemy_Controller>();
                if (e != null)
                    e.Daño(damage/10);
            }
        }
    }*/

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
