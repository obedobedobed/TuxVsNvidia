using UnityEngine;

[System.Serializable]
public class Timer
{
    public int hours = 0;
    public int minutes = 0;
    public int seconds = 0;
    private float totalSeconds;

    public void UpdateTime(float update)
    {
        totalSeconds += update;

        seconds = (int)Mathf.Floor(totalSeconds);

        if (seconds >= 60)
        {
            seconds -= 60;
            minutes++;
        }

        if (minutes >= 60)
        {
            minutes -= 60;
            hours++;
        }
    }

    public string WriteTime()
    {
        if (hours != 0)
        {
            return $"{hours}:{minutes}:{seconds}";
        }
        else
        {
            return $"{minutes}:{seconds}";
        }
    }
    
    public void ResetTime()
    {
        totalSeconds = 0;
        hours = 0;
        minutes = 0;
        seconds = 0;
    }
}
