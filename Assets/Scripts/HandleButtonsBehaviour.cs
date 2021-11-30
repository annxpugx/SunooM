using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuType { MainMenu, PauseMenu }

public class HandleButtonsBehaviour : MonoBehaviour
{
    public GameObject Menu;
    public MenuType Type;

    private Dictionary<string, Button> _mainMenuButtons = new Dictionary<string, Button>();
    private Dictionary<string, Button> _pauseMenuButtons = new Dictionary<string, Button>();

    [CanBeNull] public AudioSource audioSource;

    void Start()
    {
        LoadButtons();
        HideMenu();
    }

    private void Update()
    {
        if (Type == MenuType.PauseMenu && Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePaused();
        }
    }

    private void TogglePaused()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        ToggleMenu();

        if (audioSource == null) return;

        if(audioSource.isPlaying) audioSource.Pause();
        else audioSource.UnPause();
    }

    private void ToggleMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }

    private void HideMenu()
    {
        if (Type != MenuType.MainMenu)
        {
            Menu.SetActive(false);
        }
    }

    private void LoadButtons()
    {
        if (Type == MenuType.MainMenu)
        {
            SetMainMenuButtons();
            ConfigureMainMenuButtons();
        }
        else
        {
            SetPauseMenuButtons();
            ConfigurePauseMenuButtons();
        }
    }

    private void ConfigureMainMenuButtons()
    {
        _mainMenuButtons["play"].onClick.AddListener(LoadGame);
        _mainMenuButtons["quit"].onClick.AddListener(Application.Quit);
    }

    private void ConfigurePauseMenuButtons()
    {
        _pauseMenuButtons["resume"].onClick.AddListener(TogglePaused);
        _pauseMenuButtons["restart"].onClick.AddListener(delegate
        {
            LoadGame();
            TogglePaused();
        });
        _pauseMenuButtons["quit"].onClick.AddListener(delegate
        {
            GoToMainMenu();
            TogglePaused();
        });
        _pauseMenuButtons["close"].onClick.AddListener(TogglePaused);
    }

    private void GoToMainMenu()
    {
        HideMenu();
        SceneManager.LoadScene("Menu");
    }

    private void LoadGame()
    {
        HideMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetMainMenuButtons()
    {
        _mainMenuButtons["play"] = GameObject.FindGameObjectWithTag("main_menu_play_btn").GetComponent<Button>();
        _mainMenuButtons["quit"] = GameObject.FindGameObjectWithTag("main_menu_quit_btn").GetComponent<Button>();
    }

    private void SetPauseMenuButtons()
    {
        _pauseMenuButtons["resume"]  = GameObject.FindGameObjectWithTag("pause_menu_resume_btn").GetComponent<Button>();
        _pauseMenuButtons["restart"] = GameObject.FindGameObjectWithTag("pause_menu_restart_btn").GetComponent<Button>();
        _pauseMenuButtons["quit"]    = GameObject.FindGameObjectWithTag("pause_menu_quit_btn").GetComponent<Button>();
        _pauseMenuButtons["close"]   = GameObject.FindGameObjectWithTag("pause_menu_close_btn").GetComponent<Button>();
    }
}
