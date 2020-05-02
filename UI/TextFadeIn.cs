using SP.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
class TextFadeIn : MonoBehaviour
{
    [SerializeField] float fadeOutTime;
    [SerializeField] CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        if (gameObject.tag == "TutorialMessage")
        {
            DisplayMessage();
        }
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Health>().GetIsDead() && gameObject.tag != "TutorialMessage")
        {
            DisplayMessage();
        }
    }

    private void DisplayMessage()
    {
        StartCoroutine(FadeInAndOut(fadeOutTime));
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator FadeInAndOut(float time)
    {
        yield return FadeIn(5f);
        yield return FadeOut(5f);
    }

    public IEnumerator FadeOut(float transitionTime)
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / transitionTime;
            yield return null;
        }
    }
    public IEnumerator FadeIn(float transitionTime)
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / transitionTime;
            yield return null;
        }
    }
}
