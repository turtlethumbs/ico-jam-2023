using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;
    private bool isFlashing = false;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }
    }

    public void ResetFlash()
    {
        StopAllCoroutines();

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }

        isFlashing = false;
    }

    public void TakeDamage()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        isFlashing = true;

        // Flash the enemy sprite red
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = damageColor;
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Restore the original colors
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }

        isFlashing = false;
    }
}