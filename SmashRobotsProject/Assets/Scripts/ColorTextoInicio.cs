using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorTextoInicio : MonoBehaviour
{
    Color oc,   //Original Color
              cc;   //Current Color

    Text texto;

    [SerializeField]
    float angulo, velAng;

    void Start()
    {
        texto = GetComponent<Text>();
        oc = texto.color;
        cc = oc;
    }

    void Update()
    {
        float seno = Mathf.Sin(angulo);

        angulo += velAng * Time.deltaTime;
        cc.r = oc.r * Mathf.Abs(seno);
        cc.g = oc.g * Mathf.Abs(seno);
        cc.b = oc.b * Mathf.Abs(seno);

        texto.color = cc;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
