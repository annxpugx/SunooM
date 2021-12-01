using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneEasterEgg : MonoBehaviour
{
    public Button activatorButton;
    public GameObject sceneSelector;

    void Start()
    {
        activatorButton.gameObject.SetActive(false);
        sceneSelector.SetActive(false);

        activatorButton.onClick.AddListener(delegate { sceneSelector.SetActive(true); });

        Button[] sceneButtons = sceneSelector.GetComponentsInChildren<Button>();
        foreach (Button button in sceneButtons)
        {
            button.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(button.GetComponentInChildren<Text>().text);
            });
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("easter_egg_activator")) activatorButton.gameObject.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("easter_egg_activator"))
        {
            activatorButton.gameObject.SetActive(false);
            sceneSelector.SetActive(false);
        }
    }
}