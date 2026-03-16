using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public Canvas confirmMenu;
    public Canvas controlsMenu;

    AudioManager audioManager;
    AudioManagerMenu audioManagerMenu;

private void Awake()
{
    GameObject audioObjectMenu = GameObject.FindGameObjectWithTag("AudioMenu");
    if (audioObjectMenu != null)
    {
        audioManagerMenu = audioObjectMenu.GetComponent<AudioManagerMenu>();
    }

    GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
    if (audioObject != null)
    {
        audioManager = audioObject.GetComponent<AudioManager>();
    }

    if (audioManagerMenu == null)
        Debug.Log("AudioManagerMenu component not found on GameObject with tag 'AudioMenu'!");

    if (audioManager == null)
        Debug.Log("AudioManager component not found on GameObject with tag 'Audio'!");
}

    void Start()
    {
        confirmMenu.enabled = false;
        controlsMenu.enabled = false;
    }

    public void PlayGame() {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);

            SceneLoader.spawnPosition = new Vector2(0.1875f, -2.1492f);
            SceneLoader.defaultSpawnPosition = new Vector2(0.1875f, -2.1492f);

        SceneManager.LoadScene(2);

        if (audioManager != null)
        {
            audioManager.musicSource.Play();
        }
    }

    public void clickControls() {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);
        controlsMenu.enabled = true;
        mainMenu.SetActive(false);
    }

    public void clickBack() {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);
        controlsMenu.enabled = false;
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);
        confirmMenu.enabled = true;
        mainMenu.SetActive(false);
    }

    public void clickYes() {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);
        Application.Quit();
    }

    public void clickNo() {
        audioManagerMenu.PlaySFX(audioManagerMenu.click);
        confirmMenu.enabled = false;
        mainMenu.SetActive(true);
    }

}
