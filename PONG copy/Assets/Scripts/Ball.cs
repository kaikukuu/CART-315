using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D _rigidBody;

    public float speed = 100.0f;
    // Before the Game starts
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        //Adding force to the ball
        float x = 0.0f;
        float y = 0.0f;

        if (Random.value < 0.5f) x = -1.0f;
        else x = 1.0f;

        if (Random.value < 0.5f) y = -1.0f;
        else y = 1.0f;

        //Reducing range so that ball can angle in different ways
        y = y * Random.Range(0.5f, 0.9f);

        Vector2 direction = new Vector2(x, y);

        _rigidBody.AddForce(direction * speed);

    }

}
