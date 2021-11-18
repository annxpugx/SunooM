using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{

    private int life = 100;
    public static PlayerVida jogador;
    public HUDPlayer hud;


    void Awake () {
        jogador = this;
    }

    public int Vida{
        get{
            return life;
        }
        set{
            life -= value;
            hud.updateLife();
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Monstro monster = other.gameObject.GetComponent(typeof(Monstro)) as Monstro;
            if(monster.canTakeLife){
                jogador.Vida = 25;
                monster.canTakeLife = false;
                other.gameObject.GetComponent<Renderer>().enabled = false;
                other.gameObject.SetActive(false);
            } 
        }
    }

    public void Update()
    {
        if(Vida <= 0){
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
    }
}
