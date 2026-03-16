using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static Vector2 spawnPosition = new Vector2(0, 0);

    [SerializeField] public static Vector2 defaultSpawnPosition;

    private void Awake()
    {
        if (spawnPosition == Vector2.zero)
        {
            spawnPosition = defaultSpawnPosition;
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = spawnPosition;
        }
    }
}
