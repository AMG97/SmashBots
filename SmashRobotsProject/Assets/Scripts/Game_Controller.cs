using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public static string arma_1, arma_2;

    [SerializeField]
    GameObject JeanBot;

    [SerializeField]
    GameObject Enemy;

    [SerializeField]
    Camera_Follow cam;

    GameObject player;
    GameObject enemy;
    Bot_Movement m;
    Enemy_Controller e_c;

    [SerializeField]
    Vida v;

    [SerializeField]
    Energia e;

    [SerializeField]
    GameObject mina, pistola, taser, laser, lanzallamas;

    [SerializeField]
    Image i_boton_1, i_boton_2;

    [SerializeField]
    Sprite i_mina, i_pistola, i_taser, i_laser, i_lanzallamas;

    [SerializeField]
    TMPro.TextMeshProUGUI contador_text;
    int contador_number=3;

    [SerializeField]
    TMPro.TextMeshProUGUI timer_text;
    [SerializeField]
    int min=3,sec=0;

    [SerializeField]
    float t_arma;

    private void Start()
    {
        player = Instantiate(JeanBot);
        enemy = Instantiate(Enemy);
        m = player.GetComponent<Bot_Movement>();
        e_c = enemy.GetComponent<Enemy_Controller>();

        e_c.Set_Fin_Partida(gameObject.GetComponent<Fin_Partida>());

        Transform arma_detras_pos = player.transform.GetChild(4);
        Transform arma_derecha_pos = player.transform.GetChild(5);
        Transform arma_izquierda_pos = player.transform.GetChild(6);

        GameObject a1=null, a2=null;

        switch(arma_1)
        {
            case "LanzaMinas(Clone)":
                a1=Instantiate(mina, arma_detras_pos);
                i_boton_1.sprite= i_mina;
                break;

            case "BBGun(Clone)":
                a1=Instantiate(pistola, arma_derecha_pos);
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
        }

        switch (arma_2)
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
        }

        
        a1.transform.localScale = Vector3.Scale(a1.transform.localScale, new Vector3(80, 80, 80));
        a2.transform.localScale = Vector3.Scale(a2.transform.localScale, new Vector3(80, 80, 80));

        m.SetArma1(a1);
        m.SetArma2(a2);


        m.SetVidaEnergia(v, e);

        cam.robot = player;
        cam.StartFollow();
        Time.timeScale = 1f;
        StartCoroutine(CountDown());        
    }

    public void Shoot1()
    {
        m.DispararArma1();

    }

    public void Shoot2()
    {
        m.DispararArma2();
    }

    IEnumerator CountDown()
    {
        while(contador_number > 0)
        {
            contador_text.text = contador_number.ToString();

            yield return new WaitForSeconds(1f);

            contador_number--;
        }

        contador_text.text = "YA!";

        yield return new WaitForSeconds(1f);

        contador_text.gameObject.SetActive(false);

        m.StartPlayer();
        e_c.StartEnemy(player);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {

        while (min > 0 || sec > 0)
        {
            if(sec >= 10)
                timer_text.text =  min.ToString() + ":" + sec.ToString();
            else
                timer_text.text =  min.ToString() + ":0" + sec.ToString();
            yield return new WaitForSeconds(1f);
            sec--;
            if(sec<0)
            {
                sec = 59;
                min--;
            }
        }
        if(Time.timeScale > 0f)
        {
            if (m.GetVida() > e_c.Get_Vida())
            {
                gameObject.GetComponent<Fin_Partida>().Terminar(1);
            }
            else
            {
                gameObject.GetComponent<Fin_Partida>().Terminar(0);
            }
        }
        
    }
}
