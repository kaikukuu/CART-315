using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _direction;

    public Paddle paddle;
    public bool isFrozen;
    public bool isInverted;

    // Update is called once per frame
    private void Update()
    {
        _direction = Vector2.zero;

        if (isInverted)
        {
            if (Keyboard.current.wKey.isPressed)
                _direction = Vector2.down;
            else if (Keyboard.current.sKey.isPressed)
                _direction = Vector2.up;
        }
        else if (isFrozen)
        {
            // Set the paddle's direction to zero to stop it from moving
            _direction = Vector2.zero;
        }
        else
        {


            if (Keyboard.current.wKey.isPressed)
                _direction = Vector2.up;
            else if (Keyboard.current.sKey.isPressed)
                _direction = Vector2.down;
        }

        paddle.direction = _direction;
    }
}