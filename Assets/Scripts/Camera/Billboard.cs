using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Get the main camera
        Camera cam = Camera.main;
        if (cam == null) return;
        
        // Make the speech bubble face the camera.
        // First, rotate the speech bubble to look at the camera.
        // This approach ensures the speech bubble is always oriented to the camera's up direction.
        // It rotates the speech bubble to look in the same forward direction as the camera,
        // but with its own up aligned with the camera's up vector.
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, 
                         cam.transform.rotation * Vector3.up);
    }
}