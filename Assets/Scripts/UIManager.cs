using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    public TMP_Text scoreText;
    public TMP_Text coinText;
    public TMP_Text timeText;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        scoreText.gameObject.SetActive(false);
        coinText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance != null && !mainMenuPanel.activeSelf)
            UpdateHUD();
    }

    private void UpdateHUD()
    {
        scoreText.text = GameManager.Instance.score.ToString("D6");
        coinText.text = "x " + GameManager.Instance.coins.ToString("D2");
        timeText.text = GameManager.Instance.timeRemaining.ToString("D3");
    }

    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        coinText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        GameManager.Instance.NewGame();
    }

    public void OpenSettings() { mainMenuPanel.SetActive(false); settingsPanel.SetActive(true); }
    public void CloseSettings() { settingsPanel.SetActive(false); mainMenuPanel.SetActive(true); }
    public void QuitGame() => Application.Quit();
}