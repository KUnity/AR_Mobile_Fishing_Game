using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemInfo : MonoBehaviour
{
    public static SystemInfo instance;

    public Image backImage;
    public TextMeshProUGUI systemText;
    private float readyTime;
    private float fadeSpeed;

    /* Temp Variables */
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        backImage.color = new Color(0.6f, 0.6f, 0.6f, 0f);
        systemText.color = new Color(1f, 1f, 1f, 0f);
        readyTime = 2f;
        fadeSpeed = 2f;
    }
    
    public void SetSystemInfo(string text)
    {
        systemText.text = text;
        StopCoroutine("FadeIn");
        StopCoroutine("ReadyToFadeIn");
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while (backImage.color.a < 0.6f)
        {
            Color color = backImage.color;
            color.a += Time.deltaTime * fadeSpeed;
            backImage.color = color;

            color = systemText.color;
            color.a += Time.deltaTime * fadeSpeed;
            systemText.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartCoroutine("ReadyToFadeIn");
    }

    IEnumerator ReadyToFadeIn()
    {
        yield return new WaitForSeconds(readyTime);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        while (backImage.color.a > 0f)
        {
            Color color = backImage.color;
            color.a -= Time.deltaTime * fadeSpeed;
            backImage.color = color;

            color = systemText.color;
            color.a -= Time.deltaTime * fadeSpeed;
            systemText.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
