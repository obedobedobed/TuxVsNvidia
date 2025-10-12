using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void PlaySFX(ref AudioSource source, bool stop)
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
