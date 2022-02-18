using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform targetFollowed;
    public Vector3 offset;
    public float smoothingFactor = 0.125f;
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = targetFollowed.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothingFactor);
        transform.position = smoothedPosition;
        
    }
}
