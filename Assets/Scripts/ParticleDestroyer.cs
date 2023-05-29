using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float delay = 1f;  // You can set this to match the length of the animation

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}