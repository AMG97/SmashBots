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

    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(0.15f);
        other.GetComponent<Rigidbody>().AddForce(new Vector3(0, explosion_force, 0));
        Bot_Movement bot = other.GetComponent<Bot_Movement>();
        if (bot != null)
            bot.Daño(damage);
        Destroy(gameObject);
    }

    public float Daño()
    {
        return damage;
    }
}
