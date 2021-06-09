using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Patrol : MonoBehaviour
{
    public Tilemap tilemap;

    bool hasDestination = false;
    bool hasArrived = false;

    Vector3 destination;
    List<Vector2> path;

    public int enemyPathingRadius = 5; //this radius is used when finding the next random point to which the enemy should move
    public float randomLocationSphereCollisionPreventionRadius = 0.3f; //this is the radius of the sphere used to check if randomly located point is colliding with other world objects

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(!hasDestination)
        {
            destination = computeDestination(findRandomLocation(transform, enemyPathingRadius));
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), destination, Color.red, 5, false);

            path = computePath(transform.position, destination);

            hasDestination = true;
        }
        
        if(hasDestination && !hasArrived)
        {
            //walk
            
        }
    }

    private Vector3 computeDestination(Vector2 randomLocation)
    {

        while(Physics2D.Raycast(randomLocation, Vector2.down) == null)
        {
            //random location is not above existing tilemap, therefore repeat search
            computeDestination(findRandomLocation(transform, enemyPathingRadius));
        }

        RaycastHit2D validRaycast = Physics2D.Raycast(randomLocation, Vector2.down);
        Vector3 position = validRaycast.collider.transform.position;
        //todo: draw sphere to check
        position.y += 0.6f;
        return position;
    }

    private Vector2 findRandomLocation(Transform origin, float radius)
    {
        //pick random spot in sphere around enemy transform
        Vector2 xSearchRange = new Vector2(origin.position.x - enemyPathingRadius, origin.position.x + enemyPathingRadius);
        Vector2 ySearchRange = new Vector2(origin.position.y - enemyPathingRadius, origin.position.y + enemyPathingRadius);

        float randomX = Random.Range(xSearchRange.x, xSearchRange.y);
        float randomY = Random.Range(ySearchRange.x, ySearchRange.y);

        //check if the point is colliding with any object in the scene
        while (Physics2D.OverlapCircle(new Vector2(randomX, randomY), randomLocationSphereCollisionPreventionRadius) != null)
        {
            bool xOrY = Random.Range(0, 1) <= 0.5;
            if (xOrY)
            {
                //increment x direction
                randomX += 1.5f;
            }
            else
            {
                //increment y direction
                randomY += 1.5f;
            }
        }

        //this location is anywhere in the game world without being inside a tilemap
        return new Vector2(randomX, randomY);
    }

    /*
     * Takes two coordinates and produces a path between them which avoids obstacles
     * */
    //test
    private List<Vector2> computePath(Vector3 origin, Vector3 destination)
    {
        List<Vector2> path = new List<Vector2>();
        Vector2 direction = destination - origin;
        RaycastHit2D raycast = Physics2D.Raycast(origin, destination);

        if(raycast == null)
        {
            //no obstacle, straight path
            path.Add(origin);
            path.Add(destination);
            return path;
        } else
        {
            //raycast.point;
            //todo: implement A* pathfinding
            return null;
        }
        
        
    }
}
