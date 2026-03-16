using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FakeMoneyQuest : MonoBehaviour
{
    public string newQuestDescription;
    private bool questAlreadyUpdated = false;
    public int totalBadDollars;
    private int remaining;

     private GameObject badDollarsParent;
    private GameObject[] badDollars;

    public TMP_Text uiText;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        badDollarsParent = GameObject.Find("BadDollars");
        badDollars = GameObject.FindGameObjectsWithTag("BadDollar");
    }

    void Start()
    {
        remaining = totalBadDollars;
        UpdateUIText();
    }

    public void OnBadDollarClicked(GameObject dollar)
    {

        dollar.SetActive(false);
        remaining--;
        UpdateUIText();

        if (remaining <= 0)
        {
            if (!questAlreadyUpdated)
            {
            Debug.Log("Quest splněn! Přepínám na nový.");
            audioManager.PlaySFX(audioManager.quest);
            QuestManager.Instance.SetQuest(newQuestDescription);
            questAlreadyUpdated = true;
            }
        }
    }

public void OnGoodDollarClicked(GameObject dollar)
    {

        if (badDollarsParent != null)
        {
            badDollarsParent.SetActive(true);
        }


        foreach (GameObject badDollar in badDollars)
        {
            if (badDollar != null)
            {
                badDollar.SetActive(true);
            }
        }


        remaining = totalBadDollars;
        UpdateUIText();
    }


    private void UpdateUIText()
    {
        uiText.text = "Remaining banknotes: " + remaining;
    }
}
