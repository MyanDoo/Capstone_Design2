using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //public CanvasGroup canvasG;
    public Image image;
    float fadeTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        //canvasG = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        Color tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }   

    IEnumerator Fade(float num)
    {
        StartCoroutine(FadeIn(num));
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(FadeOut(num));
    }

    IEnumerator FadeIn(float num)
    {
        SpriteRenderer sr = image.gameObject.GetComponent<SpriteRenderer>();
        Color tempColor = image.color;

        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeTime;
            sr.color = tempColor;

            if(tempColor.a >= 1f)
                tempColor.a = 1f;
            yield return null;
        }
    }

    IEnumerator FadeOut(float num)
    {
        SpriteRenderer sr = image.gameObject.GetComponent<SpriteRenderer>();
        Color tempColor = image.color;

        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeTime;
            sr.color = tempColor;

            if (tempColor.a <= 0f)
                tempColor.a = 0f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
