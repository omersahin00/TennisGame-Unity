using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurfaceScript : MonoBehaviour
{
    private SideEnum side;

    void Start()
    {
        if (gameObject.name.Contains("Front")) side = SideEnum.Front;
        else if (gameObject.name.Contains("Back")) side = SideEnum.Back;
        else if (gameObject.name.Contains("Right")) side = SideEnum.Right;
        else if (gameObject.name.Contains("Left")) side = SideEnum.Left;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            switch(side)
            {
                case SideEnum.Front:
                    GameStatics.runFront = false;
                    break;

                case SideEnum.Back:
                    GameStatics.runBack = false;
                    break;

                case SideEnum.Right:
                    GameStatics.runRight = false;
                    break;

                case SideEnum.Left:
                    GameStatics.runLeft = false;
                    break;
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            switch (side)
            {
                case SideEnum.Front:
                    GameStatics.runFront = true;
                    break;

                case SideEnum.Back:
                    GameStatics.runBack = true;
                    break;

                case SideEnum.Right:
                    GameStatics.runRight = true;
                    break;

                case SideEnum.Left:
                    GameStatics.runLeft = true;
                    break;
            }
        }
    }
}
