using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _direction;

    public Paddle paddle;
    public int paddleDir = 0;
    // Update is called once per frame
    private void Update()
    {
        _direction = Vector2.zero;

        if (paddleDir == 0)
        {
            if (Keyboard.current.wKey.isPressed)
                _direction = Vector2.up;
            else if (Keyboard.current.sKey.isPressed)
                _direction = Vector2.down;
        }
        else if (paddleDir == 1)
        {
            if (Keyboard.current.wKey.isPressed)
                _direction = Vector2.down;
            else if (Keyboard.current.sKey.isPressed)
                _direction = Vector2.up;
        }

        paddle.direction = _direction;
    }
}