using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU };
public enum MenuState { NONE, MAIN, INVENTORY };


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
    public static event Action reset;


    // Use this for initialization
    void Start()
    {
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
                if (Input.GetKeyDown(KeyCode.I) && menuState != MenuState.INVENTORY)
                {
                    switchToInventory();
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    switchToPlaying();
                }
                break;
            case GameState.PLAYING_TO_MENU:
                switchToMenu();
                break;
            default:
                break;

        }
    }

    public void switchToPlaying()
    {
        gameState = GameState.PLAYING;
        menuState = MenuState.NONE;
        gameStateDisplay.text = "Playing";
    }

    public void switchToMenu()
    {
        gameState = GameState.MENU;
        menuState = MenuState.MAIN;
        gameStateDisplay.text = "Menu";
    }

    public void switchToInventory()
    {
        menuState = MenuState.INVENTORY;
        gameStateDisplay.text = "Inventory";
    }

    public void switchToMenu(MenuState newMenu)
    {
        menuState = newMenu;
    }

    public void restart()
    {
        if (reset != null)
        {
            reset();
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

