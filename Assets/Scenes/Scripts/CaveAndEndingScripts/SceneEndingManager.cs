using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndingManager : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private Vector2 spawnPosition;

    AudioManager audioManager;

    void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneLoader.spawnPosition = spawnPosition;
            SceneManager.LoadScene(targetSceneName);
            audioManager.musicSource.Stop();
            audioManager.walkingSource.Stop();
        }
    }
}
