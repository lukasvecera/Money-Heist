using UnityEngine;

public class InteractionTableWithPaper : MonoBehaviour
{
    public GameObject questPanel;
    public GameObject playerInteractionPanel;
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
            if (!questPanel.activeSelf) {
                questPanel.SetActive(true);
                audioManager.PlaySFX(audioManager.paper);
                playerInteractionPanel.SetActive(false);
                }
            else {
                audioManager.PlaySFX(audioManager.paper);
                questPanel.SetActive(false);
                playerInteractionPanel.SetActive(true);
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
                audioManager.PlaySFX(audioManager.paper);
                questPanel.SetActive(false);
        }
    }
}
