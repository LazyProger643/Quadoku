using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private MusicTrack _music;

    private void Start()
    {
        float masterVolume = VolumeUtils.ConvertLinearToDB(AudioParameters.GetMasterVolume());

        _audioMixer.SetFloat(nameof(AudioMixerParameters.MasterVolume), masterVolume);
        _music.PlayDelayed();

        Input.multiTouchEnabled = false;
        SceneManager.LoadScene(SceneNames.StartScreen, LoadSceneMode.Additive);
    }
}
