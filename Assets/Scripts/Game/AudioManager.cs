using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static async void PlaySFX(AudioSource source, bool stop)
    {
        if (source != null)
        {
            if (!stop)
            {
                source.Play();
            }
            else
            {
                source.Stop();
            }
        }
    }
}
