using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void PlaySFX(AudioSource source, bool stop = false)
    {
        if (source != null)
        {
            if (!stop)
            {
                source.volume *= GlobalVariables.SoundsVolume;
                source.Play();
                source.volume /= GlobalVariables.SoundsVolume;
            }
            else
            {
                source.Stop();
            }
        }
    }
}
