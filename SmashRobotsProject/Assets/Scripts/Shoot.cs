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

    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Shoot_Proyectile(float scale = 1)
    {
        GameObject g;
        if(projectile.name == "Electricidad" || projectile.name == "Fuego" || projectile.name == "PinchosShoot")
            g=Instantiate(projectile, punto_disparo.transform);
        else 
            g=Instantiate(projectile, punto_disparo.position, gameObject.transform.rotation);
        if (projectile.name == "Mina")
        {
            g.transform.position = new Vector3(g.transform.position.x, -0.14f, g.transform.position.z);
            Vector3 new_rot = g.transform.rotation.eulerAngles;
            new_rot.x = 0;
            g.transform.eulerAngles=(new_rot);
        }
        else
        {
            audio.Play();
        }

        if(scale != 1)
        {
            g.transform.localScale = new Vector3(scale, scale, scale);
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
