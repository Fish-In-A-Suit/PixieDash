using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieController : MonoBehaviour
{

    public float slingshotPower = 10f;
    public Rigidbody2D rb; //rigidbody of the player
    LineTrajectory lineTrajectory;

    //start and endpoint are clamped between minpower and maxpower
    public Vector2 minPower;
    public Vector2 maxPower;

    Vector3 dragStartPosition; //the point where player pressess LMB first
    Vector3 dragEndPosition; //the point where player releases lmb

    Camera cam; //used for converting mouse position from screen location to world location
    Vector2 force; //the final fore that will be applied to the player

    public float velocityThreshold = 1.5f; //if final velocity is lower than velocityThreshold, it is not applied. This is to prevent minimal movements that result in glitching camera.

    public AudioClip jumpSound;
    public float jumpSoundVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineTrajectory = GetComponent<LineTrajectory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragStartPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            dragStartPosition.z = 15;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;

            lineTrajectory.RenderLine(dragStartPosition, currentPoint, slingshotPower, minPower, maxPower, rb, transform);
        }

        if(Input.GetMouseButtonUp(0))
        {
            lineTrajectory.EndLine();

            dragEndPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            dragEndPosition.z = 15;

            force = new Vector2(
                Mathf.Clamp(dragStartPosition.x - dragEndPosition.x, minPower.x, maxPower.x),
                Mathf.Clamp(dragStartPosition.y - dragEndPosition.y, minPower.y, maxPower.y)
                );

            AudioSource.PlayClipAtPoint(jumpSound, transform.position, jumpSoundVolume);
            Vector2 velocity = force * slingshotPower;

            //rb.AddForce(force * slingshotPower, ForceMode2D.Impulse);

            if(velocity.magnitude > velocityThreshold)
            {
                 rb.velocity = velocity;
            }
           
        }
    }
}
