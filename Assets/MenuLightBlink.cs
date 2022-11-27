using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MenuLightBlink : MonoBehaviour
{
    public List<Light> lights;
    public float minIntensity = 3.0f;
    public float maxIntensity = 5.0f;
    public float maxRandomInterval = 0.3f;
    
    private void Start()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            StartCoroutine(BlinkLight(lights[i]));
        }
    }

    private IEnumerator BlinkLight(Light light)
    {
        while (true)
        {
            float newIntensityTarget = Random.Range(minIntensity, maxIntensity);
            light.intensity = Mathf.MoveTowards(light.intensity, newIntensityTarget, 1.0f);
            yield return new WaitForSeconds(Random.Range(0.0f, maxRandomInterval));
        }
    }
    
}
