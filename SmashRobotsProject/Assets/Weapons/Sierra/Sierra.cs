using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierra : MonoBehaviour
{
    public static bool start = false;

    [SerializeField]
    float speed, rotate_speed;

    [SerializeField]
    float max_mov;

    [SerializeField]
    float damage;

    [SerializeField]
    int dir;

    float time_rot = 100;

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            if (dir == 0)
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

            time_rot += Time.deltaTime;
            if (Mathf.Abs(transform.localPosition.x) > max_mov && time_rot > 2)
            {
                speed *= -1;
                time_rot = 0;
            }
            transform.Rotate(0, 0, rotate_speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Bot_Movement bot = other.GetComponent<Bot_Movement>();
        if (bot != null)
            bot.Daño(damage);
        else
        {
            Online_Player o = other.GetComponent<Online_Player>();
            if (o != null)
                o.Daño(damage);
            Enemy_Controller e = other.GetComponentInParent<Enemy_Controller>();
            if (e != null)
                e.Daño(damage);
        }
    }
}
