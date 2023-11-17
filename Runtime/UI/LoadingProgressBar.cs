using System;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingUtils.UI
{
    public enum ProgressBarMode
    {
        Fill,
        Anchor
    }
    public class LoadingProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private ProgressBarMode _barMode;
        [SerializeField] private float _lerpSpeed;

        private float _targetProgress;
        private float _currentProgress;
        
        public void SetProgress(float progress)
        {
            _targetProgress = progress;
        }

        private void Update()
        {
            _currentProgress = Mathf.Lerp(_currentProgress, _targetProgress, _lerpSpeed);
            SetProgressImmediate(_currentProgress);
        }

        private void SetProgressImmediate(float progress)
        {
            switch (_barMode)
            {
                case ProgressBarMode.Anchor:
                    progress = Mathf.Clamp01(progress);

                    var rectTransform = _progressBar.rectTransform;
                    var anchorMax = rectTransform.anchorMax;
                    anchorMax.x = progress;
                    rectTransform.anchorMax = anchorMax;
                    break;
                case ProgressBarMode.Fill:
                default:
                    _progressBar.fillAmount = progress;
                    break;
            }
        }
    }
}