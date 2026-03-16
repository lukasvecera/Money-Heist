using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeLock : MonoBehaviour
{
    [Header("UI References")]       
    public TMP_Text[] digitTexts;

    [Header("Combination")]
    [Tooltip("Cílový kód, např. {4,9,2,1}")]
    public int[] targetCode = new int[4] { 4, 9, 2, 1 };

    private int[] currentCode = new int[4];

    public void IncreaseDigit0() => OnArrow(0, +1);
    public void DecreaseDigit0() => OnArrow(0, -1);


    public void IncreaseDigit1() => OnArrow(1, +1);
    public void DecreaseDigit1() => OnArrow(1, -1);

    public void IncreaseDigit2() => OnArrow(2, +1);
    public void DecreaseDigit2() => OnArrow(2, -1);

    public void IncreaseDigit3() => OnArrow(3, +1);
    public void DecreaseDigit3() => OnArrow(3, -1);

    public string newQuestDescription;
    public string bannedQuest;
    private bool questAlreadyUpdated = false;

    public GameObject CloseUpChest;
    public GameObject ChestLock;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            currentCode[i] = 0;
            UpdateDisplay(i);
        }
    }

    void Update()
    {
        if (CloseUpChest.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            CloseUpChest.SetActive(false);
        }
    }


    public void OnArrow(int index, int delta)
    {

        int val = currentCode[index] + delta;
        if (val > 9) val = 0;
        else if (val < 0) val = 9;
        currentCode[index] = val;

        UpdateDisplay(index);
        CheckUnlock();
    }

    private void UpdateDisplay(int index)
    {
        digitTexts[index].text = currentCode[index].ToString();
    }

    private void CheckUnlock()
    {

        for (int i = 0; i < 4; i++)
        {
            if (currentCode[i] != targetCode[i]) return;
        }
        UnlockChest();
    }

    private void UnlockChest()
    {
        
        if (questAlreadyUpdated || QuestManager.Instance.GetCurrentQuest() == bannedQuest) return;
        
        Debug.Log("Kód sedí! Otevírám truhlu…");
        audioManager.PlaySFX(audioManager.quest);
        QuestManager.Instance.SetQuest(newQuestDescription);
        questAlreadyUpdated = true;
        ChestLock.SetActive(false);
        CloseUpChest.SetActive(true);
    }
}

