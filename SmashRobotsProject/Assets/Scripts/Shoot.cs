using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float energia;
    [SerializeField]
    Transform punto_disparo;
    // Start is called before the first frame update

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Shoot_Proyectile()
    {
        GameObject g;
        if(projectile.name == "Electricidad" || projectile.name == "Fuego")
            g=Instantiate(projectile, gameObject.transform);
        else 
            g=Instantiate(projectile, punto_disparo.position, gameObject.transform.rotation);
        if (projectile.name == "Mina")
        {
            g.transform.position = new Vector3(g.transform.position.x, 0, g.transform.position.z);
            Vector3 new_rot = g.transform.rotation.eulerAngles;
            new_rot.x = 0;
            g.transform.eulerAngles=(new_rot);
        }
    }

    public float Get_Energy()
    {
        return energia;
    }
}
