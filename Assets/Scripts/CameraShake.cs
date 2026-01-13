using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 2.0f;
    public float shakeMagnitude = 0.3f;
    private Vector3 initialPosition;

    public void TriggerShake()
    {
        initialPosition = transform.position;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition;
    }
}