using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashSpeed = 2f;
    public float flashAlpha = 0.8f;

    public void FlashScreen()
    {
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
    }
}