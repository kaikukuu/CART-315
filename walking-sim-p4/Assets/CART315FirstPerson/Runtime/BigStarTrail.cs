using UnityEngine;

namespace Cart315.FirstPerson
{
    public class SimpleBigStarTrail : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Collider _movementArea;
        [SerializeField] private float _movementSpeed = 3f;

        [Header("Trail Spawning")]
        [SerializeField] private GameObject _trailStarPrefab;
        [SerializeField] private float _spawnInterval = 0.5f;
        [SerializeField] private float _behindDistance = 0.4f;

        private Vector3 _destination;
        private float _spawnTimer;
        private float _movementHeight;

        private void Start()
        {
            // Find the plane automatically if not assigned in the Inspector
            if (_movementArea == null)
            {
                GameObject plane = GameObject.Find("Plane");
                if (plane != null) _movementArea = plane.GetComponent<Collider>();
            }

            _movementHeight = transform.position.y;

            // Pick first destination immediately
            SetNewRandomDestination();
        }

        private void Update()
        {
            MoveTowardsDestination();
            HandleTrailSpawning();
        }

        private void MoveTowardsDestination()
        {
            if (_movementArea == null || _movementSpeed <= 0f) return;

            // Smoothly move to the target destination
            // Refrenced: https://docs.unity.com/en-us/engine/6000.6/script-reference/unityengine/vector3/movetowards
            transform.position = Vector3.MoveTowards(transform.position, _destination, _movementSpeed * Time.deltaTime);

            // Dynamically rotate towards the destination so transform.forward points in the direction of travel
            Vector3 direction = (_destination - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }

            // Reached target? Pick a new one
            if (Vector3.Distance(transform.position, _destination) < 0.01f)
            {
                SetNewRandomDestination();
            }
        }

        private void SetNewRandomDestination()
        {
            if (_movementArea == null) return;

            Bounds bounds = _movementArea.bounds;

            // Pick a random point inside the plane's bounds
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            _destination = new Vector3(randomX, _movementHeight, randomZ);
        }

        private void HandleTrailSpawning()
        {
            if (_trailStarPrefab == null) return;

            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnInterval)
            {
                _spawnTimer = 0f;

                // Use the object's built-in backward direction (-transform.forward) to drop the trail item
                Vector3 spawnPosition = transform.position - (transform.forward * _behindDistance);

                Instantiate(_trailStarPrefab, spawnPosition, _trailStarPrefab.transform.rotation);
            }
        }
    }
}
