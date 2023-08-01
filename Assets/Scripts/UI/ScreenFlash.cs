using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashSpeed = 2f;
    public float flashAlpha = 0.8f;

    private void Start()
    {
        // ensure the image is disabled at start
        flashImage.gameObject.SetActive(false);
    }

    public void FlashScreen()
    {
        // enable the image before starting the flash
        flashImage.gameObject.SetActive(true);
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        Color flashColor = flashImage.color;
        flashColor.a = flashAlpha;
        flashImage.color = flashColor;

        while (flashImage.color.a > 0f)
        {
            flashColor.a -= flashSpeed * Time.deltaTime;
            flashImage.color = flashColor;
            yield return null;
        }

        // disable the image after the flash has completed
        flashImage.gameObject.SetActive(false);
    }
}