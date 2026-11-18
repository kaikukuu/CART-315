using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cart315.FirstPerson
{
    [RequireComponent(typeof(SphereCollider))]
    public class BlackHole : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Collider _movementArea;
        [SerializeField] private float _movementSpeed = 3f;

        [Header("Star Attraction")]
        [SerializeField] private float _starPullRadius = 8f;
        [SerializeField] private float _starPullSpeed = 4f;
        [SerializeField] private float _starConsumeDistance = 0.4f;

        private Vector3 _destination;
        private bool _isResetting;

        private void Start()
        {
            // If movement area isn't assigned, try to automatically find the Plane object
            if (_movementArea == null)
            {
                GameObject plane = GameObject.Find("Plane"); // Look for a GameObject named "Plane" in the scene
                if (plane != null) _movementArea = plane.GetComponent<Collider>(); // Get the collider component from the Plane object if found
            }

           // Ensure the black hole's collider is set as a trigger to detect player collisions
            GetComponent<SphereCollider>().isTrigger = true;
            GameObject pullZone = new GameObject("Star Pull Trigger");
            pullZone.transform.SetParent(transform, false); // Set the pull zone as a child of the black hole to follow its position and rotation
            SphereCollider pullCollider = pullZone.AddComponent<SphereCollider>(); // Add a SphereCollider to the pull zone to define the area where stars will be attracted
            pullCollider.isTrigger = true; // Set the pull zone collider as a trigger so it can detect stars entering the area without causing physical collisions
            float largestScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z); // Calculate the largest scale of the black hole to ensure the pull radius is scaled correctly
            pullCollider.radius = Mathf.Max(_starPullRadius, 0f) / Mathf.Max(largestScale, 0.0001f); // Set the radius of the pull zone collider based on the specified pull radius and the largest scale of the black hole to ensure consistent attraction behavior regardless of scaling

//Add a Rigidbody to the pull zone to ensure it can detect trigger events properly
            Rigidbody pullBody = pullZone.AddComponent<Rigidbody>();
            pullBody.isKinematic = true;
            pullBody.useGravity = false;
// Add the BlackHolePullZone component to handle star attraction logic
            pullZone.AddComponent<BlackHolePullZone>().Initialize(this);

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
        internal void PullStar(Collider other)
        {
            // Check if the object entering pull zone is a StarChime
            StarChime star = other.GetComponent<StarChime>();
            if (star == null) star = other.GetComponentInParent<StarChime>();
            if (star == null || star.IsCollected) return;

            star.BeginAttraction();
            Transform starTransform = star.transform;
            starTransform.position = Vector3.MoveTowards(
                starTransform.position,
                transform.position,
                Mathf.Max(_starPullSpeed, 0f) * Time.fixedDeltaTime);

            if (Vector3.Distance(starTransform.position, transform.position) <= Mathf.Max(_starConsumeDistance, 0f))
                star.Consume();
        }

        // Handles killing the player instantly when they touch the core black hole collider
        private void OnTriggerEnter(Collider other)
        {
            if (_isResetting) return;

            // Check if the object entering center is the player
            if (other.GetComponentInParent<FPController>() != null)
            {
                _isResetting = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
