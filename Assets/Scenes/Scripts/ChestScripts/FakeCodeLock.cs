using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FakeCodeLock : MonoBehaviour
{
    [Header("UI References")]       
    public TMP_Text[] digitTexts;

    private int[] currentCode = new int[4];

    public void IncreaseDigit0() => OnArrow(0, +1);
    public void DecreaseDigit0() => OnArrow(0, -1);


    public void IncreaseDigit1() => OnArrow(1, +1);
    public void DecreaseDigit1() => OnArrow(1, -1);

    public void IncreaseDigit2() => OnArrow(2, +1);
    public void DecreaseDigit2() => OnArrow(2, -1);

    public void IncreaseDigit3() => OnArrow(3, +1);
    public void DecreaseDigit3() => OnArrow(3, -1);

    private void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            currentCode[i] = 0;
            UpdateDisplay(i);
        }
    }


    public void OnArrow(int index, int delta)
    {
        int val = currentCode[index] + delta;
        if (val > 9) val = 0;
        else if (val < 0) val = 9;
        currentCode[index] = val;

        UpdateDisplay(index);
    }

    private void UpdateDisplay(int index)
    {
        digitTexts[index].text = currentCode[index].ToString();
    }

    public void ResetCode()
    {
        for (int i = 0; i < currentCode.Length; i++)
        {
            currentCode[i] = 0;
            UpdateDisplay(i);
        }
    }


}

