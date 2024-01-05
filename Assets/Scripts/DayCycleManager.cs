using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{

    public Light directionalLight;
    public Material skyboxMaterial;
    public Color morningColor;
    public Color afternoonColor;
    public Color eveningColor;
    public float transitionDuration = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Set initial light color.
        directionalLight.color = morningColor;
        skyboxMaterial.SetColor("_Tint", morningColor);
    }

    // Update is called once per frame
    public void UpdateLighting(int timeOfDay)
    {
        StopAllCoroutines();

        if (timeOfDay == 0) // Early Morning.
        {
            StartCoroutine(TransitionLightAndSkyboxColor(directionalLight.color, morningColor, transitionDuration));
        }
        else if (timeOfDay == 1) // Afternoon.
        {
            StartCoroutine(TransitionLightAndSkyboxColor(directionalLight.color, afternoonColor, transitionDuration));
        }
        else if (timeOfDay == 2) // Evening.
        {
            StartCoroutine(TransitionLightAndSkyboxColor(directionalLight.color, eveningColor, transitionDuration));
        }
    }

    private IEnumerator TransitionLightAndSkyboxColor(Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            Color currentColor = Color.Lerp(startColor, endColor, time / duration);
            directionalLight.color = currentColor;
            skyboxMaterial.SetColor("_Tint", currentColor);
            time += Time.deltaTime;
            yield return null;
        }
        directionalLight.color = endColor;
        skyboxMaterial.SetColor("_Tint", endColor);
    }
}
