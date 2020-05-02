using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.SceneManagement
{
    public class SceneTransition : MonoBehaviour
    {
        CanvasGroup canvas; 

        private void Start()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        //Alpha 1 - 0
        public IEnumerator FadeIn(float transitionTime)
        {
            while (canvas.alpha > 0f)
            {
                canvas.alpha -= Time.deltaTime / transitionTime;
                yield return null;
            }
        }
        //Alpha 0 - 1
        public IEnumerator FadeOut(float transitionTime)
        {
            while (canvas.alpha < 1f)
            {
                canvas.alpha += Time.deltaTime / transitionTime;
                yield return null;
            }
        }

        public void PreWarmFadeIn()
        {
            canvas.alpha = 1f;
        }
    }
}