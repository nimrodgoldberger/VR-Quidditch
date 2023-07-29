using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public AudioManager audioManager;

    public void TransitionToScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        // Play audio when moving to another screen
        audioManager.PlayAudio();
    }
}
