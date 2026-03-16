using UnityEngine;
using UnityEngine.UI;

public class InteractionPaperNoQuest : MonoBehaviour
{
    public GameObject closeUpPaper;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnClickPaper()
    {
        if (closeUpPaper != null)
        {
            audioManager.PlaySFX(audioManager.paper);
            closeUpPaper.SetActive(!closeUpPaper.activeSelf);
        }
    }
}


