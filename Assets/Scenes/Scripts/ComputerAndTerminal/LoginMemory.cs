using UnityEngine;

public class LoginMemory : MonoBehaviour
{
    public static LoginMemory Instance;

    public string username = "";
    public string password = "";
    public bool camerasAlreadyUpdated = false;
    public static bool ChestOpened = Interpreter.ChestOpened;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
