using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMenu : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>()
            .onClick
            .AddListener(delegate { SceneManager.LoadScene("Scenes/Menu"); });
    }
}