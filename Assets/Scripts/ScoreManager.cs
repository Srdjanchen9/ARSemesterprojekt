using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text gameWonText; // UI-Text für das "Game Won"-Ereignis
    public Button restartButton; // UI-Button für den Neustart
    public AudioSource coinSound;
    public AudioSource gameWonSound;
    public AudioSource startButtonSound;

    private int score = 0;
    public int maxScore = 5; // Maximale Punktzahl für das Gewinnen des Spiels

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
        gameWonText.gameObject.SetActive(false); // Verstecke den "Game Won"-Text zu Beginn
        restartButton.gameObject.SetActive(false); // Verstecke den Restart-Button zu Beginn
        restartButton.onClick.AddListener(RestartGame); // Füge den Listener für den Restart-Button hinzu
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        coinSound.Play();
        if (score >= maxScore)
        {
            GameWon();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void GameWon()
    {
        gameWonText.gameObject.SetActive(true); // Zeige den "Game Won"-Text an
        restartButton.gameObject.SetActive(true); // Zeige den Restart-Button an
        gameWonSound.Play();
        Debug.Log("Game Won!");
    }

    public void RestartGame()
    {
        // Lade die aktuelle Szene neu if (startButtonSound != null)
        startButtonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
