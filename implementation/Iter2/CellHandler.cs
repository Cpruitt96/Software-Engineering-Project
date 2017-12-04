using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHandler : MonoBehaviour {

    public int xValue;
    public int yValue;
    public bool hasShip;

	// Use this for initialization
	void Start () {
        hasShip = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public CellHandler(int xVal, int yVal)
    {
        xValue = xVal;
        yValue = yVal;
    }
}
