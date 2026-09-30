using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public float speed = 10.0f;

    public Vector2 direction;
    public bool isFrozen;
    public bool isInverted;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per physics update
    private void FixedUpdate()
    {
        if (direction.sqrMagnitude == 0) return;

        _rigidBody.AddForce(direction * speed);
    }

    public void Update()
    {
        if (isFrozen)
        {
            _rigidBody.linearVelocity = Vector2.zero;
        }
        else if (isInverted)
        {
            _rigidBody.linearVelocity = -_rigidBody.linearVelocity;
        }
        else
        {
            // normal behavior, do nothing
        }
    }
}