using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * This scripts stops a moving player when collided
 * */
public class MovementStopper : MonoBehaviour
{
    public Rigidbody2D player;
    public Tilemap tilemap;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = (float)(hit.point.x - 0.01 * hit.normal.x);
            hitPosition.y = (float)(hit.point.y - 0.01f * hit.normal.y);

            int tileX = tilemap.WorldToCell(hitPosition).x;
            int tileY = tilemap.WorldToCell(hitPosition).y;
            //double tileCenterx = tileX + 0.5;
            //double tileCenterY = tileY + 0.5;

            if(heightCheck((int)player.position.y, tileY) && widthCheck((int)player.position.x, tileX))
            {
                player.velocity = Vector3.zero;
                player.angularVelocity = 0;
            }
        }
    }

    /*
     * Returns true if the player's y coordinate is greater than the center of the tile
     * */
    private bool heightCheck(int playerY, int tileY)
    {
        double tileCenterY = tileY + 0.5;

        if (playerY > tileCenterY)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /*
     * Checks if the width is between tileX and tileX+1 (false if it is a "side" collision
     * */
    private bool widthCheck(int playerX, int tileX)
    {
        //Debug.Log("playerX = " + playerX + ", tileX = " + tileX + ", tileX+1 = " + (tileX + 1));
        if(playerX >= tileX && playerX <= (tileX+1))
        {
            return true;
        } else
        {
            return false;
        }
    }


}
