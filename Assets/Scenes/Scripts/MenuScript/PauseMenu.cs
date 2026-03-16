using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButtons;
    public Canvas confirmMenu;
    private bool isPaused = false;

        AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        }

        void Start() {
        confirmMenu.enabled = false;

        }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }


    public void ContinueGame()
    {
        audioManager.PlaySFX(audioManager.click);
        ResumeGame();
    }

    public void GoToMainMenu()
    {
        audioManager.PlaySFX(audioManager.click);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        audioManager.musicSource.Stop();
    }


    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.click);
        confirmMenu.enabled = true;
        pauseMenuButtons.SetActive(false);
    }

    public void clickYes() {
        audioManager.PlaySFX(audioManager.click);
        Application.Quit();
    }

    public void clickNo() {
        audioManager.PlaySFX(audioManager.click);
        confirmMenu.enabled = false;
        pauseMenuButtons.SetActive(true);
    }
}