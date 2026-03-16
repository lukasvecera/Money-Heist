using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioSource walkingSource;

    public AudioClip background;
    public AudioClip walking;
    public AudioClip walkingOnDirtGravel;
    public AudioClip paper;
    public AudioClip quest;
    public AudioClip money;
    public AudioClip speaking;
    public AudioClip denied;
    public AudioClip click;
    public AudioClip drawer;
    public AudioClip keys;
    public AudioClip book;

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

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXDialogue(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

public void PlayWalking(AudioClip clip)
{
    if (walkingSource.isPlaying && walkingSource.clip == clip) return;
    walkingSource.clip = clip;
    walkingSource.loop = true;
    walkingSource.Play();
}

    public void StopWalking()
    {
        walkingSource.Stop();
    }
}
