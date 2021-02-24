using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; //the object which the camera should follow

    public float camSmoothModifier = 0.125f; //if smooth modifier is 1, the camera moves the entire position change in one frame, if 0.5, then half way etc

    public Vector3 offset = new Vector3(0, 0, -0.5f);

    /**
     * LateUpdate is called after all Update functions have been called. 
     * This is useful to order script execution. 
     * For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
     * */
    private void LateUpdate()
    {
        Vector3 rawCameraPos = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, rawCameraPos, camSmoothModifier*Time.deltaTime);
        //multiplying with delta time to make sure smoothing occurs at the same rate no matter the fps
        
        transform.position = target.position + offset;
    }
}
