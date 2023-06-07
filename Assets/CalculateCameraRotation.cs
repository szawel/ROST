using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCameraRotation : MonoBehaviour
{
    public float camRotation = 0f;
    public float camTotalRotation = 0f;
    public int camCountRotation = 0;

    public GameObject rotationReferenceObject; // The game object whose rotation will be used as a reference
    private float previousRotation;
    private int rotationCount;
    private float totalRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial rotation of the camera as the previous rotation
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        // get the current rotation of the camera
        float currentRotation = rotationReferenceObject.transform.rotation.eulerAngles.y;

        // Calculate the difference between the current and previous rotations
        float rotationDelta = currentRotation - previousRotation;

        // handle cases where the rotation wraps around from 360 to 0 or vice versa
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

        // Update publica veriables
        camRotation = currentRotation;
        camTotalRotation = totalRotation;
        camCountRotation = rotationCount;

    }
}
