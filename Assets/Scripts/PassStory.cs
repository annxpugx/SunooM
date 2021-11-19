using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassStory : MonoBehaviour
{
    public GameObject text1, text2, text3, text4, text5;
    private Dictionary<string, Button> _mainMenuButtons = new Dictionary<string, Button>();
    public  int clickArrow=0;
    

    // Start is called before the first frame update
    void Start()
    {
        text1.SetActive(true);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        SetMainMenuButtons();
        ConfigureMainMenuButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureMainMenuButtons()
    {

         _mainMenuButtons["play"].onClick.AddListener(LoadGame);
        
    }

        private void SetMainMenuButtons()
    {
        _mainMenuButtons["play"] = GameObject.FindGameObjectWithTag("main_menu_play_btn").GetComponent<Button>();
    }
    
    private void LoadGame()
    {
            clickArrow++;
            if(clickArrow == 1){
                text1.SetActive(false);
                text2.SetActive(true);
            } else if(clickArrow == 2) {
                text2.SetActive(false);
                text3.SetActive(true);
            } else if(clickArrow == 3) {
                text3.SetActive(false);
                text4.SetActive(true);
            } else if(clickArrow == 4) {
                text4.SetActive(false);
                text5.SetActive(true);
            } else {
                SceneManager.LoadScene("initialScene");
            }
    }
}
