using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cart315.FirstPerson
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimpleBlackHole : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Collider _movementArea;
        [SerializeField] private float _movementSpeed = 3f;

        [Header("Star Attraction")]
        [SerializeField] private float _starPullRadius = 8f;
        [SerializeField] private float _starPullSpeed = 4f;
        [SerializeField] private float _starConsumeDistance = 0.4f;

        private Vector3 _destination;
        private SphereCollider _pullTrigger;
        private bool _isResetting;

        private void Start()
        {
            // If movement area isn't assigned, try to automatically find the Plane object
            if (_movementArea == null)
            {
                GameObject plane = GameObject.Find("Plane");
                if (plane != null) _movementArea = plane.GetComponent<Collider>();
            }

            // Create a second, larger trigger dynamically for pulling stars
            _pullTrigger = gameObject.AddComponent<SphereCollider>();
            _pullTrigger.isTrigger = true;
            _pullTrigger.radius = _starPullRadius / Mathf.Max(transform.lossyScale.x, 1f); // Adjust for scale

            // Pick our first destination
            SetNewRandomDestination();
        }

        private void Update()
        {
            MoveTowardsDestination();
        }

        private void MoveTowardsDestination()
        {
            if (_movementArea == null || _movementSpeed <= 0f) return;

            // Smoothly move to target
            transform.position = Vector3.MoveTowards(transform.position, _destination, _movementSpeed * Time.deltaTime);

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

            // Pick a completely random point anywhere inside the bounds of the movement plane
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            // Keep the Y position fixed to the top surface of the plane
            _destination = new Vector3(randomX, bounds.max.y, randomZ);
        }

        // --- Unity Physics Trigger Messages ---

        // Handles pulling stars continuously while they are within the gravity zone
        private void OnTriggerStay(Collider other)
        {
            // Pull Stars
            if (other.TryGetComponent(out StarChime star))
            {
                Transform starTransform = star.transform;
                starTransform.position = Vector3.MoveTowards(starTransform.position, transform.position, _starPullSpeed * Time.deltaTime);

                if (Vector3.Distance(starTransform.position, transform.position) <= _starConsumeDistance)
                {
                    star.Collect();
                }
            }
        }

        // Handles killing the player instantly when they touch the core black hole collider
        private void OnTriggerEnter(Collider other)
        {
            if (_isResetting) return;

            // Check if the object entering our center is the player
            if (other.GetComponentInParent<FPController>() != null)
            {
                _isResetting = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
