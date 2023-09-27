using System.Collections;
using UnityEngine;

public class DestructibleTorch : MonoBehaviour, IDamageable
{
    public GameObject destructionParticles;
    public float destructionForce = 5f;
    public GameObject pointLight2D; // Reference to the Point Light 2D child GameObject
    public GameObject torchSoundAmbience; // Reference to the Torch Sound Ambience child GameObject
    public SpriteRenderer torchSprite; // Reference to the SpriteRenderer component on the main GameObject

    public void Damage(float amount)
    {
        DestroyTorch();
    }

    private void DestroyTorch()
    {
        // Deactivate visual and auditory components
        pointLight2D.SetActive(false);
        torchSoundAmbience.SetActive(false);
        torchSprite.enabled = false;

        // Instantiate the destruction particles and apply force
        GameObject particles = Instantiate(destructionParticles, transform.position, Quaternion.identity);
        foreach (Transform child in particles.transform)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Random.Range(-destructionForce, destructionForce), Random.Range(-destructionForce, destructionForce)), ForceMode2D.Impulse);
            }
        }

        // Start the coroutine to despawn the pieces
        StartCoroutine(DespawnPieces(particles));

        // Schedule the deactivation of the main torch GameObject
        Invoke("DeactivateTorch", 3f);
    }

    private IEnumerator DespawnPieces(GameObject particles)
    {
        yield return new WaitForSeconds(2.7f);
        Destroy(particles);
    }

    public void ResetTorch()
    {
        // Reactivate the visual and auditory components
        pointLight2D.SetActive(true);
        torchSoundAmbience.SetActive(true);
        torchSprite.enabled = true;
    }

    private void DeactivateTorch()
    {
        gameObject.SetActive(false);
    }
}