using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class HandleSpawnMonster : MonoBehaviour
{
    public Transform player;
    public GameObject monster;
    public float spawnDistance;

    void Start()
    {
        monster.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, monster.transform.position);
        if(distance < spawnDistance || monster.activeSelf) return;

        monster.SetActive(true);
    }
}
