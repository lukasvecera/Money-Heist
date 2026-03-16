using UnityEngine;

public class MoneyDeskInteraction : MonoBehaviour
{
    public GameObject questPanel;
    public string newQuestDescription;
    public string requiredQuest;
    public string requiredQuest2;
    public string requiredQuestOnLeave;
    public GameObject playerInteractionPanel;
    private bool questAlreadyUpdated = false;
    private bool playerIsClose = false;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        if (questPanel != null)
            questPanel.SetActive(false);
    }

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
             if (questPanel != null)
            {
                string currentQuest = QuestManager.Instance.GetCurrentQuest();
                if (currentQuest == requiredQuest || currentQuest == requiredQuest2)
                {

                    if (!questAlreadyUpdated)
                    {
                        QuestManager.Instance.SetQuest(newQuestDescription);
                        questAlreadyUpdated = true;
                    }
                    audioManager.PlaySFX(audioManager.drawer);
                    questPanel.SetActive(!questPanel.activeSelf);
                    playerInteractionPanel.SetActive(false);
                }
                else if (currentQuest == requiredQuestOnLeave) {
                    questPanel.SetActive(false);
                }
                else
                {
                    audioManager.PlaySFX(audioManager.denied);
                    Debug.Log("Hráč nemá povolený požadovaný úkol.");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            playerInteractionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            playerInteractionPanel.SetActive(false);

            if (questPanel != null && questPanel.activeSelf)
                questPanel.SetActive(false);
        }
    }
}
