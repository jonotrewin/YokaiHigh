using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Quaternion originalRotation;
    private bool isShaking = false;

    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;  // Duration of the shake
    [SerializeField] private float shakeIntensity = 0.2f; // Intensity of the shake
    [SerializeField] private float shakeFrequency = 2.0f; // Frequency of the shake

    private void Start()
    {
        // Store the original rotation of the camera
        originalRotation = transform.rotation;
       
    }

    // Method to start shaking the camera
    public void ShakeCamera()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        isShaking = true;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // Randomize the rotation on the X and Y axes based on shake intensity
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;
            float z = originalRotation.z; // Keep the Z rotation intact
            float w = originalRotation.w; // Keep the W rotation intact

            transform.rotation = new Quaternion(x, y, z, w);

            elapsed += Time.deltaTime;

            // Wait for the next frame before continuing
            yield return null;
        }

        // Reset rotation to the original rotation
        transform.rotation = originalRotation;

        isShaking = false;
    }
}
