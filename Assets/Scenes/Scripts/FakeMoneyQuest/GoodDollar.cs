using UnityEngine;
using UnityEngine.UI;

public class GoodDollar : MonoBehaviour
{
    public FakeMoneyQuest questManager;
    public string requiredQuest;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnClick()
    {
        if (QuestManager.Instance.GetCurrentQuest() != requiredQuest)
        {
            audioManager.PlaySFX(audioManager.denied);
            Debug.Log("Nemůžeš interagovat s tímto objektem během tohoto questu.");
            return;
        }
        audioManager.PlaySFX(audioManager.denied);
        questManager.OnGoodDollarClicked(gameObject);
    }
}
