using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestructor : MonoBehaviour
{
    public AnimatedTile destroyAnimationTile;
    public bool isDestructorActive = true;


    public Tilemap tilemap;
    public int tileDestructDelay = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        if(tilemap != null && isDestructorActive)
        {
            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = (float)(hit.point.x - 0.01 * hit.normal.x);
                hitPosition.y = (float)(hit.point.y - 0.01f * hit.normal.y);
                //tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), destroyAnimationTile);
                StartCoroutine(DestroyTile(hitPosition));
            }
        }
    }

    IEnumerator DestroyTile(Vector3 hitPosition)
    {
        //tilemap.SetTile(tilemap.WorldToCell(hitPosition), test);
        //play sound here
        yield return new WaitForSeconds(tileDestructDelay);
        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
    }
}
