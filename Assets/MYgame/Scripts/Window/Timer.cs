using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace MYgame.Scripts.Window
{
    /// <summary>
    /// The timer
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private int _totalTime = 60;
        [SerializeField]
        private int _warningTime = 10;
        [SerializeField]
        private TextMeshProUGUI _timeText;
        [SerializeField]
        private Image _CoolDownImage;

        [SerializeField]
        private Color _warningColor;

        private Action _onTimesUp;
        private float _timeRemaining;
        private int _OldtimeRemaining;
        private bool _isWarning = false;
        private const float _timeStep = 0.2f;

        public void StartTimer(Action onTimesUp)
        {
            _onTimesUp = onTimesUp;
            _timeRemaining = _totalTime;
            _CoolDownImage.fillAmount = 1.0f;
            DOTween.Sequence()
                .AppendInterval(_timeStep)
                .AppendCallback(UpdateTimer)
                .SetLoops(-1)
                .SetId(this);
        }

        public void StopTimer()
        {
            DOTween.Kill(this);
        }

        public void AddTime(float Time)
        {
            if (_timeRemaining <= 0.0f)
                return;

            _timeRemaining += Time;

            if (_timeRemaining >= _warningTime)
                m_LiftCallBack.OnNext(Unit.Default);

            PerformanceUpdate();
        }

        private void UpdateTimer()
        {
            _OldtimeRemaining = Mathf.CeilToInt(_timeRemaining);
            _timeRemaining -= _timeStep;
            PerformanceUpdate();
        }

        private void PerformanceUpdate()
        {
            _CoolDownImage.fillAmount = _timeRemaining / (float)_totalTime;

            if (_timeRemaining <= _warningTime && !_isWarning)
            {
                _isWarning = true;
                _timeText.color = _warningColor;
            }
            else if (_isWarning && _timeRemaining >= _warningTime)
            {
                _isWarning = false;
                _timeText.color = Color.white;
            }

            int lTemptimeRemaining = Mathf.CeilToInt(_timeRemaining);

           // if (_isWarning)
            //{
            if (_OldtimeRemaining != lTemptimeRemaining)
                 m_DecreaseTimeUniRx.OnNext(lTemptimeRemaining);
           // }

            _timeText.text = lTemptimeRemaining.ToString();

            if (_timeRemaining > 0)
                return;

            _onTimesUp.Invoke();
            DOTween.Kill(this);
        }


        // ===================== UniRx ======================

        public Subject<int> m_DecreaseTimeUniRx = new Subject<int>();

        public UniRx.Subject<int> ObserverDecreaseTimeUniRx()
        {
            return m_DecreaseTimeUniRx ?? (m_DecreaseTimeUniRx = new UniRx.Subject<int>());
        }

        public Subject<Unit> m_LiftCallBack = new Subject<Unit>();

        public UniRx.Subject<Unit> ObserverLiftCallBack()
        {
            return m_LiftCallBack ?? (m_LiftCallBack = new UniRx.Subject<Unit>());
        }
        // ===================== UniRx ======================
    }
}
