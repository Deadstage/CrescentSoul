using System.Collections;
using UnityEngine;

public class DestructibleTorch : MonoBehaviour, IDamageable
{
    public GameObject destructionParticles;
    public float destructionForce = 5f;
    public GameObject pointLight2D;
    public GameObject torchSoundAmbience;
    public SpriteRenderer torchSprite;
    public ItemDrop itemDrop;

    private void OnEnable()
    {
        if (itemDrop == null)
        {
            itemDrop = GameObject.Find("ItemDropManager").GetComponent<ItemDrop>();
        }
    }

    public void Damage(float amount)
    {
        DestroyTorch();
    }

    private void DestroyTorch()
    {
        pointLight2D.SetActive(false);
        torchSoundAmbience.SetActive(false);
        torchSprite.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        GameObject particles = Instantiate(destructionParticles, transform.position, Quaternion.identity);
        foreach (Transform child in particles.transform)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Random.Range(-destructionForce, destructionForce), Random.Range(-destructionForce, destructionForce)), ForceMode2D.Impulse);
            }
        }

        itemDrop.DropItem(transform.position, "Coin");

        float rand = Random.Range(0f, 1f);
        if (rand < 0.01f)  // 1% chance
        {
            itemDrop.DropItem(transform.position, "LargeHealth");
        }
        else if (rand < 0.04f)  // 3% chance
        {
            itemDrop.DropItem(transform.position, "MediumHealth");
        }
        else if (rand < 0.1f)  // 6% chance
        {
            itemDrop.DropItem(transform.position, "SmallHealth");
        }
        // 90% chance to drop nothing
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
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void DeactivateTorch()
    {
        gameObject.SetActive(false);
    }
}