using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject smallHealthPrefab;
    public GameObject mediumHealthPrefab;
    public GameObject largeHealthPrefab;
    public GameObject coinPrefab;

    [SerializeField] private Sprite[] coinSprites;  // Array to hold your coin sprites

    public void DropItem(Vector3 position, string itemType)
    {
        GameObject itemToDrop = null;

        switch (itemType)
        {
            case "SmallHealth":
                itemToDrop = smallHealthPrefab;
                break;
            case "MediumHealth":
                itemToDrop = mediumHealthPrefab;
                break;
            case "LargeHealth":
                itemToDrop = largeHealthPrefab;
                break;
            case "Coin":
                itemToDrop = coinPrefab;
                break;
        }

        if (itemToDrop != null)
        {
            GameObject spawnedItem = Instantiate(itemToDrop, position, Quaternion.identity);
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

            if (rb != null && itemType == "Coin")  // Apply physics and random sprite only to coins
            {
                // Randomly select a sprite for the coin
                SpriteRenderer sr = spawnedItem.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    int randomIndex = Random.Range(0, coinSprites.Length);
                    sr.sprite = coinSprites[randomIndex];
                }

                // Apply random physics
                float randomForceX = Random.Range(-5f, 5f);
                float randomForceY = Random.Range(2f, 5f);
                Vector2 randomForce = new Vector2(randomForceX, randomForceY);

                float randomTorque = Random.Range(-500f, 500f);

                rb.AddForce(randomForce, ForceMode2D.Impulse);
                rb.AddTorque(randomTorque);
            }
        }
    }
}