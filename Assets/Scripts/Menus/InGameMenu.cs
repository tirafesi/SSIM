﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    // menu object with overlay
    public GameObject menuPanel;

    // main menu options (continue, restart, exit)
    public GameObject mainMenu;

    // int corresponding to start menu scene
    private const int START_MENU_SCENE = 0;

    // cursor icon
    public Texture2D cursor;

    // the player controller
    public PlayerController player;

    // shotgun sounds
    AudioSource[] shotgun_sounds;

    private void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerController>();
        shotgun_sounds = player.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver) return;


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!menuPanel.activeSelf)
            {
                // Default cursor
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

                GameManager.instance.gameStop = true;
                menuPanel.SetActive(true);
                mainMenu.SetActive(true);

                Time.timeScale = 0;

                for (int i = 0; i < shotgun_sounds.Length; i++)
                {
                    shotgun_sounds[i].Pause();
                }
            }
            else
            {
                // change cursor to crosshair and fix its hotspot
                Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);

                GameManager.instance.gameStop = false;
                menuPanel.SetActive(false);
                mainMenu.SetActive(false);

                Time.timeScale = 1;

                for (int i = 0; i < shotgun_sounds.Length; i++)
                {
                    shotgun_sounds[i].UnPause();
                }
            }
        }
    }

    public void Continue()
    {
        // change cursor to crosshair and fix its hotspot
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);

        GameManager.instance.gameStop = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        GameManager.instance.gameStop = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(0);

    }
}