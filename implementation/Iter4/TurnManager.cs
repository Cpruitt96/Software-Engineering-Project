using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PHASE {PLACEMENT, FIGHTING};

public class TurnManager : MonoBehaviour
{
    private GameState gameState = GameState.PLAYING;
    private PHASE phase;

    private BoardHandler boardHandler;
    public int currentPlayer;
    private ShipPlacement shipPlacement;
    
    public int numShips;
    public int[] shipsRemaining;
    public GameObject ship;
    GameObject currentShip;

    Camera theCamera;

    public Text winText;



    void Start()
    {
        phase = PHASE.PLACEMENT;
        boardHandler = GameObject.Find("Board").GetComponent<BoardHandler>();
        theCamera = GameObject.Find("Camera").GetComponent<Camera>();
        theCamera.transform.position = new Vector3(boardHandler.boardSize/2, -boardHandler.boardSize, -10);
        shipsRemaining = new int[2];
        shipsRemaining[0] = numShips;
        shipsRemaining[1] = numShips;
        currentShip = Instantiate(ship);

    }

    private void OnEnable()
    {
        GameController.changeGameState += updateGameState;
    }

    private void OnDisable()
    {
        GameController.changeGameState -= updateGameState;
    }

    void updateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.PLAYING:

                //Camera move towards the middle of the current player's board
                if (theCamera.transform.position != new Vector3(boardHandler.boardSize / 2, ((boardHandler.boardSize / 2) + currentPlayer * 30), -10))
                {
                        theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, new Vector3(boardHandler.boardSize / 2, ((boardHandler.boardSize / 2) + currentPlayer * 30), -10), Time.deltaTime*6);
                }

                currentPlayer = boardHandler.currentPlayer;
                shipPlacement = ship.GetComponent<ShipPlacement>();

                switch (phase)
                {
                    case PHASE.PLACEMENT:
                        //If Player 1 has no more ships to place switch to Player 2
                        if (shipsRemaining[currentPlayer] == 0 && currentPlayer == 0)
                        {
                            boardHandler.currentPlayer = 1;
                            break;
                        }
                        //if Player 2 has no more ships to place switch to FIGHTING
                        if (shipsRemaining[currentPlayer] == 0 && currentPlayer == 1)
                        {
                            Debug.Log("Fight");
                            boardHandler.currentPlayer = 0;
                            phase = PHASE.FIGHTING;
                            
                            break;
                        }
                        //If there are ships to place and there isn't a ship generated to be placed, generate one
                        if (shipsRemaining[currentPlayer] > 0 && currentShip.GetComponent<ShipPlacement>() == null )
                        {
                            Debug.Log("Instatiating ship.");
                            currentShip = Instantiate(ship);
                        }
                        break;
                    case PHASE.FIGHTING:
                        if ((shipsRemaining[0] == -numShips || shipsRemaining[1] == -numShips))
                        {
                            winText.text = "Player " + ((3 % (currentPlayer + 2))+1) + " Wins!";
                            GameObject.Find("MenuSystem").GetComponent<GameController>().switchToEnd();
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            //If the attack is succesful switch the current player
                            if (boardHandler.attackShip())
                            {
                                currentPlayer = boardHandler.currentPlayer;
                            }
                        }
                        break;
                }
                break;
        }
    }
}
