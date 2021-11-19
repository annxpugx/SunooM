using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ColisionSun : MonoBehaviour
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
        if(distance < 2){
            canvas.SetActive(true);
        } else{
             canvas.SetActive(false);
        }

       
    }
}
