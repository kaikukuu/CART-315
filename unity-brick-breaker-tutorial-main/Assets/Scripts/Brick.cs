using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);
    }

    // Flash the brick by toggling its SpriteRenderer on and off for a specified duration
    public IEnumerator Flash(float duration)
    {
        if (spriteRenderer == null)
        {
            Debug.LogError($"Brick '{name}' needs a SpriteRenderer to flash.", this);
            yield break;
        }

        bool wasEnabled = spriteRenderer.enabled;
        float interval = duration / 6f;

        // toggle the sprite renderer on and off
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
        }

        spriteRenderer.enabled = wasEnabled;
    }
}
