using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    ParticleSystem explosion;
    [SerializeField]
    float explosion_force;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(new Vector3(0, explosion_force, 0));
        Instantiate(explosion, gameObject.transform.position,gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
