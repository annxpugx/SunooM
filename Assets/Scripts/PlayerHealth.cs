using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int life = 5;
    private Image[] _hearts;

    private void Start()
    {
        _hearts = GameObject.FindWithTag("hearts").GetComponentsInChildren<Image>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monstro monster = other.gameObject.GetComponent(typeof(Monstro)) as Monstro;
            if (monster.canTakeLife)
            {
                Debug.Log("here");
                _hearts[--life].enabled = false;
                monster.canTakeLife = false;
                other.gameObject.GetComponent<Renderer>().enabled = false;
                other.gameObject.SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (life == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}