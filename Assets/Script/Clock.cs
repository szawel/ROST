using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public GameObject rotationReferenceObject;                 // Reference object for rotation calculation
    private float previousRotation;                            // Previous rotation of the reference object
    private int rotationCount;                                 // Total rotation count (360 degrees)
    private float totalRotation;                               // Total rotation of the reference object

    // public GameObject objectToToggle;
    // Start is called before the first frame update
    void Start()
    {
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Initialize previous rotation
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = rotationReferenceObject.transform.rotation.eulerAngles.y;
        float rotationDelta = currentRotation - previousRotation;
        if (rotationDelta < -180f)
            rotationDelta += 360f;                                // Handle rotation wraparound
        else if (rotationDelta > 180f)
            rotationDelta -= 360f;

        totalRotation += rotationDelta;                            // Accumulate total rotation
        rotationCount = Mathf.FloorToInt(totalRotation / 360f);    // Calculate rotation count
        previousRotation = currentRotation;

        if (rotationCount==0)
        {
            rotationCount++;
        }


        transform.Rotate(Vector3.up, Time.deltaTime*100);

        //Debug.Log("Current Rotation: " + rotationCount*2);
    }
}
