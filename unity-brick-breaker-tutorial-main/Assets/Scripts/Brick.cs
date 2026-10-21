using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];
    public int points = 100;
    public bool unbreakable;

    private SpriteRenderer spriteRenderer;
    private int health;

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

        if (!unbreakable)
        {
            health = states.Length;
            spriteRenderer.sprite = states[health - 1];
        }
    }
    public void FlashBrick()
    {
        // turn on and off the spcific indicated brick to indicate itself as the next in the pattern
        //repeat until all the steps in the pattern have been shown
        //then indicate the player's turn
        //use predetermined flashing patterns for each level of the game    
    }

    private void Hit()
    {
        if (unbreakable)
        {
            return;
        }

        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = states[health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }

}
