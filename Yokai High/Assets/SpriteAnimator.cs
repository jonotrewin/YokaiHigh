using System.Collections;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private bool isShaking = false;

    [Header("Squash & Stretch Settings")]
    [SerializeField] private float squashAmount = 0.2f;
    [SerializeField] private float stretchDuration = 0.1f;

    [Header("Shake Settings")]
    [SerializeField] public float shakeIntensity = 5f;  // Degrees of rotation
    [SerializeField] private float shakeDuration = 0.15f;

    private void Start()
    {
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        
    }

    /// <summary>
    /// Plays a squash & stretch animation.
    /// </summary>
    public void PlaySquashStretch()
    {
        StopAllCoroutines(); // Prevent overlapping effects
        StartCoroutine(SquashStretchCoroutine());
    }

    /// <summary>
    /// Shakes the sprite's rotation quickly to indicate damage.
    /// </summary>
    public void PlayShake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator SquashStretchCoroutine()
    {
        // Squash (shorter and wider)
        transform.localScale = new Vector3(originalScale.x + squashAmount, originalScale.y - squashAmount, originalScale.z);
        yield return new WaitForSeconds(stretchDuration);

        // Stretch (taller and thinner)
        transform.localScale = new Vector3(originalScale.x - squashAmount, originalScale.y + squashAmount, originalScale.z);
        yield return new WaitForSeconds(stretchDuration);

        // Return to normal
        transform.localScale = originalScale;
    }

    private IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float randomAngle = Random.Range(-shakeIntensity, shakeIntensity);
            transform.rotation = Quaternion.Euler(0, 0, originalRotation.eulerAngles.z + randomAngle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset rotation
        transform.rotation = originalRotation;
        isShaking = false;
    }
}
