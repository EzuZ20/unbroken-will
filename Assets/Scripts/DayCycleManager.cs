using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{

    public Light directionalLight;
    public Color morningColor;
    public Color afternoonColor;
    public Color eveningColor;
    public float transitionDuration = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Set initial light color.
        directionalLight.color = morningColor;
    }

    // Update is called once per frame
    public void UpdateLighting(int timeOfDay)
    {
        StopAllCoroutines();

        if (timeOfDay == 0) // Early Morning.
        {
            StartCoroutine(TransitionLightColor(directionalLight.color, morningColor, transitionDuration));
        }
        else if (timeOfDay == 1) // Afternoon.
        {
            StartCoroutine(TransitionLightColor(directionalLight.color, afternoonColor, transitionDuration));
        }
        else if (timeOfDay == 2) // Evening.
        {
            StartCoroutine(TransitionLightColor(directionalLight.color, eveningColor, transitionDuration));
        }
    }

    private IEnumerator TransitionLightColor(Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            directionalLight.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        directionalLight.color = endColor;
    }
}
