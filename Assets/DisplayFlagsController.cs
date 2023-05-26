using System.Collections;
using UnityEngine;


public class DisplayFlagsController : MonoBehaviour
{
    public GameObject objectToControl; // The game object whose display flags will be controlled
    public GameObject rotationReferenceObject; // The game object whose rotation will be used as a reference
    private float previousRotation;
    private int rotationCount;
    private float totalRotation;

    public Texture texture1; // The first texture
    public Texture texture2; // The second texture
    public Texture texture3; // The third texture

    public float Key1 = 1f;
    public float Key2 = 2f;
    public float Key3 = 3f;
    public float Key4 = 4f;
    public float Key5 = 5f;

    private Renderer objectRenderer; // The renderer component of the objectToControl

    private void Start()
    {
        objectRenderer = objectToControl.GetComponent<Renderer>();

        // Set the initial rotation of the camera as the previous rotation
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y;

    }

    private void Update()
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

        //Debug.Log("total rotation: " + totalRotation);

        // Determine which texture to apply based on the normalized rotation value
        if (totalRotation < Key2)
        {
            objectRenderer.material.mainTexture = texture1;
        }
        else if (totalRotation < Key3)
        {
            objectRenderer.material.mainTexture = texture2;
        }
        else if (totalRotation < Key4)
        {
            objectRenderer.material.mainTexture = texture3;
        }



        //else
        //{
        //    objectRenderer.material.mainTexture = texture3;
        //}
    }
}