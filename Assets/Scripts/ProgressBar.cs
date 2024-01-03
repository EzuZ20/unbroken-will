using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    private float targetProgress = 0.5f;
    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        foregroundImage = GetComponent<Image>();
        if (foregroundImage == null)
        {
            Debug.LogError("Foreground image is not set on the progress bar", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (foregroundImage.fillAmount < targetProgress)
        {
            foregroundImage.fillAmount += Time.deltaTime / updateSpeedSeconds;
        }   
    }

    public void SetProgress(float progress)
    {
        targetProgress = progress;
    }
}
