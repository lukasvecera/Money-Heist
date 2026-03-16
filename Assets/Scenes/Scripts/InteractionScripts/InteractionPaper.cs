using UnityEngine;
using UnityEngine.UI;

public class InteractionPaper : MonoBehaviour
{
    public GameObject closeUpPaper;
    public string newQuestDescription;
    public string requiredQuest;
    public string requiredQuest2;
    private bool questAlreadyUpdated = false;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnClickPaper()
    {
        if (QuestManager.Instance.GetCurrentQuest() != requiredQuest && QuestManager.Instance.GetCurrentQuest() != requiredQuest2)
        {
            Debug.Log("Nemůžeš interagovat s tímto objektem během tohoto questu.");
            audioManager.PlaySFX(audioManager.denied);
            return;
        }

        if (closeUpPaper != null)
        {
            audioManager.PlaySFX(audioManager.paper);
            closeUpPaper.SetActive(!closeUpPaper.activeSelf);
        }

        if (!questAlreadyUpdated)
        {
            if (QuestManager.Instance.GetCurrentQuest() != newQuestDescription)
            {
                audioManager.PlaySFX(audioManager.quest);
            }
            Debug.Log("Quest splněn! Přepínám na nový.");
            QuestManager.Instance.SetQuest(newQuestDescription);
            questAlreadyUpdated = true;
        }
    }
}


