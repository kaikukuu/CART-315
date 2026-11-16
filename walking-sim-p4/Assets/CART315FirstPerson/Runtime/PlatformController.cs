using UnityEngine;
using UnityEngine.InputSystem;

namespace Cart315.FirstPerson
{

    // Adapted from: 
    // https://www.youtube.com/watch?v=aJ4bVrrF2xY
    // https://www.youtube.com/watch?v=s6chmaGuDFY
    // https://www.youtube.com/watch?v=Eoc2imXWTs0

    public class PlatformController : MonoBehaviour
    {
        [Header("Vertical Ride Settings")]
        [SerializeField] private float _riseHeight = 3f;
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _pauseAtTop = 0.5f;

        [Header("Player Steering Settings")]
        [SerializeField] private float _controlSpeed = 4f;

        // Simplified positioning states
        private Vector3 _startPosition;
        private Vector3 _topPosition;
        private Vector3 _targetPosition;
        private float _pauseTimer;

        // Tracks the player currently riding the platform.
        private Transform _playerTransform;
        private FPController _playerController;
        private CharacterController _playerCharacterController;

        private void Awake()
        {
            // Calculate exact start and top coordinates ahead of time
            _startPosition = transform.position;
            _topPosition = _startPosition + Vector3.up * _riseHeight;
            _targetPosition = _topPosition;
        }

        private void Update()
        {
            // Check before moving so the player is evaluated against the current platform position.
            CheckPlayerJump();

            Vector3 previousPosition = transform.position;

            // Handle arrow key steering first
            HandleSteering();

            // Handle vertical pacing and automatic movement
            HandleVerticalRide();

            // Move the player's character controller by the same amount as the platform.
            if (_playerCharacterController != null)
                _playerCharacterController.Move(transform.position - previousPosition);
        }

        private void HandleVerticalRide()
        {
            // Hold the platform at its current height while the player is riding.
            if (_playerTransform != null)
                return;

            // If we are pausing at the top, count down and do nothing else
            if (_pauseTimer > 0f)
            {
                _pauseTimer -= Time.deltaTime;
                return;
            }

            // Move smoothly towards our current target position
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);

            // Check if we reached our target destination
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                if (_targetPosition == _topPosition)
                {
                    // Reached the top! Start pause timer and set next target to the bottom
                    _pauseTimer = _pauseAtTop;
                    _targetPosition = _startPosition;
                }
                else
                {
                    // Reached the bottom! Wait until player lands on it again to reset target
                    _targetPosition = _topPosition;
                }
            }
        }

        private void HandleSteering()
        {
            // Only steer if a player is actively riding the platform
            if (_playerTransform == null || _controlSpeed <= 0f) return;

            Keyboard keyboard = Keyboard.current;
            if (keyboard == null) return;

            // More intuitive input direction 
            // X = Left/Right, Z = Up/Down
            Vector3 inputDir = Vector3.zero;
            if (keyboard.rightArrowKey.isPressed) inputDir.x += 1f;
            if (keyboard.leftArrowKey.isPressed) inputDir.x -= 1f;
            if (keyboard.upArrowKey.isPressed) inputDir.z += 1f;
            if (keyboard.downArrowKey.isPressed) inputDir.z -= 1f;

            if (inputDir != Vector3.zero)
            {
                // Move the platform horizontally; Update carries the player by the same amount.
                transform.position += inputDir.normalized * (_controlSpeed * Time.deltaTime);
            }
        }

        internal void PlayerLanded(FPController player)
        {
            if (player == null || _playerTransform == player.transform)
                return;

            ReleasePlayer();

            _playerController = player;
            _playerTransform = player.transform;
            _playerCharacterController = player.GetComponent<CharacterController>();
        }

        private void CheckPlayerJump()
        {
            // Keep carrying the player until they jump; a raycast can briefly miss the platform.
            if (_playerController != null && _playerController.Velocity.y > 0f)
                ReleasePlayer();
        }

        private void ReleasePlayer()
        {
            _playerTransform = null;
            _playerController = null;
            _playerCharacterController = null;
        }

        private void OnDisable()
        {
            // Clear references without changing the hierarchy during platform deactivation.
            ReleasePlayer();
        }
    }
}
