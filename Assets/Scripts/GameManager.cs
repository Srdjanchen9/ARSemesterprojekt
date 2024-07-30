using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource gameMusic;

    void Start()
    {
        gameMusic.Play();
    }
}
