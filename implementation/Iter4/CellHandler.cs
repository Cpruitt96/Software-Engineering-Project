using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHandler : MonoBehaviour {

    public int xValue;
    public int yValue;
    public bool hasShip;
    public bool attackAttempted;

	void Start () {
        hasShip = false;
        attackAttempted = false;
	}

    public CellHandler(int xVal, int yVal)
    {
        xValue = xVal;
        yValue = yVal;
    }
}
