using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnAround : MonoBehaviour
{
    float time;
    void Update()
    {
        
        time +=Time.deltaTime;
        if(time>=3){
            //transform.Rotate(new Vector3(gameObject.transform.rotation.x,gameObject.transform.rotation.y + 180f,gameObject.transform.rotation.z)*Time.deltaTime);
            //transform.Rotate(new Vector3(0,-180,0)*Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0,-180,0);
            time=0;
        }
    }
}
