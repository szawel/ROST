using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revealed : MonoBehaviour
{

    public float Key1 = 1f;

    public GameObject rotationReferenceObject;                 // Reference object for rotation calculation
    private float previousRotation;                            // Previous rotation of the reference object
    private int rotationCount;                                 // Total rotation count (360 degrees)
    private float totalRotation;                               // Total rotation of the reference object

    public GameObject objectToToggle;


    // Start is called before the first frame update
    void Start()
    {
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Initialize previous rotation
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Get current rotation
        float rotationDelta = currentRotation - previousRotation;                           // Calculate rotation change

        if (rotationDelta < -180f)
            rotationDelta += 360f;                                // Handle rotation wraparound
        else if (rotationDelta > 180f)
            rotationDelta -= 360f;

        totalRotation += rotationDelta;                            // Accumulate total rotation
        rotationCount = Mathf.FloorToInt(totalRotation / 360f);    // Calculate rotation count
        previousRotation = currentRotation;

        //Debug.Log("Current Rotation: " + totalRotation);

        // Toggle the object on and off based on totalRotation
        if (totalRotation >= Key1)
        {
            objectToToggle.SetActive(false); // Turn off the object
        }
        else
        {
            objectToToggle.SetActive(true); // Turn on the object
        }
    }
}
