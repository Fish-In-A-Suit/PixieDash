using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineTrajectory : MonoBehaviour
{

    LineRenderer lr;
    public int numTrajectorySegments = 250; //the number of trajectory segments
    //test

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 dragStartPosition, Vector3 dragEndPosition, float powerAmplifier, Vector2 minPower, Vector2 maxPower, Rigidbody2D rigidbody, Transform transforms)
    {
        Vector2 force = new Vector2(
            Mathf.Clamp(dragStartPosition.x - dragEndPosition.x, minPower.x, maxPower.x),
            Mathf.Clamp(dragStartPosition.y - dragEndPosition.y, minPower.y, maxPower.y)
            );

        Vector2 velocity = force * powerAmplifier;

        Vector2[] expectedTrajectory = CalculateTrajectoryPlot(rigidbody, (Vector2)transform.position, velocity, numTrajectorySegments);
        lr.positionCount = expectedTrajectory.Length;

        Vector3[] positions = new Vector3[expectedTrajectory.Length];
        for(int i = 0; i<positions.Length; i++)
        {
            positions[i] = expectedTrajectory[i];
        }

        lr.SetPositions(positions);
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }

    private Vector2[] CalculateTrajectoryPlot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep; 

        for(int i = 0; i<steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
}
