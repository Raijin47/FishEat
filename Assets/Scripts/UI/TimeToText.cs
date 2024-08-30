using TMPro;
using UnityEngine;

namespace Neoxider
{
    public enum TimeFormat
    {
        Seconds,
        Minutes,
        MinutesSeconds,
        HoursMinutesSeconds
    }

    [AddComponentMenu("Neoxider/" + "Tools/" + nameof(TimeToText))]
    public class TimeToText : MonoBehaviour
    {
        public TimeFormat timeFormat = TimeFormat.MinutesSeconds;
        public string startAddText;
        public string endAddText;
        public TMP_Text text;

        public void UpdateText(float time = 0)
        {
            text.text = startAddText + FormatTime(time, timeFormat) + endAddText;
        }

        public static string FormatTime(float time, TimeFormat format = TimeFormat.Seconds)
        {
            int hours = (int)(time / 3600);
            int minutes = (int)((time % 3600) / 60);
            int seconds = (int)(time % 60);

            switch (format)
            {
                case TimeFormat.Seconds:
                    return $"{(int)time:D2}";
                case TimeFormat.Minutes:
                    return $"{(int)(time / 60)}";
                case TimeFormat.MinutesSeconds:
                    return $"{minutes:D2}:{seconds:D2}";
                case TimeFormat.HoursMinutesSeconds:
                    return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
                default:
                    return "00";
            }
        }

        private void OnValidate()
        {
            text = GetComponent<TMP_Text>();
        }
    }
}
