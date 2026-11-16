using UnityEngine;

namespace Cart315.FirstPerson
{
    public class StarChime : MonoBehaviour
    {
        private AudioSource audioSource;
        private MeshRenderer meshRenderer;
        private Collider starCollider;
        private bool hasBeenCollected;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            meshRenderer = GetComponent<MeshRenderer>();
            starCollider = GetComponent<Collider>();
        }

        // When the player runs into a star
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object running into the star is the Player
            if (other.CompareTag("Player"))
                Collect();
        }

        public void Collect()
        {
            if (hasBeenCollected)
                return;

            hasBeenCollected = true;

            if (meshRenderer != null)
                meshRenderer.enabled = false;
            if (starCollider != null)
                starCollider.enabled = false;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
