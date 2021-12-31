using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class OnlineController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    public Vector3 pos1;
    public Vector3 pos2;

    PhotonView view;
    public static string arma_1, arma_2;



    [SerializeField]
    AudioSource au_cont;
    [SerializeField]
    AudioClip cont, ya;

    [SerializeField]
    TMPro.TextMeshProUGUI timer_text;
    [SerializeField]
    int min = 3, sec = 0;
    [SerializeField]
    TMPro.TextMeshProUGUI contador_text;
    int contador_number = 3;

    Online_Player pl;

    void Start()
    {
        view = GetComponent<PhotonView>();
        CreatePlayers();
    }


    public void CreatePlayers()
    {
        object[] armas = new object[2];
        armas[0] = arma_1;
        armas[1] = arma_2;

        GameObject p;
        if (PhotonNetwork.IsMasterClient)
        {
            p=PhotonNetwork.Instantiate(playerPrefab.name, pos1, Quaternion.identity,0, armas );
            contador_text.text = "eSPeRANDO CONTRINCANTe...";
            contador_text.fontSize = 100;
        }
        else
        {
            p=PhotonNetwork.Instantiate(playerPrefab.name, pos2, Quaternion.Euler(0, 180, 0),0, armas);
            view.RPC("RPC_Start", RpcTarget.AllViaServer);

        }

        pl = p.GetComponent<Online_Player>();
    }

    [PunRPC]
    public void RPC_Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        contador_text.text = "3";
        contador_text.fontSize = 360;


        while (contador_number > 0)
        {
            contador_text.text = contador_number.ToString();
            au_cont.clip = cont;
            au_cont.Play();

            yield return new WaitForSeconds(1f);

            contador_number--;
        }

        contador_text.text = "YA!";
        au_cont.clip = ya;
        au_cont.Play();

        yield return new WaitForSeconds(1f);

        contador_text.gameObject.SetActive(false);

        StartCoroutine(Timer());
    }




    IEnumerator Timer()
    {
        Online_Player.start = true;
        Sierra.start = true;
        DisparoAutomatico.start = true;

        while (min > 0 || sec > 0)
        {
            if (sec >= 10)
                timer_text.text = min.ToString() + ":" + sec.ToString();
            else
                timer_text.text = min.ToString() + ":0" + sec.ToString();
            yield return new WaitForSeconds(1f);
            sec--;
            if (sec < 0)
            {
                sec = 59;
                min--;
            }
        }

        if (Time.timeScale > 0f)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            float vida_mine=0, vida_other=0;
            Online_Player p1 = players[0].GetComponent<Online_Player>();
            Online_Player p2 = players[1].GetComponent<Online_Player>();
            
            if (p1.ViewMine())
                vida_mine = p1.Vida();
            else
                vida_other = p1.Vida();

            if (p2.ViewMine())
                vida_mine = p2.Vida();
            else
                vida_other = p2.Vida();


            if (vida_mine > vida_other)
            {
                gameObject.GetComponent<Fin_Partida_Online>().Terminar(1);
            }
            else
            {
                gameObject.GetComponent<Fin_Partida_Online>().Terminar(0);
            }
        }
    }

    public void Shoot1()
    {
        pl.DispararArma1();

    }

    public void Shoot2()
    {
        pl.DispararArma2();
    }
}
