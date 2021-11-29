using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionFinalScene : MonoBehaviour
{
     public Transform player;
    public GameObject canvas;

    void Start()
    {
         canvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, this.transform.position);
        if(distance < 1){
            canvas.SetActive(true);
        } else{
             canvas.SetActive(false);
        }
       
    }
}
