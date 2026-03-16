using UnityEngine;

public class CaveCollisionDestroy : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject InteractionPanel;
    public string requiredQuest;

    void Update()
    {
        if (requiredQuest == QuestManager.Instance.GetCurrentQuest())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPanel.SetActive(false);
        }
    }
}
