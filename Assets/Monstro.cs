// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Monstro : MonoBehaviour
// {
//     public Monstro monster;
//     public bool takeLife = false;

//     void Awake()
//     {
//         monster = this;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if(other.gameObject.CompareTag("Player"))
//         {
//             gameObject.GetComponent<Renderer>().enabled = false;
//             takeLife = true;
//         }
//     }

//     private void Update()
//     {
//         if(takeLife){
//             Jogador.jogador.Vida = 10;
//             Destroy(gameObject);
//         }
//     }
// }
