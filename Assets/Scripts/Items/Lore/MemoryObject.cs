using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // Don't forget to add this namespace to access Light2D properties

public class MemoryObject : MonoBehaviour
{
    public Memories MemoryData; // The data for this memory
    private bool playerInRange = false; // To check if the player is in range to collect the memory

    private PlayerInputHandler playerInputHandler;
    private MemoriesManager memoriesManager;

    public float rotationSpeed = 60f; // Speed of rotation, adjust this value as needed
    public float floatSpeed = 0.5f; // Speed of floating, adjust this value as needed
    public float floatHeight = 0.3f; // Height of floating, adjust this value as needed

    public float maxOuterRadius = 2.27f; // Max outer radius of the light
    public float minOuterRadius = 0.1f; // Min outer radius of the light, adjust as needed
    public float maxIntensity = 0.27f; // Max intensity of the light
    public float minIntensity = 0.1f; // Min intensity of the light, adjust as needed
    public float lightPulseSpeed = 1.0f; // Speed of the light pulsing effect, adjust as needed

    private Vector3 initialPosition;
    private Light2D pointLight; // Reference to the Light2D component

    private void Start()
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        memoriesManager = FindObjectOfType<MemoriesManager>();

        if (MemoryData == null)
        {
            Debug.LogError("MemoryData is not assigned on " + gameObject.name);
        }
        else
        {
            // Check if this memory has been collected and deactivate if necessary
            if (memoriesManager.IsMemoryCollected(MemoryData.ID, MemoryData.Type))
            {
                gameObject.SetActive(false);
            }
        }

        initialPosition = transform.position;
        pointLight = GetComponentInChildren<Light2D>(); // Get the Light2D component from the child objects
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (playerInputHandler != null)
            {
                if (playerInputHandler.InteractionInput)
                {
                    //Debug.Log("Interaction input detected");
                    CollectMemory();
                    playerInputHandler.UseInteractionInput(); // Reset the interaction input
                }
                else
                {
                    //Debug.Log("Interaction input not detected");
                }
            }
            else
            {
                //Debug.Log("PlayerInputHandler not found");
            }
        }

        // Rotate the object constantly and make it float up and down
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        transform.position = initialPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);

        // Create a pulsing light effect
        if (pointLight != null)
        {
            float pulse = Mathf.PingPong(Time.time * lightPulseSpeed, maxOuterRadius - minOuterRadius) + minOuterRadius;
            pointLight.pointLightOuterRadius = pulse;
            pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, (pulse - minOuterRadius) / (maxOuterRadius - minOuterRadius));
        }
    }
    

    private void CollectMemory()
    {
        if (memoriesManager != null && MemoryData != null)
        {
            if (!memoriesManager.IsMemoryCollected(MemoryData.ID, MemoryData.Type)) // Updated this line
            {
                Debug.Log("Memory collected");
                memoriesManager.AddMemory(MemoryData);
                gameObject.SetActive(false); // Deactivate this GameObject to indicate the memory has been collected
            }
            else
            {
                Debug.Log("Memory already collected");
            }
        }
        else
        {
            if (memoriesManager == null)
            {
                Debug.Log("MemoriesManager not found");
            }
            if (MemoryData == null)
            {
                Debug.Log("MemoryData is null");
            }
        }
    }
}