using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float flickerRate;
    [SerializeField] float flickerStrength;
    [SerializeField] float lightIntensity;
    float timer = 0f;

    private void Update()
    {
        if(timer > flickerRate)
        {
            float lowerFlicker = 0f;
            lowerFlicker -= flickerStrength;
            Mathf.Clamp(lowerFlicker, 0f, Mathf.Infinity);
            float upperFlicker = 0f;
            upperFlicker += flickerStrength;
            light.intensity = Random.Range(lowerFlicker + lightIntensity, upperFlicker + lightIntensity);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
