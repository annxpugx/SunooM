using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{

    public Text vida;

    // Start is called before the first frame update
    void Start()
    {
        vida.text = PlayerVida.jogador.Vida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizaVida()
    {
        vida.text = PlayerVida.jogador.Vida.ToString();
    }
}
