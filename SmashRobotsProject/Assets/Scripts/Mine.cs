using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    ParticleSystem explosion;
    [SerializeField]
    float explosion_force;

    [SerializeField]
    float damage;


    [SerializeField]
    float distance_back;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.rotation.eulerAngles; 
        transform.rotation = Quaternion.Euler(0, rot.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, gameObject.transform.position + new Vector3(0,0.1f,0), gameObject.transform.rotation);
        yield return new WaitForSeconds(0.15f);
        other.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, explosion_force, 0));
        Bot_Movement bot = other.GetComponent<Bot_Movement>();
        if (bot != null)
        {
            bot.Daño(damage);
        }
        else
        {
            Enemy_Controller e = other.GetComponentInParent<Enemy_Controller>();
            if (e != null)
            {
                e.Daño(damage);
            }
        }
        Destroy(gameObject);
    }

    public float Daño()
    {
        return damage;
    }
}
