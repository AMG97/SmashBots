using UnityEngine;
using System.Collections;
public class turn : MonoBehaviour {
     float time;
    // Update is called once per frame
    void Update () {
        time +=Time.deltaTime;
        if(time>=3){
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
            print("Girate");
            //Vector3(0,180,0);
            //Quaternion(0,1,0,0);
            //transform.Rotate(new Vector3(gameObject.transform.rotation.x,gameObject.transform.rotation.y + 180,gameObject.transform.rotation.z)*Time.deltaTime);
            time=0;
        }

        //Vector3(0,0,0);
        //Quaternion(0,0,0,1);
        //transform.Rotate(new Vector3(gameObject.transform.rotation.x,gameObject.transform.rotation.y + 15,gameObject.transform.rotation.z)*Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Euler(0,0,0);
		//transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime); // probar esto
        //transform.eulerAngles = new Vector3(0, 0, 0);
    }
}