using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public float MaxReduction = 0.1f;
    public float MaxIncrease = 0.1f;
    public float RateDamping = 0.1f;
    public float Strength = 300;

    private Light lightSource;
    private float baseIntensity;

    public void Start()
    {
        lightSource = GetComponent<Light>();
        if (lightSource != null)
        {
            baseIntensity = lightSource.intensity;
            StartCoroutine(DoFlicker());
        }
    }

    private IEnumerator DoFlicker()
    {
        while (true)
        {
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(baseIntensity - MaxReduction, baseIntensity + MaxIncrease), Strength * Time.deltaTime);
            yield return new WaitForSeconds(RateDamping);
        }
    }
}
