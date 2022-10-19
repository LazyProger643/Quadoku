using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeDialog : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _audioMixer;

    public void Show()
    {
        float volume = AudioParameters.GetMasterVolume();

        _slider.value = volume;

        SetVolumeText(volume);

        gameObject.SetActive(true);
    }

    public void OnVolumeChanged(float value)
    {
        SetVolumeText(value);

        _audioMixer.SetFloat(nameof(AudioMixerParameters.MasterVolume), VolumeUtils.ConvertLinearToDB(value));

        AudioParameters.SetMasterVolume(value);
    }

    private void SetVolumeText(float volume)
    {
        _text.text = $"ÇÂÓÊ: {(int)(volume * 100)}%";
    }
}
