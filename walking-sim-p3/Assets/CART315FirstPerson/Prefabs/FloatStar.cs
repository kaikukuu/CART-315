using UnityEngine;

public class FloatStar : MonoBehaviour
{
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private float _frequency = 2f;
    [SerializeField] private float _pulseAmount = 0.12f;
    [SerializeField] private float _beatsPerSecond = 1.2f;

    private Vector3 _startPosition;
    private Vector3 _startScale;

    private void Start()
    {
        // Store the initial position and scale of the star for floating and pulsing calculations
        _startPosition = transform.position;
        _startScale = transform.localScale;
    }

    private void Update()
    {
        // Floating motion
        Vector3 position = _startPosition;
        position.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.position = position;

        // Pulsing motion
        float phase = Mathf.Repeat(Time.time * _beatsPerSecond, 1f); // Phase goes from 0 to 1 over time
        float firstBeat = GetPulse(phase, 0.12f, 0.08f); // First beat is stronger
        float secondBeat = GetPulse(phase, 0.32f, 0.11f) * 0.6f; // Second beat is weaker
        transform.localScale = _startScale * (1f + _pulseAmount * (firstBeat + secondBeat) // Use the sum of both beats to scale the star from its original size

        );
    }

    // Helper function to calculate the pulse value based on the phase, center, and half-width
    private static float GetPulse(float phase, float center, float halfWidth)
    {
        // Calculate the distance from the current phase to the center of the pulse
        float distance = Mathf.Abs(phase - center);
        if (distance >= halfWidth)
            return 0f;

        return Mathf.Sin((1f - distance / halfWidth) * Mathf.PI * 0.5f); // Use a sine function to create a smooth pulse shape
    }
}
