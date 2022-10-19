using UnityEngine;

public static class AudioParameters
{
    public static void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(nameof(AudioMixerParameters.MasterVolume), Mathf.Clamp(value, 0, 1));
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(nameof(AudioMixerParameters.MasterVolume), AudioMixerParameters.MasterVolume);
    }
}