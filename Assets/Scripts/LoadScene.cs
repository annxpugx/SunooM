using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform player;
    public string scene;
    public float activationDistance;

    private Transform _this;
    
    void Start()
    {
        _this = GetComponent<Transform>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, _this.position);
        if (distance < activationDistance) SceneManager.LoadScene(scene);
    }
}
