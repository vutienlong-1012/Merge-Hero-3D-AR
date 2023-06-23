using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR
{
    public class TimeManager : Singleton<TimeManager>
    {
        bool isPaused;

        public bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                if (value)
                    SaveTimeToPlayerPref();
            }
        }


        [SerializeField, ReadOnly] float levelPlayingTime = 0;
        [SerializeField, ReadOnly] bool isCounting = false;
        private void Start()
        {
            if (IsOnlineInADifferentDay())
            {
                StaticVariables.IsAlreadyGetTodayDailyGift = false;
                StaticVariables.OnlineRewardClaimedCount = 0;
                StaticVariables.RemainTimeCountDownOnlineReward = 60;
            }
        }

        private void Update()
        {
            if (isCounting)
                levelPlayingTime += Time.deltaTime;
        }

        public void StartCountLevelPlayingTime()
        {
            isCounting = true;
            levelPlayingTime = 0;
        }

        public void StopCountLevelPlayingTime()
        {
            isCounting = false;
        }

        public float GetLevelPlayingTime()
        {
            return levelPlayingTime;
        }

        void OnApplicationFocus(bool hasFocus)
        {
            IsPaused = !hasFocus;
        }

        void OnApplicationPause(bool pauseStatus)
        {
            IsPaused = pauseStatus;
        }

        public void SaveTimeToPlayerPref()
        {
            StaticVariables.LastOnlineTime = DateTime.Now.ToString();
        }

        public bool IsOnlineInADifferentDay()
        {
            DateTime _now = DateTime.Now;
            DateTime _lastTime = DateTime.Parse(StaticVariables.LastOnlineTime);
            if (_lastTime.Year != _now.Year || _lastTime.Month != _now.Month || _lastTime.Day != _now.Day)
            {
                return true;
            }
            else
                return false;
        }

        public void StartCountdown(float _countdownTime, Text _countdownText = null, Action<float> _onUpdateAction = null, Action _onCompleteAction = null)
        {
            StartCoroutine(_CountdownCoroutine());

            IEnumerator _CountdownCoroutine()
            {
                while (_countdownTime > 0f)
                {
                    // Update the countdown timer
                    _countdownTime -= Time.deltaTime;
                    _countdownTime = Mathf.Clamp(_countdownTime, 0, _countdownTime);
                    _onUpdateAction?.Invoke(_countdownTime);
                    // Display the countdown time
                    if (_countdownText != null)
                        _countdownText.text = Helpers.FormatTime(_countdownTime);

                    yield return null;
                }

                // Countdown has reached zero
                // Do something when countdown finishes (e.g., start a game, trigger an event)
                _onCompleteAction?.Invoke();
            }
        }
    }
}