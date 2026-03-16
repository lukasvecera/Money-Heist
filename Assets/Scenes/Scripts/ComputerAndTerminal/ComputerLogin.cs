using UnityEngine;
using TMPro;
using System.Collections;

public class ComputerLogin : MonoBehaviour
{

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_Text errorText;
    public string correctUsername = "admin";
    public string correctPassword = "1234";

    public string newQuestDescriptionCameras;
    private bool questAlreadyUpdatedCameras = false;

    public GameObject loginPanel;
    public GameObject terminalPanel;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
{
    usernameField.text = LoginMemory.Instance.username;
    passwordField.text = LoginMemory.Instance.password;
    errorText.text = "";
}

public void TryLogin()
{
    string username = usernameField.text;
    string password = passwordField.text;


    LoginMemory.Instance.username = username;
    LoginMemory.Instance.password = password;

    if (username == correctUsername && password == correctPassword)
    {
        audioManager.PlaySFX(audioManager.click);
        loginPanel.SetActive(false);
        terminalPanel.SetActive(true);

        if (!questAlreadyUpdatedCameras && !LoginMemory.Instance.camerasAlreadyUpdated)
        {
            Debug.Log("Quest splněn! Přepínám na nový.");
            audioManager.PlaySFX(audioManager.quest);
            QuestManager.Instance.SetQuest(newQuestDescriptionCameras);
            LoginMemory.Instance.camerasAlreadyUpdated = true;
            questAlreadyUpdatedCameras = true;
        }
    }
       else
        {
            audioManager.PlaySFX(audioManager.denied);
            StartCoroutine(ShowErrorMessage("Wrong access data, try it again", 2f));
            Debug.Log("Špatné přihlašovací údaje");
        }
    }

    private IEnumerator ShowErrorMessage(string message, float duration)
    {
        errorText.text = message;
        yield return new WaitForSeconds(duration);
        errorText.text = "";
    }

}
