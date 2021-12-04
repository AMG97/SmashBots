using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField]
    float max_vida;
    float vida;

    [SerializeField]
    Slider slider;

    [SerializeField]
    Canvas canvas;

    Camera cam;

    [SerializeField]
    float velocidad_vida;

    Fin_Partida fin;

    bool start = false;
    GameObject player;


    [SerializeField]
    float speed_r, speed_m, distancia_min;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Shoot arma_1, arma_2;


    [SerializeField]
    float arma_disparar;

    float arma1_time, arma2_time;

    public enum EstadoIA
    {
        SeguirJugador,   //The splash image is moving on the screen
        Huir
    }  //Time out, a pressed key or the splash image is just moved, go to main menu scene

    public EstadoIA estado = EstadoIA.SeguirJugador;

    [SerializeField]
    Vector3 runAwayPoint;

    [SerializeField]
    float distancia_huir;

    [SerializeField]
    float t_check_atack;

    float check_atack_act;

    float t_in_following;

    // Start is called before the first frame update
    void Start()
    {
        vida = max_vida;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            arma1_time += Time.deltaTime;
            arma2_time += Time.deltaTime;
            check_atack_act += Time.deltaTime;

            slider.value = Mathf.Lerp(slider.value, (vida / max_vida), velocidad_vida);

            if (slider.value <= 0.01 && Time.timeScale > 0)
            {
                fin.Terminar(1);
            }

            switch (estado)
            {
                case EstadoIA.SeguirJugador:
                    FollowPlayer();
                    break;

                case EstadoIA.Huir:
                    RunAway();
                    break;

            }

            OnDrawGizmos();
        }
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(cam.transform);
        canvas.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void Daño(float d)
    {
        vida -= d;
    }

    public void StartEnemy(GameObject pl)
    {
        start = true;
        player = pl;
    }

    public float Get_Vida()
    {
        return vida;
    }

    public void Set_Fin_Partida(Fin_Partida f)
    {
        fin = f;
    }


    void FollowPlayer()
    {
        t_in_following += Time.deltaTime;
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.transform.position - transform.position;

        targetDirection.y = 0;

        float d = Vector3.Distance(player.transform.position, transform.position);

        if (d < distancia_min)
        {
            RotateToPoint(targetDirection);
        }
        else
        {
            GoToPoint(targetDirection);
        }

        if (check_atack_act > t_check_atack)
        {

            if (d < distancia_min * 1.3 && arma1_time > arma_disparar + Random.Range(-1,1))
            {
                Debug.Log("Cerca y ATACO");
                arma_1.Shoot_Proyectile();
                arma1_time = 0;
                StartCoroutine(RunAwayLater(1));
            }
            else if (t_in_following > 3 && arma2_time > arma_disparar + Random.Range(-1,1))
            {
                arma_2.Shoot_Proyectile();
                arma2_time = 0;
            }

            check_atack_act = 0;
        }
    }

    void RunAway()
    {
        Vector3 targetDirection = runAwayPoint - transform.position;
        GoToPoint(targetDirection);

        float d = Vector3.Distance(transform.position, runAwayPoint);
        if(d < distancia_min/2)
        {
            estado = EstadoIA.SeguirJugador;
        }
    }


    void ChooseRunAwayPoint()
    {
        t_in_following = 0;
        runAwayPoint = transform.position;
        runAwayPoint.z -= distancia_huir + Random.Range(-1,1);
        float x;
        do
        {
            x = Random.Range(runAwayPoint.x - distancia_huir, runAwayPoint.x + distancia_huir);
        } while (Mathf.Abs(x) > 7);

        runAwayPoint.x = x;
        runAwayPoint.y = 0;
    }

    void RotateToPoint(Vector3 point)
    {
        // The step size is equal to speed times frame time.
        float singleStep = speed_r * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, point, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void GoToPoint(Vector3 point)
    {
        RotateToPoint(point);
        //transform.position = Vector3.MoveTowards(transform.position, point, speed_m * Time.deltaTime);
        transform.Translate(transform.forward * speed_m* Time.deltaTime, Space.World);
    }


    IEnumerator RunAwayLater(float time)
    {
        yield return new WaitForSeconds(time);
        ChooseRunAwayPoint();
        estado = EstadoIA.Huir;
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(runAwayPoint, 0.4f);
    }

}
