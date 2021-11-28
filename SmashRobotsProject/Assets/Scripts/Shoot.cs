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

    [TextArea][SerializeField]
    string nombre, descripcion;
    // Start is called before the first frame update
    public void Shoot_Proyectile()
    {
        GameObject g;
        if(projectile.name == "Electricidad" || projectile.name == "Fuego")
            g=Instantiate(projectile, gameObject.transform);
        else 
            g=Instantiate(projectile, punto_disparo.position, gameObject.transform.rotation);
        if (projectile.name == "Mina")
        {
            g.transform.position = new Vector3(g.transform.position.x, -0.14f, g.transform.position.z);
            Vector3 new_rot = g.transform.rotation.eulerAngles;
            new_rot.x = 0;
            g.transform.eulerAngles=(new_rot);
        }
    }

    public float Get_Energy()
    {
        return energia;
    }

    public float Get_Daño()
    {
        if(projectile.GetComponent<Projectile>() != null)
            return projectile.GetComponent<Projectile>().Daño();
        else
            return projectile.GetComponent<Mine>().Daño();
    }

    public string Get_Descripcion()
    {
        return descripcion;
    }
    public string Get_Nombre()
    {
        return nombre;
    }
}
