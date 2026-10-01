using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public float speed = 100.0f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ResetBall()
    {
        _rigidBody.linearVelocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        transform.position = Vector3.zero;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = (Random.value < 0.5f ? -1.0f : 1.0f) * Random.Range(0.5f, 0.9f);

        Vector2 direction = new Vector2(x, y);

        _rigidBody.AddForce(direction * speed);
    }

// Adds a boost to the ball's spped
    public void AddTurboBoost(float boostMultiplier)
    {
        _rigidBody.linearVelocity *= boostMultiplier;
    }

    // Inverts the ball's direction
    public void ResetBallDirection()
    {
        _rigidBody.linearVelocity = new Vector2(-_rigidBody.linearVelocity.x, -_rigidBody.linearVelocity.y);
    }

    public void ResetBallSpeed()
    {
        _rigidBody.linearVelocity = _rigidBody.linearVelocity.normalized * speed;
    }

    // Prevents the ball from moving too slowly in the x direction after a collision with a paddle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // make sure the ball's x velocity is at least 30% of its total speed after hitting a paddle
        Vector2 velocity = _rigidBody.linearVelocity;
        float minX = velocity.magnitude * 0.3f;

        // If the ball's x velocity is too small, adjust it to ensure it doesn't get stuck moving vertically
        if (Mathf.Abs(velocity.x) < minX)
        {
            // make the x velocity at least minX, keeping the same direction
            velocity.x = velocity.x < 0 ? -minX : minX;
            velocity.y = Mathf.Sign(velocity.y) *
                         Mathf.Sqrt(velocity.sqrMagnitude - minX * minX);
            _rigidBody.linearVelocity = velocity;
        }
    }
}