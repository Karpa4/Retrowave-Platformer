using System;
using System.Collections;
using UnityEngine;

namespace Features.SceneLoading.Scripts
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private AnimationCurve hideCurve;
        [SerializeField] private AnimationCurve showCurve;
        private const float Interval = 0.02f;

        public bool IsShown { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Hide()
        {
            StartCoroutine(DoFade(hideCurve, OnHide));
        }

        public void Show()
        {
            ChangeRaycastState(true);
            StartCoroutine(DoFade(showCurve, OnShow));
        }
            
        private IEnumerator DoFade(AnimationCurve curve, Action callback = null)
        {
            float time = 0;
            while (time < curve.keys[curve.length-1].time)
            {
                canvasGroup.alpha = curve.Evaluate(time);
                time += Interval;
                //yield return null;
                yield return new WaitForSecondsRealtime(Interval);
            }
            callback?.Invoke();
        }

        private void OnHide()
        {
            ChangeRaycastState(false);
            IsShown = false;
        }

        private void OnShow()
        {
            IsShown = true;
        }

        private void ChangeRaycastState(bool isEnable) => 
            canvasGroup.blocksRaycasts = isEnable;
    }
}
