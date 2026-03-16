using UnityEngine;

public class MetalChestInteraction : MonoBehaviour
{
    public GameObject questPanel;
    public string newQuestDescription;
    public string requiredQuest;
    public string requiredQuest2;
    public string requiredQuest3;
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
        if (playerIsClose && Input.GetKeyDown(KeyCode.E) && Interpreter.ChestOpened == true)
        {

            string currentQuest = QuestManager.Instance.GetCurrentQuest();
            if (currentQuest == requiredQuest || currentQuest == requiredQuest2 || currentQuest == requiredQuest3)
            {

                if (!questAlreadyUpdated)
                {
                    audioManager.PlaySFX(audioManager.quest);
                    QuestManager.Instance.SetQuest(newQuestDescription);
                    questAlreadyUpdated = true;
                }
                if (!questPanel.activeSelf) {
                questPanel.SetActive(true);
                playerInteractionPanel.SetActive(false);
                }
            else {
                questPanel.SetActive(false);
                playerInteractionPanel.SetActive(true);
            }

            }
            else
            {
                Debug.Log("Hráč nemá povolený požadovaný úkol.");
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
