using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject NPCName;
    public GameObject playerInteractionPanel;
    public TMP_Text dialogueText;

    [Header("Dialogy")]
    public string[] defaultDialogue;
    public string[] inactiveDialogue;
    
    private string[] currentDialogue;
    private int index = 0;

    [Header("Quest")]
    public string relatedQuestName;
    public string allowedQuestName;

    public GameObject contButton;
    public float wordSpeed = 0.03f;
    public bool playerIsClose;

    private WaypointMover waypointMover;
    private CharacterMovement playerMovement;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }

    void Start()
    {
        waypointMover = GetComponent<WaypointMover>();
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
    {
        if (!dialoguePanel.activeInHierarchy)
        {
            string currentQuest = QuestManager.Instance.GetCurrentQuest();

            if (currentQuest == allowedQuestName)
            {
                currentDialogue = defaultDialogue;
                audioManager.PlaySFX(audioManager.quest);
                QuestManager.Instance.SetQuest(relatedQuestName);
            }
            else
            {
                currentDialogue = inactiveDialogue;
            }

            if (currentDialogue != null && currentDialogue.Length > 0)
            {
                index = 0;
                dialoguePanel.SetActive(true);
                playerInteractionPanel.SetActive(false);
                StartCoroutine(Typing());
                waypointMover?.StopMovement();
                playerMovement.enabled = false;

            }
            else
            {
                Debug.LogWarning($"{gameObject.name}: currentDialogue je null nebo prázdné.");
            }
        }

    }

    if (currentDialogue != null && index < currentDialogue.Length && dialogueText.text == currentDialogue[index])
    {
        contButton.SetActive(true);
    }
}

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);


        audioManager.StopSFX();
        playerMovement.enabled = true;
    }

IEnumerator Typing()
{
    dialogueText.text = "";
    audioManager.PlaySFXDialogue(audioManager.speaking);

    foreach (char letter in currentDialogue[index].ToCharArray())
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(wordSpeed);
    }


    audioManager.StopSFX();
}

    public void NextLine()
    {
        audioManager.PlaySFX(audioManager.click);
        contButton.SetActive(false);

        if (index < currentDialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            waypointMover?.StopMovement();
            NPCName.SetActive(true);
            playerInteractionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            NPCName.SetActive(false);
            playerInteractionPanel.SetActive(false);
            waypointMover?.StartMovement();
        }
    }
}

