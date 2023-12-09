using System;
using TMPro;
using UnityEngine;

namespace UITools
{
    public static class Models
    {
        public static void ChangeLayer(GameObject gfxModel)
        {
            int LayerUI = LayerMask.NameToLayer("UI");
            gfxModel.layer = LayerUI;
            foreach (Transform child in gfxModel.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = LayerMask.NameToLayer("UI");
            }
        }

        public static void ResetPosition(GameObject gfxModel)
        {
            gfxModel.transform.localPosition = Vector3.zero;
            gfxModel.transform.localEulerAngles = Vector3.zero;
            gfxModel.transform.localScale = Vector3.one;
        }
    }
    public static class Timer
    {
        public static void DisplayTime(TextMeshProUGUI timerText, float timeToDisplay)
        {
            if (timeToDisplay <= 0)
            {
                timeToDisplay = 0;
            }

            float hours = Mathf.FloorToInt(timeToDisplay / 3600);
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        public static DateTime ConvertStringToDateTime(string dateTime)
        {
            System.Globalization.CultureInfo enUS = new("en-US");
            DateTime convertedTime = DateTime.Parse(dateTime, enUS);

            return convertedTime;
        }

        public static float GetDifferenceInSeconds(DateTime currentTime, DateTime dateTime)
        {
            var timeDifference = dateTime.Subtract(currentTime);
            float differenceInSeconds = (timeDifference.Hours * 3600) + (timeDifference.Minutes * 60) + timeDifference.Seconds;

            return differenceInSeconds;
        }
    }
}