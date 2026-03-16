using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public GameObject terminal;
    public string newQuestDescriptionCameras;
    public string newQuestDescriptionChest;
    public string requiredQuestCameras;
    public string requiredQuestChest;
    private bool questAlreadyUpdated = false;
    private bool questChestAlreadyUpdated = false;
    public static bool ChestOpened = false;
    List <string> response = new List<string>();

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


public List<string> Interpret(string userInput) {
    response.Clear();

    string[] args = userInput.Split();
    if (args.Length == 0) {
        response.Add("No command entered.");
        return response;
    }

    switch (args[0]) {
        case "/help":
            response.Add("'/exit' - Exits the terminal.");
            response.Add("'/hackcameras' - Hacks cameras.");
            response.Add("'/hackchest-ENTEREDCHESTCODE' - Hacks the chest.");
            break;

        case "/hackcameras":
            if (!questAlreadyUpdated && QuestManager.Instance.GetCurrentQuest() == requiredQuestCameras) {
                response.Add("Cameras hacked successfully.");
                Debug.Log("Quest splněn! Přepínám na nový.");
                audioManager.PlaySFX(audioManager.quest);
                QuestManager.Instance.SetQuest(newQuestDescriptionCameras);
                questAlreadyUpdated = true;
            } else if (questAlreadyUpdated) {
                audioManager.PlaySFX(audioManager.denied);
                response.Add("Cameras already hacked.");
            }
            break;

        case "/hackchest-CHEST686":
            if (!questChestAlreadyUpdated && QuestManager.Instance.GetCurrentQuest() == requiredQuestChest) {
                response.Add("Chest hacked successfully.");
                ChestOpened = true;
                Debug.Log("Quest splněn! Přepínám na nový.");
                audioManager.PlaySFX(audioManager.quest);
                QuestManager.Instance.SetQuest(newQuestDescriptionChest);
                questChestAlreadyUpdated = true;
            } else if (questChestAlreadyUpdated) {
                audioManager.PlaySFX(audioManager.denied);
                response.Add("Chest already hacked.");
            } else {
                audioManager.PlaySFX(audioManager.denied);
                response.Add("Invalid command for quest.");
            }
            break;

        case "/exit":
            response.Add("Exiting terminal...");
            terminal.SetActive(false);
            break;

        default:
            audioManager.PlaySFX(audioManager.denied);
            response.Add("Command not recognized. Type '/help' for a list of commands.");
            break;
    }

    return response;
}
}
