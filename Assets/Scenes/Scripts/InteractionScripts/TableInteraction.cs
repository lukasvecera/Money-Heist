using UnityEngine;

public class TableInteraction : MonoBehaviour
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
                audioManager.PlaySFX(audioManager.drawer);
                questPanel.SetActive(true);
                playerInteractionPanel.SetActive(false);
                }
            else {
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
                questPanel.SetActive(false);
        }
    }
}
