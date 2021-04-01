using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script should be attached to enemies. handles firing at the player
public class Aggressor : MonoBehaviour
{
    Transform player;
    public LayerMask raycastIgnoreLayer; //raycast should ignore the enemy!

    // Start is called before the first frame update
    void Start()
    {
        //find player
        player = GameObject.Find("player-body1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerVisible();
    }

    bool isPlayerVisible()
    {
        Vector2 rayDirection = player.position - transform.position;
        Debug.DrawLine(player.position, transform.position, Color.green, 2, false); //enable gizmos if line isn't drawn
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, rayDirection, 1000, ~raycastIgnoreLayer); //~ inverts the layer mask!
        Debug.Log(rayHit.collider.name);
        if(rayHit.collider != null)
        {
            if(rayHit.collider.name.Equals("player-body1")) {
                Debug.Log("player visible");
                return true;
            } else
            {
                Debug.Log("player not visible");
                return false;
            }
        } else
        {
            Debug.Log("player not visible");
            return false;
        }
    }
}
