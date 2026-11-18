using UnityEngine;

namespace Cart315.FirstPerson
{
    public interface IStarAttractable
    {
        void BeginAttraction();
    }

    public class StarChime : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Renderer[] _renderers;
        private Collider[] _colliders;
        private bool _hasBeenCollected;

        public bool IsCollected => _hasBeenCollected;

        private void Awake()
        {
            Rigidbody body = GetComponent<Rigidbody>();
            if (body == null)
                body = gameObject.AddComponent<Rigidbody>();

            body.isKinematic = true;
            body.useGravity = false;

            _audioSource = GetComponent<AudioSource>();
            _renderers = GetComponentsInChildren<Renderer>();
            _colliders = GetComponentsInChildren<Collider>();
        }

        // When the player runs into a star
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<FPController>() != null || other.CompareTag("Player"))
                CollectByPlayer();
        }

        internal void BeginAttraction()
        {
            IStarAttractable floating = GetComponent<IStarAttractable>();
            if (floating != null) floating.BeginAttraction();
        }

        internal void CollectByPlayer()
        {
            if (!TryCollect()) return;
            StarCollectionManager.Instance.RegisterPlayerCollection();
        }

        internal void Consume()
        {
            TryCollect();
        }

        private bool TryCollect()
        {
            if (_hasBeenCollected) return false;
            _hasBeenCollected = true;

            foreach (Renderer item in _renderers)
                item.enabled = false;
            foreach (Collider item in _colliders)
                item.enabled = false;

            StarCollectionManager.Instance.UnregisterSpawnedStar();

            if (_audioSource != null && _audioSource.clip != null)
            {
                _audioSource.Play();
                Destroy(gameObject, _audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }

            return true;
        }
    }
}
