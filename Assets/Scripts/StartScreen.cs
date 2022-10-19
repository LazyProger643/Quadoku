using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.UnloadSceneAsync(SceneNames.StartScreen);
        SceneManager.LoadScene(SceneNames.GameScreen, LoadSceneMode.Additive);
    }
}
