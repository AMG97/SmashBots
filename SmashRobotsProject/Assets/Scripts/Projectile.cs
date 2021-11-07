using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float max_time_alive, damage;
    [SerializeField]
    Vector3 speed;

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
        gameObject.transform.localPosition += speed*Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Hacer daño a lo que se haya chocao
        Destroy(gameObject);
    }
}
