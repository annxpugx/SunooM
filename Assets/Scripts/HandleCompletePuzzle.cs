using System;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleCompletePuzzle : MonoBehaviour
{
    public string scene;

    public PropsAltar altar1;
    public PropsAltar altar2;
    public PropsAltar altar3;
    public PropsAltar altar4;

    private PropsAltar _altar;

    private void Start()
    {
        _altar = GetComponent<PropsAltar>();
    }

    void Update()
    {
        if (_altar == null) return;

        if (Completed() && _altar.activated)
        {
            SceneManager.LoadScene(scene);
        }
    }

    private bool Completed()
    {
        return altar1.activated && altar2.activated && altar3.activated && altar4.activated;
    }
}
