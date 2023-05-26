using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private float turnSpeed = 45f;
    [SerializeField] private float maxRotationAngle = 90f;
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float maxScale = 1.2f;

    public float Key5 = 5f;

    public GameObject rotationReferenceObject;                 // Reference object for rotation calculation
    private float previousRotation;                            // Previous rotation of the reference object
    private int rotationCount;                                 // Total rotation count (360 degrees)
    private float totalRotation;

    public List<GameObject> wavpoints;
    //public float speed = 2f;
    public int index = 0;
    public bool isLoop = true;

    private Animator animator;

    private Vector3 previousPosition;
    private float velocity;

    public GameObject objectToToggle;                          // Object to toggle on and off

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        RandomizeScale();
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Initialize previous rotation
    }

    // Update is called once per frame
    //    void Update()
    //    {
    //        var velocity = Vector3.forward * Input.GetAxis("Vertical") * speed;
    //        transform.Translate(velocity * Time.deltaTime);
    //        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed );
    //        animator.SetFloat("Speed", velocity.z);
    //    }

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


        Vector3 destination = wavpoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        //var velocity = Vector3.forward * speed;

        // Calculate rotation towards the destination
        Quaternion targetRotation = Quaternion.LookRotation(destination - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Smoothly rotate towards the target rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationAngle * Time.deltaTime);
        transform.rotation = newRotation;

        // Calculate velocity
        velocity = Vector3.Distance(transform.position, previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        transform.position = newPos;
        animator.SetFloat("Speed", velocity);

        float distance = Vector3.Distance(transform.position, destination);

        if(distance <= 0.5)
        {
            if(index < wavpoints.Count - 1)
            {
                index++;
            }
            else
            {
                if (isLoop)
                {
                    index = 0;
                }
            }
        }
        // Toggle the object on and off based on totalRotation
        if (totalRotation < Key5)
        {
            objectToToggle.SetActive(false); // Turn off the object
        }
        else
        {
            objectToToggle.SetActive(true); // Turn on the object
        }

    }
    void RandomizeScale()
    {
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}
