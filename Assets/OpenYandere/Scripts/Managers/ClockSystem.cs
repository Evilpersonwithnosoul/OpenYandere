using OpenYandere.Managers.Traits;
using OpenYandere.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OpenYandere.Managers
{
    internal class ClockSystem : Singleton<ClockSystem>
    {
        protected float timer = 0.0f;
        protected int hour = 0, minute = 0;
        [SerializeField] protected int startingHour;
        [SerializeField] protected bool clockEnabled = true, use24HourFormat = false;
        [SerializeField, Range(0.0f, 10.0f)] protected float clockSpeed;

        public enum DayPhase { Morning, Afternoon, Evening, Night }
        public enum DayOfWeek { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

        protected DayOfWeek currentDay = DayOfWeek.Monday; // começar na segunda-feira por padrão
        protected DayPhase currentPhase = DayPhase.Morning;

        //UI
        [SerializeField] protected TextMeshProUGUI clockTimeText, dayOfWeekText, phaseOfDayText;

        //Events
        public delegate void ClockEvent();
        public event ClockEvent OnTimeChanged;

        protected void Awake()
        {
            startingHour = startingHour;
            UpdateUI();
        }

        protected void Start()
        {
            StartCoroutine(UpdateClockTimer());
        }

        private IEnumerator UpdateClockTimer()
        {
            clockTimeText.text = GetTimeString();
            while (clockEnabled)
            {
                ProgressTime();
                yield return null;
            }
        }

        protected void ProgressTime()
        {
            timer += Time.deltaTime * clockSpeed;
            if (timer > 1.0f)
            {
                timer = 0;
                minute++;
                if (minute % 60 == 0)
                {
                    minute = 0;
                    hour++;
                    UpdateDayPhase(); // Atualize a fase do dia aqui
                    if (hour > 23)
                    {
                        hour = 0;
                        ProgressDay(); // Progresso do dia da semana
                    }
                }
                UpdateUI();
                OnTimeChanged?.Invoke();
            }
        }

        public void SetTime(int hour, int minute)
        {
            this.hour = hour;
            this.minute = minute;
            clockTimeText.text = GetTimeString();
            OnTimeChanged?.Invoke();
        }
        public int GetTimeMilitary()
        {
            return hour * 100 + minute;
        }
        protected void UpdateUI()
        {
            clockTimeText.text = GetTimeString();
            phaseOfDayText.text = currentPhase.ToString();
            dayOfWeekText.text = currentDay.ToString();

        }

        private void UpdateDayPhase()
        {
            currentPhase = hour < 12 ? DayPhase.Morning :
                           hour < 18 ? DayPhase.Afternoon :
                           hour < 21 ? DayPhase.Evening : DayPhase.Night;
        }
        private void ProgressDay()
        {
            currentDay = (DayOfWeek)(((int)currentDay + 1) % 7);
        }


        private string GetTimeString()
        {
            int displayHour = use24HourFormat ? hour : hour % 12 == 0 ? 12 : hour % 12;
            string hourText = displayHour < 10 ? "0" + displayHour : displayHour.ToString();
            string minuteText = minute < 10 ? "0" + minute : minute.ToString();
            return hourText + ":" + minuteText + (use24HourFormat ? "" : hour < 12 ? " AM" : " PM");
        }

    }
}