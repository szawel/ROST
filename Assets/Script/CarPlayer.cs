using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2f;                 // Movement speed of the car
    [SerializeField] private float turnSpeed = 45f;           // Rotation speed of the car
    [SerializeField] private float maxRotationAngle = 90f;    // Maximum rotation angle for the car

    [SerializeField] private float minScale = 0.8f;           // Minimum scale of the car
    [SerializeField] private float maxScale = 1.2f;           // Maximum scale of the car

    public List<GameObject> waypoints;                        // List of waypoints for the car's movement
    public int index = 0;                                      // Current index of the waypoint
    public bool isLoop = true;                                 // Flag indicating if the car should loop through waypoints

    private Animator animator;                                 // Animator component for controlling animations

    private Vector3 previousPosition;                          // Previous position of the car for velocity calculation
    private float velocity;                                    // Velocity of the car

    public float Key1 = 1f;                                    // Key value for speed adjustment
    public float Key2 = 2f;                                    // Key value for speed adjustment
    public float Key3 = 3f;                                    // Key value for speed adjustment
    public float Key4 = 4f;                                    // Key value for speed adjustment
    public float Key5 = 5f;                                    // Key value for speed adjustment

    public GameObject rotationReferenceObject;                 // Reference object for rotation calculation
    private float previousRotation;                            // Previous rotation of the reference object
    private int rotationCount;                                 // Total rotation count (360 degrees)
    private float totalRotation;                               // Total rotation of the reference object

    public GameObject objectToToggle;                          // Object to toggle on and off

    private void Start()
    {
        animator = GetComponent<Animator>();                   // Get the Animator component of the car
        RandomizeScale();                                      // Randomize the scale of the car
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Initialize previous rotation
    }

    private void FixedUpdate()
    {
        float currentRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Get current rotation
        float rotationDelta = currentRotation - previousRotation;                           // Calculate rotation change

        if (rotationDelta < -180f)
            rotationDelta += 360f;                                // Handle rotation wraparound
        else if (rotationDelta > 180f)
            rotationDelta -= 360f;

        totalRotation += rotationDelta;                            // Accumulate total rotation
        rotationCount = Mathf.FloorToInt(totalRotation / 360f);    // Calculate rotation count
        previousRotation = currentRotation;                        // Update previous rotation

        // Adjust speed based on total rotation
        if (totalRotation < Key2)
            speed = 9f;
        else if (totalRotation < Key3)
            speed = 7f;
        else if (totalRotation < Key4)
            speed = 0f;

        Vector3 destination = waypoints[index].transform.position;                     // Get the destination waypoint
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime); // Move towards the destination

        Quaternion targetRotation = Quaternion.LookRotation(destination - transform.position); // Calculate rotation towards the destination
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); // Smoothly rotate towards the target rotation

        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationAngle * Time.deltaTime); // Smoothly rotate towards the target rotation with maximum angle
        transform.rotation = newRotation;                        // Apply the new rotation

        velocity = Vector3.Distance(transform.position, previousPosition) / Time.deltaTime; // Calculate velocity
        previousPosition = transform.position;                   // Update previous position

        transform.position = newPos;                              // Update the position
        animator.SetFloat("Speed", velocity);                     // Set the animation speed based on velocity

        float distance = Vector3.Distance(transform.position, destination); // Calculate distance to the destination waypoint

        if (distance <= 0.5)
        {
            // Move to the next waypoint or loop back to the first one
            if (index < waypoints.Count - 1)
                index++;
            else
            {
                if (isLoop)
                    index = 0;
            }
        }

        // Toggle the object on and off based on totalRotation
        if (totalRotation <= Key5)
        {
            objectToToggle.SetActive(false); // Turn off the object
        }
        else
        {
            objectToToggle.SetActive(true); // Turn on the object
        }
    }

    private void RandomizeScale()
    {
        float randomScale = Random.Range(minScale, maxScale);     // Generate a random scale within the specified range
        transform.localScale = new Vector3(randomScale, randomScale, randomScale); // Apply the randomized scale to the car
    }
}
