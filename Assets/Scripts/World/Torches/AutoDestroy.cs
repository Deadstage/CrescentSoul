using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 2.7f;  // Time in seconds before the object is destroyed

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}