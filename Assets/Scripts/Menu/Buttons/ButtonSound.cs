using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        source.clip = audioClip;
    }

    public void OnClick()
    {
        AudioManager.PlaySFX(source);
    }
}