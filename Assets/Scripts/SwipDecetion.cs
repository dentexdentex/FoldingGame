using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipDecetion : MonoBehaviour
{
    public  Folding2 player;
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;

    void Update()
    {
        //testing for pc



        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }
        if (fingerDown)
        {
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                player.MoveUp();
                Debug.Log("swipe up");
            }
        }
        if(fingerDown&& Input.GetMouseButtonUp(0))
        {
            fingerDown = false; 
        }

    }
}
