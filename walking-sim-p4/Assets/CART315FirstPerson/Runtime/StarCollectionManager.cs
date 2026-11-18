using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cart315.FirstPerson
{
    public class StarCollectionManager : MonoBehaviour
    {
        private static StarCollectionManager _instance;

        [SerializeField] private int _goal = 15;
        [SerializeField] private float _completionFadeDuration = 1.5f;
        [SerializeField] private float _completionHoldDuration = 0.35f;
        [SerializeField] private Image _progressFill;
        [SerializeField] private Text _countLabel;
        [SerializeField] private Image _fadeOverlay;

        private int _collectedStars;
        private int _activeSpawnedStars;
        private bool _isCompleting;


        public static StarCollectionManager Instance
        {
            get
            {
                //If the instance already exists, return it
                if (_instance != null) return _instance;
                //If the instance doesn't exist, find it in the scene
                _instance = FindFirstObjectByType<StarCollectionManager>();
                return _instance;
            }
        }

        public int CollectedStars => _collectedStars; // The number of stars the player has collected
        public int Goal => Mathf.Max(_goal, 1);// The number of stars the player needs to collect to complete the level
        public int ActiveSpawnedStars => _activeSpawnedStars;// The number of stars that have been spawned but not yet collected

        public event Action<int, int> CountChanged;// Event that is triggered when the number of collected stars changes. The first parameter is the current count, and the second parameter is the goal.

        private void Awake()
        {

            _instance = this; //Set this instance as the singleton instance

            RefreshHud(); // Update the HUD to reflect the initial state
        }

        private void OnDestroy()
        {
            if (_instance == this) // Clear the singleton instance if this instance is being destroyed
                _instance = null;
        }

        //Regiseter a star collection event, incrementing the collected star count and checking for level completion
        public void RegisterPlayerCollection()
        {
            if (_isCompleting || _collectedStars >= Goal) return;

            _collectedStars++;
            RefreshHud();
            CountChanged?.Invoke(_collectedStars, Goal);

            if (_collectedStars >= Goal)
            {
                _isCompleting = true;
                StartCoroutine(FadeToWhiteAndRestart());
            }
        }

        public void RegisterSpawnedStar()
        {
            _activeSpawnedStars++;
        }

        public void UnregisterSpawnedStar()
        {
            // Decrement the count of active spawned stars, ensuring it doesn't go below zero
            _activeSpawnedStars = Mathf.Max(0, _activeSpawnedStars - 1);
        }

        // Refreshes the HUD elements to reflect the current number of collected stars and the goal
        private void RefreshHud()
        {
            if (_progressFill != null)
                _progressFill.fillAmount = Mathf.Clamp01((float)_collectedStars / Goal);

            if (_countLabel != null)
                _countLabel.text = $"Stars: {_collectedStars} / {Goal}";
        }
        // Coroutine to fade the screen to white and restart the scene after a delay
        private IEnumerator FadeToWhiteAndRestart()
        {
            float duration = Mathf.Max(_completionFadeDuration, 0f);
            float elapsed = 0f;
            Color overlayColor = Color.white;

            // Fade the overlay to white over the specified duration
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                overlayColor.a = Mathf.Clamp01(elapsed / duration);
                _fadeOverlay.color = overlayColor;
                yield return null;
            }
            // Hold the white screen for a specified duration before restarting the scene
            overlayColor.a = 1f;
            _fadeOverlay.color = overlayColor;
            yield return new WaitForSecondsRealtime(Mathf.Max(_completionHoldDuration, 0f));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
