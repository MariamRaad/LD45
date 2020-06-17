using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxesManager : MonoBehaviour
{
    public bool startsLeft;
    public bool startsRight;
    public bool destroyed;

    public TaxesController tc;

    // Start is called before the first frame update
    void Start()
    {
        startsLeft = true;
        startsRight = false;
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        destroyed = tc.isDestroyed;

        if (destroyed)
        {
            //Debug.Log("obj is destroyed");
            ToggleStartPosition();
            //Debug.Log("right " + startsRight);
            //Debug.Log("left " + startsLeft);
            destroyed = false;
            tc.isDestroyed = false;
        }
    }

    private void ToggleStartLeft()
    {
        if (startsLeft)
        {
            startsLeft = false;
        }
        else if (startsLeft == false)
        {
            startsLeft = true;
        }
    }

    private void ToggleStartRight()
    {
        if (startsRight)
        {
            startsRight = false;
        }
        else if (startsRight == false)
        {
            startsRight = true;
        }
    }

    private void ToggleStartPosition()
    {
        ToggleStartLeft();
        ToggleStartRight();

        /*
        if (startsLeft)
        {
            ToggleStartLeft(); //false
            ToggleStartRight(); //true
        }
        else if (startsRight)
        {
            ToggleStartLeft(); //true
            ToggleStartRight(); //false
        }
        */
    }
}