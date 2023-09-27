using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOriginalPosition : MonoBehaviour
{
    public Vector2 originalPosition;
    private DestructibleTorch destructibleTorch; // Reference to the DestructibleTorch script

    private void Awake()
    {
        this.originalPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        destructibleTorch = GetComponent<DestructibleTorch>(); // Get the DestructibleTorch component
    }

    public void TorchRespawn()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = this.originalPosition;

        // Reset the torch to its original state
        if (destructibleTorch != null)
        {
            destructibleTorch.ResetTorch();
        }
        else
        {
            Debug.LogError("DestructibleTorch component not found!");
        }
    }
}