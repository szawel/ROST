using UnityEngine;

public class CameraRotationCounter : MonoBehaviour
{
    private float totalRotation;
    private float previousRotation;
    private int rotationCount;

    private void Start()
    {
        // Set the initial rotation of the camera as the previous rotation
        previousRotation = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        // Get the current rotation of the camera
        float currentRotation = transform.rotation.eulerAngles.y;

        // Calculate the difference between the current and previous rotations
        float rotationDelta = currentRotation - previousRotation;

        // Handle cases where the rotation wraps around from 360 to 0 or vice versa
        if (rotationDelta < -180f)
        {
            rotationDelta += 360f;
        }
        else if (rotationDelta > 180f)
        {
            rotationDelta -= 360f;
        }

        // Add the rotation delta to the total rotation
        totalRotation += rotationDelta;

        // Calculate the rotation count based on the total rotation
        rotationCount = Mathf.FloorToInt(totalRotation / 360f);

        // Update the previous rotation value
        previousRotation = currentRotation;

        


    //Debug.Log("Total Rotation: " + totalRotation + "Rotation Count: ");
    // Debug.Log("Rotation Count: " + rotationCount);
    // Debug.Log("Previous Rotation: " + previousRotation);


    // Use the totalRotation and rotationCount values as needed
    // ...
}

    // Example method to demonstrate accessing the rotationCount value from another script
    public int GetRotationCount()
    {
        return rotationCount;
        //public float myVariable = totalRotation;
}
}
