using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoReturnToMenu : MonoBehaviour
{
    [SerializeField] private float delay = 55.5f;
    [SerializeField] private string menuSceneName = "MainMenu";
    public string newQuestDescription;

    void Start()
    {
        Invoke(nameof(ReturnToMenu), delay);
    }

    private void ReturnToMenu()
    {
        QuestManager.Instance.SetQuest(newQuestDescription);
        Debug.Log("Přepínám quest na první úkol.");
        SceneManager.LoadScene(menuSceneName);
    }
}