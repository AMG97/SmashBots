using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Vector3 local_position;
    [SerializeField]
    float energy; // esto de momento no lo utilizo
    // Start is called before the first frame update

    //ESTO ES SOLO DE PRUEBA
    float max_time = 3;
    float time = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= max_time)
        {
            time = 0;
            Shoot_Proyectile();
        }
    }

    public void Shoot_Proyectile()
    {
        //aqui hacer algo de la energía
        GameObject shot = Instantiate(projectile, gameObject.transform);
        shot.transform.localPosition += local_position;
    }
}
