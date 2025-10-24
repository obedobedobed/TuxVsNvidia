using UnityEngine;

[System.Serializable]
public class Timer
{
    private int hours = 0;
    private int minutes = 0;
    private int seconds = 0;
    private float fSeconds;
    public float totalSeconds { get; private set; }

    public void UpdateTime(float update)
    {
        fSeconds += update;
        totalSeconds += update;
        seconds = (int)fSeconds;

        if (seconds >= 60)
        {
            seconds -= 60;
            fSeconds -= 60;
            minutes++;
        }

        if (minutes >= 60)
        {
            minutes -= 60;
            hours++;
        }
    }

    public string WriteTime(bool onlySeconds = false)
    {
        if (hours != 0 && !onlySeconds)
        {
            return $"{hours}:{minutes}:{seconds}";
        }
        else if (hours == 0 && !onlySeconds)
        {
            return $"{minutes}:{seconds}";
        }
        else
        {
            return $"{seconds}";
        }
    }

    public void ResetTime()
    {
        fSeconds = 0;
        totalSeconds = 0;
        hours = 0;
        minutes = 0;
        seconds = 0;
    }
}
