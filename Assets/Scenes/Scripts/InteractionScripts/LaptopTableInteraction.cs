using UnityEngine;

public class LaptopTableInteraction : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject terminalPanel;
    public CharacterMovement playerMovement;
    public GameObject playerInteractionPanel;
    private bool playerIsClose = false;

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (!loginPanel.activeSelf)
            {
                loginPanel.SetActive(true);
                playerMovement.enabled = false;
                playerInteractionPanel.SetActive(false);
            }
            else
            {
                loginPanel.SetActive(false);
                playerMovement.enabled = true;
                playerInteractionPanel.SetActive(true);
            }
        }
        if (loginPanel.activeSelf || terminalPanel.activeSelf)
        {
            playerMovement.enabled = false;
            playerInteractionPanel.SetActive(false);
        }
        if (terminalPanel.activeSelf)
        {
            loginPanel.SetActive(true);
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
        }
    }
}