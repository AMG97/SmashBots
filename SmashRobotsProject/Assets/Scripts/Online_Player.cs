using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Online_Player : MonoBehaviourPunCallbacks, IPunObservable, IPunInstantiateMagicCallback
{
    PhotonView view;
    FixedJoystick joystick;
    Rigidbody rb;

    [SerializeField]
    float speed;

    [SerializeField]
    float max_angle, rotation_wheel;

    [SerializeField]
    Transform FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel;

    [SerializeField]
    Slider slider_vida, slider_energia;

    Shoot_Online arma1=null, arma2=null;

    Camera_Follow cam;

    int add = 0;
    public static bool start = false;

    //Cosas de vida
    float vida, vida_max = 100;
    const float velocidad_vida = 2;
    Image barra_vida;
    Canvas canvas;

    //Cosas energia
    float energia, energia_max = 100;
    [SerializeField]
    float incremento;
    Image barra_energia;

    [SerializeField]
    GameObject mina, pistola, taser, laser, lanzallamas, pinchos;

    GameObject a1, a2;


    Image i_boton_1, i_boton_2;

    [SerializeField]
    Sprite i_mina, i_pistola, i_taser, i_laser, i_lanzallamas, i_pinchos;

    Transform arma_detras_pos, arma_derecha_pos, arma_izquierda_pos;

    [SerializeField]
    Fin_Partida_Online f;


    // Start is called before the first frame update
    void Start()
    {

        vida = vida_max;
        

        energia = 0;

        f = GameObject.Find("OnlineController").GetComponent<Fin_Partida_Online>();

        view = GetComponent<PhotonView>();
        joystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
        rb = GetComponent<Rigidbody>();

        cam = GameObject.Find("Camera").GetComponent<Camera_Follow>();

        if (transform.eulerAngles.y == 180)
        {
            add = 180;
        }

        if (view.IsMine)
        {
            cam.robot = gameObject;
            cam.StartFollow();

            slider_vida.gameObject.SetActive(false);
            barra_vida = GameObject.Find("Vida").GetComponent<Image>();
            barra_energia = GameObject.Find("Energia").GetComponent<Image>();
        }
        else
        {
            GetComponent<AudioListener>().enabled = false;
            canvas = slider_vida.gameObject.GetComponentInParent<Canvas>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (start == true)
        {
            updateVidaEnergia();
            if (view.IsMine)
            {

                float x = joystick.Vertical;
                float z = joystick.Horizontal;


                if (x != 0 || z != 0)
                {
                    float hipo = Mathf.Sqrt(x * x + z * z);
                    float dif = 1.0f / hipo;

                    x = x * dif;
                    z = z * dif;

                    float rot_y = Mathf.Atan2(z, x) * 180 / Mathf.PI;
                    var qr = Quaternion.Euler(0, rot_y + add, 0);


                    rb.transform.rotation = Quaternion.Lerp(transform.rotation, qr, max_angle * Time.deltaTime);


                    rb.AddForce(-rb.transform.forward * speed * Time.deltaTime);

                    FrontLeftWheel.Rotate(new Vector3(-rotation_wheel * Time.deltaTime, 0, 0));
                    FrontRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
                    BackRightWheel.Rotate(new Vector3(rotation_wheel * Time.deltaTime, 0, 0));
                    BackLeftWheel.Rotate(new Vector3(-rotation_wheel * Time.deltaTime, 0, 0));
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }
            }
        }
    }

    void updateVidaEnergia()
    {
        if(view.IsMine)
        {
            barra_vida.fillAmount = Mathf.Lerp(barra_vida.fillAmount, (vida / vida_max), velocidad_vida);
            barra_vida.color = Color.Lerp(Color.red, Color.green, (vida / vida_max));

            if (barra_vida.fillAmount <= 0.01 && Time.timeScale > 0)
            {
                f.Terminar(0);
            }


            if (energia < energia_max)
            {
                energia += incremento * Time.deltaTime;
                if (energia > energia_max)
                    energia = energia_max;
            }


            barra_energia.fillAmount = Mathf.Lerp(barra_energia.fillAmount, (energia / energia_max), velocidad_vida);
            barra_energia.color = Color.Lerp(Color.gray, Color.blue, (energia / energia_max));
        }

        else
        {
            slider_vida.value = Mathf.Lerp(slider_vida.value, (vida / vida_max), velocidad_vida);
            slider_energia.value = Mathf.Lerp(slider_energia.value, (energia / energia_max), velocidad_vida);

            if(slider_vida.value <= 0.01 && Time.timeScale > 0)
            {
                f.Terminar(1);
            }
            
            canvas.transform.LookAt(cam.transform);
            canvas.transform.Rotate(new Vector3(0, 180, 0));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(vida);
            stream.SendNext(energia);
        }
        else
        {
            vida = (float)stream.ReceiveNext();
            energia = (float)stream.ReceiveNext();
        }
    }

    public void Daño(float damage)
    {
        if(view.IsMine)
            vida -= damage;
    }

    public void SetArmas(GameObject a, GameObject b)
    {
        arma1 = a.GetComponent<Shoot_Online>();
        arma2 = b.GetComponent<Shoot_Online>();
    }


    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] a = info.photonView.InstantiationData;

        i_boton_1 = GameObject.Find("Image_Arma1").GetComponent<Image>();
        i_boton_2 = GameObject.Find("Image_Arma2").GetComponent<Image>();

        Transform arma_detras_pos = transform.GetChild(4);
        Transform arma_derecha_pos = transform.GetChild(5);
        Transform arma_izquierda_pos = transform.GetChild(6);

        GameObject a1=null, a2=null;

        Sprite boton_1_sprite, boton_2_sprite;

        boton_1_sprite = i_boton_1.sprite;
        boton_2_sprite = i_boton_2.sprite;

        switch ((string)a[0])
        {
            case "LanzaMinas(Clone)":
                a1 = Instantiate(mina, arma_detras_pos);
                i_boton_1.sprite = i_mina;
                break;

            case "BBGun(Clone)":
                a1 = Instantiate(pistola, arma_derecha_pos);
                i_boton_1.sprite = i_pistola;
                break;

            case "Lanzallamas(Clone)":
                a1 = Instantiate(lanzallamas, arma_derecha_pos);
                i_boton_1.sprite = i_lanzallamas;
                break;

            case "RayoLaser(Clone)":
                a1 = Instantiate(laser, arma_derecha_pos);
                i_boton_1.sprite = i_laser;
                break;

            case "Taser(Clone)":
                a1 = Instantiate(taser, arma_derecha_pos);
                i_boton_1.sprite = i_taser;
                break;

            case "Pinchos(Clone)":
                a1 = Instantiate(pinchos, arma_derecha_pos);
                i_boton_1.sprite = i_pinchos;
                break;
        }

        switch ((string)a[1])
        {
            case "LanzaMinas(Clone)":
                a2 = Instantiate(mina, arma_detras_pos);
                i_boton_2.sprite = i_mina;
                break;

            case "BBGun(Clone)":
                a2 = Instantiate(pistola, arma_izquierda_pos);
                i_boton_2.sprite = i_pistola;
                break;

            case "Lanzallamas(Clone)":
                a2 = Instantiate(lanzallamas, arma_izquierda_pos);
                i_boton_2.sprite = i_lanzallamas;
                break;

            case "RayoLaser(Clone)":
                a2 = Instantiate(laser, arma_izquierda_pos);
                i_boton_2.sprite = i_laser;
                break;

            case "Taser(Clone)":
                a2 = Instantiate(taser, arma_izquierda_pos);
                i_boton_2.sprite = i_taser;
                break;

            case "Pinchos(Clone)":
                a2 = Instantiate(pinchos, arma_izquierda_pos);
                i_boton_2.sprite = i_pinchos;
                break;
        }
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            i_boton_1.sprite = boton_1_sprite;
            i_boton_2.sprite = boton_2_sprite;
        }

        SetArmas(a1, a2);
    }



    public void StartPlayer()
    {
        start = true;
    }

    private void OnDestroy()
    {
        Destroy(arma1);
        Destroy(arma2);
    }

    public void DispararArma1()
    {
        if (start && Disparar(arma1.Get_Energy()))
        {
            //arma1.Shoot_Proyectile();
            view.RPC("RPC_Shoot", RpcTarget.AllViaServer, 1);
        }

    }

    public void DispararArma2()
    {
        if (start && Disparar(arma2.Get_Energy()))
        {
            //arma2.Shoot_Proyectile();
            view.RPC("RPC_Shoot", RpcTarget.AllViaServer, 2);
        }
    }

    [PunRPC]
    public void RPC_Shoot(int arma)
    {
        Shoot_Online s;

        if (arma == 1)
            s = arma1;
        else
            s = arma2;

        GameObject projectile = s.getProyectile();
        Transform punto_disparo = s.getpuntoDisparo();



        GameObject g;
        if (projectile.name == "Electricidad" || projectile.name == "Fuego" || projectile.name == "PinchosShoot")
            g = Instantiate(projectile, punto_disparo.transform);
        else
            g = Instantiate(projectile, punto_disparo.position, gameObject.transform.rotation);
        if (projectile.name == "Mina")
        {
            g.transform.position = new Vector3(g.transform.position.x, -0.14f, g.transform.position.z);
            Vector3 new_rot = g.transform.rotation.eulerAngles;
            new_rot.x = 0;
            g.transform.eulerAngles = (new_rot);
        }
        else
        {
            s.PlayAudio();
        }

        Projectile p = g.GetComponent<Projectile>();
        if(p!=null)
        {
            p.setViewID(view.ViewID);
        }

    }

    public bool Disparar(float consumo_energia)
    {
        if (consumo_energia <= energia)
        {
            energia -= consumo_energia;
            return true;
        }

        return false;
    }

    public int getViewID()
    {
        return view.ViewID;
    }

    public bool ViewMine()
    {
        return view.IsMine;
    }

    public float Vida()
    {
        return vida;
    }
}
