using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeCoroutine : MonoBehaviour
{

    public bool shouldStartCoroutine = false;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.15f;

    public void Update()
    {
        if(shouldStartCoroutine == true)
        {
            shouldStartCoroutine = false;
            StartCoroutine(Shake(shakeDuration, shakeMagnitude));
        }
    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        int count = 0;

        while(computeCondition(elapsed, duration))
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Debug.Log("loopCount = " + count + "; elapsed = " + elapsed + "; duration = " + duration);
            Debug.Log("x = " + x + ", y = " + y);

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            Debug.Log("elapsed+=delta : " + elapsed);
            count++;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private bool computeCondition(float elapsed, float duration)
    {
        Debug.Log("compute condition: elapsed = " + elapsed + ", duration = " + duration);
        if(elapsed < duration)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void setShouldCameraShake(bool value)
    {
        shouldStartCoroutine = value;
    }
}
