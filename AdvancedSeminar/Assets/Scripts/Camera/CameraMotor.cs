using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public GameObject[] mThingsToEnable;

    void Start()
    {
        for (int i = 0; i < mThingsToEnable.Length; i++)
        {
            if (!mThingsToEnable[i].active)
            {
                mThingsToEnable[i].SetActive(true);
            }
        }

    }

    void Update()
    {

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;
        Debug.Log("Camera Shake******************");
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
