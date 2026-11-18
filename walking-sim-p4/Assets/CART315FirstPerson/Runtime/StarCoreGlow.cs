using UnityEngine;

namespace Cart315.FirstPerson
{
    public class StarCoreGlow : MonoBehaviour
    {
        [SerializeField] private float _emissionStrength = 1.5f;

        private StarCollectionManager _collectionManager;
        private Material _starMaterial;
// Initialize the glow effect based on the player's progress in collecting stars
        private void OnEnable()
        {
            _collectionManager = StarCollectionManager.Instance;
            Renderer starRenderer = GetComponent<Renderer>();
            _starMaterial = starRenderer.material; // Create a unique instance of the material for this star to avoid affecting other stars
            if (_starMaterial.HasProperty("_EmissionColor"))// Check if the material has an emission property
                _starMaterial.EnableKeyword("_EMISSION"); // Enable emission on the material if it has an emission property

            _collectionManager.CountChanged += UpdateGlow;
            UpdateGlow(_collectionManager.CollectedStars, _collectionManager.Goal);
        }

        private void OnDisable()
        {
            if (_collectionManager != null)// Unsubscribe from the CountChanged event to avoid memory leaks
            //avoid memory leaks because the StarCollectionManager is a singleton and will persist across scenes, while this object may be destroyed when changing scenes
                _collectionManager.CountChanged -= UpdateGlow; 
        }

        private void OnDestroy()
        {
            if (_starMaterial != null)
                Destroy(_starMaterial);
        }

        private void UpdateGlow(int collectedStars, int goal)
        {// Calculate the progress as a value between 0 and 1
            float progress = Mathf.Clamp01((float)collectedStars / Mathf.Max(goal, 1));
            Color starColor = ColorAtProgress(progress);
// Set the color of the star material based on the progress
            if (_starMaterial.HasProperty("_BaseColor"))
                _starMaterial.SetColor("_BaseColor", starColor);
            if (_starMaterial.HasProperty("_Color"))
                _starMaterial.SetColor("_Color", starColor);
            if (_starMaterial.HasProperty("_EmissionColor"))
                _starMaterial.SetColor("_EmissionColor", starColor * _emissionStrength);
        }

        private static Color ColorAtProgress(float progress)
        {
            Color[] colors =
            {
                new Color(1f, 0.05f, 0.01f),// Dark red
                new Color(1f, 0.25f, 0f),// Orange
                new Color(1f, 0.8f, 0f),// Yellow
                Color.white// White
            };
            // Calculate the index of the color based on progress and interpolate between colors
            float colorProgress = progress * (colors.Length - 1);
            int colorIndex = Mathf.Min(Mathf.FloorToInt(colorProgress), colors.Length - 2);
            // Interpolate between the two colors based on the fractional part of colorProgress
            return Color.Lerp(colors[colorIndex], colors[colorIndex + 1], colorProgress - colorIndex); 
        }
    }
}
