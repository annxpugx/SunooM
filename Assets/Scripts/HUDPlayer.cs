using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDPlayer : MonoBehaviour
{

    public Text VIDA;

    void Start()
    {
        VIDA.text = PlayerVida.jogador.Vida.ToString();
    }


    public void updateLife()
    {
        VIDA.text = PlayerVida.jogador.Vida.ToString();
    }

}
