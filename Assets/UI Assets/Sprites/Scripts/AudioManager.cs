using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent AudioManager from being destroyed on scene change
        }
        else
        {
            // Destroy the duplicate AudioManager
            Destroy(gameObject);
        }
    }


    public void PlayAudio()
    {
        // Play the audio when moving to a new screen
        GetComponent<AudioSource>().Play();
    }

    public void StopAudio()
    {
        // Stop the audio when entering the target screen
        GetComponent<AudioSource>().Stop();
    }

    // Other audio-related functions as needed
}
