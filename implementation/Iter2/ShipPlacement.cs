using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacement : MonoBehaviour
{
    private Vector2 mousePos;

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3((int)Mathf.Round(mousePos.x), (int)Mathf.Round(mousePos.y), -1);
        if (Input.GetMouseButtonDown(0))
        {
            if (GameObject.Find("Board").GetComponent<BoardHandler>().isPlaceable())
            {
                GameObject.Find("Board").GetComponent<BoardHandler>().placeShip();
                Destroy(GetComponent<ShipPlacement>());
            } 
        }
        if (Input.GetMouseButtonDown(1))
        {
            float zRotation = 90.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - zRotation);
        }
    }


}