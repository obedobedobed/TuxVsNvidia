using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        slider.value = GlobalVariables.SoundsVolume;
    }

    public void OnSlide()
    {
        GlobalVariables.SoundsVolume = slider.value;
        PlayerPrefs.SetFloat("SoundsVolume", slider.value);
        Debug.Log(slider.value);
    }
}
