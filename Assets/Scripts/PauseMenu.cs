using LostOnTenebris;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    [SerializeField] FPSController playerMove;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstSelected;

    private void Start()
    {
        VoltarJogo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if(GameIsPaused)
            {
                VoltarJogo();
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstSelected);
                Pause();
            }
        }
    }

    public void VoltarJogo()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerMove.OnUnpause();

    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerMove.OnPause();
    }

    public void LoadMenu()
    {   
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        System.Console.WriteLine("Saindo do Jogo...");
        Application.Quit();
    }
}
