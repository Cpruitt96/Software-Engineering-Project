using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {
    public int boardSize;
    public GameObject[,,] boards;

    public int currentPlayer;
    public int[] playerShips;

    public GameObject cell;

    private Vector2 mousePos;

	// Use this for initialization
	void Start () {
        currentPlayer = 0;
        playerShips = new int[2];
        playerShips[0] = 0;
        playerShips[1] = 0;

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
                    currentCell.transform.position = new Vector3(j, k + (i*30), 0);
                    CellHandler cellHandler = currentCell.GetComponent<CellHandler>();
                    cellHandler.xValue = i;
                    cellHandler.yValue = j;
                    boards[i, j, k] = currentCell;

                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
    }

    public void placeShip()
    {
        if (isPlaceable())
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponent<CellHandler>().hasShip = true;
            playerShips[currentPlayer] += 1;
            GameObject.Find("TurnManager").GetComponent<TurnManager>().shipsRemaining[currentPlayer]--;
        }
    }

    public bool isPlaceable()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((int)Mathf.Round(mousePos.x) >= 0 && (int)Mathf.Round(mousePos.x) <= boardSize && (int)Mathf.Round(mousePos.y) >= 0 + (currentPlayer * 30) && (int)Mathf.Round(mousePos.y) <= boardSize + (currentPlayer * 30) && !boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponent<CellHandler>().hasShip)
        {
            return true;
        }
        return false;
    }

    public bool attack()
    {
        if (isAttackable())
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log((3 % (currentPlayer +2)) + " " + (int)Mathf.Round(mousePos.x) + " " + ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30)));
            if(boards[(3 % (currentPlayer + 2)), (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))].GetComponent<CellHandler>().hasShip)
            {
                boards[(3 % (currentPlayer + 2)), (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))].GetComponentInChildren<SpriteRenderer>().color = Color.red;
                boards[currentPlayer, (int)Mathf.Round(mousePos.x), ((int)Mathf.Round(mousePos.y) - (currentPlayer * 30))].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                GameObject.Find("TurnManager").GetComponent<TurnManager>().shipsRemaining[3 % (currentPlayer + 2)]--;
                currentPlayer = 3 % (currentPlayer + 2);
            } else
            {
                boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                currentPlayer = 3 % (currentPlayer + 2);
            }
            return true;
        }
        return false;
    }

    public bool isAttackable()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((int)Mathf.Round(mousePos.x) >= 0 && (int)Mathf.Round(mousePos.x) <= boardSize && (int)Mathf.Round(mousePos.y) >= 0 + (currentPlayer * 30) && (int)Mathf.Round(mousePos.y) <= boardSize + (currentPlayer * 30) && boards[currentPlayer, (int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y) - (currentPlayer * 30)].GetComponentInChildren<SpriteRenderer>().color != Color.grey)
        {
            return true;
        }
        return false;
    }
}
