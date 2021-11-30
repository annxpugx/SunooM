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
        canvas.SetActive(distance < 1);
    }
}