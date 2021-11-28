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
    // Start is called before the first frame update
    void Start()
    {
        time_alive = 0;
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
        if (time_alive > Time.deltaTime)
        {
            Bot_Movement bot = other.GetComponent<Bot_Movement>();
            if (bot != null)
                bot.Da�o(damage);
            //Hacer da�o a lo que se haya chocao
            Destroy(gameObject);
        }
    }

    public float Da�o()
    {
        return damage;
    }
}
