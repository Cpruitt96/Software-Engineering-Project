using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {
    private GameState gameState = GameState.PLAYING;

    //Adjustable board size (x & y)
    public int boardSize;
    //Multidimensional array stores each player's board of cells.
    public GameObject[,,] boards;

    //Current player and current number of ships
    public int currentPlayer;
    public int[] playerShips;
    
    //Cell prefab reference used for generation
    public GameObject cell;

    //Mouse position for placement and attacking
    private Vector2 mousePos;

    
    
    void Start() {
        currentPlayer = 0;
        playerShips = new int[2];
        playerShips[0] = 0;
        playerShips[1] = 0;
            
        //Generation of board
        boards = new GameObject[2, boardSize, boardSize];
        //Players
        for (int i = 0; i < 2; i++)
        {
            //X
            for (int j = 0; j < boardSize; j++)
            {
                //Y
                for (int k = 0; k < boardSize; k++)
                {
                    GameObject currentCell = Instantiate(cell);
                    currentCell.transform.position = new Vector3(j, k + (i * 30), 0);
                    CellHandler cellHandler = currentCell.GetComponent<CellHandler>();
                    cellHandler.xValue = i;
                    cellHandler.yValue = j;
                    boards[i, j, k] = currentCell;

                }
            }
        }
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

    void Update() {
        //Checks for lose condition
        if ((playerShips[0] == GameObject.Find("TurnManager").GetComponent<TurnManager>().numShips*-2 || playerShips[1] == -GameObject.Find("TurnManager").GetComponent<TurnManager>().numShips*-2))
        {
            GameObject.Find("MenuSystem").GetComponent<GameController>().switchToEnd();
        }
    }

    //Places a ship on the board if it is a valid location
    public void placeShip(GameObject Ship)
    {
        if (isPlaceable() && gameState == GameState.PLAYING)
        {
            //Grabs mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Assigns the ship to the cell
            Ship.transform.parent = boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].gameObject.transform;
            boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponent<CellHandler>().hasShip = true;
            //Subtract one from player's ship count
            playerShips[currentPlayer] += 1;
            //Switch turns
            GameObject.Find("TurnManager").GetComponent<TurnManager>().shipsRemaining[currentPlayer]--;
        }
    }

    //Checks if a location is valid for placement
    public bool isPlaceable()
    {
        //Grab mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isOnBoard())
        {
            //Check if cell doesn't have a ship
            if(!boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponent<CellHandler>().hasShip)
            {
                return true;
            } 
        }
        return false;
    }

    //Attempts an attack on the enemy board
    public bool attackShip()
    {
        if (isAttackable() && gameState == GameState.PLAYING)
        {
            //Grab mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Use mouse position to determine opponent's cell
            GameObject attackedCell = boards[(3 % (currentPlayer + 2)), (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))];
            //Check if opponent's cell has a ship
            if (attackedCell.GetComponent<CellHandler>().hasShip)
            {
                //Make the opponent's cell indicate it has been attempted
                attackedCell.GetComponent<CellHandler>().attackAttempted = true;
                //Make ship red
                attackedCell.transform.GetChild(3).GetComponentInChildren<SpriteRenderer>().color = Color.red;
                //Make cooresponding cell on Player's board green to indicate a successful attack
                boards[currentPlayer, (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                //Subtract one from opponent's remaining ship count
                GameObject.Find("TurnManager").GetComponent<TurnManager>().shipsRemaining[3 % (currentPlayer + 2)]--;
                //Opponent becomes active player
                currentPlayer = 3 % (currentPlayer + 2);
            } else
            {
                //Make the opponent's cell indicate it has been attempted
                attackedCell.GetComponent<CellHandler>().attackAttempted = true;
                //Make player's cell gray to indicate failed attack
                boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                //Opponent becomes active player
                currentPlayer = 3 % (currentPlayer + 2);
            }
            return true;
        }
        return false;
    }

    //Checks if a location is valid for attack
    public bool isAttackable()
    {
        //Get mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Check if location in on board and if an attack has already been attempted
        if (isOnBoard())
        {
            if(!boards[(3 % (currentPlayer + 2)), (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))].GetComponent<CellHandler>().attackAttempted)
            {
                return true;
            }
        }
        return false;
    }

    //Checks if a location is on the board
    public bool isOnBoard()
    {
        //Get mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Check if rounded mouse.x & mouse.y are within the bounds of the board
        if ((int)Mathf.Round(mousePos.x) >= 0 && (int)Mathf.Round(mousePos.x) <= boardSize && (int)Mathf.Round(mousePos.y) >= 0 + (currentPlayer * 30) && (int)Mathf.Round(mousePos.y) <= boardSize + (currentPlayer * 30))
        {
            return true;
        }
        return false;
    }
}
