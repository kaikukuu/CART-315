using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{

    private Rigidbody2D _rigidBody;

    public float speed = 100.0f;
    public Vector2 direction;

    // Before the Game starts
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            direction = Vector2.up;

        else if (Keyboard.current.sKey.isPressed)
            direction = Vector2.down;

    }
    //Method so 
    private void FixedUpdate()

    {
        if (direction.sqrMagnitude == 0) return;

        _rigidBody.AddForce(direction * speed);
    }
}
