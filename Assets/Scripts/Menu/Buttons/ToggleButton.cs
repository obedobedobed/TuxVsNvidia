using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private bool isEnabled = false;
    [SerializeField] private WhatsDoing whatsDoing;

    private void Start()
    {
        switch (whatsDoing)
        {
            case WhatsDoing.SigmaMode:
                if (GlobalVariables.SigmaMode)
                {
                    isEnabled = true;
                }
                else if (!GlobalVariables.SigmaMode)
                {
                    isEnabled = false;
                }
                break;
        }

        if (isEnabled)
        {
            GetComponent<Image>().sprite = onSprite;
        }
    }

    public void OnClick()
    {
        if (isEnabled)
        {
            GetComponent<Image>().sprite = offSprite;
            isEnabled = false;
            Change(false);
        }
        else if (!isEnabled)
        {
            GetComponent<Image>().sprite = onSprite;
            isEnabled = true;
            Change(true);
        }
    }

    private void Change(bool changeTo)
    {
        switch (whatsDoing)
        {
            case WhatsDoing.SigmaMode:
                GlobalVariables.SigmaMode = changeTo;
                break;
        }
    }

    public enum WhatsDoing
    {
        SigmaMode
    }
}
