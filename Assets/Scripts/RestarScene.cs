using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestarScene : MonoBehaviour
{
  [SerializeField] private Button MyButton = null; // assign in the editor
  public string scene;

  void Start()
  {
      MyButton.onClick.AddListener(() => { LoadScene(scene); });
  }

  public void LoadScene(string scene)
  {
      SceneManager.LoadScene(scene);
  }
}
