using UnityEngine;

public class CameraRotationTracker : MonoBehaviour
{
    private float cameraRotation;

    private void Update()
    {
        // Get the rotation of the camera
        Quaternion rotation = transform.rotation;

        // Convert the rotation to Euler angles and take the absolute value of the y-axis rotation
        float rotationY = Mathf.Abs(rotation.eulerAngles.y);

        // Update the camera rotation value
        cameraRotation = rotationY;


        // Use the cameraRotation value as needed
        // ...
    }

    // Example method to demonstrate accessing the cameraRotation value from another script
    public float GetCameraRotation()
    {
        return cameraRotation;
    }
}
