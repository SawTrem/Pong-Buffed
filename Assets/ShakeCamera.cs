using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public IEnumerator Shake(float duration, float amount)
    {
        Vector3 defaultPos = transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration) 
        {
            float x = Random.Range(-1f, 1f) * amount;
            float y = Random.Range(-1f, 1f) * amount;

            transform.localPosition = new Vector3(x, y, defaultPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = defaultPos;
    }
}
