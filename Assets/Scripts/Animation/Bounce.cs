using UnityEngine;

public class RandomBounce : MonoBehaviour
{
    public float minAmplitude = -1f;
    public float maxAmplitude = 1f;
    public float minFrequency = 1f;
    public float maxFrequency = 4f;

    private float amplitude;
    private float frequency;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // Randomly assign amplitude and frequency within specified ranges
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        frequency = Random.Range(minFrequency, maxFrequency);
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, y, 0);
    }
}
