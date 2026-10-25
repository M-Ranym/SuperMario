using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; } = 0;
    public int score { get; private set; } = 0;
    public int timeRemaining { get; private set; } = 400;

    private float timeCounter;
    private bool timerActive;

    private void Awake()
    {
        if (Instance != null) { DestroyImmediate(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
    }

    private void Update()
    {
        if (timerActive && timeRemaining > 0)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 1f)
            {
                timeRemaining--;
                timeCounter -= 1f;
                if (timeRemaining <= 0) ResetLevel(0f);
            }
        }
    }

    public void NewGame()
    {
        lives = 3; coins = 0; score = 0; timeRemaining = 400; timeCounter = 0f; timerActive = true;
        LoadLevel(1, 1);
    }

    public void LoadLevel(int w, int s)
    {
        world = w; stage = s;
        timeRemaining = 400; timeCounter = 0f; timerActive = true;
        SceneManager.LoadScene($"{w}-{s}");
    }

    public void NextLevel() => LoadLevel(world, stage + 1);

    public void ResetLevel(float delay) { CancelInvoke(); Invoke(nameof(ResetLevel), delay); }
    public void ResetLevel()
    {
        lives--;
        if (lives > 0) LoadLevel(world, stage);
        else NewGame(); 
    }

    public void AddCoin() { coins++; AddScore(200); if (coins == 100) { coins = 0; AddLife(); } }
    public void AddScore(int points) => score += points;
    public void AddLife() => lives++;
}