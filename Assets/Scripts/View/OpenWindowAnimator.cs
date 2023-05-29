using System;
using System.Collections;
using UnityEngine;

namespace Odometr
{
    public class OpenWindowAnimator : MonoBehaviour
    {
        [SerializeField] private float time;
        private RectTransform rectTransform;
        private float timer;
        private float widthOrigin;
        private float heightOrigin;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            widthOrigin = rectTransform.rect.width;
            heightOrigin = rectTransform.rect.height;
        }

        public void AnimateOpen()
        {
            StartCoroutine(Opening());
        }

        public void AnimateClose(Action finishCallback)
        {
            StartCoroutine(Closing(finishCallback));
        }

        private IEnumerator Opening()
        {
            timer = 0f;
            rectTransform.sizeDelta = Vector2.zero;
            while (rectTransform.sizeDelta.x < widthOrigin)
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;
                rectTransform.sizeDelta = new Vector2(widthOrigin * (timer / (time / 2f)), heightOrigin * (timer / (time / 2f)));
            }
            rectTransform.sizeDelta = new Vector2(widthOrigin, heightOrigin);
        }

        private IEnumerator Closing(Action finishCallback)
        {
            timer = 0f;
            while (rectTransform.sizeDelta.x > 0)
            {
                yield return new WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;
                rectTransform.sizeDelta = new Vector2(widthOrigin - (widthOrigin * (timer / (time / 2f))), heightOrigin - (heightOrigin * (timer / (time / 2f))));
            }
            rectTransform.sizeDelta = Vector2.zero;
            finishCallback?.Invoke();
        }
    }
}