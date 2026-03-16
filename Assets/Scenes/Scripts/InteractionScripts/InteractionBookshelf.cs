using UnityEngine;
using UnityEngine.UI;

public class InteractionBookshelf : MonoBehaviour
{
    public GameObject book;
    public string newQuestDescription;
    public string requiredQuest;
    private bool questAlreadyUpdated = false;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnClickBook()
    {
        if (QuestManager.Instance.GetCurrentQuest() != requiredQuest)
        {
            audioManager.PlaySFX(audioManager.denied);
            Debug.Log("Nemůžeš interagovat s tímto objektem během tohoto questu.");
            return;
        }

        if (book == GameObject.FindGameObjectWithTag("Keys"))
        {
            audioManager.PlaySFX(audioManager.keys);
            Destroy(book);
        } else if (book == GameObject.FindGameObjectWithTag("Mask"))
        {
            audioManager.PlaySFX(audioManager.money);
            Destroy(book);
        } else if (book == GameObject.FindGameObjectWithTag("Book"))
        {
            audioManager.PlaySFX(audioManager.book);
            Destroy(book);
        }

        if (!questAlreadyUpdated)
        {
            Debug.Log("Quest splněn! Přepínám na nový.");
            audioManager.PlaySFX(audioManager.quest);
            QuestManager.Instance.SetQuest(newQuestDescription);
            questAlreadyUpdated = true;
        }
    }
}


