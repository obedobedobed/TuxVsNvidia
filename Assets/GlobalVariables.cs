using UnityEngine;

public static class GlobalVariables
{
    public static float SoundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);
    public static bool SigmaMode = GetBool("SigmaMode", false);

    public static void SaveBool(bool toSave, string key)
    {
        int boolToInt = 0;
        if (toSave) boolToInt = 1;
        PlayerPrefs.SetInt(key, boolToInt);
    }

    private static bool GetBool(string key, bool defaultValue)
    {
        int valueInt = PlayerPrefs.GetInt(key, 0);
        if (valueInt == 1 || valueInt == 0) return valueInt == 1;
        else return defaultValue;
    }
}