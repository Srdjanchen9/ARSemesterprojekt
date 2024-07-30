using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource startMenuMusic;
    public AudioSource startButtonSound;

    void Start()
    {
        if (startMenuMusic != null)
        {
            startMenuMusic.Play();
        }
        else
        {
            Debug.LogError("StartMenuMusic AudioSource is not set.");
        }
    }

    public void StartGame()
    {
        if (startButtonSound != null)
        {
            startButtonSound.Play();
        }
        else
        {
            Debug.LogError("StartButtonSound AudioSource is not set.");
        }

        // Lade die nächste Szene
        SceneManager.LoadScene("ArDemoScene");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
