using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU, END };
public enum MenuState { NONE, MAIN, END};


public class GameController : MonoBehaviour
{
    private GameState _gameState;
    private MenuState _menuState;
    public Text gameStateDisplay;
    private Vector3 originalPosition;
    public GameState gameState
    {
        set
        {
            _gameState = value;
            if (changeGameState != null)
            {
                changeGameState(gameState);
            }
        }
        get
        {
            return _gameState;
        }
    }
    public MenuState menuState
    {
        set
        {
            _menuState = value;
            if (changeMenu != null)
            {
                changeMenu(menuState);
            }
        }
        get
        {
            return _menuState;
        }
    }

    public static event Action<MenuState> changeMenu;
    public static event Action<GameState> changeGameState;
    private bool menuExists;


    // Use this for initialization

    void Start()
    {
        if (!menuExists)
        {
            menuExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        switchToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.MENU:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.MENU_TO_PLAYING;
                }
                break;
            case GameState.MENU_TO_PLAYING:
                switchToPlaying();
                break;
            case GameState.PLAYING:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.PLAYING_TO_MENU;
                }
                break;
            case GameState.PLAYING_TO_MENU:
                switchToMenu();
                break;
            case GameState.END:
                break;
            default:
                break;

        }
    }

    public void switchToPlaying()
    {
        gameState = GameState.PLAYING;
        menuState = MenuState.NONE;
        //gameStateDisplay.text = "Playing";
    }

    public void switchToMenu()
    {
        gameState = GameState.MENU;
        menuState = MenuState.MAIN;
        //gameStateDisplay.text = "Menu";
    }

    public void switchToEnd()
    {
        gameState = GameState.END;
        menuState = MenuState.END;
        //gameStateDisplay.text = "END";
    }

    public void switchToMenu(MenuState newMenu)
    {
        menuState = newMenu;
    }

    public void restart()
    {
        switchToMenu();
        SceneManager.LoadScene("BattleshipScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

