using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public float rotationThreshold = 90f;  // Rotation threshold for switching audio clips

    public AudioClip audioClip1;  // Audio clip for condition 1
    public AudioClip audioClip2;  // Audio clip for condition 2
    public AudioClip audioClip3;  // Audio clip for condition 3

    private AudioSource audioSource;  // Audio source component


    public GameObject rotationReferenceObject;                 // Reference object for rotation calculation
    private float previousRotation;                            // Previous rotation of the reference object
    private int rotationCount;                                 // Total rotation count (360 degrees)
    private float totalRotation;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previousRotation = rotationReferenceObject.transform.rotation.eulerAngles.y; // Initialize previous rotation
    }

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


        float rotationY = totalRotation;

        // Check condition 1
        if (rotationY >= 0f && rotationY <= rotationThreshold)
        {
            audioSource.clip = audioClip1;
        }
        // Check condition 2
        else if (rotationY > rotationThreshold && rotationY <= rotationThreshold * 2)
        {
            audioSource.clip = audioClip2;
        }
        // Check condition 3
        else if (rotationY > rotationThreshold * 2 && rotationY <= rotationThreshold * 3)
        {
            audioSource.clip = audioClip3;
        }

        // Play the audio clip
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
